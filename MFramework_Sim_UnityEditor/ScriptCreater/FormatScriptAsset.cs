using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

/* 
*********************************************************************
Copyright (C) 2019 The #CompanyName. All Rights Reserved.
 
File Name:           #SCRIPTNAME#.cs
Discription:         #Discription
Author:              #AuthorName
CreateTime:          #CreateTime
********************************************************************* 
*/

namespace MFramework_Sim_UnityEditor
{
    public class FormatScriptAsset : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string allText = File.ReadAllText(path);

                string Year = System.DateTime.Now.Year.ToString();
                string CompanyName = "arHop Studio";
                string AuthorName = "Korchagin";
                string CreateTime = System.DateTime.Now.Year + "/"
                    + System.DateTime.Now.Month + "/"
                    + System.DateTime.Now.Day + " "
                    + System.DateTime.Now.Hour + ":"
                    + System.DateTime.Now.Minute + ":"
                    + System.DateTime.Now.Second;
                string Discription = "Be fully careful of  Code modification!";

                allText = allText
                    .Replace("#CompanyName", CompanyName)
                    .Replace("#Year", Year)
                    .Replace("#Discription", Discription)
                    .Replace("#AuthorName", AuthorName)
                    .Replace("#CreateTime", CreateTime);

                File.WriteAllText(path, allText);
            }
            AssetDatabase.Refresh();
        }
    }
}