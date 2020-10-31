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
            //GUI style 设置
            GUIStyle firstLevelStyle = new GUIStyle(GUI.skin.label);
            firstLevelStyle.alignment = TextAnchor.UpperLeft;
            firstLevelStyle.fontStyle = FontStyle.Normal;
            firstLevelStyle.fontSize = 11;
            firstLevelStyle.wordWrap = true;

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
            boxStyle.fontStyle = FontStyle.Bold;
            boxStyle.alignment = TextAnchor.UpperLeft;

            if (manager == null)
            {
                return;
            }
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            GUILayout.BeginVertical("", boxStyle);
            EditorGUILayout.LabelField("注册的 delayActionDict：" + manager.taskStateDict.Count.ToString());
            GUILayout.EndVertical();
            this.Repaint();
        }
    }
}
