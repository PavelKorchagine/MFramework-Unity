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
    public class UIPanelBase : UIBase
    {
        protected bool isFirstEnter = true;
        protected RectTransform rectTran;

        public virtual void GetComponentOV()
        {

        }

        public override void OnInit(params object[] paras)
        {
            base.OnInit(paras);

            foreach (var item in paras)
            {
            }

        }

        public override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// 界面被显示出来 被实例化时
        /// </summary>
        public virtual void OnEnter()
        {
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
        public virtual void OnExit()
        {
            transform.SetAsFirstSibling();
        }

        protected virtual void FirstOnEnter()
        {
            rectTran = transform as RectTransform;
        }

    }

}