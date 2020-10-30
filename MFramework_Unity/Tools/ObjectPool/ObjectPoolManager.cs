using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           ObjectPoolManager.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/8/24 11:21:16
**************************************************************************************************************
*/

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// ObjectType
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// 
        /// </summary>
        WorldObject,
        /// <summary>
        /// 
        /// </summary>
        UI,
    }

    /// <summary>
    /// ObjectPoolManager
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour
    {
        #region ObjectPoolManager Instance

        /// <summary>
        /// 
        /// </summary>
        protected static bool initialized;

        /// <summary>
        /// 
        /// </summary>
        protected static ObjectPoolManager instance;
        /// <summary>
        /// 
        /// </summary>
        public static ObjectPoolManager Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                GameObject go = new GameObject("ObjectPoolManager");
                instance = go.AddComponent<ObjectPoolManager>();

                InitializeRoots();
            }
        }

        private static void InitializeRoots()
        {
            if (_objectPoolsRoot == null)
            {
                _objectPoolsRoot = new GameObject("ObjectPoolsRoot").transform;
                _objectPoolsRoot.localPosition = _objectPoolsRoot.localEulerAngles = Vector3.zero;
                _objectPoolsRoot.localScale = Vector3.one;
            }

            if (_objectPoolsUIRoot == null)
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                if (canvas == null)
                {
                    canvas = new GameObject("Canvas").AddComponent<Canvas>();
                }
                _objectPoolsUIRoot = new GameObject("ObjectPoolsUIRoot").AddComponent<RectTransform>();
                _objectPoolsUIRoot.SetParent(canvas.transform);
                _objectPoolsUIRoot.localPosition = _objectPoolsUIRoot.localEulerAngles = Vector3.zero;
                _objectPoolsUIRoot.localScale = Vector3.one;
            }
        }

        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            instance = this;
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;

        }

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Type, IEnumerable> objectPoolDict = new Dictionary<Type, IEnumerable>();
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<Type, object> poolDict = new Dictionary<Type, object>();
        /// <summary>
        /// 
        /// </summary>
        [HideInInspector] public bool _showParameter = true;

        /// <summary>
        /// 
        /// </summary>
        public static Transform ObjectPoolsRoot
        {
            get
            {
                InitializeRoots();
                return _objectPoolsRoot;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected static Transform _objectPoolsRoot;

        /// <summary>
        /// 
        /// </summary>
        public static RectTransform ObjectPoolsUIRoot
        {
            get
            {
                InitializeRoots();
                return _objectPoolsUIRoot;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected static RectTransform _objectPoolsUIRoot;

        /// <summary>
        /// SetRootTran
        /// </summary>
        /// <param name="tar"></param>
        /// <param name="type"></param>
        public void SetRootTran(Transform tar, ObjectType type = ObjectType.WorldObject)
        {
            switch (type)
            {
                case ObjectType.WorldObject:
                    _objectPoolsRoot = tar;
                    break;
                case ObjectType.UI:
                    _objectPoolsUIRoot = tar as RectTransform;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// GetBornPointTran
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Transform GetBornPointTran<T>() where T : Component
        {
            try
            {
                //Debug.Log("⑤ ObjectPoolManager:[GetBornPointTran<T>] " + typeof(T));

                Transform root = null;
                //foreach (var item in poolDict.Keys)
                //{
                //    Debug.Log("⑤ typeof(T) == Type] Type = "+ item+" -- " + (typeof(T) == item));
                //}

                //Debug.Log("⑤ ObjectPoolManager:[poolDict.ContainsKey(typeof(T))] " + poolDict.ContainsKey(typeof(T)));
                if (poolDict.ContainsKey(typeof(T)))
                {
                    ObjectPool<T> _pool = poolDict[typeof(T)] as ObjectPool<T>;
                    //Debug.Log(_pool.type);

                    root = _pool.type == ObjectType.WorldObject ? ObjectPoolsRoot
                               : ObjectPoolsUIRoot;

                    if (root != null)
                    {
                        Transform tar = root.Find(typeof(T).Name);
                        if (tar == null)
                        {
                            tar = _pool.type == ObjectType.WorldObject ? new GameObject(typeof(T).Name).transform
                                : new GameObject(typeof(T).Name).AddComponent<RectTransform>();
                        }
                        tar.parent = root;
                        tar.localPosition = tar.localEulerAngles = Vector3.zero;
                        tar.localScale = Vector3.one;
                        return tar;
                    }
                }

                
                return null;
            }
            catch (Exception)
            {
                Debug.LogError("【严重错误】 ObjectPoolsRoot Or ObjectPoolsUIRoot 为空");
                return null;
            }

        }

        /// <summary>
        /// GetObjectPool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="it"></param>
        /// <param name="ori"></param>
        /// <param name="bornPointTran"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectPool<T> GetObjectPool<T>(T it, int ori, Transform bornPointTran = null, ObjectType type = ObjectType.WorldObject) where T : Component
        {
            if (!initialized)
            {
                Initialize();
            }
            try
            {
                if (!objectPoolDict.ContainsKey(typeof(T)))
                {
                    if (!poolDict.ContainsKey(typeof(T))) poolDict.Add(typeof(T), new ObjectPool<T>(type));

                    Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                    ObjectPool<T> objectPool = new ObjectPool<T>(it, ori, bornPointTran, type);
                    //ObjectPool<T> objectPool = null;
                    objectPoolQueue.Enqueue(objectPool);
                    objectPoolDict.Add(typeof(T), objectPoolQueue);
                }
                else if (objectPoolDict.ContainsKey(typeof(T)) && objectPoolDict[typeof(T)] as Queue<ObjectPool<T>> != null)
                {
                    Queue<ObjectPool<T>> objectPoolQueue = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
                    if (objectPoolQueue.Count <= 0)
                    {
                        ObjectPool<T> objectPool = new ObjectPool<T>(it, ori, bornPointTran, type);
                        objectPoolQueue.Enqueue(objectPool);
                    }
                }

                return (objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>).Dequeue();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GetObjectPool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="it"></param>
        /// <param name="ori"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectPool<T> GetObjectPool<T>(T it, int ori, ObjectType type = ObjectType.WorldObject) where T : Component
        {
            if (!initialized)
            {
                Initialize();
            }
            try
            {
                if (!objectPoolDict.ContainsKey(typeof(T)))
                {
                    if (!poolDict.ContainsKey(typeof(T))) poolDict.Add(typeof(T), new ObjectPool<T>(type));

                    Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                    ObjectPool<T> objectPool = new ObjectPool<T>(it, ori, type);
                    //ObjectPool<T> objectPool = null;
                    objectPoolQueue.Enqueue(objectPool);
                    objectPoolDict.Add(typeof(T), objectPoolQueue);
                }
                else if (objectPoolDict.ContainsKey(typeof(T)) && objectPoolDict[typeof(T)] as Queue<ObjectPool<T>> != null)
                {
                    Queue<ObjectPool<T>> objectPoolQueue = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
                    if (objectPoolQueue.Count <= 0)
                    {
                        ObjectPool<T> objectPool = new ObjectPool<T>(it, ori, type);
                        objectPoolQueue.Enqueue(objectPool);
                    }
                }

                return (objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>).Dequeue();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GetObjectPool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="it"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectPool<T> GetObjectPool<T>(T it, ObjectType type = ObjectType.WorldObject) where T : Component
        {
            if (!initialized)
            {
                Initialize();
            }
            try
            {
                if (!objectPoolDict.ContainsKey(typeof(T)))
                {
                    if (!poolDict.ContainsKey(typeof(T))) poolDict.Add(typeof(T), new ObjectPool<T>(type));

                    Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                    ObjectPool<T> objectPool = new ObjectPool<T>(it, 5, type);
                    //ObjectPool<T> objectPool = null;
                    objectPoolQueue.Enqueue(objectPool);
                    objectPoolDict.Add(typeof(T), objectPoolQueue);
                }
                else if (objectPoolDict.ContainsKey(typeof(T)) && objectPoolDict[typeof(T)] as Queue<ObjectPool<T>> != null)
                {
                    Queue<ObjectPool<T>> objectPoolQueue = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
                    if (objectPoolQueue.Count <= 0)
                    {
                        ObjectPool<T> objectPool = new ObjectPool<T>(it, 5, type);
                        objectPoolQueue.Enqueue(objectPool);
                    }
                }

                return (objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>).Dequeue();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// ResumeObjectPool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectPool"></param>
        public void ResumeObjectPool<T>(ObjectPool<T> objectPool) where T : Component
        {
            try
            {
                if (!objectPoolDict.ContainsKey(typeof(T)))
                {
                    Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                    objectPoolDict.Add(typeof(T), objectPoolQueue);
                }

                Queue<ObjectPool<T>> que = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
                que.Enqueue(objectPool);

            }
            catch (Exception)
            {

            }
        }


        #region CustomEditor(typeof(ObjectPoolManager)
#if UNITY_EDITOR


        [CanEditMultipleObjects]
        [CustomEditor(typeof(ObjectPoolManager))]
        public class ObjectPoolManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                ObjectPoolManager manager = (ObjectPoolManager)target;
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
                manager._showParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager._showParameter);
                EditorGUILayout.EndToggleGroup();
                GUILayout.EndVertical();

                if (manager._showParameter)
                {
                    GUILayout.BeginVertical("", boxStyle);
                    base.OnInspectorGUI();

                    if (Application.isPlaying)
                    {
                        GUILayout.Label($"objectPoolDict", boxStyle);
                        foreach (var item in ObjectPoolManager.objectPoolDict.Keys)
                        {
                            GUILayout.Label($"Type: {item}");
                            foreach (var iteme in ObjectPoolManager.objectPoolDict[item])
                            {
                                GUILayout.Label($"ObjectPool<T>: {iteme}");
                            }
                        }
                        GUILayout.Label($"poolDict", boxStyle);
                        foreach (var item in ObjectPoolManager.poolDict.Keys)
                        {
                            GUILayout.Label($"Type: {item}");
                        }
                    }
                    //GUILayout.BeginHorizontal("", boxStyle);
                    //GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                }
        #endregion

                this.Repaint();
            }
        }
#endif
        #endregion

    }

}