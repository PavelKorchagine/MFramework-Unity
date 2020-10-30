using System;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// MonoSingletonSimple
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingletonSimple<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        /// <summary>
        /// Instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;

        /// <summary>
        /// OnDestroy
        /// </summary>
        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}
