using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.InputSystem
{
    public class AutoSetLaymask : MonoBehaviour
    {
        [SerializeField] private int layerIndex = 0;
        [SerializeField] private bool isContainChild = false;
        [SerializeField] private List<GameObject> expList = new List<GameObject>();

        private void Awake()
        {
            gameObject.layer = layerIndex;
            if (!isContainChild) return;

            Transform[] childs = transform.GetComponentsInChildren<Transform>(true);
            int length = childs.Length;
            for (int i = 0; i < length; i++)
            {
                if (expList.Contains(childs[i].gameObject)) continue;
                childs[i].gameObject.layer = layerIndex;
            }
        }

        [ContextMenu("ManualSetLaymask")]
        public void ManualSetLaymask()
        {
            gameObject.layer = layerIndex;
            if (!isContainChild) return;

            Transform[] childs = transform.GetComponentsInChildren<Transform>(true);
            int length = childs.Length;
            for (int i = 0; i < length; i++)
            {
                if (expList.Contains(childs[i].gameObject)) continue;
                childs[i].gameObject.layer = layerIndex;
            }
        }

    }
}