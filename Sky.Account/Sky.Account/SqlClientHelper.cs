﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace Sky.Account
{
	public class SqlClientHelper
	{
		public static string connectString = "server=mssql.sql52.eznowdata.com;user id=sq_audiobook;pwd=sq222222;database=sq_audiobook;MultipleActiveResultSets=true;";
		public static SqlConnection conn = new SqlConnection ();

		public SqlClientHelper ()
		{
		}

		public static SqlConnection CurrentConn()
		{
			try 
			{
				//连接数据库
				if (conn.State != System.Data.ConnectionState.Open)
				{
					conn.Close();
					conn.ConnectionString = connectString;
					conn.Open();
				}

				return conn;
			} 
			catch (Exception exp)
			{
				HttpContext.Current.Response.Write ("===CONN CREATE ERR===\r\n{0}" + exp.ToString ());
				return null;
			}
//			if (null == this.conn) {
//				this.conn = new SqlConnection ();
//				this.conn.ConnectionString = "server=mssql.sql52.eznowdata.com;user id=sq_audiobook;pwd=sq222222;database=sq_audiobook";
//				this.conn.Open();
//			}
//
//			return this.conn;
		}

		/// <summary>
		/// 连接数据库，数据库文件在工程目录下
		/// </summary>
		/// <returns>The conn.</returns>
		/// <param name="dbName">Db name.</param>
		/// <param name="pwd">Pwd.</param>
		public static SqlConnection CreateConn()
		{
			try 
			{
				//连接数据库
				SqlConnection sqlConn = new SqlConnection ("server=mssql.sql52.eznowdata.com;user id=sq_audiobook;pwd=sq222222;database=sq_audiobook");
				return sqlConn;
			} 
			catch (Exception exp)
			{
				HttpContext.Current.Response.Write ("===CONN CREATE ERR===\r\n{0}" + exp.ToString ());
				return null;
			}
		}

		/// <summary>
		/// 开启数据库连接
		/// </summary>
		/// <param name="conn">Conn.</param>
		public static void Open(SqlConnection conn)
		{
			if (conn.State == System.Data.ConnectionState.Closed) {
				conn.Open ();
			}
		}

		/// <summary>
		/// 执行数据库语句
		/// </summary>
		/// <returns>The reader.</returns>
		/// <param name="conn">Conn.</param>
		/// <param name="sql">Sql.</param>
		public static ArrayList ExecuteReader(SqlConnection conn, string sql)
		{
			try
			{
				SqlCommand cmd = new SqlCommand ();
				cmd.CommandText = sql;
				cmd.Connection = conn;

				SqlDataReader reader = cmd.ExecuteReader ();
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

				cmd.Dispose();
				reader.Close();

				if (list.Count > 0) {
					return list;
				}

				return null;
			}
			catch (Exception exp) 
			{
				HttpContext.Current.Response.Write ("===SQL EXECUTE READER ERR===\r\n{0}" + exp.ToString ());
				return null;
			}
		}

		/// <summary>
		/// 执行无返回值的sql语句
		/// </summary>
		/// <returns><c>true</c>, if no query was executed, <c>false</c> otherwise.</returns>
		/// <param name="conn">Conn.</param>
		/// <param name="sql">Sql.</param>
		public static int ExecuteNoQuery(SqlConnection conn, string sql)
		{
			try
			{
				SqlCommand cmd = new SqlCommand ();
				cmd.CommandText = sql;
				cmd.Connection = conn;
				int exe = cmd.ExecuteNonQuery ();
				cmd.Dispose();
				return exe;
			}
			catch (Exception exp) 
			{
				HttpContext.Current.Response.Write ("===SQL EXECUTE NOQUERY ERR===\r\n{0}" + exp.ToString ());
				return -1;
			}
		}
	}
}

