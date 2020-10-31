using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           MonoDriver.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/6/3 16:12:27
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    /// <summary>
    /// MonoDriver
    /// </summary>
    public class MonoDriver : MonoBehaviour
    {
        #region Awake
        ////protected virtual void Awake()
        ////{
        ////}
        ////protected virtual void OnDestroy()
        ////{
        ////}
        //protected virtual void OnEnable()
        //{
        //    //driver.Register(this);
        //}
        //protected virtual void OnDisable()
        //{
        //    //driver.Remove(this);
        //} 
        #endregion

        /// <summary>
        /// ShowParameter
        /// </summary>
        [HideInInspector] public bool _showParameter = true;
        /// <summary>
        /// baseMonos
        /// </summary>
        [HideInInspector] public List<BaseMonoAbstract> baseMonos = new List<BaseMonoAbstract>();

        /// <summary>
        /// CloneBaseMonoColl
        /// </summary>
        /// <returns></returns>
        public List<BaseMonoAbstract> CloneBaseMonoColl()
        {
            List<BaseMonoAbstract> temp = new List<BaseMonoAbstract>();
            temp.AddRange(baseMonos);
            return temp;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="baseMono"></param>
        /// <returns></returns>
        public bool Register(BaseMonoAbstract baseMono)
        {
            if (!baseMonos.Contains(baseMono))
            {
                baseMonos.Add(baseMono);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="baseMono"></param>
        /// <returns></returns>
        public bool Remove(BaseMonoAbstract baseMono)
        {
            if (baseMonos.Contains(baseMono))
            {
                baseMonos.Remove(baseMono);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            baseMonos.Clear();
        }

        /// <summary>
        /// GetLastItem
        /// </summary>
        /// <returns></returns>
        public BaseMonoAbstract GetLastItem()
        {
            return baseMonos[baseMonos.Count - 2];
        }

        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnUpdateAbstract();
            }
        }

        /// <summary>
        /// FixedUpdate
        /// </summary>
        public void FixedUpdate()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnFixUpdateAbstract();
            }
        }

        /// <summary>
        /// LateUpdate
        /// </summary>
        public void LateUpdate()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnLateUpdateAbstract();
            }
        }


        #region UNITY_EDITOR
#if UNITY_EDITOR

        private bool _showParameter = true;

        [CustomEditor(typeof(MonoDriver))]
        public class MonoDriverEditor : Editor
        {
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
#endif
        #endregion


    }
}