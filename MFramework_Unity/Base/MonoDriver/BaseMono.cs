using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity
{
    public class BaseMono : BaseMonoAbstract
    {
        protected virtual void Reset()
        {
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }
        protected sealed override void Awake()
        {
            base.Awake();
        }
        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected sealed override void Start()
        {
            base.Start();
        }
        protected override void OnAwake()
        {
        }

        protected override void OnStart()
        {
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnGODestroy()
        {
        }

        public override void OnInit()
        {
        }

        public override void OnDie()
        {

        }
        public override void OnReset()
        {
        }
        protected override void OnFixUpdate()
        {

        }

        protected override void OnLateUpdate()
        {

        }

        public override void OnInit(params object[] args)
        {
        }

        public override void OnGet()
        {

        }

        public virtual void OnEnter()
        {
        }
        public virtual void OnExit()
        {
        }
    }
}