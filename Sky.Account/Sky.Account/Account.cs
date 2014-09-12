using System;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sky.Account
{
	public class Account
	{
		public Account ()
		{
		}

		/// <summary>
		/// 获得数据库表名
		/// </summary>
		/// <returns>The account database.</returns>
		public static string GetAccountDatabase()
		{
			string dbName = "sky_account.db";
			//return Environment.CurrentDirectory + "/" + dbName;
			return "D:\\wwwroot\\admin1214\\wwwroot\\" + dbName;
		}
			
		/// <summary>
		/// 创建用户数据库
		/// </summary>
		/// <returns><c>true</c>, if table was created, <c>false</c> otherwise.</returns>
		public static bool CreateTable()
		{
//			bool cf = SqliteHelper.CreateFile (GetAccountDatabase());
//			if (true == cf) {
//				SqliteConnection conn = SqliteHelper.CreateConn (GetAccountDatabase (), null);
//				string sql = "CREATE TABLE [User] " +
//				             "([UserID] INTEGER PRIMARY KEY, " +
//				             "[UserName] VARCHAR(20) NOT NULL, " +
//				             "[Password] VARCHAR(255) NOT NULL, " +
//				             "[Coin] INTEGER NOT NULL DEFAULT 0, " +
//				             "[CreatedTime] TimeStamp NOT NULL DEFAULT (datetime('now','localtime')), " +
//				             "[LoginTime] TimeStamp NOT NULL DEFAULT (datetime('now','localtime')))";
//				int success = SqliteHelper.ExecuteNoQuery (conn, sql);
//				return (success == -1) ? false : true;
//			} else {
//				return false;
//			}
			return true;
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="pwd">Pwd.</param>
		public static bool Register(string userName, string pwd)
		{
			bool success = false;
			//检查sql语句是否合法，防止sql注入
			if (StringHelper.CheckSqlString (userName) || StringHelper.CheckSqlString (pwd)) {
				success = false;
			} else {
				using(SqlConnection conn = SqlClientHelper.CreateConn ()) {

					SqlClientHelper.Open (conn);
					string sql = "SELECT [UserName] FROM [User] WHERE [UserName] = '" + userName + "'";
					ArrayList list = SqlClientHelper.ExecuteReader (conn, sql);

					//若存在帐号
					if (list != null) {
						success = false;
					} else {
						sql = "INSERT INTO [User] ([Username], [Password], [Coin]) VALUES ('" + userName + "', '" + pwd + "', 0)";
						int exe = SqlClientHelper.ExecuteNoQuery (conn, sql);
						success = (exe == -1) ? false : true;
					}
				}
			}

			return success;
		}

		/// <summary>
		/// 帐号登陆
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="pwd">Pwd.</param>
		public static bool Login(string userName, string pwd)
		{
			bool success = false;
			//检查sql语句是否合法，防止sql注入
			if (StringHelper.CheckSqlString (userName) || StringHelper.CheckSqlString (pwd)) {
				success = false;
			} else {
				SqlConnection conn = SqlClientHelper.CurrentConn ();
				string sql = "SELECT [UserName], [Password] FROM User WHERE [UserName] = '" + userName + "'";
				ArrayList list = SqlClientHelper.ExecuteReader (conn, sql);

				if (list == null || list.Count != 1) {
					success = false;
				} else {
					Dictionary<string, string> dic = (Dictionary<string, string>)list [0];
					string sqlName = "";
					string sqlPwd = "";
					dic.TryGetValue ("0", out sqlName);
					dic.TryGetValue ("1", out sqlPwd);
					if (!sqlName.Equals (userName) || !sqlPwd.Equals (pwd)) {
						success = false;
					} else {
						sql = "UPDATE [User] SET [LoginTime] = (datetime('now','localtime')) WHERE [UserName] = '" + userName + "'";
						SqlClientHelper.ExecuteNoQuery (conn, sql);
						success = true;
					}
				}
			}

			return success;
		}

		/// <summary>
		/// 列出所有内容
		/// </summary>
		public static string List()
		{
			string reslut = "";
			SqlConnection conn = SqlClientHelper.CreateConn ();
			ArrayList list = SqlClientHelper.ExecuteReader (conn, "SELECT * FROM [User]");

			if (list == null || list.Count == 0) {
				reslut = null;
			} else {
				StringBuilder sb = new StringBuilder ();
				foreach (Dictionary<string, string> dic in list) {
					for (int i=0; i<dic.Count; i++) {
						string value = "";
						dic.TryGetValue (i.ToString (), out value);
						sb.Append (value + "|");
					}
					sb.Append ("<br>");
				}
				reslut = sb.ToString ();
			}

			if (conn.State == System.Data.ConnectionState.Open) {
				conn.Close ();
			}
			conn.Dispose ();

			return reslut;
		}

		/// <summary>
		/// 获得用户数据，用于客户端交互
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="userID">User I.</param>
		public static Dictionary<string, string> LoadData(int userID)
		{
			SqlConnection conn = SqlClientHelper.CurrentConn ();
			string sql = "SELECT [UserName], [Coin] FROM [User] WHERE [UserID] = " + userID;
			ArrayList list = SqlClientHelper.ExecuteReader (conn, sql);
			if (conn.State == System.Data.ConnectionState.Open) {
				conn.Close ();
			}
			conn.Dispose ();

			if (list == null || list.Count != 1) {
				return null;
			} else {

				Dictionary<string, string> dic = (Dictionary<string, string>)list [0];
				return dic;
			}
		}

		/// <summary>
		/// 增加积分，由友商服务器回调时调用，不可用于客户端交互
		/// </summary>
		/// <returns>The coin.</returns>
		/// <param name="userID">User I.</param>
		/// <param name="coin">Coin.</param>
		public static Dictionary<string, string> AddCoin(int userID, int coin)
		{
			if (coin <= 0)
				return null;

			SqlConnection conn = SqlClientHelper.CurrentConn ();
			string sql = "SELECT [Coin] FROM [User] WHERE [UserID] = " + userID;
			ArrayList list = SqlClientHelper.ExecuteReader (conn, sql);

			if (list == null || list.Count != 1) {
				return null;
			} else {
				Dictionary<string, string> dic = (Dictionary<string, string>)list [0];
				string sqlCoin = "";
				dic.TryGetValue ("0", out sqlCoin);

				int newCoin = Convert.ToInt16 (sqlCoin) + coin;
				sql = "UPDATE [User] SET [Coin] = '" + newCoin.ToString() + "' WHERE [UserID] = " + userID;
				int success = SqlClientHelper.ExecuteNoQuery (conn, sql);
				if (success == -1) {
					return null;
				} else {
					return LoadData (userID);
				}
			}
		}
	}
}

