using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// GameObjectExtension
    /// </summary>
    public static class GameObjectExtension
    {
        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this GameObject go) where T : Component
        {
            T t = go.GetComponent<T>();
            if (t == null)
                t = go.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static GameObject FindInChildrenExpend(this GameObject obj, string target)
        {
            Transform tra = obj.transform.FindInChildrenExpend(target);
            if (tra == null)
                return null;
            else
                return tra.gameObject;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static GameObject FindInChildrenWithTagExpend(this GameObject obj, string tag)
        {
            Transform tra = obj.transform.FindInChildrenWithTagExpend(tag);
            if (tra == null)
                return null;
            else
                return tra.gameObject;
        }

        /// <summary>
        /// 静态方法 获取子指定类型物体目标（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetComponentRealInChildren<T>(this GameObject obj)
        {
            T t = default(T);
            var ts = obj.GetComponentsInChildren<T>(true);
            if (ts != null && ts.Length > 0)
            {
                t = ts[0];
            }
            return t;
        }

        /// <summary>
        /// 静态方法 获取子指定类型物体目标（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetComponentRealInChildren<T>(this Object obj)
        {
            T t = default(T);
            T[] ts = null;

            // 所有的类型判断方法
            #region 所有的类型判断方法
            if (obj is GameObject || typeof(GameObject).IsAssignableFrom(obj.GetType()))
            {
                //Debug.Log("go 是个GameObject 组件");
            }
            else if (obj is Component || obj.GetType().IsSubclassOf(typeof(Component)) || typeof(Component).IsAssignableFrom(obj.GetType()))
            {
                //Debug.Log("go 是个 Component 组件");
            }
            #endregion

            if (obj is GameObject)
            {
                try
                {
                    ts = (obj as GameObject).GetComponentsInChildren<T>(true);
                }
                catch (Exception) { }
            }
            else if (obj is Component)
            {
                try
                {
                    ts = (obj as Component).GetComponentsInChildren<T>(true);
                }
                catch (Exception) { }
            }
            if (ts != null && ts.Length > 0)
            {
                t = ts[0];
            }
            return t;
        }

        /// <summary>
        /// 静态方法 获取子指定类型物体目标（包括隐藏物体），返回所有找到的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="excludeSelf"></param>
        /// <param name="unActi"></param>
        /// <param name="firstChild"></param>
        /// <returns></returns>
        public static T[] GetComponentsRealInChildren<T>(this GameObject obj
            , bool excludeSelf = true
            , bool unActi = true
            , bool firstChild = true) where T : Component
        {
            List<T> list = new List<T>();
            list = obj.GetComponentsInChildren<T>(unActi).ToList();

            if (list.Count > 0 && excludeSelf)
            {
                var self = obj.GetComponent<T>();
                if (self != null && list.Contains(self))
                {
                    list.Remove(self);
                }
            }

            if (!firstChild) return list.ToArray();

            var temp = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].transform.parent == obj.transform)
                {
                    temp.Add(list[i]);
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// 静态方法 获取子指定类型物体目标（包括隐藏物体），返回所有找到的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="excludeSelf"></param>
        /// <param name="unActi"></param>
        /// <param name="firstChild"></param>
        /// <returns></returns>
        public static T[] GetComsRealInChr<T>(this GameObject obj
            , bool excludeSelf = true
            , bool unActi = true
            , bool firstChild = true)
        {
            List<Transform> listTra = obj.GetComponentsRealInChildren<Transform>(excludeSelf, unActi, firstChild).ToList();
            List<T> list = new List<T>();
            for (int i = 0; i < listTra.Count; i++)
            {
                T t = listTra[i].GetComponent<T>();
                if (t != null) list.Add(t);
            }

            return list.ToArray();
        }

    }

    
}
