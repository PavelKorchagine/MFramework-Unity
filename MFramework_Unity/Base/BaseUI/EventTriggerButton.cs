using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFramework_Unity;
using DG.Tweening;
using MFramework_Unity.Tools;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           EventTriggerButton.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 19:56:35
**************************************************************************************************************
*/

/*
 *      PointerEnter = 0,
        PointerExit = 1,
        PointerDown = 2,
        PointerUp = 3,
        PointerClick = 4,
        Drag = 5,
        Drop = 6,
        Scroll = 7,
        UpdateSelected = 8,
        Select = 9,
        Deselect = 10,
        Move = 11,
        InitializePotentialDrag = 12,
        BeginDrag = 13,
        EndDrag = 14,
        Submit = 15,
        Cancel = 16
*/

namespace MFramework_Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchDown(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouch(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchUp(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchClick(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchEnter(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchExit(BaseEventData eventData);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public delegate void OnTriggerTouchStay(BaseEventData eventData);

    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchClickedEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchClickedEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchEnterEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchEnterEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchExitEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchExitEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchStayEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchStayEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchDownEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchDownEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchUpEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchUpEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchDoubleClickedEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchDoubleClickedEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerTouchThreeClickedEvent : UnityEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchThreeClickedEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerBeginDragEvent : UnityEvent<BaseEventData>
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerBeginDragEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerEndDragEvent : UnityEvent<BaseEventData>
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerEndDragEvent()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TriggerOnDragEvent : UnityEvent<BaseEventData>
    {
        /// <summary>
        /// 
        /// </summary>
        public TriggerOnDragEvent()
        {

        }
    }


    /// <summary>
    /// EventTriggerButton
    /// </summary>
    [RequireComponent(typeof(EventTrigger))]
    public class EventTriggerButton : UIBaseMono
    {
        /// <summary>
        /// 
        /// </summary>
        protected EventTrigger EventTrigger
        {
            get
            {
                if (m_EventTrigger == null)
                {
                    m_EventTrigger = gameObject.GetComponentReal<EventTrigger>();
                }
                return m_EventTrigger;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected EventTrigger m_EventTrigger;
        /// <summary>
        /// 
        /// </summary>
        protected bool m_AllowTouchTriggerring = false;
        /// <summary>
        /// 
        /// </summary>
        protected bool b_Triggerring = false;
        /// <summary>
        /// 
        /// </summary>
        protected bool b_TouchStaying = false;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchDown onTriggerTouchDown;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouch onTriggerTouch;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchUp onTriggerTouchUp;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchClick onTriggerTouchClick;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchEnter onTriggerTouchEnter;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchExit onTriggerTouchExit;
        /// <summary>
        /// 
        /// </summary>
        public OnTriggerTouchStay onTriggerTouchStay;

        /// <summary>
        /// 
        /// </summary>
        protected BaseEventData eventData;
        /// <summary>
        /// 
        /// </summary>
        protected Vector3 defaultScale = Vector3.one;
        /// <summary>
        /// 
        /// </summary>
        protected float rate = 1.05f;
        /// <summary>
        /// 
        /// </summary>
        protected float dur = 0.5f;

        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchClickedEvent onClick { get; set; } = new TriggerTouchClickedEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchDownEvent onTouchDown { get; set; } = new TriggerTouchDownEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchUpEvent onTouchUp { get; set; } = new TriggerTouchUpEvent();
        /// <summary>
        /// 鼠标载入
        /// </summary>
        public TriggerTouchEnterEvent onTouchEnter { get; set; } = new TriggerTouchEnterEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchExitEvent onTouchExit { get; set; } = new TriggerTouchExitEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchStayEvent onTouchStay { get; set; } = new TriggerTouchStayEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchEvent onTouch { get; set; } = new TriggerTouchEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerTouchDoubleClickedEvent onTouchDoubleClicked { get; set; } = new TriggerTouchDoubleClickedEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerBeginDragEvent onTriggerBeginDrag { get; set; } = new TriggerBeginDragEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerEndDragEvent onTriggerEndDrag { get; set; } = new TriggerEndDragEvent();
        /// <summary>
        /// 
        /// </summary>
        public TriggerOnDragEvent onTriggerDrag { get; set; } = new TriggerOnDragEvent();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EventTrigger GetEventTrigger()
        {
            return EventTrigger;
        }

        /// <summary>
        /// SetDefaultTouchState
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="dur"></param>
        public void SetDefaultTouchState(float rate = 1.05f, float dur = 0.5f)
        {
            this.rate = rate;
            this.dur = dur;
            onTriggerTouchEnter += OnTriggerTouchEnterDefault;
            onTriggerTouchExit += OnTriggerTouchExitDefault;
        }

        /// <summary>
        /// OnTriggerTouchEnterDefault
        /// </summary>
        /// <param name="eventData"></param>
        private void OnTriggerTouchEnterDefault(BaseEventData eventData)
        {
            transform.DOKill();
            transform.DOScale(defaultScale * rate, dur);
        }

        /// <summary>
        /// OnTriggerTouchExitDefault
        /// </summary>
        /// <param name="eventData"></param>
        private void OnTriggerTouchExitDefault(BaseEventData eventData)
        {
            transform.DOKill();
            transform.DOScale(defaultScale, dur);
        }

        /// <summary>
        /// RemoveAllTouchStateListeners
        /// </summary>
        public void RemoveAllTouchStateListeners()
        {
            onTriggerTouchEnter = null;
            onTriggerTouchExit = null;
        }

        /// <summary>
        /// Awake
        /// </summary>
        protected override void OnAwake()
        {
            defaultScale = transform.localScale;

            // 注册按下
            RegisterEventTrigger(EventTrigger, EventTriggerType.PointerDown, (bae) =>
            {
                if (onTriggerTouchDown != null)
                {
                    onTriggerTouchDown(bae);
                }
                this.eventData = bae;
                b_Triggerring = true;

                if (onTouchDown != null)
                {
                    onTouchDown.Invoke();
                }
            });
            // 注册抬起
            RegisterEventTrigger(EventTrigger, EventTriggerType.PointerUp, (bae) =>
            {
                if (onTriggerTouchUp != null)
                {
                    onTriggerTouchUp(bae);
                }
                this.eventData = null;
                b_Triggerring = false;

                if (onTouchUp != null)
                {
                    onTouchUp.Invoke();
                }
            });
            // 注册点击
            RegisterEventTrigger(EventTrigger, EventTriggerType.PointerClick, (bae) =>
            {
                if (onTriggerTouchClick != null)
                {
                    onTriggerTouchClick(bae);
                }

                if (onClick != null)
                {
                    onClick.Invoke();
                }
            });
            // 注册进入
            RegisterEventTrigger(EventTrigger, EventTriggerType.PointerEnter, (bae) =>
            {
                if (onTriggerTouchEnter != null)
                {
                    onTriggerTouchEnter(bae);
                }
                b_TouchStaying = true;
                if (onTouchEnter != null)
                {
                    onTouchEnter.Invoke();
                }
            });
            // 注册退出
            RegisterEventTrigger(EventTrigger, EventTriggerType.PointerExit, (bae) =>
            {
                if (onTriggerTouchExit != null)
                {
                    onTriggerTouchExit(bae);
                }
                b_TouchStaying = false;
                if (onTouchExit != null)
                {
                    onTouchExit.Invoke();
                }
            });
            // 注册按住
            m_AllowTouchTriggerring = true;
            //RemoveEventTrigger(eventTrigger);

            // 注册开始拖拽
            RegisterEventTrigger(EventTrigger, EventTriggerType.BeginDrag, (bae) =>
            {
                if (onTriggerBeginDrag != null)
                {
                    onTriggerBeginDrag.Invoke(bae);
                }
            });

            // 注册结束拖拽
            RegisterEventTrigger(EventTrigger, EventTriggerType.EndDrag, (bae) =>
            {
                if (onTriggerEndDrag != null)
                {
                    onTriggerEndDrag.Invoke(bae);
                }
            });

            // 注册拖拽中
            RegisterEventTrigger(EventTrigger, EventTriggerType.Drag, (bae) =>
            {
                if (onTriggerDrag != null)
                {
                    onTriggerDrag.Invoke(bae);
                }
            });

        }

        /// <summary>
        /// Update
        /// </summary>
        protected override void OnUpdate()
        {
            if (m_AllowTouchTriggerring == false)
            {
                return;
            }
            if (b_Triggerring)
            {
                if (onTriggerTouch != null)
                {
                    onTriggerTouch(eventData);
                }

                if (onTouch != null)
                {
                    onTouch.Invoke();
                }

            }
            if (b_TouchStaying)
            {
                if (onTriggerTouchStay != null)
                {
                    onTriggerTouchStay(eventData);
                }

                if (onTouchStay != null)
                {
                    onTouchStay.Invoke();
                }
            }
        }

        /// <summary>
        /// RegisterEventTrigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="callback"></param>
        protected void RegisterEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventTriggerType;
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);

        }

        /// <summary>
        /// RegisterEventTrigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventTriggerType"></param>
        protected void RemoveEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType)
        {
            EventTrigger.Entry entry = null;

            for (int i = 0; i < trigger.triggers.Count; i++)
            {
                if (trigger.triggers[i].eventID == eventTriggerType)
                {
                    entry = trigger.triggers[i];

                    if (trigger.triggers.Contains(entry))
                        trigger.triggers.Remove(entry);
                }
            }
        }

        /// <summary>
        /// RemoveEventTrigger 功能未实现
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="callback"></param>
        protected void RemoveEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = null;

            for (int i = 0; i < trigger.triggers.Count; i++)
            {
                // trigger.triggers[i].callback == callback 语法错误
                if (trigger.triggers[i].eventID == eventTriggerType /*&& trigger.triggers[i].callback == callback*/)
                {
                    entry = trigger.triggers[i];

                    if (trigger.triggers.Contains(entry))
                        trigger.triggers.Remove(entry);
                }
            }
        }

        /// <summary>
        /// RemoveEventTrigger
        /// </summary>
        /// <param name="trigger"></param>
        protected void RemoveEventTrigger(EventTrigger trigger)
        {
            trigger.triggers.Clear();
        }


    }
}