using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MFramework_Unity.Tools;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           BaseManager.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 20:30:37
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    /// <summary>
    /// BaseManager
    /// </summary>
    public class BaseManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        protected Facade facade;

        /// <summary>
        /// 
        /// </summary>
        [ContextMenu("GetComponent")]
        public virtual void Get()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paras"></param>
        public virtual void OnInit(params object[] paras)
        {
            facade = paras.FindTarget<Facade>();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnInit()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnExit()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnResetRealtime()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnLateUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnFixedUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnGOjDestroy()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Reset()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnEnable()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDisable()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Update()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void LateUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void FixedUpdate()
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
        protected virtual void OnDestroy()
        {

        }

    }

}
