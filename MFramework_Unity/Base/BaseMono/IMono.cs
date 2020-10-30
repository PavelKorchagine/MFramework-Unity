using System;
using System.Collections.Generic;

namespace MFramework_Unity
{
    /// <summary>
    /// IMono
    /// </summary>
    public interface IMono
    {
        /// <summary>
        /// OnCreate
        /// </summary>
        void OnCreate();
        /// <summary>
        /// OnGet
        /// </summary>
        void OnGet();
        /// <summary>
        /// 
        /// </summary>
        void OnReset();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        void OnInit(params object[] args);
        /// <summary>
        /// 
        /// </summary>
        void OnInit();
        /// <summary>
        /// 
        /// </summary>
        void OnEnter();
        /// <summary>
        /// 
        /// </summary>
        void OnExit();
        /// <summary>
        /// 
        /// </summary>
        void OnDie();
    }


}
