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
    public abstract class BaseMonoAbstract : MonoBehaviour, IMono
    {
        /// <summary>
        /// driver
        /// </summary>
        protected MonoDriver driver;

        #region Unity API

        /// <summary>
        /// OnValidate
        /// </summary>
        protected virtual void OnValidate()
        {

        }

        /// <summary>
        /// Reset
        /// </summary>
        protected virtual void Reset()
        {
        }

        /// <summary>
        /// Awake
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
        /// OnDestroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            driver = null;
            OnGODestroy();
        }

        /// <summary>
        /// OnEnable
        /// </summary>
        protected virtual void OnEnable()
        {
            driver.Register(this);
        }

        /// <summary>
        /// OnDisable
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
        /// OnPreRender
        /// </summary>
        protected virtual void OnPreRender()
        {

        }

        /// <summary>
        /// OnPostRender
        /// </summary>
        protected virtual void OnPostRender()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="focus"></param>
        protected virtual void OnApplicationFocus(bool focus)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pause"></param>
        protected virtual void OnApplicationPause(bool pause)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnApplicationQuit()
        {

        }

        #endregion

        #region 扩展方法

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

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnCreate();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnGet();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnReset();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnInit(params object[] args);

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// OnEnter
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// OnExit
        /// </summary>
        public abstract void OnExit();

        /// <summary>
        /// 
        /// </summary>
        public abstract void OnDie();


        #endregion


    }
}