using System;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// ObserveManager
    /// </summary>
    public class ObserveManager : MonoBehaviour
    {
        /// <summary>
        /// pairs
        /// </summary>
        public static Dictionary<Type, List<IObserver>> pairs = new Dictionary<Type, List<IObserver>>();

        #region pairsBools
        /// <summary>
        /// pairsBools
        /// </summary>
        public static Dictionary<Type, bool> pairsBools = new Dictionary<Type, bool>();
        #endregion

        //字段定义
        /// <summary>
        /// ShowParameter
        /// </summary>
        public static bool ShowParameter = false;
        private static List<DelayedNodifyMessage> current = new List<DelayedNodifyMessage>();
        private static Queue<DelayedNodifyMessage> pool = new Queue<DelayedNodifyMessage>();

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }

        #region Instance

        private static bool initialized;

        private static ObserveManager instance;
        /// <summary>
        /// Instance
        /// </summary>
        public static ObserveManager Instance
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
                GameObject go = new GameObject("ObserveManager");
                instance = go.AddComponent<ObserveManager>();
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


        //API

        /// <summary>
        /// Update
        /// </summary>
        protected virtual void Update()
        {
            HandleDelayedNodify();
            CheckNull();
        }

        /// <summary>
        /// 检查的时间间隔
        /// </summary>
        public static short checkInterval = 300;
        private static short interval = 0;

        /// <summary>
        /// CheckNull
        /// </summary>
        public static void CheckNull()
        {
            interval++;

            if (interval < checkInterval)
                return;
            else if (interval >= checkInterval)
            {
                interval = 0;
                lock (pairs)
                {
                    Queue<ObNull> _obnull = new Queue<ObNull>();
                    foreach (var item in pairs.Keys)
                    {
                        foreach (var iteme in pairs[item])
                        {
                            if (iteme.Equals(null))
                                _obnull.Enqueue(new ObNull(item, iteme));
                        }
                    }

                    foreach (var item in _obnull)
                    {
                        pairs[item.type].Remove(item.observer);
                    }
                   
                }
                
            }
        }

        /// <summary>
        /// CheckNullOnce
        /// </summary>
        public static void CheckNullOnce()
        {
            lock (pairs)
            {
                Queue<ObNull> _obnull = new Queue<ObNull>();
                foreach (var item in pairs.Keys)
                {
                    foreach (var iteme in pairs[item])
                    {
                        if (iteme.Equals(null))
                            _obnull.Enqueue(new ObNull(item, iteme));
                    }

                }

                //Debug.Log("CheckNull, nulls: " + nulls.Count);
                foreach (var item in _obnull)
                {
                    pairs[item.type].Remove(item.observer);
                }
            }
        }

        //Func
        private void HandleDelayedNodify()
        {
            if (pool.Count == 0) return;
            current.Clear();
            lock (pool)
            {
                while (pool.Count > 0)
                {
                    current.Add(pool.Dequeue());
                }
            }
            try
            {
                for (int i = 0; i < current.Count; i++)
                {
                    for (int j = 0; j < pairs[current[i].observeType].Count; j++)
                    {
                        pairs[current[i].observeType][j].ListenMethod(current[i].observeType, current[i].code, current[i].msg);
                    }
                }
            }
            catch (Exception)
            {
                //Debug.LogFormat("<i><color=yellow> - HandleDelayedNodify Exception - over </color></i>");
            }
        }

        /// <summary>
        /// AddListener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observer"></param>
        public static void AddListener<T>(IObserver observer)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(typeof(T)))
                {
                    pairs.Add(typeof(T), new List<IObserver>());

                    pairsBools.Add(typeof(T), false);
                }
                if (!pairs[typeof(T)].Contains(observer))
                {
                    pairs[typeof(T)].Add(observer);
                }
            }
        }

        /// <summary>
        /// AddListener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="observer"></param>
        public static void AddListener<T>(T t, IObserver observer)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(t.GetType()))
                {
                    pairs.Add(t.GetType(), new List<IObserver>());

                    pairsBools.Add(typeof(T), false);
                }
                if (!pairs[t.GetType()].Contains(observer))
                {
                    pairs[t.GetType()].Add(observer);
                }
            }
        }

        /// <summary>
        /// RemoveListener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observer"></param>
        public static void RemoveListener<T>(IObserver observer)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(typeof(T))) return;
                if (!pairs[typeof(T)].Contains(observer)) return;
                pairs[typeof(T)].Remove(observer);
            }
        }

        /// <summary>
        /// RemoveListener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="observer"></param>
        public static void RemoveListener<T>(T t, IObserver observer)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(t.GetType())) return;
                if (!pairs[t.GetType()].Contains(observer)) return;
                pairs[t.GetType()].Remove(observer);
            }
        }

        /// <summary>
        /// RemoveListener 少用
        /// </summary>
        /// <param name="observer"></param>
        [Obsolete("少用")]
        public static void RemoveListener(IObserver observer)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                foreach (var item in pairs.Values)
                {
                    if (item.Contains(observer))
                    {
                        item.Remove(observer);
                    }
                }
            }
        }

        /// <summary>
        /// Notify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static void Notify<T>(long code, object msg)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(typeof(T))) return;
                for (int i = 0; i < pairs[typeof(T)].Count; i++)
                {
                    IObserver observer = pairs[typeof(T)][i];
                    if (observer == null)
                        pairs[typeof(T)].RemoveAt(i);
                    else
                        pairs[typeof(T)][i].ListenMethod(typeof(T), code, msg);

                }
            }
        }

        /// <summary>
        /// Notify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static void Notify<T>(T t, long code, object msg)
        {
            Initialize();
            CheckNullOnce();

            lock (pairs)
            {
                if (!pairs.ContainsKey(t.GetType())) return;
                for (int i = 0; i < pairs[t.GetType()].Count; i++)
                {
                    IObserver observer = pairs[t.GetType()][i];
                    if (observer == null)
                        pairs[t.GetType()].RemoveAt(i);
                    else
                        pairs[t.GetType()][i].ListenMethod(t.GetType(), code, msg);
                }
            }
        }

        /// <summary>
        /// DelayedNodify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static void DelayedNodify<T>(long code, object msg)
        {
            Initialize();
            CheckNullOnce();

            if (!pairs.ContainsKey(typeof(T))) return;
            lock (pool)
            {
                pool.Enqueue(new DelayedNodifyMessage(typeof(T), code, msg));
            }
        }

        /// <summary>
        /// DelayedNodify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static void DelayedNodify<T>(T t, long code, object msg)
        {
            Initialize();
            CheckNullOnce();

            if (!pairs.ContainsKey(t.GetType())) return;
            lock (pool)
            {
                pool.Enqueue(new DelayedNodifyMessage(typeof(T), code, msg));
            }
        }
    }

}