using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    /*
    * 在代码中使用时如何开启某个Layers？
    * LayerMask mask = 1 << 你需要开启的Layers层。
    * LayerMask mask = 0 << 你需要关闭的Layers层。
    * 举几个个栗子：
    * LayerMask mask = 1 << 2; 表示开启Layer2。
    * LayerMask mask = 0 << 5;表示关闭Layer5。
    * LayerMask mask = 1<<2|1<<8;表示开启Layer2和Layer8。
    * LayerMask mask = 0<<3|0<<7;表示关闭Layer3和Layer7。
    * 上面也可以写成：
    * LayerMask mask = ~（1<<3|1<<7）;表示关闭Layer3和Layer7。
    * LayerMask mask = 1<<2|0<<4;表示开启Layer2并且同时关闭Layer4.
    */

    /// <summary>
    /// RayExtensionCast 结构体
    /// </summary>
    public struct RayExtensionCast
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsRayCast;
        /// <summary>
        /// 
        /// </summary>
        public GameObject GameObjRayCast;
        /// <summary>
        /// 
        /// </summary>
        public string GameObjRayCastName;
        /// <summary>
        /// 
        /// </summary>
        public Vector3 Vector3RayCastPoint;
        /// <summary>
        /// 
        /// </summary>
        public float Distance;
        /// <summary>
        /// 
        /// </summary>
        public Ray RealRay;
    }

    /// <summary>
    /// 自定义的射线
    /// </summary>
    public struct RayExtension
    {
        /// <summary>
        /// 是否在Scene场景中画线
        /// </summary>
        public static bool isDebugDrawLine = true;

        /// <summary>
        /// GetMouseRayCast
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <returns></returns>
        public static RayExtensionCast GetMouseRayCast(Camera mainCamera)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray);
        }

        /// <summary>
        /// RayExtensionCast
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        private static RayExtensionCast PhysicsRayCast(Ray ray)
        {
            RaycastHit rH;

            RayExtensionCast _rayCast = new RayExtensionCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray
            };

            if (Physics.Raycast(ray, out rH))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;

                if (isDebugDrawLine)
                {
                    Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
                }

#if UNITY_EDITOR
                Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
#endif
            }

            return _rayCast;
        }

        /// <summary>
        /// GetCast
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public static RayExtensionCast GetCast(Ray ray)
        {
            return PhysicsRayCast(ray);
        }

        /// <summary>
        /// GetCast
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="maxDistance"></param>
        /// <param name="myLayerMask"></param>
        /// <returns></returns>
        public static RayExtensionCast GetCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1)
        {
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        /// <summary>
        /// GetMouseRayCast
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <param name="maxDistance"></param>
        /// <param name="myLayerMask"></param>
        /// <returns></returns>
        public static RayExtensionCast GetMouseRayCast(Camera mainCamera, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        /// <summary>
        /// PhysicsRayCast
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="maxDistance"></param>
        /// <param name="myLayerMask"></param>
        /// <returns></returns>
        private static RayExtensionCast PhysicsRayCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1)
        {
            RaycastHit rH;

            RayExtensionCast _rayCast = new RayExtensionCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray
            };

            if (Physics.Raycast(ray, out rH, maxDistance, myLayerMask))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;

                if (isDebugDrawLine)
                {
                    if (myLayerMask == 1)
                        Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
                }

#if UNITY_EDITOR
                if (myLayerMask == 1)
                    Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
#endif
            }

            return _rayCast;
        }

        /// <summary>
        /// GetRayCast
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="maxDistance"></param>
        /// <param name="myLayerMask"></param>
        /// <returns></returns>
        public static RayExtensionCast GetRayCast(Transform tran, float maxDistance = 1000, int myLayerMask = -1)
        {
            return GetRayCast(tran.position, tran.forward, maxDistance, myLayerMask);
        }

        /// <summary>
        /// GetRayCast
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="forward"></param>
        /// <param name="maxDistance"></param>
        /// <param name="myLayerMask"></param>
        /// <returns></returns>
        public static RayExtensionCast GetRayCast(Vector3 pos, Vector3 forward, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = new Ray(pos, forward);
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }
    }

}