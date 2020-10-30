using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MFramework_Unity.Tools;
using System.Linq;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           GameFacade.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 20:26:8
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    /// <summary>
    /// GameFacade
    /// </summary>
    public abstract class Facade : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public static Facade Instance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, object> hasDicts = new Dictionary<string, object>();
        /// <summary>
        /// 
        /// </summary>
        public static Hashtable hashtable = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {
            Instance = this;

            ObserveManager.Instance.SayHello();
            DOTweenTimerManager.Instance.SayHello();
            ObjectPoolManager.Instance.SayHello();
            Loom.Current.SayHello();
            ResourceIOTool.GetInstance().SayHello();
            TimerManager.Instance.SayHello();


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
        protected virtual void OnDestroy()
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Update()
        {

        }

    }

}