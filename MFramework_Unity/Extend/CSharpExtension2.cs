using System;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// CSharpExtension2
    /// </summary>
    public static class CSharpExtension2
    {
        /// <summary>
        /// MergerArray 合并 String 数组
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        [Obsolete]
        public static Vector3[] MergerArray(Vector3[] First, Vector3[] Second)
        {
            Vector3[] result = new Vector3[First.Length + Second.Length];
            First.CopyTo(result, 0);
            Second.CopyTo(result, First.Length);
            return result;
        }

        /// <summary>
        /// Connect 合并 Vector3 数组
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [Obsolete]
        public static Vector3[] Concat(this Vector3[] source, Vector3[] target)
        {
            Vector3[] result = new Vector3[source.Length + target.Length];
            source.CopyTo(result, 0);
            target.CopyTo(result, source.Length);
            return result;
        }

        /// <summary>
        /// 数组追加
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="str">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        [Obsolete]
        public static string[] MergerArray(string[] Source, string str)
        {
            string[] result = new string[Source.Length + 1];
            Source.CopyTo(result, 0);
            result[Source.Length] = str;
            return result;
        }

        /// <summary>
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="StartIndex">原数组的起始位置</param>
        /// <param name="EndIndex">原数组的截止位置</param>
        /// <returns></returns>
        [Obsolete]
        public static string[] SplitArray(string[] Source, int StartIndex, int EndIndex)
        {
            try
            {
                string[] result = new string[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 不足长度的前面补空格,超出长度的前面部分去掉
        /// </summary>
        /// <param name="First">要处理的数组</param>
        /// <param name="byteLen">数组长度</param>
        /// <returns></returns>
        [Obsolete]
        public static string[] MergerArray(string[] First, int byteLen)
        {
            string[] result;
            if (First.Length > byteLen)
            {
                result = new string[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = First[i + First.Length - byteLen];
                return result;
            }
            else
            {
                result = new string[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = " ";
                First.CopyTo(result, byteLen - First.Length);
                return result;
            }
        }

        /// <summary>
        /// Connect 合并数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T[] Concat<T>(this T[] source, T[] target)
        {
            T[] result = new T[source.Length + target.Length];
            source.CopyTo(result, 0);
            target.CopyTo(result, source.Length);
            return result;
        }

        /// <summary>
        /// 数组追加
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="data">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static T[] Concat<T>(this T[] source, T data)
        {
            T[] result = new T[source.Length + 1];
            source.CopyTo(result, 0);
            result[source.Length] = data;
            return result;
        }

        /// <summary>
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="StartIndex">原数组的起始位置</param>
        /// <param name="EndIndex">原数组的截止位置</param>
        /// <returns></returns>
        public static T[] Split<T>(this T[] Source, int StartIndex, int EndIndex)
        {
            try
            {
                T[] result = new T[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.LogWarning(ex.Message);
                return new T[0];
            }
        }

        /// <summary>
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="StartIndex">原数组的起始位置</param>
        /// <param name="EndIndex">原数组的截止位置</param>
        /// <returns></returns>
        public static T[] SplitSafe<T>(this T[] Source, int StartIndex, int EndIndex)
        {
            try
            {
                if (StartIndex < 0) StartIndex = 0;
                if (EndIndex > Source.Length - 1) EndIndex = Source.Length - 1;

                T[] result = new T[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.LogWarning(ex.Message);
                return new T[0];
            }
        }

        /// <summary>
        /// 不足长度的前面补空格,超出长度的前面部分去掉
        /// </summary>
        /// <param name="First">要处理的数组</param>
        /// <param name="byteLen">数组长度</param>
        /// <returns></returns>
        public static T[] Merger<T>(this T[] First, int byteLen)
        {
            T[] result;
            if (First.Length > byteLen)
            {
                result = new T[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = First[i + First.Length - byteLen];
                return result;
            }
            else
            {
                result = new T[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = default(T);
                First.CopyTo(result, byteLen - First.Length);
                return result;
            }
        }

        /// <summary>
        /// 数组插入数值
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="insertIndex">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static T[] Insert<T>(this T[] source, int insertIndex)
        {
            if (insertIndex <= -1)
            {
                insertIndex = 0;
            }
            if (insertIndex >= source.Length)
            {
                insertIndex = source.Length;
            }
            T[] first = new T[insertIndex];
            //source.Split(0, insertIndex - 1);
            source.CopyToSafe(first, 0);
            T[] second = new T[source.Length - insertIndex];
            source.CopyToSafe(second, insertIndex);

            source = first.Concat(default(T)).Concat(second);
            return source;
        }

        /// <summary>
        /// 数组插入数值
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="insertIndex">字符串</param>
        /// <param name="data">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static T[] Insert<T>(this T[] source, T data, int insertIndex)
        {
            if (insertIndex <= -1)
            {
                insertIndex = 0;
            }
            if (insertIndex >= source.Length)
            {
                insertIndex = source.Length;
            }
            T[] first = new T[insertIndex];
            //source.Split(0, insertIndex - 1);
            source.CopyToSafe(first, 0);
            T[] second = new T[source.Length - insertIndex];
            source.CopyToSafe(second, insertIndex);

            source = first.Concat(data).Concat(second);
            return source;
        }

        /// <summary>
        /// 数组插入数值
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="insertIndex">字符串</param>
        /// <param name="datas">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static T[] Insert<T>(this T[] source, T[] datas, int insertIndex)
        {
            if (insertIndex <= -1)
            {
                insertIndex = 0;
            }
            if (insertIndex >= source.Length)
            {
                insertIndex = source.Length;
            }
            T[] first = new T[insertIndex];
            //source.Split(0, insertIndex - 1);
            source.CopyToSafe(first, 0);
            T[] second = new T[source.Length - insertIndex];
            source.CopyToSafe(second, insertIndex);

            source = first.Concat(datas).Concat(second);
            return source;
        }


        /// <summary>
        /// 数组插入数值
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="startIndex">字符串</param>
        /// <param name="target">字符串</param>
        /// <param name="autoRaise">target长度自动增长</param>
        /// <param name="autoReduce">target长度自动缩进</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static void CopyToSafe<T>(this T[] source, T[] target, int startIndex
            , bool autoRaise = false, bool autoReduce = false)
        {
            if (target.Length >= source.Length - startIndex)
            {
                source.CopyTo(target, startIndex);
                // 自动缩进
                if (autoReduce)
                {
                    source = source.SplitSafe(0, source.Length - startIndex - 1);
                }
            }
            else
            {
                try
                {
                    if (autoRaise)
                    {
                        // 自动增长
                        target = new T[source.Length - startIndex];
                        source.CopyTo(target, startIndex);
                    }
                    else
                    {
                        // 不自动增长
                        for (int i = 0; i <= target.Length; i++)
                            target[i] = source[i + startIndex];
                    }
                    
                }
                catch (IndexOutOfRangeException ex)
                {
                    Debug.LogWarning(ex.Message);
                }
            }

        }
    }
}
