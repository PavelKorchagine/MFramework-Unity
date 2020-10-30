using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MFramework_UnityEditor.Tools
{
    /// <summary>
    /// CoroutineTaskManagerEditor
    /// </summary>
    [ExecuteInEditMode]
    [CanEditMultipleObjects] //sure why not
    [CustomEditor(typeof(CoroutineTaskManager))]
    public class CoroutineTaskManagerEditor : Editor
    {
        /// <summary>
        /// OnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CoroutineTaskManager manager = target as CoroutineTaskManager;

            if (manager == null)
            {
                return;
            }
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            EditorGUILayout.LabelField("注册的 delayActionDict：" + manager.taskStateDict.Count.ToString());

            this.Repaint();
        }
    }
}
