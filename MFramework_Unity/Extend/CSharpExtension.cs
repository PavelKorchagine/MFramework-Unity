using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Object = UnityEngine.Object;
#region !UNITY_EDITOR && UNITY_WEBGL
#if !UNITY_EDITOR && UNITY_WEBGL
using Newtonsoft.Json;
#else
using Newtonsoft.Json;
#endif
#endregion
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// CSharpExtension
    /// </summary>
    public static class CSharpExtension
    {
        /// <summary>
        /// 对char[]的 ToStringFromArray 方法 （转字符串）
        /// </summary>
        /// <param name="chArray"></param>
        /// <returns></returns>
        public static string ToStringFromArray(this char[] chArray)
        {
            return new string(chArray);
        }

        /// <summary>
        /// 对List<char>的 ToSort 方法 （排序）
        /// </summary>
        /// <param name="chArray"></param>
        /// <returns></returns>
        public static List<char> ToSort(this List<char> chArray)
        {
            chArray.Sort();
            return chArray;
        }

        /// <summary>
        /// 公共方法 _Normal 格式化一个字符串，自动去掉前面和后面的空格字符
        /// </summary>
        /// <param name="str">Target</param>
        /// <param name="useRecursive">是否使用递归</param>
        /// <returns></returns>
        public static string ToNormal(this string str)
        {
            str = _Normal(str);

            //Regex regex = new Regex(@"[a-zA-Z]");
            //regex.IsMatch(str);

            return str;
        }

        /// <summary>
        /// 格式化Json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFormatJsonString(this string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 私有方法 _Normal 格式化一个字符串，自动去掉前面和后面的空格字符
        /// </summary>
        /// <param name="str">Target</param>
        /// <param name="useRecursive">是否使用递归</param>
        /// <returns></returns>
        private static string _Normal(string str)
        {
            #region 另外一种方法
            /*
            if (str.IndexOf(" ") == 0)
            {
                tempStr = str.Substring(1, str.Length - 1);
                _Normal(tempStr);
            }
            else if (str.LastIndexOf(" ") == str.Length - 1)
            {
                tempStr = str.Substring(0, str.Length - 1);
                _Normal(tempStr);
            }
            return tempStr;
            */
            #endregion

            char[] chs = str.ToCharArray();
            int startindex = 0, endindex = 1;

            for (int i = 0; i < chs.Length; i++)
                if (chs[i] != ' ') { startindex = i; break; }

            for (int i = chs.Length - 1; i >= 0; i--)
                if (chs[i] != ' ') { endindex = i; break; }

            char[] chser = new char[endindex - startindex + 1];
            for (int i = 0; i < chs.Length; i++)
                if (i >= startindex && i <= endindex)
                    chser[i - startindex] = chs[i];

            return chser.ToStringFromArray();
        }

        /// <summary>
        /// 公共方法 SelectLast 选择最后一个 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T _Getlast<T>(this T[] str)
        {
            return str[str.Length - 1];
        }

    }
}