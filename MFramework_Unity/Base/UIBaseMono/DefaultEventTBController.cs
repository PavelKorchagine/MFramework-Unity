using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFramework_Unity;
using DG.Tweening;
using MFramework_Unity.Tools;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           EventTriggerButton.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 19:56:35
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    /// <summary>
    /// EventTriggerButton
    /// </summary>
    [RequireComponent(typeof(EventTriggerButton))]
    public class DefaultEventTBController : UIBaseMono
    {
        /// <summary>
        /// 
        /// </summary>
        protected EventTriggerButton eventTrigger;
        [SerializeField] private float rate = 1.05f;
        [SerializeField] private float dur = 0.5f;

        /// <summary>
        /// Start
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            eventTrigger = gameObject.GetComponentReal<EventTriggerButton>();
            eventTrigger.SetDefaultTouchState(rate, dur);
        }

    }
}