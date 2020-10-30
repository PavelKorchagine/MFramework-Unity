using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// 时间定时器
    /// </summary>
    public class TimerGo : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public TimerType timerType;
        /// <summary>
        /// 
        /// </summary>
        public OnTimerGoStartEvent onStart = new OnTimerGoStartEvent();
        /// <summary>
        /// 
        /// </summary>
        public OnTimerGoEndEvent onEnd = new OnTimerGoEndEvent();
        /// <summary>
        /// 
        /// </summary>
        public OnTimerGoingEvent enGoing = new OnTimerGoingEvent();

        /// <summary>
        /// 
        /// </summary>
        public TimerGo Start()
        {
            onStart.Invoke(0);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimerGo Stop()
        {
            onEnd.Invoke(1);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimerGo Pause()
        {

            return this;
        }

    }
}
