using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;

namespace Sky.Account
{
	public class SqliteHelper
	{
		public SqliteHelper ()
		{
		}

		/// <summary>
		/// 创建数据库文件，数据库文件在工程目录下
		/// </summary>
		/// <param name="dbName">Db name.</param>
		public static bool CreateFile(string datasource)
		{
			try 
			{
				HttpContext.Current.Response.Write ("datasource : " + datasource);
				SqliteConnection.CreateFile (datasource);
				return true;
			}
			catch (Exception exp) 
			{
				HttpContext.Current.Response.Write ("===FILE CREATE ERR===\r\n{0}" + exp.ToString ());
				return false;
			}
		}

		/// <summary>
		/// 连接数据库，数据库文件在工程目录下
		/// </summary>
		/// <returns>The conn.</returns>
		/// <param name="dbName">Db name.</param>
		/// <param name="pwd">Pwd.</param>
		public static SqliteConnection CreateConn(string datasource, string pwd)
		{
			try 
			{
				//连接数据库
				SqliteConnection conn = new SqliteConnection ();
				SqliteConnectionStringBuilder connstr = new SqliteConnectionStringBuilder ();
				connstr.DataSource = datasource;
				if (pwd != null) {
					connstr.Password = pwd;
				}
				conn.ConnectionString = connstr.ToString();
				conn.Open ();
				return conn;
			} 
			catch (Exception exp)
			{
				HttpContext.Current.Response.Write ("===CONN CREATE ERR===\r\n{0}" + exp.ToString ());
				return null;
			}
		}

		/// <summary>
		/// 执行数据库语句
		/// </summary>
		/// <returns>The reader.</returns>
		/// <param name="conn">Conn.</param>
		/// <param name="sql">Sql.</param>
		public static ArrayList ExecuteReader(SqliteConnection conn, string sql)
		{
			try
			{
				SqliteCommand cmd = new SqliteCommand ();
				cmd.CommandText = sql;
				cmd.Connection = conn;

				SqliteDataReader reader = cmd.ExecuteReader ();
				int fieldCount = reader.FieldCount;

				ArrayList list = new ArrayList();
				while (reader.Read ()) 
				{
					Dictionary<string, string> dic = new Dictionary<string, string>();
					for (int index = 0; index < fieldCount; index++) {
						//Type tp = reader.GetType;
						//reader.
						var key = index.ToString();
						var value = reader.GetValue(index).ToString();
						dic.Add(key, (value != null) ? value : "");
					}
					list.Add(dic);
				}

				if (list.Count > 0) {
					return list;
				}

				return null;
			}
			catch (Exception exp) 
			{
				HttpContext.Current.Response.Write ("===SQL EXECUTE ERR===\r\n{0}" + exp.ToString ());
				return null;
			}
		}

		/// <summary>
		/// 执行无返回值的sql语句
		/// </summary>
		/// <returns><c>true</c>, if no query was executed, <c>false</c> otherwise.</returns>
		/// <param name="conn">Conn.</param>
		/// <param name="sql">Sql.</param>
		public static int ExecuteNoQuery(SqliteConnection conn, string sql)
		{
			try
			{
				SqliteCommand cmd = new SqliteCommand ();
				cmd.CommandText = sql;
				cmd.Connection = conn;
				return cmd.ExecuteNonQuery ();
			}
			catch (Exception exp) 
			{
				HttpContext.Current.Response.Write ("===SQL EXECUTE ERR===\r\n{0}" + exp.ToString ());
				return -1;
			}
		}
	}
}

