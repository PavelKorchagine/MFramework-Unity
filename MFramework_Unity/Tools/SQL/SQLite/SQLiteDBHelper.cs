using Mono.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

namespace MFramework_Unity.Tools
{
    /// <summary>  
    /// 说明：这是一个针对System.Data.SQLite的数据库常规操作封装的通用类。  
    /// </summary>  
    public class SQLiteDBHelper
    {
        #region SQLiteDBHelper
        //private string connectionString = string.Empty;
        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="dbPath">SQLite数据库文件路径</param>  
        //public SQLiteDBHelper(string dbPath)
        //{
        //    this.connectionString = "Data Source=" + dbPath;
        //}
        /// <summary>   
        #endregion

        /// 创建SQLite数据库文件  
        /// </summary>  
        /// <param name="dbPath">要创建的SQLite数据库文件路径</param>  
        public static void CreateDB(string dbPath, out string msg)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(connection))
                {
                    command.CommandText = $"CREATE TABLE Test(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = $"DROP TABLE IF EXISTS Test";
                    command.ExecuteNonQuery();
                    msg = "返回正常";
                    //connection.Close();

                }
            }
        }

        /// <summary>  
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。  
        /// </summary>  
        /// <param name="dbPath">dbPath</param>  
        /// <param name="sql">要执行的增删改的SQL语句</param>  
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>  
        /// <returns></returns>  
        public static int ExecuteNonQuery(string dbPath, string sql, SqliteParameter[] parameters, out string retrunMsg)
        {
            int affectedRows = 0;
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SqliteCommand command = new SqliteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        try
                        {
                            affectedRows = command.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                retrunMsg = $"数据库命令{sql}执行成功，正常返回";
                            }
                            else
                            {
                                retrunMsg = $"数据库命令{sql}执行成功，数据库中没有记录，命令没有对数据库产生影响";
                            }
                        }
                        catch (Exception e)
                        {
                            retrunMsg = $"数据库命令{sql}执行失败，返回：{e.ToString()}";
                        }
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }

        /// <summary>  
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例  
        /// </summary>  
        /// <param name="sql">要执行的查询语句</param>  
        /// <param name="dbPath">dbPath</param>  
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>  
        /// <returns></returns>  
        public static SqliteDataReader ExecuteReader(string dbPath, string sql, SqliteParameter[] parameters)
        {
            SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath);
            SqliteCommand command = new SqliteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>  
        /// 执行一个查询语句，返回一个包含查询结果的DataTable  
        /// </summary>  
        /// <param name="sql">要执行的查询语句</param>  
        /// <param name="dbPath">dbPath</param>  
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>  
        /// <returns></returns>  
        public static DataTable ExecuteDataTable(string dbPath, string sql, SqliteParameter[] parameters)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqliteDataAdapter adapter = new SqliteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }

        /// <summary>  
        /// 执行一个查询语句，返回查询结果的第一行第一列  
        /// </summary>  
        /// <param name="sql">要执行的查询语句</param>  
        /// <param name="dbPath">dbPath</param>  
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>  
        /// <returns></returns>  
        public static DataTable ExecuteScalar(string dbPath, string sql, SqliteParameter[] parameters)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqliteDataAdapter adapter = new SqliteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }

        /// <summary>  
        /// 查询数据库中的所有数据类型信息  
        /// </summary>  
        /// <returns></returns>  
        public static DataTable GetSchema(string dbPath)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns)  
                //{  
                //  Console.WriteLine(column.ColumnName);  
                //}  
                return data;
            }
        }
    }
}