using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// TranformEx
    /// </summary>
    public static class TranformExtension
    {
        /// <summary>
        /// FindInChr
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <param name="unActi"></param>
        /// <returns></returns>
        public static Transform FindInChr(this Transform tran, string target, bool unActi = true)
        {
            Transform forreturn = null;
            Transform[] trans = tran.GetComponentsInChildren<Transform>(unActi);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// FindInChr
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="unActi"></param>
        /// <returns></returns>
        public static T FindInChr<T>(this Transform tran, bool unActi = true) where T : Component
        {
            T forreturn = null;
            T[] trans = tran.GetComponentsInChildren<T>(unActi);
            if (trans != null && trans.Length > 0) forreturn = trans[0];
            return forreturn;
        }

        /// <summary>
        /// FindInChr
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <param name="unActi"></param>
        /// <returns></returns>
        public static T FindInChr<T>(this Transform tran, string target, bool unActi = true) where T : Component
        {
            T forreturn = null;
            T[] trans = tran.GetComponentsInChildren<T>(unActi);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// FindInChrUnActi
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Transform FindInChrUnActi(this Transform tran, string target)
        {
            Transform forreturn = null;
            Transform[] trans = tran.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// ToClone
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToClone(this string str)
        {
            return str + "(Clone)";
        }

        /// <summary>
        /// ClearChildren
        /// </summary>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static Transform ClearChildren(this Transform tra)
        {
            // 方法一 有问题
            //int childCount = tra.childCount;
            //for (int i = 0; i < childCount; i++)
            //{
            //    UnityEngine.Object.Destroy(tra.GetChild(0).gameObject);
            //    Debug.Log(10);
            //}

            // 方法二 通过
            //foreach (Transform item in tra)
            //{
            //    UnityEngine.Object.Destroy(tra.GetChild(0).gameObject);
            //}
            // 方法三 通过
            for (int i = tra.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(tra.GetChild(i).gameObject);
            }
            // 方法四 通过
            //List<Transform> te = tra.GetComponentsInChildren<Transform>(true).ToList();
            //te.Remove(tra);
            //for (int i = 0; i < te.Count; i++)
            //{
            //    UnityEngine.Object.Destroy(te[i].gameObject);
            //}
            //te.Clear();

            return tra;
        }

        /// <summary>
        /// FindOrClone
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Transform FindOrClone(this Transform tran, string str)
        {
            Transform temp = tran.Find(str);
            if (temp == null) temp = tran.Find(str.ToClone());
            return temp;
        }

        /// <summary>
        /// FindOrCreate
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Transform FindOrCreate(this Transform tran, string str)
        {
            if (tran == null) return null;

            Transform temp = tran.Find(str);
            if (temp == null)
            {
                temp = new GameObject(str).transform;
                temp.transform.SetParent(tran);
                temp.transform.localPosition = temp.transform.localEulerAngles = Vector3.zero;
                temp.transform.localScale = Vector3.one;
            }
            return temp;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="target">目标物体的名字</param>
        /// <returns></returns>
        public static Transform FindInChildrenExpend(this Transform tran, string target)
        {
            Transform forreturn = null;

            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体的泛型型脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T FindInChildrenExpend<T>(this Transform tran, string target)
        {
            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    return t.GetComponent<T>();
                }
            }

            return default(T);
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回所有的物体的泛型型脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<T> FindInChildrenExpend<T>(this Transform tran)
        {
            List<Transform> temp = GetChildren(tran);

            List<T> tempT = new List<T>();

            foreach (Transform t in temp)
            {
                T tt = t.GetComponent<T>();
                if (tt != null)
                {
                    tempT.Add(tt);
                }
            }

            return tempT;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="target">目标物体的名字</param>
        public static Transform FindInChildrenWithTagExpend(this Transform tran, string target)
        {
            Transform forreturn = null;

            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.tag == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// GetChildren
        /// </summary>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static List<Transform> GetChildren(Transform tra)
        {
            //tempChildren.Clear();

            List<Transform> tempChildren = new List<Transform>();
            _GetChildren(tra, ref tempChildren);

            return tempChildren;
        }

        /// <summary>
        /// _GetChildren
        /// </summary>
        /// <param name="tra"></param>
        /// <param name="tempChildren"></param>
        /// <returns></returns>
        private static List<Transform> _GetChildren(Transform tra, ref List<Transform> tempChildren)
        {
            foreach (Transform item in tra)
            {
                tempChildren.Add(item);
                if (item.childCount > 0)
                {
                    _GetChildren(item, ref tempChildren);
                }
            }
            return tempChildren;
        }

        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        public static RectTransform FindInChildren(this RectTransform tran, string target)
        {
            RectTransform forreturn = null;
            foreach (RectTransform t in tran.GetComponentsInChildren<RectTransform>())
            {
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// 设置Tranform属性
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="config"></param>
        public static void SetTransformConfig(this Transform tran, TransformExtensionConfig config)
        {
            tran.SetParent(config.parent);

            tran.localPosition = config.localPosition;
            tran.localEulerAngles = config.localEulerAngles;
            tran.localScale = config.localScale;
        }

        /// <summary>
        /// 获取Tranform属性
        /// </summary>
        /// <param name="tran"></param>
        public static TransformExtensionConfig GetTransformConfig(this Transform tran)
        {
            TransformExtensionConfig config = new TransformExtensionConfig();
            config.parent = tran.parent;

            config.localPosition = tran.localPosition;
            config.localEulerAngles = tran.localEulerAngles;
            config.localScale = tran.localScale;

            return config;
        }


        /// <summary>
        /// 寻找子物体组件（第一个）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="tran"></param>
        /// <param name="excludeSelf">是否排除自身</param>
        /// <param name="unActi">是否包含隐藏的子物体</param>
        /// <param name="firstChild">是否是第一层的子物体</param>
        /// <returns></returns>
        public static T FindTarget<T>(this Transform tran, bool excludeSelf = true, bool unActi = true, bool firstChild = true) where T : Component
        {
            var all = tran.GetComponentsInChildren<T>(unActi).ToList();
            if (excludeSelf && all.Count > 0 && all.Contains(tran.GetComponent<T>()))
            {
                all.Remove(tran.GetComponent<T>());
            }
            if (!firstChild)
            {
                if (all.Count > 0) return all.ToArray()[0];
                else return null;
            }
            var temp = new List<T>();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].transform.parent == tran)
                {
                    temp.Add(all[i]);
                }
            }

            if (temp.Count > 0) return temp.ToArray()[0];
            else return null;

        }


        /// <summary>
        /// 寻找子物体组件（数组）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="tran"></param>
        /// <param name="excludeSelf">是否排除自身</param>
        /// <param name="unActi">是否包含隐藏的子物体</param>
        /// <param name="firstChild">是否是第一层的子物体</param>
        /// <returns></returns>
        public static T[] FindTargets<T>(this Transform tran, bool excludeSelf = true, bool unActi = true, bool firstChild = true) where T : Component
        {
            var all = tran.GetComponentsInChildren<T>(unActi).ToList();
            if (excludeSelf && all.Count > 0 && all.Contains(tran.GetComponent<T>()))
            {
                all.Remove(tran.GetComponent<T>());
            }
            if (!firstChild) return all.ToArray();

            var temp = new List<T>();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].transform.parent == tran)
                {
                    temp.Add(all[i]);
                }
            }
            return temp.ToArray();
        }

        /// <summary>
        /// 位置重置
        /// </summary>
        /// <param name="tran"></param>
        public static void Centralizate(this Transform tran)
        {
            tran.localPosition = tran.localEulerAngles = Vector3.zero;
            tran.localScale = Vector3.one;

        }

        /// <summary>
        /// GetCanvas
        /// </summary>
        /// <param name="tran"></param>
        public static Canvas GetCanvas(this Transform tran)
        {
            Canvas c = tran.Find("Canvas") ? tran.Find("Canvas").GetComponent<Canvas>() : null;
            if (c==null)
            {
                c = tran.GetComponentInChildren<Canvas>();
            }

            if (c == null)
            {
                c = UnityEngine.Object.FindObjectOfType<Canvas>();
            }

            if (c == null)
            {
                var cs = tran.GetComponentsInChildren<Canvas>(true);
                if (cs.Length > 0)
                {
                    c = cs[0];
                }
            }

            if (c == null)
            {
                var cs = UnityEngine.Resources.FindObjectsOfTypeAll<Canvas>();
                for (int i = 0; i < cs.Length; i++)
                {
                    if (cs[i].gameObject.scene != null)
                    {
                        c = cs[i];
                        break;
                    }
                }
            }

            if (c == null)
            {
                var canvasGo = new GameObject("Canvas");
                canvasGo.transform.SetParent(tran);
                canvasGo.transform.Centralizate();
                c = canvasGo.AddComponent<Canvas>();
            }

            return c;

        }

        /// <summary>
        /// GetCanvasTran
        /// </summary>
        /// <param name="tran"></param>
        public static Transform GetCanvasTran(this Transform tran)
        {
            return tran.GetCanvas().transform;

        }

        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this Transform tr) where T : Component
        {
            T t = tr.GetComponent<T>();
            if (t == null)
                t = tr.gameObject.AddComponent<T>();
            return t;
        }

    }

}