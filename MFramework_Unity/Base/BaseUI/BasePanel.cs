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
File Name/文件名:           UIPanelBase.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 19:54:49
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    /// <summary>
    /// UIPanelBase
    /// </summary>
    public class BasePanel : BaseMono, IObserver
    {
        /// <summary>
        /// isFirstEnter
        /// </summary>
        protected bool isFirstEnter = true;
        /// <summary>
        /// rectTran
        /// </summary>
        protected RectTransform rectTran;

        /// <summary>
        /// GetComponentOV
        /// </summary>
        public virtual void GetComponentOV()
        {
        }

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="paras"></param>
        public override void OnInit(params object[] paras)
        {
            base.OnInit(paras);
        }

        /// <summary>
        /// OnInit
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// 界面被显示出来 被实例化时
        /// </summary>
        public override void OnEnter()
        {
            base.OnEnter();
            transform.SetAsLastSibling();
            if (isFirstEnter)
            {
                FirstOnEnter();
                isFirstEnter = false;
            }
        }

        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {
            transform.SetAsFirstSibling();
        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {
            transform.SetAsLastSibling();
        }

        /// <summary>
        /// 界面不显示,退出这个界面，界面被关系
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();
            transform.SetAsFirstSibling();
        }

        /// <summary>
        /// FirstOnEnter
        /// </summary>
        protected virtual void FirstOnEnter()
        {
            rectTran = transform as RectTransform;
        }

        /// <summary>
        /// ListenMethod
        /// </summary>
        /// <param name="observeType"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public virtual void ListenMethod(Type observeType, long code, object msg)
        {
        }
    }

}