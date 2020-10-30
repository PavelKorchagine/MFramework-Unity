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
        #region ����post����
        public static string HttpPost(string str)
        {
            string url = "http://localhost:8563/nfo/dd";
            byte[] data = Encoding.UTF8.GetBytes(str);//���ַ���ת��Ϊ�ֽ�

            return HttpPost(url, data);
        }

        public static string HttpPost(string url, string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);//���ַ���ת��Ϊ�ֽ�
            return HttpPost(url, data);
        }

        public static string HttpPost(string url, byte[] data)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            req.ContentLength = data.Length; //���󳤶�

            using (Stream reqStream = req.GetRequestStream()) //��ȡ
            {
                reqStream.Write(data, 0, data.Length);//��ǰ����д���ֽ�
                reqStream.Close(); //�رյ�ǰ��
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //��Ӧ���
            Stream stream = resp.GetResponseStream();
            //��ȡ��Ӧ����
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion

        /// <summary>
        /// GET�������ȡ���
        /// </summary>
        public static string HttpGet(string Url, string postDataStr)
        {
            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET"; //��������ʽ
            request.ContentType = "text/html;charset=UTF-8"; //������������

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //������Ӧ
            Stream myResponseStream = response.GetResponseStream(); //�����Ӧ��

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);//��UTF8���뷽ʽ��ȡ����
            string retString = myStreamReader.ReadToEnd();//��ȡ����

            myStreamReader.Close();//�ر���
            myResponseStream.Close();
            return retString;
        }

    }
}