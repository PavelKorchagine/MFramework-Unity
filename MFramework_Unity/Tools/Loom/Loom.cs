using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;
#region UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.Tools
{
    //  有了Loom这个工具类，在很多涉及UnityEngine对象的耗时计算还是可以得到一个解决方法的：
    //  如在场景中用A* 算法进行大量的数据计算
    //  变形网格中操作大量的顶点
    //  持续的要运行上传数据到服务器
    //  二维码识别等图像处理
    //  Loom简单而又巧妙，佩服Loom的作者。
    /// <summary>
    /// Loom
    /// </summary>
    public class Loom : MonoBehaviour
    {
        private static Loom _current;
        /// <summary>
        /// Current
        /// </summary>
        public static Loom Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        private int _count;
        /// <summary>
        /// 最大线程数
        /// </summary>
        public static int maxThreads = 8;
        private static int numThreads;
        private List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();
        private List<Action> _currentActions = new List<Action>();
        private static bool initialized;
        private List<Action> _actions = new List<Action>();
        private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

        /// <summary>
        /// 延迟方法队列元素
        /// </summary>
        public struct DelayedQueueItem
        {
            /// <summary>
            /// 延迟时间
            /// </summary>
            public float time;
            /// <summary>
            /// 延迟方法
            /// </summary>
            public Action action;
        }

        private void Awake()
        {
            _current = this;
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;
        }

        /// <summary>
        /// SayHello
        /// </summary>
        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }

        private void OnDisable()
        {
            if (_current == this)
            {
                _current = null;
            }
        }

        private void Update()
        {
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }
            for (int i = 0; i < _currentActions.Count; i++)
            {
                _currentActions[i]();
            }
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                    _delayed.Remove(item);
                for (int i = 0; i < _currentDelayed.Count; i++)
                {
                    _delayed.Remove(_currentDelayed[i]);
                }
            }
            for (int i = 0; i < _currentDelayed.Count; i++)
            {
                _currentDelayed[i].action();
            }
        }

        /// <summary>
        /// GetActions
        /// </summary>
        public List<Action> GetActions
        {
            get { return _actions; }
        }

        private static void Initialize()
        {
            if (!initialized)
            {

                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("Loom");
                _current = g.AddComponent<Loom>();
            }
        }

        /// <summary>
        /// QueueOnMainThread
        /// </summary>
        /// <param name="action"></param>
        public static void QueueOnMainThread(Action action)
        {
            Initialize();

            QueueOnMainThread(action, 0f);
        }

        /// <summary>
        /// QueueOnMainThread
        /// </summary>
        /// <param name="action"></param>
        /// <param name="time"></param>
        public static void QueueOnMainThread(Action action, float time)
        {
            Initialize();

            if (time != 0)
            {
                lock (Current._delayed)
                {
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
            }
            else
            {
                lock (Current._actions)
                {
                    Current._actions.Add(action);
                }
            }
        }

        /// <summary>
        /// RunAsync
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Thread RunAsync(Action a)
        {
            Initialize();

            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private static void RunAction(object action)
        {
            try
            {
                ((Action)action)();
            }
            catch
            {
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }
        }

        //Scale a mesh on a second thread
        //怎么实现一个函数内使用多线程计算又保持函数体内代码的顺序执行，
        //印象中使用多线程就是要摆脱代码块的顺序执行，但这里是把原本一个函数分拆成为两部分：
        //一部分在C#线程中使用，另一部还是得在Unity的MainThread中使用，怎么解决呢，还得看例子：
        //这个例子是对Mesh的顶点进行放缩，同时也是一个使用闭包（closure）和lambda表达式的一个很好例子。
        //看完例子，是不是很有把项目中一些耗时的函数给拆分出来，D.S.Qiu就想用这个方法来改进下NGUI的底层机制（看下性能不能改进）。
        //D.S.Qiu在编程技术掌握还是一个菜鸟，Thread还是停留在实现Runable接口或继承Thread的一个水平上，
        //对多线程编程的认识还只是九牛一毛。本来我以为Loom的实现会比较复杂，当我发现只有100多行的代码是大为惊叹，
        //这也得益于现在语言的改进，至少从语言使用的便利性上还是有很大的进步的。
        private void ScaleMesh(Mesh mesh, float scale)
        {
            //Get the vertices of a mesh
            var vertices = mesh.vertices;
            //Run the action on a new thread
            Loom.RunAsync(() =>
            {
                //Loop through the vertices
                for (var i = 0; i < vertices.Length; i++)
                {
                    //Scale the vertex
                    vertices[i] = vertices[i] * scale;
                }
                //Run some code on the main thread
                //to update the mesh
                Loom.QueueOnMainThread(() =>
                {
                    //Set the vertices
                    mesh.vertices = vertices;
                    //Recalculate the bounds
                    mesh.RecalculateBounds();
                });

            });
        }


        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(Loom))]
        public class LoomEditor : Editor
        {
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
                //EditorGUILayout.BeginVertical("box");
                //foreach (var item in manager.GetActions)
                //{
                //    EditorGUILayout.LabelField(item.ToString());

                //}
                //EditorGUILayout.EndVertical();

                //EditorGUILayout.BeginVertical("box");
                //foreach (var item in manager.uiMgr.GetPanelDict)
                //{
                //    //EditorGUILayout.LabelField(item.Key.ToString() + " : " + item.Value.name);

                //}
                //EditorGUILayout.EndVertical();

                this.Repaint();
            }
        }
#endif
        #endregion
    }
}