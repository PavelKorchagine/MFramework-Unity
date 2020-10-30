using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    public class NetTool
    {
        #region 发送post请求
        public static string HttpPost(string str)
        {
            string url = "http://localhost:8563/nfo/dd";
            byte[] data = Encoding.UTF8.GetBytes(str);//把字符串转换为字节

            return HttpPost(url, data);
        }

        public static string HttpPost(string url, string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);//把字符串转换为字节
            return HttpPost(url, data);
        }

        public static string HttpPost(string url, byte[] data)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            req.ContentLength = data.Length; //请求长度

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion

        /// <summary>
        /// GET请求与获取结果
        /// </summary>
        public static string HttpGet(string Url, string postDataStr)
        {
            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET"; //设置请求方式
            request.ContentType = "text/html;charset=UTF-8"; //设置内容类型

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //返回响应
            Stream myResponseStream = response.GetResponseStream(); //获得响应流

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);//以UTF8编码方式读取该流
            string retString = myStreamReader.ReadToEnd();//读取所有

            myStreamReader.Close();//关闭流
            myResponseStream.Close();
            return retString;
        }

    }
}