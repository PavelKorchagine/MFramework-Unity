using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MFramework_UnityEditor.Tools
{
    /// <summary>
    /// DOTweenTimerManager
    /// </summary>
    [ExecuteInEditMode]
    [CanEditMultipleObjects] //sure why not
    [CustomEditor(typeof(DOTweenTimerManager))]
    public class DOTweenTimerManagerEditor : Editor
    {
        /// <summary>
        /// OnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DOTweenTimerManager manager = target as DOTweenTimerManager;
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
            manager.ShowParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager.ShowParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager.ShowParameter)
            {
                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                EditorGUILayout.LabelField("注册的 delayActionDict：" + DOTweenTimerManager.delayActionDict.Count.ToString());
                foreach (var item in DOTweenTimerManager.delayActionDict)
                {
                    EditorGUILayout.LabelField(item.Key.Target.ToString() + " :: " + item.Key.Method.Name + " :: " + item.Value.ToString());
                }
                EditorGUILayout.LabelField("注册的 delayTrans：" + DOTweenTimerManager.delayTrans.Count.ToString());
                foreach (var item in DOTweenTimerManager.delayTrans)
                {
                    EditorGUILayout.LabelField(item.ToString());
                }
                this.Repaint();
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
