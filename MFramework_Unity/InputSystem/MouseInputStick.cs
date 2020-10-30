using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.InputSystem
{
    /// <summary>
    /// MouseInputStick
    /// </summary>
    public class MouseInputStick : MonoBehaviour
    {
        [SerializeField] protected Transform cursor;
        protected Vector3 forward = Vector3.forward;
        protected Vector3 oriEulerAngle;

        protected virtual void Start()
        {
            oriEulerAngle = transform.eulerAngles;
        }

        public virtual Vector3 GetCursorPos()
        {
            if (cursor == null) return Vector3.zero;
            return cursor.position;
        }

        private Vector3 GetVirtualRayCastPoint()
        {
            return Vector3.zero;
        }

        public void SetCursorPostion(Vector3 valuer)
        {
            cursor.localPosition = Vector3.forward * Vector3.Distance(transform.position, valuer);
            cursor.eulerAngles = oriEulerAngle;
        }

        private void Update()
        {
        
        }

        public Transform GetCursor()
        {
            return cursor;
        }
    }
}