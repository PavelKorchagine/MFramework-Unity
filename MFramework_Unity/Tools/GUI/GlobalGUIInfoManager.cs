using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.Profiling;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Networking;

namespace MFramework_Unity.Tools
{
    public class GlobalGUIInfoManager : MonoBehaviour
    {
        private bool Enable = true;
        private bool IsShowGUIEditor = false;
        private string texturName;
        private string msg;
        internal List<string> mssgs = new List<string>();
        private GUIStyle style;
        private Texture2D Texture2;
        public ulong index;
        private Vector2 large;

        public void OnInit(params object[] args)
        {

        }
        public void OnInit()
        {
            if (!Enable) return;

            texturName = "GUI/GUIBG";
            //Texture2 = Resources.Load<Texture2D>(texturName);

            Texture2 = null;
            Font font = null;
            style = new GUIStyle();
            style.font = font;
            style.fontSize = 12;
            style.normal.background = Texture2;    //设置背景填充  
            style.normal.textColor = Color.white;
            style.hover.textColor = Color.yellow;
            style.onHover.textColor = Color.green;

            msg = "请按 LeftControl + G 按键，调出GUI输出信息!! ";
            large = new Vector2((int)(Screen.width * 530.0f / 1280.0f), Screen.height - 200);

            Application.logMessageReceived += LogHandler;
        }

        private void LogHandler(string condition, string stackTrace, UnityEngine.LogType type)
        {
            Add(condition);
        }

        private Vector2 scrollViewVector = new Vector2(0, 85);
        private void OnGUI()
        {
            GUILayout.Space(20);
            string me_re = string.Format("总内存:{0},已占用内存:{1}MB,空闲中内存:{2}MB,总Mono堆内存:{3}MB,已占用Mono堆内存:{4}MB."
                , (Profiler.GetTotalReservedMemoryLong() / 1000000.0f).ToString("F2")
                , (Profiler.GetTotalAllocatedMemoryLong() / 1000000.0f).ToString("F2")
                , (Profiler.GetTotalUnusedReservedMemoryLong() / 1000000.0f).ToString("F2")
                , (Profiler.GetMonoHeapSizeLong() / 1000000.0f).ToString("F2")
                , (Profiler.GetMonoUsedSizeLong() / 1000000.0f).ToString("F2"));
            GUILayout.Label(me_re, style);
            GUILayout.Label(string.Format("FPS:{0}", _fps.ToString()), style);
            GUILayout.Label(string.Format("屏幕大小:{0}, 屏幕解析率:{1}", new Vector2(Screen.width, Screen.height), Screen.currentResolution), style);
            if (GUILayout.Button("ShowOrHide", GUILayout.Width(100), GUILayout.Height(25)))
            {
                Enable = !Enable;
            }
            if (!Enable) return;
            GUILayout.Space(5);
            GUILayout.Label(msg, style);
            GUILayout.BeginVertical();
            GUILayout.Space(5);
            //for (int i = 0; i < mssgs.Count; i++)
            //{
            //    GUILayout.Label(string.Format("{0}", mssgs[i]), style);
            //    GUILayout.Space(1);
            //}
            large = new Vector2((int)(Screen.width * 530.0f / 1280.0f), Screen.height - 200);
            scrollViewVector = GUILayout.BeginScrollView(scrollViewVector, GUILayout.Width(large.x), GUILayout.Height(large.y));
            for (int i = 0; i < mssgs.Count; i++)
            {
                GUILayout.Label(string.Format("{0}", mssgs[i]), style);
                GUILayout.Space(1);
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.Space(5);
            if (mssgs.Count > 0 && GUILayout.Button("Clear", GUILayout.Width(50), GUILayout.Height(30)))
            {
                mssgs.Clear();
            }
        }
        private void OnDestroy()
        {
            if (!Enable) return;

            Resources.UnloadAsset(Texture2);
        }
        public void Add(string str)
        {
            index++;
            str = string.Format("({0}){1}", index.ToString(), str);
            mssgs.Add(str);
            Refresh();
        }

        public void Refresh()
        {
            if (mssgs.Count > 1000)
            {
                ReduceAtZero();
            }
        }

        private void ReduceAtZero()
        {
            mssgs.RemoveAt(0);
        }

        private void AfterInitRes(bool result)
        {
            GXmlDocument bKXml = new GXmlDocument("");
            //this.Enable = bKXml.GetValue<bool>("Enable");
            //this.IsShowGUI = bKXml.GetValue<bool>("IsShowGUI");
            //this.texturName = bKXml.GetValue<string>("texturName");
            //modeDisPlayMode = bKXml.GetValue<GlobalDisPlayMode>("modeDisPlayMode");
            //Debug.LogError(modeDisPlayMode.ToString());

            //TestType<bool>(typeof(bool));
            //TestType<bool>(typeof(int));
            //TestType<bool>(typeof(float));
            //TestType<GlobalDisPlayMode>(typeof(bool));
            //TestType<GlobalDisPlayMode>(typeof(GlobalDisPlayMode));

            if (Enable) InitDisPlayMode();

            msg = string.Format("<i><color=yellow> - Please press F5 to display/hide debugging information ! -  over </color></i>");
            StartCoroutine(Enumerator());
        }

        private void InitDisPlayMode()
        {
#if UNITY_EDITOR
            //InitOnGUIDisPlay();
#elif UNITY_ANDROID || UNITY_IOS
        // 客户端
        //InitUGUIDisPlay();
#elif UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WBEGL
        // 控制端
        //InitOnGUIDisPlay();
#else
        // 未定义
#endif

        }

        private IEnumerator DownLoad(string url, string savepath, Action<bool> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            //Debug.LogError("url : " + url);
            request.timeout = 6000;
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.LogWarningFormat("request.error >>>>>>>> !!!  " + request.error + " From " + url);
                //callback(false);
            }
            else
            {
                if (File.Exists(savepath))
                    File.Delete(savepath);
                File.WriteAllBytes(savepath, request.downloadHandler.data);
                if (File.Exists(savepath))
                    callback(true);
                else
                    callback(false);
            }
            if (File.Exists(savepath))
                File.Delete(savepath);
        }

        private IEnumerator DownLoadFromCloud47(string url, string savepath, Action<bool> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            //Debug.LogError("url : " + url);
            request.timeout = 6000;
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.LogError("request.error >>>>>>>> !!!  " + request.error + " From " + url);
                callback(false);
            }
            else
            {
                if (File.Exists(savepath))
                    File.Delete(savepath);
                File.WriteAllBytes(savepath, request.downloadHandler.data);
                if (File.Exists(savepath))
                    callback(true);
                else
                    callback(false);
            }
            if (File.Exists(savepath))
                File.Delete(savepath);
        }

        private WaitForSeconds wait = new WaitForSeconds(5);
        private WaitForEndOfFrame frame = new WaitForEndOfFrame();
        private IEnumerator Enumerator()
        {
            while (true)
            {
                yield return wait;
                yield return frame;
                Check();
            }
        }
        private bool Check()
        {
            if (mssgs.Count > 10)
            {
                ReduceAtZero();
                return true;
            }

            return false;
        }

        private int _fps = 0;
        private Color _fpsColor = Color.white;
        private int _frameNumber = 0;
        private float _lastShowFPSTime = 0f;

        protected virtual void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftControl) && UnityEngine.Input.GetKeyUp(KeyCode.G))
            {
                Enable = !Enable;
            }

            _frameNumber += 1;
            float time = Time.realtimeSinceStartup - _lastShowFPSTime;
            if (time >= 1)
            {
                _fps = (int)(_frameNumber / time);
                _frameNumber = 0;
                _lastShowFPSTime = Time.realtimeSinceStartup;
            }
        }

        public enum SettingsType
        {
            None,
            Cloud,
            Native,
            Control,
            Other
        }
        public enum DisPlayMode
        {
            None,
            OnGUIDisPlay,
            UGUIDisPlay,
            Other
        }

        #region BKGlobalGUIInfoManagerEditor
#if UNITY_EDITOR
        [ExecuteInEditMode]
        [CanEditMultipleObjects] //sure why not
        [CustomEditor(typeof(GlobalGUIInfoManager))]
        public class GlobalGUIInfoManagerEditor : Editor
        {
            protected void OnEnable()
            {
            }

            public override void OnInspectorGUI()
            {
                GlobalGUIInfoManager manager = target as GlobalGUIInfoManager;
                GUI.changed = false;
                serializedObject.Update();
                //GUI style 设置
                GUIStyle firstLevelStyle = new GUIStyle(GUI.skin.label);
                firstLevelStyle.alignment = TextAnchor.UpperLeft;
                firstLevelStyle.fontStyle = FontStyle.Normal;
                firstLevelStyle.fontSize = 11;
                firstLevelStyle.wordWrap = true;

                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                boxStyle.fontStyle = FontStyle.Bold;
                boxStyle.alignment = TextAnchor.UpperLeft;

                #region ShowParameter
                GUILayout.BeginVertical("", boxStyle);
                manager.IsShowGUIEditor = EditorGUILayout.BeginToggleGroup(string.Format("IsShowGUIEditor"), manager.IsShowGUIEditor);
                EditorGUILayout.EndToggleGroup();
                GUILayout.EndVertical();

                if (manager.IsShowGUIEditor)
                {
                    foreach (var item in manager.mssgs)
                    {
                        GUILayout.BeginVertical("", boxStyle);
                        GUILayout.Label("-- " + item + " -", firstLevelStyle);
                        GUILayout.EndVertical();
                    }
                }
                #endregion

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(manager);
                }
                serializedObject.ApplyModifiedProperties();
            }
        }

#endif
        #endregion
    }

    public enum GlobalDisPlayMode
    {
        None,
        FollewHead,
        FollewPlayer,
        FollewOther,
        StayOriState,
        Other
    }

}