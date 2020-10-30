using MFramework_Unity.Tools;
using System;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity
{
    /// <summary>
    /// BaseMonoAbstract
    /// </summary>
    public abstract class BaseMonoAbstract : MonoBehaviour
    {
        /// <summary>
        /// driver
        /// </summary>
        protected MonoDriver driver;

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {
            if (transform.root == null)
                driver = transform.GetComponentReal<MonoDriver>();
            else
                driver = transform.root.GetComponentReal<MonoDriver>();
            OnAwake();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDestroy()
        {
            driver = null;
            OnGODestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnEnable()
        {
            driver.Register(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDisable()
        {
            driver.Remove(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Start()
        {
            OnStart();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnUpdateAbstract()
        {
            OnUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnFixUpdateAbstract()
        {
            OnFixUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnLateUpdateAbstract()
        {
            OnLateUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnInit(params object[] args);

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnGet();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnDie();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnReset();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnAwake();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnUpdate();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnFixUpdate();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnLateUpdate();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnGODestroy();

    }
}