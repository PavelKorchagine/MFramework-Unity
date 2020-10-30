using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// ObjectEx
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetVarName(this Object obj)
        {
            return nameof(obj);
        }

        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetVarName<T>(this T t) where T : Object
        {
            return nameof(t);
        }

        /// <summary>
        /// 扩展 获取变量名称(字符串)
        /// </summary>
        /// <param name="var_name"></param>
        /// <param name="exp">可以使lamda表达式</param>
        /// <returns>return string</returns>
        public static string GetVarName<T>(this T var_name, Expression<Func<T, T>> exp)
        {
            return ((MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>return string</returns>
        public static string GetVarName<T>(Expression<Func<T, T>> exp)
        {
            return ((MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取变量名称(字符串)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="it"></param>
        /// <returns></returns>
        public static string GetName<T>(this Object obj, T it) where T : class
        {
            return typeof(T).GetProperties()[0].Name;
        }

        /// <summary>
        /// 获取变量名称(字符串)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetName2<T>(this Object obj, Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }

        /// <summary>
        /// 获取变量名称(字符串)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetName3<T>(this Object obj, Func<T> expr)
        {
            return expr.Target.GetType().Module.ResolveField(BitConverter.ToInt32(expr.Method.GetMethodBody().GetILAsByteArray(), 2)).Name;
        }

        /// <summary>
        /// 获取变量名称(字符串)
        /// 警告不能在匿名方法里写其它否则报错
        /// </summary>
        /// <param name="var_name">要获取变量名的变量</param>
        /// <returns>变量名</returns>
        public static string GetVarNameReal<T>(this T var_name)
        {
            Expression<Func<T, T>> exp = GetExpression<T>(q => var_name);
            return ((MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取对应的数据结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static Expression<Func<T, T>> GetExpression<T>(Expression<Func<T, T>> exp)
        {
            return exp;
        }
      
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return temp.ToArray();
        }
     
        /// <summary>
        /// FindObjectsOfBehaviourExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfBehavEx<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInScene<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name || item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindAllObjectsOfTypeInScene 寻找当前场景中所有的 T 的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindAllObjsOfTyExInScene<T>() where T : Component
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name || item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInSceneUnDestr<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInSceneDestr<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="justChildren"></param>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>(this Object obj, bool justChildren) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="justChildren"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>(bool justChildren, Transform root) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null) continue;
                if (justChildren && item.transform.root == root)
                    temp.Add(item);
                else if (!justChildren)
                    temp.Add(item);
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return default(T);
        }
      
        /// <summary>
        /// FindObjectOfTypeExpend in root
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjOfTyEx<T>(Transform root) where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && item.transform.parent == root)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return default(T);
        }
      
        /// <summary>
        /// FindPrefabObjectOfTypeExpend 返回相关类型的预制体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefObjOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name == null)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return default(T);
        }
      
        /// <summary>
        /// FindPrefabObjectOfTypeExpend 返回相关类型的预制体数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindPrefObjsOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
      
        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefObjOfBehavEx<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray()[0];
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, position, rotation, parent);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
       
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parent"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Transform parent, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, parent);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
       
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
       
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, position, rotation);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
       
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, parent, instantiateInWorldSpace);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectsOfBehaviourExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfBehaviourExpend<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpendInScene<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    if (item.gameObject.scene.name == SceneManager.GetActiveScene().name)
                    {
                        temp.Add(item);
                    }

                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="justChildren"></param>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>(this Object obj, bool justChildren) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="justChildren"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>(bool justChildren, Transform root) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (justChildren)
                {
                    if (item.gameObject.scene.name != null && item.transform.root == root)
                    {
                        temp.Add(item);
                    }
                }
                else
                {
                    if (item.gameObject.scene.name != null)
                    {
                        temp.Add(item);
                    }
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// FindObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjectOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }

        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefabObjectOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name == null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这不是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }

        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefabObjectOfBehaviourExpend<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name == null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这不是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }
        
    }
}