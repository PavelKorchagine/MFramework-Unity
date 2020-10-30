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
    public class UIBase : MonoBehaviour
    {
        public virtual void OnInit(params object[] paras)
        {

        }

        public virtual void OnInit()
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

    }

}