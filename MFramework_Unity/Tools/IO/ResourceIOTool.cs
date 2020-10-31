﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// 资源读取器，负责从不同路径读取资源
    /// </summary>
    public class ResourceIOTool : MonoBehaviour
    {
        private static ResourceIOTool instance;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ResourceIOTool GetInstance()
        {
            if (instance == null)
            {
                GameObject resourceIOTool = new GameObject();
                resourceIOTool.name = "ResourceIO";
                DontDestroyOnLoad(resourceIOTool);

                instance = resourceIOTool.AddComponent<ResourceIOTool>();
            }

            return instance;
        }

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }

        #region 读操作
        /// <summary>
        /// ReadStringByFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadStringByFile(string path)
        {
            StringBuilder line = new StringBuilder();
            try
            {
                if (!File.Exists(path))
                {
                    Debug.Log("path dont exists ! : " + path);
                    return "";
                }

                StreamReader sr = File.OpenText(path);
                line.Append(sr.ReadToEnd());

                sr.Close();
                sr.Dispose();
            }
            catch (Exception e)
            {
                Debug.Log("Load text fail ! message:" + e.Message);
            }

            return line.ToString();
        }

        /// <summary>
        /// ReadStringByBundle
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadStringByBundle(string path)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(path);

            TextAsset ta = null;

#if UNITY_5 || UNITY_2017
            TextAsset ta = (TextAsset)ab.mainAsset;
#elif UNITY_2018 || UNITY_2019
            TextAsset ta = (TextAsset)ab.mainAsset;
#endif
            if (ta == null) return string.Empty;

            string content = ta.ToString();
            ab.Unload(true);

            return content;
        }

        /// <summary>
        /// ReadStringByResource
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadStringByResource(string path)
        {
            path = FileTool.RemoveExpandName(path);
            TextAsset text = (TextAsset)Resources.Load(path);

            if (text == null)
            {
                return "";
            }
            else
            {
                return text.text;
            }
        }

        /// <summary>
        /// ReadTextureByFile
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D ReadTextureByFile(string path, int width, int height)
        {
            //创建文件读取流
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            //释放文件读取流
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;

            //创建Texture
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            return texture;
        }

        ///// <summary>
        ///// WWW同步加载一个对象
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public static AssetBundle AssetsBundleLoadByWWW(string url)
        //{
        //    AssetBundle result = null;

        //    foreach (AssetBundle obj in LoadWWW(url))
        //    {
        //        result = obj;
        //    }

        //    if(result == null)
        //    {
        //        throw new Exception("AssetsBundleLoadByWWW Exception: URL: ->" + url + "<- ");
        //    }

        //    return result;
        //}

        //public static IEnumerable<AssetBundle> LoadWWW(string url)
        //{
        //    WWW www = new WWW(url);

        //    yield return www.assetBundle;

        //    if (www.isDone == false || www.error != null)
        //    {
        //        Debug.LogError("LoadWWW Error URL: ->" + url + "<- error: " + www.error);
        //    }
        //}

        /// <summary>
        /// ResourceLoadAsync
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void ResourceLoadAsync(string path, LoadCallBack callback)
        {
            GetInstance().MonoLoadMethod(path, callback);
        }

        /// <summary>
        /// MonoLoadMethod
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public void MonoLoadMethod(string path, LoadCallBack callback)
        {
            StartCoroutine(MonoLoadByResourcesAsync(path, callback));
        }

        LoadState m_loadState = new LoadState();
        /// <summary>
        /// MonoLoadByResourcesAsync
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator MonoLoadByResourcesAsync(string path, LoadCallBack callback)
        {
            ResourceRequest status = Resources.LoadAsync(path);

            while (!status.isDone)
            {
                m_loadState.UpdateProgress(status);
                callback(m_loadState, null);

                yield return 0;
            }

            m_loadState.UpdateProgress(status);
            callback(m_loadState, status.asset);
        }

        /// <summary>
        /// 异步加载单个assetsbundle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void AssetsBundleLoadAsync(string path, AssetBundleLoadCallBack callback)
        {
            GetInstance().MonoLoadAssetsBundleMethod(path, callback);
        }

        /// <summary>
        /// MonoLoadAssetsBundleMethod
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public void MonoLoadAssetsBundleMethod(string path, AssetBundleLoadCallBack callback)
        {
            StartCoroutine(MonoLoadByAssetsBundleAsync(path, callback));
        }

        /// <summary>
        /// MonoLoadByAssetsBundleAsync
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator MonoLoadByAssetsBundleAsync(string path, AssetBundleLoadCallBack callback)
        {
#if !UNITY_WEBGL
            AssetBundleCreateRequest status = AssetBundle.LoadFromFileAsync(path);
            LoadState loadState = new LoadState();

            while (!status.isDone)
            {
                loadState.UpdateProgress(status);
                callback(loadState, null);

                yield return 0;
            }
            if (status.assetBundle != null)
            {
                status.assetBundle.name = path;
            }

            loadState.UpdateProgress(status);
            callback(loadState, status.assetBundle);
#else
        WWW www = new WWW(path);
        LoadState loadState = new LoadState();

        while (!www.isDone)
        {
            loadState.UpdateProgress(www);
            callback(loadState, null);

            yield return 0;
        }
        if (www.assetBundle != null)
        {
            www.assetBundle.name = path;
        }

        loadState.UpdateProgress(www);
        callback(loadState, www.assetBundle);
#endif
        }

#if UNITY_5 || UNITY_2017
        /// <summary>
        /// 异步加载WWW
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void WWWLoadAsync(string url, WWWLoadCallBack callback)
        {
            GetInstance().MonoLoadWWWethod(url, callback);
        }

        public void MonoLoadWWWethod(string url, WWWLoadCallBack callback)
        {
            StartCoroutine(MonoLoadByWWWAsync(url, callback));
        }

        public IEnumerator MonoLoadByWWWAsync(string url, WWWLoadCallBack callback)
        {
            WWW www = new WWW(url);
            LoadState loadState = new LoadState();

            while (!www.isDone)
            {

                loadState.UpdateProgress(www);
                callback(loadState, www);

                yield return 0;
            }

            loadState.UpdateProgress(www);
            callback(loadState, www);
        }

#elif UNITY_2018 || UNITY_2019

#endif

        #endregion

        #region 写操作
#if !UNITY_WEBGL || UNITY_EDITOR

        //web Player 不支持写操作
        /// <summary>
        /// 写操作 web Player 不支持写操作
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteStringByFile(string path, string content)
        {
            byte[] dataByte = Encoding.GetEncoding("UTF-8").GetBytes(content);

            CreateFile(path, dataByte);
        }

        /// <summary>
        /// WriteTexture2DByFile
        /// </summary>
        /// <param name="path"></param>
        /// <param name="texture"></param>
        public static void WriteTexture2DByFile(string path, Texture2D texture)
        {
            byte[] dataByte = texture.EncodeToPNG();

            CreateFile(path, dataByte);
        }

        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.Log("File:[" + path + "] dont exists");
            }
        }

        /// <summary>
        /// CreateFile
        /// </summary>
        /// <param name="path"></param>
        /// <param name="byt"></param>
        public static void CreateFile(string path, byte[] byt)
        {
            try
            {
                FileTool.CreatFilePath(path);
                File.WriteAllBytes(path, byt);

            }
            catch (Exception e)
            {
                Debug.LogError("File Create Fail! \n" + e.Message);
            }
        }

#endif

        #endregion

    }

    /// <summary>
    /// AssetBundleLoadCallBack
    /// </summary>
    /// <param name="state"></param>
    /// <param name="bundlle"></param>
    public delegate void AssetBundleLoadCallBack(LoadState state, AssetBundle bundlle);
#if UNITY_5 || UNITY_2017
    public delegate void WWWLoadCallBack(LoadState loadState, WWW www);
#elif UNITY_2018 || UNITY_2019

#endif

    /// <summary>
    /// UnityRequestCallBack
    /// </summary>
    /// <param name="loadState"></param>
    /// <param name="www"></param>
    public delegate void UnityRequestCallBack(LoadState loadState, UnityWebRequest www);

    /// <summary>
    /// LoadCallBack
    /// </summary>
    /// <param name="loadState"></param>
    /// <param name="resObject"></param>
    public delegate void LoadCallBack(LoadState loadState, object resObject);

    /// <summary>
    /// LoadState
    /// </summary>
    public class LoadState
    {
        private static LoadState completeState;

        /// <summary>
        /// CompleteState
        /// </summary>
        public static LoadState CompleteState
        {
            get
            {
                if (completeState == null)
                {
                    completeState = new LoadState();
                    completeState.isDone = true;
                    completeState.progress = 1;
                }
                return completeState;
            }
        }

        //public object asset;
        /// <summary>
        /// isDone
        /// </summary>
        public bool isDone;

        /// <summary>
        /// progress
        /// </summary>
        public float progress;

        /// <summary>
        /// UpdateProgress
        /// </summary>
        /// <param name="resourceRequest"></param>
        public void UpdateProgress(ResourceRequest resourceRequest)
        {
            isDone = resourceRequest.isDone;
            progress = resourceRequest.progress;
        }

        /// <summary>
        /// UpdateProgress
        /// </summary>
        /// <param name="assetBundleCreateRequest"></param>
        public void UpdateProgress(AssetBundleCreateRequest assetBundleCreateRequest)
        {
            isDone = assetBundleCreateRequest.isDone;
            progress = assetBundleCreateRequest.progress;
        }

#if UNITY_5 || UNITY_2017
        public void UpdateProgress(WWW www)
        {
            isDone = www.isDone;
            progress = www.progress;
        }
#elif UNITY_2018 || UNITY_2019

#endif


    }


}