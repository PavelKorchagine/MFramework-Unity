using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// UnityEx
    /// </summary>
    public static class UnityExtension
    {
      
        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="com"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this Component com) where T : Component
        {
            T t = com.GetComponent<T>();

            if (t == null)
                t = com.gameObject.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// ViewportPointToAnchoredPosition
        /// </summary>
        /// <param name="ori"></param>
        /// <param name="widthhight"></param>
        /// <returns></returns>
        public static Vector2 ViewportPointToAnchoredPosition(Vector2 ori, Vector2 widthhight)
        {
            return new Vector2((ori.x - 0.5f) * widthhight.x, (ori.y - 0.5f) * widthhight.y);
        }

        /// <summary>
        /// 生成缩略图 指定目标的长宽
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight, string savePath = "")
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);

            for (int i = 0; i < result.height; ++i)
            {
                for (int j = 0; j < result.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(j, i, newColor);
                }
            }
            result.Apply();
            return result;
            //File.WriteAllBytes(savePath, result.EncodeToJPG());
        }
        /// <summary>
        /// 生成缩略图 指定目标的缩放的百分比
        /// </summary>
        /// <param name="source"></param>
        /// <param name="percent"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static Texture2D ScaleTexture(Texture2D source, double percent, string savePath = "")
        {
            Texture2D result = new Texture2D(int.Parse(Math.Round(source.width * percent).ToString()), int.Parse(Math.Round(source.height * percent).ToString()), TextureFormat.ARGB32, false);
            for (int i = 0; i < result.height; ++i)
            {
                for (int j = 0; j < result.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(j, i, newColor);
                }
            }
            result.Apply();
            return result;
            //File.WriteAllBytes(savePath, result.EncodeToJPG());
        }

    }
}