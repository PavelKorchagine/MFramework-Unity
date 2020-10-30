using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity
{
    /// <summary>
    /// 所有Mono物体组件的基类
    /// </summary>
    public class BaseMono : BaseMonoAbstract
    {
        /// <summary>
        /// 自动初始化
        /// </summary>
        [Tooltip("自动初始化,请在该组件不纳入框架(MFramework)时勾选,纳入框架(MFramework)时取消勾选")]
        public bool autoToInit = false;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnValidate()
        {
            base.OnValidate();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
        }
        /// <summary>
        /// 
        /// </summary>
        protected sealed override void Awake()
        {
            base.Awake();
        }
        /// <summary>
        /// 
        /// </summary>
        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
        }
        /// <summary>
        /// 
        /// </summary>
        protected sealed override void Start()
        {
            base.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnPreRender()
        {
            base.OnPreRender();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnPostRender()
        {
            base.OnPostRender();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplicationFocus(bool focus)
        {
            base.OnApplicationFocus(focus);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplicationPause(bool pause)
        {
            base.OnApplicationPause(pause);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }

        /// <summary>
        /// OnAwake 在自动初始化时，带参的OnInit方法传入的是null
        /// </summary>
        protected override void OnAwake()
        {
            if (autoToInit)
                OnInit(null);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnGODestroy()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStart()
        {
            if (autoToInit)
                OnInit();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnUpdate()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnFixUpdate()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnLateUpdate()
        {

        }

        /// <summary>
        /// OnCreate
        /// </summary>
        public override void OnCreate()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnGet()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnReset()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnInit(params object[] args)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnInit()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnEnter()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnExit()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public override void OnDie()
        {

        }

    }
}