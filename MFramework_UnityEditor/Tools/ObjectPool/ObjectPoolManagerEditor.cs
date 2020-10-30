using System;
using System.Collections.Generic;
using UnityEditor;
using MFramework_Unity;
using UnityEngine;
using MFramework_Unity.Tools;

namespace MFramework_UnityEditor
{
    /// <summary>
    /// ObjectPoolManagerEditor CanEditMultipleObjects CustomEditor(typeof(ObjectPoolManager))
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ObjectPoolManager))]
    public class ObjectPoolManagerEditor : Editor
    {
        /// <summary>
        /// OnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            ObjectPoolManager manager = (ObjectPoolManager)target;
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

            #region ShowParameter
            GUILayout.BeginVertical("", boxStyle);
            manager._showParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager._showParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager._showParameter)
            {
                GUILayout.BeginVertical("", boxStyle);
                base.OnInspectorGUI();

                if (Application.isPlaying)
                {
                    GUILayout.Label($"objectPoolDict", boxStyle);
                    foreach (var item in ObjectPoolManager.objectPoolDict.Keys)
                    {
                        GUILayout.Label($"Type: {item}");
                        foreach (var iteme in ObjectPoolManager.objectPoolDict[item])
                        {
                            GUILayout.Label($"ObjectPool<T>: {iteme}");
                        }
                    }
                    GUILayout.Label($"poolDict", boxStyle);
                    foreach (var item in ObjectPoolManager.poolDict.Keys)
                    {
                        GUILayout.Label($"Type: {item}");
                    }
                }
                //GUILayout.BeginHorizontal("", boxStyle);
                //GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            #endregion

            this.Repaint();
        }
    }
}
