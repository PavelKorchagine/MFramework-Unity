using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;
using System.Runtime.InteropServices;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           IMGUIPerformanceDebugger.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

namespace MFramework_Unity.Tools
{
    public class IMGUIPerformanceDebugger : MonoBehaviour
    {
        /// <summary>
        /// 是否允许调试
        /// </summary>
        public bool AllowDebugging = true;

        private DebugType _debugType = DebugType.Console;
        private List<LogData> _logInformations = new List<LogData>();
        private int _currentLogIndex = -1;
        private int _infoLogCount = 0;
        private int _warningLogCount = 0;
        private int _errorLogCount = 0;
        private int _fatalLogCount = 0;
        private bool _showInfoLog = true;
        private bool _showWarningLog = true;
        private bool _showErrorLog = true;
        private bool _showFatalLog = true;
        private Vector2 _scrollLogView = Vector2.zero;
        private Vector2 _scrollCurrentLogView = Vector2.zero;
        private Vector2 _scrollSystemView = Vector2.zero;
        private bool _expansion = false;
        private Rect _windowRect = new Rect(100, 100, 100, 60);

        private int _fps = 0;
        private Color _fpsColor = Color.white;
        private int _frameNumber = 0;
        private float _lastShowFPSTime = 0f;
        //private GUIStyle style;
        private const string GUIFONT = "GUIRes/simfang";
        private string memoryStats;

        [Tooltip("Interval (in seconds) between log entries")]
        public uint LogIntervalSeconds = 1;

        #region UNITY_WEBGL DllImport __Internal __Internal __Internal __Internal
#if UNITY_WEBGL
    [DllImport("__Internal")]
    public static extern uint GetTotalMemorySize();
    [DllImport("__Internal")]
    public static extern uint GetTotalStackSize();
    [DllImport("__Internal")]
    public static extern uint GetStaticMemorySize();
    [DllImport("__Internal")]
    public static extern uint GetDynamicMemorySize();
#endif
        #endregion
        private void Log()
        {
#if UNITY_EDITOR
            var total = Profiler.GetTotalReservedMemoryLong() / 1024 / 1024;
            var used = Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024;
            var free = Profiler.GetTotalUnusedReservedMemoryLong() / 1024 / 1024;
            memoryStats = string.Format("WebGL Memory - Total: {0}MB, Used: {1}MB, Free: {2}MB", total, used, free);
            //Debug.Log(memoryStats);
#elif !UNITY_EDITOR && UNITY_WEBGL
            var total = GetTotalMemorySize() / 1024 / 1024;
            var used = GetUsedMemorySize() / 1024 / 1024;
            var free = GetFreeMemorySize() / 1024 / 1024;
            memoryStats = string.Format("WebGL Memory - Total: {0}MB, Used: {1}MB, Free: {2}MB", total, used, free);
            //Debug.Log(memoryStats);
#endif

        }
        private static uint GetFreeMemorySize()
        {
#if UNITY_EDITOR
            return (uint)Profiler.GetTotalUnusedReservedMemoryLong();
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            return 0;
#elif !UNITY_EDITOR && UNITY_ANDROID
            return 0;
#elif !UNITY_EDITOR && UNITY_IPHONE
            return 0;
#elif !UNITY_EDITOR && UNITY_WEBGL
            return GetTotalMemorySize() - GetUsedMemorySize();
#else
            return 0;
#endif
        }
        public static uint GetUsedMemorySize()
        {
#if UNITY_EDITOR
            return (uint)Profiler.GetTotalAllocatedMemoryLong();
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            return 0;
#elif !UNITY_EDITOR && UNITY_ANDROID
            return 0;
#elif !UNITY_EDITOR && UNITY_IPHONE
            return 0;
#elif !UNITY_EDITOR && UNITY_WEBGL
            return GetTotalStackSize() + GetStaticMemorySize() + GetDynamicMemorySize();
#else
            return 0;
#endif
        }
        private void Start()
        {
            //style = new GUIStyle();
            //style.font = Resources.Load<Font>(GUIFONT);

            if (AllowDebugging)
            {
                Application.logMessageReceived += LogHandler;
            }
            _windowRect = new Rect(Screen.width - 150, 30, 120, 60);

            //InvokeRepeating("Log", 0, LogIntervalSeconds);
        }

        private void Application_logMessageReceived(string condition, string stackTrace, UnityEngine.LogType type)
        {
        }

        private void Update()
        {
            if (AllowDebugging)
            {
                _frameNumber += 1;
                float time = Time.realtimeSinceStartup - _lastShowFPSTime;
                if (time >= 1)
                {
                    _fps = (int)(_frameNumber / time);
                    _frameNumber = 0;
                    _lastShowFPSTime = Time.realtimeSinceStartup;
                    Log();
                }
            }
        }
        private void OnDestory()
        {
            if (AllowDebugging)
            {
                Application.logMessageReceived -= LogHandler;
            }
        }
        private void LogHandler(string condition, string stackTrace, UnityEngine.LogType type)
        {
            LogData log = new LogData();
            log.time = DateTime.Now.ToString("HH:mm:ss");
            log.message = condition;
            log.stackTrace = stackTrace;

            if (type == UnityEngine.LogType.Assert)
            {
                log.type = "Fatal";
                _fatalLogCount += 1;
            }
            else if (type == UnityEngine.LogType.Exception || type == UnityEngine.LogType.Error)
            {
                log.type = "Error";
                _errorLogCount += 1;
            }
            else if (type == UnityEngine.LogType.Warning)
            {
                log.type = "Warning";
                _warningLogCount += 1;
            }
            else if (type == UnityEngine.LogType.Log)
            {
                log.type = "Info";
                _infoLogCount += 1;
            }

            _logInformations.Add(log);

            if (_warningLogCount > 0)
            {
                _fpsColor = Color.yellow;
            }
            if (_errorLogCount > 0)
            {
                _fpsColor = Color.red;
            }
        }

        private void OnGUI()
        {
            if (AllowDebugging)
            {
                if (_expansion)
                {
                    _windowRect = GUI.Window(0, _windowRect, ExpansionGUIWindow, "DEBUGGER");
                }
                else
                {
                    _windowRect = GUI.Window(0, _windowRect, ShrinkGUIWindow, "DEBUGGER");
                }
            }
        }
        private void ExpansionGUIWindow(int windowId)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

            #region title
            GUILayout.BeginHorizontal();
            GUI.contentColor = _fpsColor;
            if (GUILayout.Button("FPS:" + _fps, GUILayout.Height(30)))
            {
                _expansion = false;
                _windowRect.width = 100;
                _windowRect.height = 60;
            }
            GUI.contentColor = (_debugType == DebugType.Console ? Color.white : Color.gray);
            if (GUILayout.Button("Console", GUILayout.Height(30)))
            {
                _debugType = DebugType.Console;
            }
            GUI.contentColor = (_debugType == DebugType.Memory ? Color.white : Color.gray);
            if (GUILayout.Button("Memory", GUILayout.Height(30)))
            {
                _debugType = DebugType.Memory;
            }
            GUI.contentColor = (_debugType == DebugType.System ? Color.white : Color.gray);
            if (GUILayout.Button("System", GUILayout.Height(30)))
            {
                _debugType = DebugType.System;
            }
            GUI.contentColor = (_debugType == DebugType.Screen ? Color.white : Color.gray);
            if (GUILayout.Button("Screen", GUILayout.Height(30)))
            {
                _debugType = DebugType.Screen;
            }
            GUI.contentColor = (_debugType == DebugType.Quality ? Color.white : Color.gray);
            if (GUILayout.Button("Quality", GUILayout.Height(30)))
            {
                _debugType = DebugType.Quality;
            }
            GUI.contentColor = (_debugType == DebugType.Environment ? Color.white : Color.gray);
            if (GUILayout.Button("Environment", GUILayout.Height(30)))
            {
                _debugType = DebugType.Environment;
            }
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
            #endregion

            #region console
            if (_debugType == DebugType.Console)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Clear"))
                {
                    _logInformations.Clear();
                    _fatalLogCount = 0;
                    _warningLogCount = 0;
                    _errorLogCount = 0;
                    _infoLogCount = 0;
                    _currentLogIndex = -1;
                    _fpsColor = Color.white;
                }
                GUI.contentColor = (_showInfoLog ? Color.white : Color.gray);
                _showInfoLog = GUILayout.Toggle(_showInfoLog, "Info [" + _infoLogCount + "]");
                GUI.contentColor = (_showWarningLog ? Color.white : Color.gray);
                _showWarningLog = GUILayout.Toggle(_showWarningLog, "Warning [" + _warningLogCount + "]");
                GUI.contentColor = (_showErrorLog ? Color.white : Color.gray);
                _showErrorLog = GUILayout.Toggle(_showErrorLog, "Error [" + _errorLogCount + "]");
                GUI.contentColor = (_showFatalLog ? Color.white : Color.gray);
                _showFatalLog = GUILayout.Toggle(_showFatalLog, "Fatal [" + _fatalLogCount + "]");
                GUI.contentColor = Color.white;
                GUILayout.EndHorizontal();

                _scrollLogView = GUILayout.BeginScrollView(_scrollLogView, "Box", GUILayout.Height(165));
                for (int i = 0; i < _logInformations.Count; i++)
                {
                    bool show = false;
                    Color color = Color.white;
                    switch (_logInformations[i].type)
                    {
                        case "Fatal":
                            show = _showFatalLog;
                            color = Color.red;
                            break;
                        case "Error":
                            show = _showErrorLog;
                            color = Color.red;
                            break;
                        case "Info":
                            show = _showInfoLog;
                            color = Color.white;
                            break;
                        case "Warning":
                            show = _showWarningLog;
                            color = Color.yellow;
                            break;
                        default:
                            break;
                    }

                    if (show)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Toggle(_currentLogIndex == i, ""))
                        {
                            _currentLogIndex = i;
                        }
                        GUI.contentColor = color;
                        GUILayout.Label("[" + _logInformations[i].type + "] ");
                        GUILayout.Label("[" + _logInformations[i].time + "] ");
                        GUILayout.Label(_logInformations[i].message);
                        GUILayout.FlexibleSpace();
                        GUI.contentColor = Color.white;
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();

                _scrollCurrentLogView = GUILayout.BeginScrollView(_scrollCurrentLogView, "Box", GUILayout.Height(100));
                if (_currentLogIndex != -1)
                {
                    GUILayout.Label(_logInformations[_currentLogIndex].message + "\r\n\r\n" + _logInformations[_currentLogIndex].stackTrace);
                }
                GUILayout.EndScrollView();
            }
            #endregion

            #region memory
            else if (_debugType == DebugType.Memory)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Memory Information");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");
#if UNITY_5
            GUILayout.Label("memoryStats:" + memoryStats);
            GUILayout.Label("GetTotalReservedMemory:" + Profiler.GetTotalReservedMemory() / 1000000 + "MB");
            GUILayout.Label("GetTotalAllocatedMemory:" + Profiler.GetTotalAllocatedMemory() / 1000000 + "MB");
            GUILayout.Label("GetTotalUnusedReservedMemory:" + Profiler.GetTotalUnusedReservedMemory() / 1000000 + "MB");
            GUILayout.Label("GetMonoHeapSize:" + Profiler.GetMonoHeapSize() / 1000000 + "MB");
            GUILayout.Label("GetMonoUsedSize:" + Profiler.GetMonoUsedSize() / 1000000 + "MB");
#endif
#if UNITY_2017
                GUILayout.Label("memoryStats:" + memoryStats);
                GUILayout.Label("GetTotalReservedMemoryLong:" + Profiler.GetTotalReservedMemoryLong() / 1000000 + "MB");
                GUILayout.Label("GetTotalAllocatedMemoryLong:" + Profiler.GetTotalAllocatedMemoryLong() / 1000000 + "MB");
                GUILayout.Label("GetTotalUnusedReservedMemoryLong:" + Profiler.GetTotalUnusedReservedMemoryLong() / 1000000 + "MB");
                GUILayout.Label("GetMonoHeapSizeLong:" + Profiler.GetMonoHeapSizeLong() / 1000000 + "MB");
                GUILayout.Label("GetMonoUsedSizeLong:" + Profiler.GetMonoUsedSizeLong() / 1000000 + "MB");
#endif
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Resources.UnloadUnusedAssets"))
                {
                    Resources.UnloadUnusedAssets();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("GC.Collect"))
                {
                    GC.Collect();
                }
                GUILayout.EndHorizontal();
            }
            #endregion

            #region system
            else if (_debugType == DebugType.System)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("System Information");
                GUILayout.EndHorizontal();

                _scrollSystemView = GUILayout.BeginScrollView(_scrollSystemView, "Box");
                GUILayout.Label("OperatingSystem：" + SystemInfo.operatingSystem);
                GUILayout.Label("SystemMemorySize：" + SystemInfo.systemMemorySize + "MB");
                GUILayout.Label("ProcessorType：" + SystemInfo.processorType);
                GUILayout.Label("ProcessorCount：" + SystemInfo.processorCount);
                GUILayout.Label("GraphicsDeviceName：" + SystemInfo.graphicsDeviceName);
                GUILayout.Label("GraphicsDeviceType：" + SystemInfo.graphicsDeviceType);
                GUILayout.Label("GraphicsMemorySize：" + SystemInfo.graphicsMemorySize + "MB");
                GUILayout.Label("GraphicsDeviceID：" + SystemInfo.graphicsDeviceID);
                GUILayout.Label("GraphicsDeviceVendor：" + SystemInfo.graphicsDeviceVendor);
                GUILayout.Label("GraphicsDeviceVendorID：" + SystemInfo.graphicsDeviceVendorID);
                GUILayout.Label("DeviceModel：" + SystemInfo.deviceModel);
                GUILayout.Label("DeviceName：" + SystemInfo.deviceName);
                GUILayout.Label("DeviceType：" + SystemInfo.deviceType);
                GUILayout.Label("DeviceUniqueIdentifier：" + SystemInfo.deviceUniqueIdentifier);
                GUILayout.EndScrollView();
            }
            #endregion

            #region screen
            else if (_debugType == DebugType.Screen)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Screen Information");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");
                GUILayout.Label("DPI：" + Screen.dpi);
                GUILayout.Label("currentResolution：" + Screen.currentResolution.ToString());
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("FullScreen"))
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !Screen.fullScreen);
                }
                GUILayout.EndHorizontal();
            }
            #endregion

            #region Quality
            else if (_debugType == DebugType.Quality)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Quality Information");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");
                string value = "";
                if (QualitySettings.GetQualityLevel() == 0)
                {
                    value = " [LowestQualityLevel]";
                }
                else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 1)
                {
                    value = " [HighestQualityLevel]";
                }

                GUILayout.Label("GetQualityLevel：" + QualitySettings.names[QualitySettings.GetQualityLevel()] + value);
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("DecreaseLevel"))
                {
                    QualitySettings.DecreaseLevel();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("IncreaseLevel"))
                {
                    QualitySettings.IncreaseLevel();
                }
                GUILayout.EndHorizontal();
            }
            #endregion

            #region Environment
            else if (_debugType == DebugType.Environment)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Environment Information");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical("Box");
                GUILayout.Label("productName：" + Application.productName);
#if UNITY_5
            GUILayout.Label("bundleIdentifier：" + Application.bundleIdentifier, style);
#endif
#if UNITY_7
            GUILayout.Label("identifier：" + Application.identifier, style);
#endif
                GUILayout.Label("version：" + Application.version);
                GUILayout.Label("unityVersion：" + Application.unityVersion);
                GUILayout.Label("companyName：" + Application.companyName);
                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Quit"))
                {
                    Application.Quit();
                }
                GUILayout.EndHorizontal();
            }
            #endregion
        }
        private void ShrinkGUIWindow(int windowId)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

            GUI.contentColor = _fpsColor;
            if (GUILayout.Button("FPS:" + _fps, GUILayout.Width(80), GUILayout.Height(30)))
            {
                _expansion = true;
                _windowRect.width = 600;
                _windowRect.height = 360;
            }
            GUI.contentColor = Color.white;
        }
    }
    public struct LogData
    {
        public string time;
        public string type;
        public string message;
        public string stackTrace;
    }
    public enum DebugType
    {
        Console,
        Memory,
        System,
        Screen,
        Quality,
        Environment
    }
}