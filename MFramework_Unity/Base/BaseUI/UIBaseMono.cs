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
File Name/文件名:           UIBase.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 19:55:23
**************************************************************************************************************
*/

namespace MFramework_Unity
{

    /// <summary>
    /// UIBase
    /// </summary>
    public class UIBaseMono : BaseMono
    {
        /// <summary>
        /// OnAwake
        /// </summary>
        protected override void OnAwake()
        {
            base.OnAwake();
        }

        /// <summary>
        /// OnGODestroy
        /// </summary>
        protected override void OnGODestroy()
        {
            base.OnGODestroy();
        }

        /// <summary>
        /// OnEnable
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        /// <summary>
        /// OnDisable
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        /// <summary>
        /// OnStart
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
        }

        /// <summary>
        /// OnUpdate
        /// </summary>
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        /// <summary>
        /// 有参数的初始化方法
        /// </summary>
        /// <param name="args"></param>
        public override void OnInit(params object[] args)
        {
            base.OnInit(args);
        }

        /// <summary>
        /// 无参数的初始化方法
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// 进入/启用/激活 方法
        /// </summary>
        public override void OnEnter()
        {
            base.OnEnter();
        }

        /// <summary>
        /// 退出/禁止/灭活 方法
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();
        }

    }

}