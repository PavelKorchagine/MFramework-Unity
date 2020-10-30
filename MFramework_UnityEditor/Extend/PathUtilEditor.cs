using System;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

namespace MFramework_UnityEditor.Extend
{
    public class PathUtilEditor
    {
        /// <summary>
        /// preferences folder 
        /// </summary>
        public static string GetPreferencesFolderPath()
        {
            return InternalEditorUtility.unityPreferencesFolder;
        }

        /// <summary>
        /// editor assembly path 
        /// </summary>
        /// <returns></returns>
        public static string GetEditorAssemblyPath()
        {
            return InternalEditorUtility.GetEditorAssemblyPath();
        }

        /// <summary>
        /// engine assembly path 
        /// </summary>
        public static string GetEngineAssemblyPath()
        {
            return InternalEditorUtility.GetEngineAssemblyPath();
        }

        /// <summary>
        /// layout folder path 
        /// </summary>
        /// <returns></returns>
        public static string GetLayoutFolderPath()
        {
            string result = InternalEditorUtility.unityPreferencesFolder + Path.DirectorySeparatorChar + "Layouts";
            return result = result.Replace('\\', '/');
        }

        /// <summary>
        /// Assetstore download folder path 
        /// </summary>
        public static string GetAssetstoreDownloadPath()
        {
            string path = string.Empty;

            if (SystemInfo.operatingSystem.Contains("Windows"))
            {
                path = InternalEditorUtility.unityPreferencesFolder + Path.DirectorySeparatorChar + "../../Asset Store";
            }
            else if (SystemInfo.operatingSystem.Contains("Mac"))
            {
                path = InternalEditorUtility.unityPreferencesFolder + Path.DirectorySeparatorChar + "../../../Unity/Asset Store";
            }
            else
                Debug.LogError("Unknown platform. Cannot resolve Assetstore download folder for this platform.");

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// editor log path
        /// </summary>
        public static string GetEditorLogPath()
        {
            string path = string.Empty;

            if (SystemInfo.operatingSystem.Contains("Windows"))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/../Library/Logs/Unity/";
            }
            else if (SystemInfo.operatingSystem.Contains("Mac"))
            {
                path = Environment.GetEnvironmentVariable("LocalAppData") + "/Unity/Editor/";
            }
            else
                Debug.LogError("Unknown platform. Cannot resolve Log file folder for this platform.");

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Retrieves path of an external script editor. e.g) path of where VisualStudio etc.
        /// </summary>
        //public static string GetExternalScriptEditorPath()
        //{
        //    return InternalEditorUtility.GetExternalScriptEditor();
        //}
    }
}
