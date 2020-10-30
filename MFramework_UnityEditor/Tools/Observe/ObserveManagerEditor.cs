using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MFramework_UnityEditor.Tools
{
    /// <summary>
    /// ObserveManager CanEditMultipleObjects CustomEditor(typeof(ObserveManager))
    /// </summary>
    [ExecuteInEditMode]
    [CanEditMultipleObjects] //sure why not
    [CustomEditor(typeof(ObserveManager))]
    public class ObserveManagerEditor : Editor
    {
        /// <summary>
        /// OnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            ObserveManager manager = target as ObserveManager;
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
            ObserveManager.ShowParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), ObserveManager.ShowParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (ObserveManager.ShowParameter)
            {
                foreach (var item in ObserveManager.pairs.Keys)
                {
                    //GUILayout.Label(string.Format("<b> item - {0} - over </b>", item), firstLevelStyle);
                    GUILayout.BeginVertical("", boxStyle);
                    try
                    {
                        ObserveManager.pairsBools[item] = EditorGUILayout.BeginToggleGroup(string.Format("<b> item - {0} - over </b>", item), ObserveManager.pairsBools[item]);
                        // showItem Control
                        if (ObserveManager.pairsBools[item])
                        {
                            foreach (var iteme in ObserveManager.pairs[item])
                            {
                                GUILayout.Label("- " + iteme + " - over", firstLevelStyle);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    
                    EditorGUILayout.EndToggleGroup();
                    GUILayout.EndVertical();
                    // showItem End
                    this.Repaint();
                }
            }
            #endregion
            if (GUI.changed)
            {
                EditorUtility.SetDirty(manager);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
