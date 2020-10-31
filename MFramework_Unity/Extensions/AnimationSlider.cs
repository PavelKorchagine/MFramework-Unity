#region using ReferenceLibrary(.dll)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text;
using System.IO;
using UnityEngine.Events;
#endregion

namespace MFramework_Unity
{
    /// <summary>
    /// OnAnimationProcessEvent
    /// </summary>
    public class OnAnimationProcessEvent : UnityEvent<object> { }

    /// <summary>
    /// AnimationSlider
    /// </summary>
    public class AnimationSlider : MonoBehaviour
    {
        /// <summary>
        /// excuteClip
        /// </summary>
        [SerializeField] protected string excuteClip = "ex";
        /// <summary>
        /// ani
        /// </summary>
        protected Animation ani
        {
            get
            {
                if (_ani == null)
                {
                    _ani = gameObject.GetComponent<Animation>();
                }
                return _ani;
            }
        }
        /// <summary>
        /// _ani
        /// </summary>
        [SerializeField] protected Animation _ani;
        /// <summary>
        /// process
        /// </summary>
        public float process
        {
            get
            {
                return _process;
            }
            set
            {
                _process = value;
                ToProcess(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Range(0, 1)] [SerializeField] protected float _process = 0;
        /// <summary>
        /// 
        /// </summary>
        public float CurrentTimer = 0;
        /// <summary>
        /// 
        /// </summary>
        public bool IsTest = false;
        /// <summary>
        /// 
        /// </summary>
        public OnAnimationProcessEvent onProcessEvent = new OnAnimationProcessEvent();
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] protected int excuteLayer = 0;

        private void Start()
        {
            //Debug.Log(ani.GetClip(excuteClip).length);
            //Debug.Log(ani[excuteClip].length);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="second"></param>
        public void ToTimerSecond(float second)
        {
            if (ani == null) return;
            //var process = second / ani.GetCurrentAnimatorStateInfo(excuteLayer).length;
            var process = second / ani.clip.length;
            //ani.Play(excuteClip, excuteLayer, process);
            //ani.CrossFade(ani.clip.name, )
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excuteClip"></param>
        /// <param name="excuteLayer"></param>
        /// <param name="second"></param>
        public void ToTimerSecond(string excuteClip, int excuteLayer = 0, float second = 0)
        {
            if (ani == null) return;
            //var process = second / ani.GetCurrentAnimatorStateInfo(excuteLayer).length;
            //ani.Play(excuteClip, excuteLayer, process);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        public void ToProcess(float process)
        {
            if (ani == null) return;
            //ani.Play(excuteClip, excuteLayer, process);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excuteClip"></param>
        /// <param name="excuteLayer"></param>
        /// <param name="process"></param>
        public void ToProcess(string excuteClip, int excuteLayer = 0, float process = 0)
        {
            if (ani == null) return;
            this.excuteClip = excuteClip;
            this.excuteLayer = excuteLayer;
            // ani.Play(excuteClip, excuteLayer, process);
        }

        private void Update()
        {
            if (ani == null) return;

            //ani.PlayQueued()
        }
    }
}
