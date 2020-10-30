using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// ObjectPool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : Component
    {
        // Start is called before the first frame update
        /// <summary>
        /// 
        /// </summary>
        public T t;
        /// <summary>
        /// 
        /// </summary>
        public Queue<T> pool = new Queue<T>();
        /// <summary>
        /// ���������
        /// </summary>
        public Transform bornPointTran;
        /// <summary>
        /// 
        /// </summary>
        public ObjectType type;

        /// <summary>
        /// 
        /// </summary>
        public ObjectPool() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public ObjectPool(ObjectType type = ObjectType.WorldObject)
        {
            this.type = type;
        }

        /// <summary>
        /// ObjectPool
        /// </summary>
        /// <param name="t"></param>
        /// <param name="volume"></param>
        /// <param name="bornPointTran"></param>
        /// <param name="type"></param>
        public ObjectPool(T t, int volume = 5, Transform bornPointTran = null, ObjectType type = ObjectType.WorldObject)
        {
            this.t = t;
            if (bornPointTran == null) this.bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
            else this.bornPointTran = bornPointTran;
            this.type = type;

            Debug.LogWarning("[ObjectPoolManager] ObjectPool:" + this.bornPointTran + ", ObjectType:" + this.type);

            if (t != null)
            {
                for (int i = 0; i < volume; i++)
                {
                    pool.Enqueue(GetOne());
                }
            }
            else
            {
                Debug.LogError($"�����ش�������Ϊ {typeof(T)} �����{t} Ϊ�գ����飡");
            }

        }

        /// <summary>
        /// ObjectPool
        /// </summary>
        /// <param name="t"></param>
        /// <param name="volume"></param>
        /// <param name="type"></param>
        public ObjectPool(T t, int volume = 5, ObjectType type = ObjectType.WorldObject)
        {
            this.t = t;
            this.bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
            this.type = type;

            Debug.LogWarning("[ObjectPoolManager] ObjectPool:" + this.bornPointTran + ", ObjectType:" + this.type);

            if (t != null)
            {
                for (int i = 0; i < volume; i++)
                {
                    pool.Enqueue(GetOne());
                }
            }
            else
            {
                Debug.LogError($"�����ش�������Ϊ {typeof(T)} �����{t} Ϊ�գ����飡");
            }
        }

        private T GetOne()
        {
            if (bornPointTran == null)
            {
                bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
            }

            T t = UnityEngine.Object.Instantiate(this.t, bornPointTran);
            t.gameObject.SetActive(false);

            return t;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            if (pool.Count <= 0)
            {
                pool.Enqueue(GetOne());
            }
            return pool.Dequeue();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="t"></param>
        public void ResumeObject(T t)
        {
            t.gameObject.SetActive(false);
            if (bornPointTran == null)
                bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
            Transform tar = t.transform;
            tar.SetParent(bornPointTran);
            tar.localPosition = tar.localEulerAngles = Vector3.zero;
            tar.localScale = Vector3.one;
            pool.Enqueue(t);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                var go = pool.Dequeue();
                UnityEngine.Object.Destroy(go.gameObject);
            }
        }

    }
}