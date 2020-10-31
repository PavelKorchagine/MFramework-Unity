using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;

namespace MFramework_Unity
{
    /// <summary>
    /// 模块化
    /// </summary>
    public class BaseModule : BaseMono, IObserver
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
