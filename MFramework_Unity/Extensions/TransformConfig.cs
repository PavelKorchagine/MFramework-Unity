using System;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// TransformConfig Transform 配置信息结构体
    /// </summary>
    [Serializable]
    public struct TransformExtensionConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public Transform parent;
        /// <summary>
        /// 
        /// </summary>
        public Vector3 localPosition;
        /// <summary>
        /// 
        /// </summary>
        public Vector3 localEulerAngles;
        /// <summary>
        /// 
        /// </summary>
        public Vector3 localScale;

        /// <summary>
        /// Transform 配置信息结构体 构造函数
        /// </summary>
        /// <param name="tran"></param>
        public TransformExtensionConfig(Transform tran = null)
        {
            if (tran != null)
            {
                parent = tran.parent;
                localPosition = tran.localPosition;
                localEulerAngles = tran.localEulerAngles;
                localScale = tran.localScale;
            }
            else
            {
                parent = null;
                localPosition = Vector3.zero;
                localEulerAngles = Vector3.zero;
                localScale = Vector3.one;
            }

        }

        /// <summary>
        /// Transform 配置信息结构体 构造函数
        /// </summary>
        /// <param name="localpos"></param>
        /// <param name="localeular"></param>
        /// <param name="localscale"></param>
        /// <param name="parent"></param>
        public TransformExtensionConfig(Vector3 localpos, Vector3 localeular, Vector3 localscale, Transform parent = null)
        {
            this.parent = parent;
            this.localPosition = localpos;
            this.localEulerAngles = localeular;
            this.localScale = localscale;
        }
    }
}
