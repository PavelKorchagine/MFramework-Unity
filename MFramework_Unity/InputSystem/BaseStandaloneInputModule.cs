///////////////////////////////////////////////////////////////////////////////
// Copyright 2017-2019  Vrtist Technology Co., Ltd. All Rights Reserved.
// File:  
// Author: 
// Date:  
// Discription:  
/////////////////////////////////////////////////////////////////////////////// 

#define HTCVIVE
#define PC
#define OCULUS_QUEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
#region UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.InputSystem
{
    /// <summary>
    /// abstract
    /// </summary>
    public class BaseStandaloneInputModule : MonoBehaviour
    {
        /************************************    属性字段  *************************************/

        /// <summary>
        /// rayDistance
        /// </summary>
        public float rayDistance = 15;
        /// <summary>
        /// objRayCast
        /// </summary>
        public GameObject objRayCast;
        /// <summary>
        /// rayCast
        /// </summary>
        [HideInInspector] public RayCast rayCast = new RayCast();
        protected RayCast virtualRayCast;
        [SerializeField] protected Camera mainCamera;
        [SerializeField] protected MouseInputStick inputStick;
        protected Transform _originPivot;
        protected GameObject selectObject;
        protected Ray _ray;
        protected RaycastHit rH;
        protected IPointDownHandler _realPointDownObject;
        protected float _rotation;
        protected WaitForSeconds waitseconds = new WaitForSeconds(0.5f);
        protected bool Show;
        protected bool isShowOnGUI = false;
        protected IPointDownHandler _pointDownHandler;
        public bool isUIOcclusion = true;
        private bool IsOverGameObject = false;
        public bool isHitOnUI = false;
        [SerializeField] private bool enableDoubleClick = false;
        public float doubleClickInterval = 0.3f;
        [SerializeField] private float _doubleClick = 0;
        protected IPointClickHandler lastClickHandler;
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
        [SerializeField] protected LayerMask realRayPointLayerMask = 1 << 0 | 1 << 5;//1 : 2的0次方
        protected Vector3 virtualPoint;
        protected LayerMask virtualPointLayerMask = 1 << 9;//512 : 2的9次方
        public GUIStyle style1;

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Awake()
        {
            GetMainCamera();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paras"></param>
        public virtual void OnInit(params object[] paras)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnInit()
        {

        }

        /// <summary>
        /// UpdateEventMainCamera
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <param name="inputStickTra"></param>
        public virtual void UpdateEventMainCamera(Camera mainCamera, Transform inputStickTra = null)
        {
            if (this.mainCamera == mainCamera)
            {
                if (this.inputStick == null && inputStickTra != null)
                {
                    this.inputStick = inputStickTra.GetComponent<MouseInputStick>();
                }
            }
            if (this.mainCamera != mainCamera)
            {
                this.mainCamera = mainCamera;
                if (inputStickTra != null)
                {
                    this.inputStick = inputStickTra.GetComponent<MouseInputStick>();
                }
            }
        }

        /************************************ 私有方法  *********************************/
        /// <summary>
        /// ExecuteHandler
        /// </summary>
        /// <param name="go"></param>
        protected virtual void ExecuteHandler(GameObject go)
        {
            if (enableDoubleClick)
            {
                _doubleClick -= Time.deltaTime;

                if (_doubleClick <= 0)
                {
                    enableDoubleClick = false;
                    lastClickHandler = null;

                }
            }

            // 检测是否点击到UI层上，用于鼠标穿透
            if (isUIOcclusion && IsClickUI())
            {
                if (selectObject != null)
                {
                    IPointExitHandler hander = selectObject.GetComponent<IPointExitHandler>();
                    if (hander != null)
                    {
                        hander.OnPointExit(rayCast);
                    }
                    selectObject = null;

                }

                return;
            }

            if (selectObject != null && selectObject != go)
            {
                IPointExitHandler hander = selectObject.GetComponent<IPointExitHandler>();
                if (hander != null)
                {
                    hander.OnPointExit(rayCast);
                }
                selectObject = null;
            }

            if (go != null)
            {
                if (selectObject != go)
                {
                    IPointEnterHandler hander = go.GetComponent<IPointEnterHandler>();
                    if (hander != null)
                    {
                        hander.OnPointEnter(rayCast);
                    }
                    selectObject = go;

                }
#if HTCVIVE
#endif
                if (UnityEngine.Input.GetMouseButtonDown(0) && go != null)
                {
                    _pointDownHandler = go.GetComponent<IPointDownHandler>();
                    if (_pointDownHandler != null)
                    {
                        _realPointDownObject = _pointDownHandler.OnPointDown(rayCast);
                    }
                }
                else if (UnityEngine.Input.GetMouseButtonUp(0) && selectObject != null)
                {
                    IPointUpHandler hander = go.GetComponent<IPointUpHandler>();
                    if (hander != null)
                    {
                        hander.OnPointUp(rayCast);
                        if (hander == _realPointDownObject)
                        {
                            IPointClickHandler handerClick = go.GetComponent<IPointClickHandler>();
                            if (handerClick != null)
                            {
                                handerClick.OnPointClick(rayCast);

                                if (lastClickHandler != null && handerClick == lastClickHandler)
                                {
                                    IPointDoubleClickHandler handerDClick = go.GetComponent<IPointDoubleClickHandler>();
                                    if (handerDClick != null)
                                    {
                                        handerDClick.OnPointDoubleClick(rayCast);
                                    }

                                }

                                lastClickHandler = handerClick;
                                _doubleClick = doubleClickInterval;
                                enableDoubleClick = true;
                            }
                        }
                        }

                        

                    selectObject = null;
                }
            }
        }

        /// <summary>
        /// 检查是否满足条件
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckCondition()
        {
            bool isConditioned = false;
            //#if !UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_STANDALONE_OSX
            isConditioned = true;
            return isConditioned;
        }

        /// <summary>
        /// 获取射线 和 摄取物体
        /// </summary>
        protected virtual GameObject GetRayCast()
        {
            return null;
        }

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Start()
        {
        }

        /// <summary>
        /// GetMainCamera
        /// </summary>
        public virtual Camera GetMainCamera()
        {
            if (this.mainCamera != null)
            {
                return this.mainCamera;
            }

            Camera mainCamera = null;

            if (mainCamera == null && GameObject.FindGameObjectWithTag("MainCamera"))
            {
                mainCamera = Camera.main;
            }

            if (mainCamera == null && GameObject.FindGameObjectWithTag("ActiveEventCamera"))
            {
                mainCamera = GameObject.FindGameObjectWithTag("ActiveEventCamera").GetComponent<Camera>();
            }

            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }

            this.mainCamera = mainCamera;

            return mainCamera;
        }

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Update()
        {
            if (GetMainCamera() == null)
            {
                return;
            }

            // 真实物体交互
            rayCast = _Ray.GetMouseRayCast(mainCamera, rayDistance, realRayPointLayerMask);
            if (rayCast.RealRay.direction != Vector3.zero && inputStick) inputStick.transform.forward = rayCast.RealRay.direction;

            //inputStick.transform.forward = rayCast.RealRay.direction;
            ExecuteHandler(rayCast.GameObjRayCast);
            objRayCast = rayCast.GameObjRayCast;
            // 获取虚拟坐标
            virtualRayCast = _Ray.GetMouseVirtualRayCast(mainCamera, 100, virtualPointLayerMask);
            if (virtualRayCast.IsRayCast)
            {
                virtualPoint = virtualRayCast.Vector3RayCastPoint;
                if (inputStick) inputStick.SetCursorPostion(virtualPoint);
            }
        }

        /// <summary>
        /// 检测是否点击在UI上
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsClickUI()
        {
            if (EventSystem.current != null)
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                return results.Count > 0;
            }
            return false;
        }

        /// <summary>
        /// FixedUpdate
        /// </summary>
        protected virtual void FixedUpdate()
        {

        }

        /// <summary>
        /// OnGUI
        /// </summary>
        protected virtual void OnGUI()
        {
            if (rayCast.IsRayCast)
            {
                //style1 = new GUIStyle();
                style1.fontSize = 15;
                style1.normal.textColor = Color.white;
                if (rayCast.ModuleInfo != null)
                {
                    if (rayCast.ModuleInfo.GetGameObjLayer() == 0)
                    {
                        style1.normal.textColor = Color.red;
                    }
                    else
                    {
                        style1.normal.textColor = Color.gray;
                    }
                    GUI.Label(new Rect(UnityEngine.Input.mousePosition.x - 20, Screen.height - UnityEngine.Input.mousePosition.y - 30, 400, 50), rayCast.ModuleInfo.GetGuiShowInfo(), style1);
                }
                //else
                //    GUI.Label(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - 30, 400, 50), rayCast.GameObjRayCastName, style1);
            }

        }
    }

}