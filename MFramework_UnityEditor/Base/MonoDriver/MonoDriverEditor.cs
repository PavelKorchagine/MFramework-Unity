using MFramework_Unity;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MFramework_UnityEditor
{
    /// <summary>
    /// MonoDriverEditor
    /// </summary>
    [CustomEditor(typeof(MonoDriver))]
    public class MonoDriverEditor : Editor
    {
        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            MonoDriver manager = target as MonoDriver;

            #region GUI style 设置
            GUI.changed = false;
            serializedObject.Update();
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
            #endregion

            GUILayout.BeginVertical("", boxStyle);
            manager._showParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager._showParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager._showParameter)
            {
                GUILayout.BeginVertical("", boxStyle);
                base.OnInspectorGUI();
                //GUILayout.BeginHorizontal("", boxStyle);
                //GUILayout.EndHorizontal();

                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                EditorGUILayout.LabelField("注册的 BaseMono：" + manager.baseMonos.Count.ToString());
                foreach (var item in manager.baseMonos)
                {
                    EditorGUILayout.LabelField("(" + manager.baseMonos.IndexOf(item).ToString() + ") " + item);
                }
                GUILayout.EndVertical();

                this.Repaint();
            }
        }
    }
}
