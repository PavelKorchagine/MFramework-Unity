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
        protected Facade facade;

        [ContextMenu("GetComponent")]
        public virtual void Get()
        {

        }

        public virtual void OnInit(params object[] paras)
        {
            foreach (var item in paras)
            {
                if (item is Facade)
                {
                    facade = item as Facade;
                }
            }
        }

        public virtual void OnInit()
        {

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }


        public virtual void OnResetRealtime()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnGOjDestroy()
        {

        }

        protected virtual void Reset()
        {

        }

        protected virtual void Awake()
        {

        }
        protected virtual void OnEnable()
        {

        }
        protected virtual void OnDisable()
        {

        }
        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void OnApplicationPause(bool pause)
        {

        }

        protected virtual void OnDestroy()
        {

        }

    }

}
