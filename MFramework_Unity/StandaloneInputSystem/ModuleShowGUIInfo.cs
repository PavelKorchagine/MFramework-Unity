using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

   /* 
   *********************************************************************
   Copyright (C) 2019 The Company Name
   File Name:           ModuleShowGUIInfo.cs
   Author:              Korchagin
   CreateTime:          2019
   ********************************************************************* 
   */

namespace MFramework_Unity.InputSystem
{
    public class ModuleShowGUIInfo : MonoBehaviour
    {
        [SerializeField] private string guiShowInfo;

        private void Reset()
        {
            guiShowInfo = string.Format("[{0}:{1}] {2}", gameObject.layer, LayerMask.LayerToName(gameObject.layer), gameObject.name);
        }

        public string GetGuiShowInfo()
        {
            if (string.IsNullOrEmpty(guiShowInfo)) return gameObject.name;
            return guiShowInfo;
        }

        public int GetGameObjLayer()
        {
            return gameObject.layer;
        }

    }
}