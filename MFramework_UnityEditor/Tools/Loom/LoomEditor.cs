using MFramework_Unity.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MFramework_UnityEditor.Tools
{
    /// <summary>
    /// LoomEditor ExecuteInEditMode CanEditMultipleObjects CustomEditor(typeof(Loom))
    /// </summary>
    [ExecuteInEditMode]
    [CanEditMultipleObjects] //sure why not
    [CustomEditor(typeof(Loom))]
    public class LoomEditor : Editor
    {
        /// <summary>
        /// OnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Loom manager = target as Loom;

            if (manager == null)
            {
                return;
            }
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            EditorGUILayout.LabelField(manager.GetActions.Count.ToString());

            this.Repaint();
        }
    }
}
