using UnityEditor;
using System.Reflection;

/* 
*********************************************************************
Copyright (C) 2018 The Company Name
File Name:           NewBehaviourScript.cs
Author:              mwt
CreateTime:          2019/8/15 15:28:1
********************************************************************* 
*/

namespace MFramework_Sim_UnityEditor
{
    public class CustomProjectWindowUtil
    {
        public static void CreateScriptUtil(string path, string templete)
        {
            MethodInfo method = null;
#if UNITY_4 || UNITY_5 || UNITY_2017_0 || UNITY_2017_1 || UNITY_2017_2 || UNITY_2017_3 || UNITY_2017_4 || UNITY_2017_5
            method = typeof(UnityEditor.ProjectWindowUtil).GetMethod("CreateScriptAsset",
                BindingFlags.Static | BindingFlags.NonPublic);
#elif UNITY_2018 || UNITY_2019_0 || UNITY_2019_1 || UNITY_2019_2 || UNITY_2019_3 || UNITY_2019_3_OR_NEWER //UNITY_5_3_OR_NEWER
            method = typeof(UnityEditor.ProjectWindowUtil).GetMethod("CreateScriptAssetFromTemplateFile",
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
#else
            method = typeof(UnityEditor.ProjectWindowUtil).GetMethod("CreateScriptAssetFromTemplateFile",
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
#endif

            //UnityEngine.Debug.Log(method);
            if (method != null)
                method.Invoke(null, new object[] { path, templete });
        }

        [MenuItem("Assets/Create/CreateNormalMonoC#", false, 80)]
        public static void CreateMonoCSharp()
        {
            CustomProjectWindowUtil.CreateScriptUtil(@"Templates\78-MyMonoCSharp.cs.txt", "NewMonoCSharp.cs");
        }

        [MenuItem("Assets/Create/C# BaseMono Script", false, 80)]
        public static void CreateMyBaseMono()
        {
            CustomProjectWindowUtil.CreateScriptUtil(@"Templates\81-MyBaseMono.cs.txt", "NewBaseMono.cs");
        }

        [MenuItem("Assets/Create/C# UIBaseMono Script", false, 80)]
        public static void CreateMyUIBaseMono()
        {
            CustomProjectWindowUtil.CreateScriptUtil(@"Templates\85-MyUIBaseMono.cs.txt", "NewUIBaseMono.cs");
        }

    }

}
