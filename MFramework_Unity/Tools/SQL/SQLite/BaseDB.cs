using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
    public class BaseDB
    {
        protected string dbPath = string.Empty;
        protected string table = "user";

        public BaseDB(string dbPath)
        {
            this.dbPath = dbPath;
        }


        public BaseDB(string dbPath, string table)
        {
            this.dbPath = dbPath;
            this.table = table;
        }


        public virtual void Create()
        {

        }

    }
}