using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion
using System;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// DOTweenTimerManager
    /// </summary>
    public class DOTweenTimerManager : MonoBehaviour
    {
        #region Instance

        private static bool initialized;

        private static DOTweenTimerManager instance;
        public static DOTweenTimerManager Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }
        private static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                GameObject go = new GameObject("DOTweenTimerManager");
                instance = go.AddComponent<DOTweenTimerManager>();
            }
        }
        private void Awake()
        {
            instance = this;
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;
        }
        #endregion

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }
        public bool ShowParameter = false;
        protected static bool isDefaultInited;
        public static readonly Queue<Transform> delayTrans = new Queue<Transform>();
        public static readonly Dictionary<TweenCallback, Transform> delayActionDict = new Dictionary<TweenCallback, Transform>();

        /// <summary>
        /// AddDelayCallback
        /// </summary>
        /// <param name="callbackAction"></param>
        /// <param name="delayDur"></param>
        /// <param name="isFocusAdd"></param>
        public static void AddDelayCallback(Action callbackAction, float delayDur, bool isFocusAdd = false)
        {
            TweenCallback callback = () => callbackAction();
            _AddDelayCallback(callback, delayDur, isFocusAdd);
         
        }

        /// <summary>
        /// AddDelayCallback
        /// </summary>
        /// <param name="callbackAction"></param>
        public static void AddDelayCallback(Action callbackAction)
        {
            TweenCallback callback = () => callbackAction();
            _AddDelayCallback(callback, 0.5f, false);

        }

        protected static void _AddDelayCallback(TweenCallback callback, float delayDur, bool isFocusAdd = false)
        {
            Initialize();

            if (!isDefaultInited)
            {
                DefaultInit();
                isDefaultInited = true;
            }
            if (isFocusAdd)
            {
                RemoveDelayCallback(callback);
            }
            if (delayTrans.Count <= 0)
            {
                delayTrans.Enqueue(GetTran());
            }
            if (delayActionDict.ContainsKey(callback)) return;

            delayActionDict.Add(callback, delayTrans.Dequeue());
            delayActionDict[callback].DOLocalMove(Vector3.zero, delayDur).OnComplete(() =>
            {
                callback();
                delayTrans.Enqueue(delayActionDict[callback]);
                delayActionDict.Remove(callback);
            });
        }
        protected static void RemoveDelayCallback(TweenCallback callback)
        {
            if (!delayActionDict.ContainsKey(callback)) return;

            delayActionDict.Remove(callback);
        }
        protected static void DefaultInit()
        {
            for (int i = 0; i < 5; i++)
            {
                delayTrans.Enqueue(GetTran());

            }
        }
        protected static Transform GetTran()
        {
            Transform tr = new GameObject("DelayTran").transform;
            tr.parent = instance.transform;
            tr.localPosition = Vector3.zero;
            return tr;
        }
        protected void Update()
        {
            //if (UnityEngine.Input.GetKeyUp("1"))
            //{
            //    TweenCallback call = Test;
            //    AddDelayCallback(call, 3);
            //}
        }

        protected void Test()
        {
            Debug.Log(111);
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(DOTweenTimerManager))]
        public class DOTweenTimerManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                //base.OnInspectorGUI();
                DOTweenTimerManager manager = target as DOTweenTimerManager;
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
                manager.ShowParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager.ShowParameter);
                EditorGUILayout.EndToggleGroup();
                GUILayout.EndVertical();

                if (manager.ShowParameter)
                {
                    if (manager == null)
                    {
                        return;
                    }
                    if (!EditorApplication.isPlaying)
                    {
                        return;
                    }
                    EditorGUILayout.LabelField("注册的 delayActionDict：" + manager.delayActionDict.Count.ToString());
                    foreach (var item in manager.delayActionDict)
                    {
                        EditorGUILayout.LabelField(item.Key.Target.ToString() + " :: " + item.Key.Method.Name + " :: " + item.Value.ToString());
                    }
                    EditorGUILayout.LabelField("注册的 delayTrans：" + manager.delayTrans.Count.ToString());
                    foreach (var item in manager.delayTrans)
                    {
                        EditorGUILayout.LabelField(item.ToString());
                    }
                    this.Repaint();
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
}