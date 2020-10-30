using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;

namespace MFramework_Unity
{
    /// <summary>
    /// 模块化
    /// </summary>
    public class InterateBaseModule : InterateBaseMono, IObserver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public override void OnInit(params object[] args)
        {
            base.OnInit(args);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();
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
