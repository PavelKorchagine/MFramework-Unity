using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using System.Text;
using System.IO;

namespace MFramework_Unity.Tools
{
    public class SQLiteManager : SQLBaseManager
    {
        //连接
        public SqliteConnection connection;

        public SQLiteManager() : base()
        {

        }

        public void CreateDB(string _dbPath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(_dbPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_dbPath));
                }
                // 获取扩展名， 获取含有后缀的文件名， 获取不含有后缀的文件名, 获取文件所在的目录, 当前程序目录,获取文件的全路径
                if (Path.GetExtension(_dbPath) == "" || Path.GetFileName(_dbPath) == ""
                    || Path.GetFileNameWithoutExtension(_dbPath) == ""
                    || Path.GetDirectoryName(_dbPath) == "" || System.Environment.CurrentDirectory == ""
                    || Path.GetFullPath(_dbPath) == "")
                {

                }
                connection = new SqliteConnection("data source=" + _dbPath);

                // 如果没有数据库，Open 也会新创建一个数据库
                connection.Open();
                connection.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("在进行创建和连接数据库时： " + e);
            }

        }

        public void DeleteDB(string _dbPath)
        {
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
        }

        public ConnectionState GetSqlConnectState()
        {
            return connection.State;
        }

        public override void Connect(string _path)
        {
            //初始化地址
            var _dbPath = "Data source = " + _path;
            try
            {
                //创建连接
                connection = new SqliteConnection(_dbPath);
                //打开数据库
                connection.Open();
                connection.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("在进行创建和连接数据库时： " + e);
            }

        }

        public void Test()
        {
            //打开数据库
            connection.Open();

            //创建命令行
            var command = connection.CreateCommand();

            //编辑命令
            command.CommandText = SqliteTool.GetExecuteReaderStr("table", "test");

            //保存获取信息
            var reader = command.ExecuteReader();
            //设置是否存在表 Student
            bool isExit = false;
            //读取数据库
            //while (reader.Read())
            //{
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        if (reader.GetValue(i).ToString() != "0")
            //        {
            //            isExit = true;
            //            Debug.Log("已存在 : " + reader.GetValue(i));
            //        }
            //    }
            //}

            //清除命令
            command.Dispose();
            command = null;
            //关闭SqliteDataReader
            reader.Close();

            //如果不存在Student该表
            if (!isExit)
            {
                string commandtext1 = "Create Table test(Sid text,Sname text,Ssex text, Sage text, Sdept text, sex int)";
                string data;
                bool result = connection.CreateAndExecuteCommand(commandtext1, out data);
                if (result)
                {
                    Debug.Log(data);
                }
                else
                {
                    Debug.LogError(data);
                }
            }
        }

        public void TestCreateTable(string tableName = "test")
        {
            //打开数据库
            connection.Open();

            //创建命令行
            var command = connection.CreateCommand();

            //编辑命令
            command.CommandText = SqliteTool.GetExecuteReaderStr("table", "test");

            //保存获取信息
            var reader = command.ExecuteReader();
            //设置是否存在表 Student
            bool isExit = false;
            //读取数据库
            //while (reader.Read())
            //{
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        if (reader.GetValue(i).ToString() != "0")
            //        {
            //            isExit = true;
            //            Debug.Log("已存在 : " + reader.GetValue(i));
            //        }
            //    }
            //}

            //清除命令
            command.Dispose();
            command = null;
            //关闭SqliteDataReader
            reader.Close();

            //如果不存在Student该表
            if (!isExit)
            {
                string commandtext1 = "Create Table test(Sid text,Sname text,Ssex text, Sage text, Sdept text, sex int)";
                string data;
                bool result = connection.CreateAndExecuteCommand(commandtext1, out data);
                if (result)
                {
                    Debug.Log(data);
                }
                else
                {
                    Debug.LogError(data);
                }
            }
        }

        protected override void Dispose()
        {
            connection.Close();
        }
    }


    public static class SqliteConnectionExtension
    {
        public static bool CreateAndExecuteCommand(this SqliteConnection connection, string cmdStr, out string msg)
        {
            msg = "";
            if (connection == null || string.IsNullOrEmpty(cmdStr)) return false;
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = cmdStr;
                //无返回值
                command.ExecuteNonQuery();
                command.Dispose();
                command = null;
                connection.Close();
                msg = "数据库命令 <i><color=yellow>" + cmdStr + "</color></i> 执行成功，正常返回";

                return true;
            }
            catch (System.Exception e)
            {
                msg = "数据库命令 <i><color=red>" + cmdStr + "</color></i> 执行失败，返回：" + e.ToString();
                return false;
            }

        }

        /// <summary>
        /// 整理数据库
        /// </summary>
        /// <param name="connection"></param>
        public static void Order(this SqliteConnection connection, out string retrunMsg)
        {
            retrunMsg = "";
            if (connection == null) return;
            try
            {
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();

                cmd.CommandText = "VACUUM";
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connection.Close();
            }
            catch (System.Exception e)
            {
                retrunMsg = e.ToString();
            }

        }
    }

    public class SqliteTool
    {
        public static string GetExecuteReaderStr(string _type, string _name)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //string data = "Select count(*) from sqlite_master where type = \'table\' and name = \'Teacher\'";
            stringBuilder.Append("Select count(*) from sqlite_master where type = \'")
                .Append(_type)
                .Append("\' and name = \'")
                .Append(_name)
                .Append("\'");

            return stringBuilder.ToString();

        }

        public static void CreateDB(string _dbName)
        {
            string path = Application.dataPath + "/" + _dbName;
            SqliteConnection cn = new SqliteConnection("data source=" + path);

            // 如果没有数据库，Open 也会新创建一个数据库
            cn.Open();
            cn.Close();
        }

        public static void DeleteDB(string _dbName)
        {
            string path = Application.dataPath + "/" + _dbName;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        //---添加表
        public static void CreateTable(SqliteConnection cn, string table)
        {
            if (cn == null) return;

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = cn;
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {table}(id varchar(4),score int)";
                //cmd.CommandText = "CREATE TABLE IF NOT EXISTS t1(id varchar(4),score int)";
                //注意上面那句被注释掉的 CREATE TABEL IF NOT EXISTS ，一般情况下用这句比较好
                //，如果原来就有同名的表，没有这句就会出错。SQL 语句其实也不用全部大写
                //，全部大写是 SQL 语句约定俗成的（令我想起读书的时候学的 COBOL），全部小写也不会出错。

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            cn.Close();
        }

        //---删除表
        public static void DeleteTable(SqliteConnection cn, string table)
        {
            if (cn == null) return;

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = cn;
                cmd.CommandText = $"DROP TABLE IF EXISTS {table}";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            cn.Close();
        }

        // 查询表结构
        public static DataTable ReadDataTable(SqliteConnection cn, string tableName)
        {
            DataTable dataTable = null;

            if (cn == null) return null;

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.CommandText = $"PRAGMA table_info('{tableName}')";
                //写法一：用DataAdapter和DataTable类，记得要 using System.Data
                SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                #region foreach DataRow
                //foreach (DataRow r in dataTable.Rows)
                //{
                //    Debug.Log($"{r["cid"]},{r["name"]},{r["type"]},{r["notnull"]},{r["dflt_value"]},{r["pk"]} ");
                //} 
                #endregion

                #region 用DataReader
                //写法二：用DataReader，这个效率高些
                //SqliteDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    for (int i = 0; i < reader.FieldCount; i++)
                //    {
                //        Debug.Log($"{reader[i]},");
                //    }
                //}
                //reader.Close(); 
                #endregion

                cmd.Dispose();
            }

            cn.Close();
            return dataTable;

        }

        //---遍历查询表结构
        public static void QueryAllTableInfo(SqliteConnection cn, string tableName)
        {
            if (cn == null) return;

            if (cn.State != System.Data.ConnectionState.Open)
            {
                cn.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE TYPE='table' ";
                SqliteDataReader sr = cmd.ExecuteReader();
                List<string> tables = new List<string>();
                while (sr.Read())
                {
                    tables.Add(sr.GetString(0));
                }
                //datareader 必须要先关闭，否则 commandText 不能赋值
                sr.Close();
                foreach (var a in tables)
                {
                    cmd.CommandText = $"PRAGMA TABLE_INFO({a})";
                    sr = cmd.ExecuteReader();
                    while (sr.Read())
                    {
                        Debug.Log($"{sr[0]} {sr[1]} {sr[2]} {sr[3]}");
                    }
                    sr.Close();
                }

                cmd.Dispose();
            }
            cn.Close();
        }

        /// <summary>
        /// 更改表名
        /// </summary>
        public static void ChangeTableName(SqliteConnection sqliteConnection, string source, string target)
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = $"ALTER TABLE {source} RENAME TO {target}";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();
        }

        /// <summary>
        /// 增添列（字段）
        /// </summary>
        public static void AddTableColumn(SqliteConnection sqliteConnection, string _name, string _type)
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = $"ALTER TABLE t1 ADD COLUMN {_name} {_type}";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }

        /// <summary>
        /// 读取创建表时的 SQL 语句
        /// </summary>
        public static void ReadCreateTable()
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT sql FROM sqlite_master WHERE TYPE='table'";
            SqliteDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                Debug.Log(sr[0].ToString());
            }
            sr.Close();

            cmd.Dispose();
            cn.Close();
        }

        /// <summary>
        /// 更改列名
        /// </summary>
        public static void ChangeColumnName(SqliteConnection sqliteConnection, string source, string target)
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            //   int _index = list.IndexOf(list.Where(x => x.name == str[2]).First());
            cmd.CommandText = $"ALTER TABLE {source} RENAME TO {target}";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cn.Close();
        }

        public static void InsertSimple(params object[] datas)
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = "INSERT INTO t1 VALUES('99999',11)";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }


        public static void Insert(params object[] datas)
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            string s = "123456";
            int n = 10;
            cmd.CommandText = "INSERT INTO t1(id,age) VALUES(@id,@age)";
            cmd.Parameters.Add("id", DbType.String).Value = s;
            cmd.Parameters.Add("age", DbType.Int32).Value = n;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }


        public static void Replace()
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            string s = "123456";
            int n = 30;
            cmd.CommandText = "REPLACE INTO t1(id,age) VALUES(@id,@age)";
            cmd.Parameters.Add("id", DbType.String).Value = s;
            cmd.Parameters.Add("age", DbType.Int32).Value = n;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }

        public static void Update()
        {
            string path = @"d:\test\123.sqlite";
            SqliteConnection cn = new SqliteConnection("data source=" + path);
            cn.Open();
            SqliteCommand cmd = cn.CreateCommand();

            string s = "333444";
            int n = 30;
            cmd.CommandText = "UPDATE t1 SET id=@id,age=@age WHERE id='0123456789'";
            cmd.Parameters.Add("id", DbType.String).Value = s;
            cmd.Parameters.Add("age", DbType.Int32).Value = n;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }

        public static void Delete(SqliteConnection cn, string table, string id)
        {
            //string path = @"d:\test\123.sqlite";
            //SqliteConnection cn = new SqliteConnection("data source=" + path);

            if (cn == null) return;
            if (cn.State != ConnectionState.Open) cn.Open();
            if (cn.State != ConnectionState.Open) return;

            SqliteCommand cmd = cn.CreateCommand();
            cmd.CommandText = $"DELETE FROM {table} WHERE id={id}";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }

        public static void Select(SqliteConnection cn, string table)
        {
            //string path = @"d:\test\123.sqlite";
            //SqliteConnection cn = new SqliteConnection("data source=" + path);

            if (cn == null) return;
            if (cn.State != ConnectionState.Open) cn.Open();
            if (cn.State != ConnectionState.Open) return;
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = $"SELECT rowid FROM {table} ";
            var sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                Debug.Log($"{sr.GetString(0)} {sr.GetInt32(1).ToString()}");
            }
            sr.Close();

            cmd.Dispose();
            cn.Close();
        }

        public static void SelectCount(SqliteConnection cn, string table)
        {
            if (cn == null) return;
            if (cn.State != ConnectionState.Open) cn.Open();
            if (cn.State != ConnectionState.Open) return;
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT count(*) FROM t1";
            var sr = cmd.ExecuteReader();
            sr.Read();
            Debug.Log(sr.GetInt32(0).ToString());

            cmd.Dispose();
            cn.Close();
        }

        /// <summary>
        /// ---事务
        /// 事务就是对数据库一组按逻辑顺序操作的执行单元。
        /// 用事务的好处就是成熟的数据库都对 密集型的磁盘 IO 操作之类进行优化，而且还能进行撤回回滚操作。其实在上面改变列名的示例中就用过。
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="cmd"></param>
        public static void TransActionOperate(SqliteConnection cn, SqliteCommand cmd)
        {
            using (SqliteTransaction tr = cn.BeginTransaction())
            {
                string s = "";
                int n = 0;
                cmd.CommandText = "INSERT INTO t2(id,score) VALUES(@id,@score)";
                cmd.Parameters.Add("id", DbType.String);
                cmd.Parameters.Add("score", DbType.Int32);
                for (int i = 0; i < 10; i++)
                {
                    s = i.ToString();
                    n = i;
                    cmd.Parameters[0].Value = s;
                    cmd.Parameters[1].Value = n;
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
            }
        }

        /// <summary>
        /// 整理数据库
        /// SQLite 的自带命令 VACUUM。用来重新整理整个数据库达到紧凑之用，比如把删除的彻底删掉等等。
        /// </summary>
        /// <param name="cn"></param>
        public static void Order(SqliteConnection cn)
        {
            if (cn == null) return;
            if (cn.State != ConnectionState.Open) cn.Open();
            if (cn.State != ConnectionState.Open) return;
            SqliteCommand cmd = cn.CreateCommand();

            cmd.CommandText = "VACUUM";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cn.Close();
        }

    }

}