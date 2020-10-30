using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// RealConfigsManager
    /// </summary>
    public class RealConfigsManager : MonoBehaviour
    {
        #region RealConfigsManager Instance

        /// <summary>
        /// 
        /// </summary>
        protected static bool initialized;

        /// <summary>
        /// 
        /// </summary>
        protected static RealConfigsManager instance;
        /// <summary>
        /// 
        /// </summary>
        public static RealConfigsManager Instance
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
                GameObject go = new GameObject("RealConfigsManager");
                instance = go.AddComponent<RealConfigsManager>();
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
            Debug.LogWarningFormat("[{0}] say: Hello£¬World !! {1}", this, System.DateTime.Now);
        }

        #endregion

        private void Refresh()
        {
            //Debug.Log(realConfigs.ToString());

        }


    }
}