using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// IEnumerableExtension
    /// </summary>
    public static partial class IEnumerableExtension
    {
        /// <summary>
        /// Foreach
        /// </summary>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void Foreach(this IEnumerable array, Action<object> callback)
        {
            foreach (var item in array)
            {
                if (callback != null)
                {
                    callback(item);

                }
            }
        }

        /// <summary>
        /// 公共方法 从集合中找到指定类型的组件,返回第一个找到的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ra"></param>
        /// <returns></returns>
        public static T FindTarget<T>(this IEnumerable ra) where T : Object
        {
#if NET_2_0_SUBSET || NET_2_0
            T t = default(T);
#elif NET_4_6
            T t = default;
#else
            T t = default(T);
#endif
            foreach (var item in ra)
            {
                if (item is T) return item as T;
            }
            return t;
        }

        /// <summary>
        /// 公共方法 从集合中找到指定类型的组件,返回所有的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ra"></param>
        /// <returns></returns>
        public static T[] FindTargets<T>(this IEnumerable ra) where T : Object
        {
            T[] ts = null;
            List<T> tars = new List<T>();
            foreach (var item in ra)
            {
                if (item is T)
                {
                    tars.Add(item as T);
                }
            }
            ts = tars.ToArray();
            return ts;
        }

    }
}
