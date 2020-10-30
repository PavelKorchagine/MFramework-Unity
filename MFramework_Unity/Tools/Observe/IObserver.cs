using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserver
    {
        //void ListenMethod(ObserveEvent observeEvent, long code, object msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observeType"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        void ListenMethod(Type observeType, long code, object msg);
    }

    /// <summary>
    /// ObNull
    /// </summary>
    public struct ObNull
    {
        /// <summary>
        /// type
        /// </summary>
        public Type type;
        /// <summary>
        /// observer
        /// </summary>
        public IObserver observer;

        /// <summary>
        /// ObNull
        /// </summary>
        /// <param name="type"></param>
        /// <param name="observer"></param>
        public ObNull(Type type, IObserver observer)
        {
            this.type = type;
            this.observer = observer;
        }

    }
}