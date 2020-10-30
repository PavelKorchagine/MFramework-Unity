using UnityEngine;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// CameraExtension
    /// </summary>
    public static partial class CameraExtension
    {
        /// <summary>
        /// CaptureCamera ½ØÆÁ
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Texture2D CaptureCamera(this Camera camera, Rect rect)
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;
            camera.Render();

            RenderTexture.active = renderTexture;

            var screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(renderTexture);

            return screenShot;
        }

        /// <summary>
        /// MouseToWorldPosition
        /// </summary>
        /// <param name="eventCamera"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3 MouseToWorldPosition(Camera eventCamera, Vector3 pos)
        {
            if (eventCamera == null)
                return Vector3.zero;

            Vector3 screenSpace = eventCamera.WorldToScreenPoint(pos);
            Vector3 newSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            return eventCamera.ScreenToWorldPoint(newSpace);
        }

        /// <summary>
        /// MouseToWorldPosition
        /// </summary>
        /// <param name="eventCamera"></param>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static Vector3 MouseToWorldPosition(Camera eventCamera, Transform tra)
        {
            return MouseToWorldPosition(eventCamera, tra.position);
        }
    }
}