using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// UGUITools
    /// </summary>
    public class UGUITools
    {
        /// <summary>
        /// GetUGUIArPos
        /// </summary>
        /// <param name="_sceneCamera"></param>
        /// <param name="_sceneCanvas"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static Vector2 GetUGUIArPos(Camera _sceneCamera, Canvas _sceneCanvas, Transform tr)
        {
            Vector2 vector = Vector2.zero;

            if (_sceneCamera == null)
            {
                _sceneCamera = Camera.main;
            }
            if (_sceneCamera == null)
            {
                _sceneCamera = UnityEngine.Object.FindObjectOfType<Camera>();
            }

            if (_sceneCanvas == null)
            {
                _sceneCanvas = UnityEngine.Object.FindObjectOfType<Canvas>();
            }

            if (_sceneCamera == null || _sceneCanvas == null || tr == null)
            {
                return vector;
            }

            switch (_sceneCanvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    break;
                case RenderMode.ScreenSpaceCamera:
                    break;
                case RenderMode.WorldSpace:
                    break;
                default:
                    break;
            }

            Vector3 screenPoint = _sceneCamera.WorldToScreenPoint(tr.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_sceneCanvas.transform as RectTransform,
                                screenPoint, _sceneCanvas.worldCamera, out vector);
            return vector;
        }

        /// <summary>
        /// GetUGUIArPos
        /// </summary>
        /// <param name="_sceneCamera"></param>
        /// <param name="_sceneCanvas"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static Vector2 GetUGUIArPos(Camera _sceneCamera, Canvas _sceneCanvas, Vector3 pos)
        {
            Vector2 vector = Vector2.zero;

            if (_sceneCamera == null)
            {
                _sceneCamera = Camera.main;
            }
            if (_sceneCamera == null)
            {
                _sceneCamera = UnityEngine.Object.FindObjectOfType<Camera>();
            }

            if (_sceneCanvas == null)
            {
                _sceneCanvas = UnityEngine.Object.FindObjectOfType<Canvas>();
            }

            if (_sceneCamera == null || _sceneCanvas == null)
            {
                return vector;
            }

            switch (_sceneCanvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    break;
                case RenderMode.ScreenSpaceCamera:
                    break;
                case RenderMode.WorldSpace:
                    break;
                default:
                    break;
            }

            Vector3 screenPoint = _sceneCamera.WorldToScreenPoint(pos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_sceneCanvas.transform as RectTransform,
                                screenPoint, _sceneCanvas.worldCamera, out vector);
            return vector;
        }

        /// <summary>
        /// 检测是否点击在UI上
        /// </summary>
        /// <returns></returns>
        public static bool IsClickUI()
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

    }
}