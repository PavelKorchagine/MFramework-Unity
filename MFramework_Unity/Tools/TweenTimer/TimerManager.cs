using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// TimerManager 时间定时器，延迟定时器，（有限/无限）循环定时器，秒定时器
    /// 帧定时器，特定帧定时器，倒计时
    /// </summary>
    public class TimerManager : MonoBehaviour
    {
        /// <summary>
        /// 时间定时器队列
        /// </summary>
        public Queue<TimerGo> timerQueue = new Queue<TimerGo>();

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }

        #region Instance

        private static bool initialized;

        private static TimerManager instance;
        /// <summary>
        /// Instance
        /// </summary>
        public static TimerManager Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }
        private static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                GameObject go = new GameObject("TimerManager");
                instance = go.AddComponent<TimerManager>();
            }
        }
        private void Awake()
        {
            instance = this;
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;
        }
        #endregion

    }



    /// <summary>
    /// 时间定时器类型
    /// </summary>
    public enum TimerType
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class OnTimerGoStartEvent : UnityEvent<float>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class OnTimerGoEndEvent : UnityEvent<float>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class OnTimerGoingEvent : UnityEvent<float>
    {

    }

}
