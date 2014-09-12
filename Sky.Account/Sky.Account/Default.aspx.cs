using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data.SqlClient;
using System.Collections;
using System.Text;


namespace Sky.Account
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class Default : System.Web.UI.Page
	{
		public void btnCreate_Click (object sender, EventArgs args)
		{
			SqlConnection sqlCon = new SqlConnection();
			sqlCon.ConnectionString = "server=mssql.sql52.eznowdata.com;user id=sq_audiobook;pwd=sq222222;database=sq_audiobook";
			sqlCon.Open();
			string sql = "select * from User";
			SqlCommand cmd = new SqlCommand(sql, sqlCon);
			SqlDataReader reader =  cmd.ExecuteReader();
			int fieldCount = reader.FieldCount;

			ArrayList list = new ArrayList();
			while (reader.Read ()) 
			{
				Dictionary<string, string> dic = new Dictionary<string, string>();
				for (int index = 0; index < fieldCount; index++) {
					var key = index.ToString();
					var value = reader.GetValue(index).ToString();
					dic.Add(key, (value != null) ? value : "");
				}
				list.Add(dic);
			}
			StringBuilder sb = new StringBuilder ();
			foreach (Dictionary<string, string> dic in list) {
				for (int i=0; i<dic.Count; i++) {
					string value = "";
					dic.TryGetValue (i.ToString (), out value);
					sb.Append (value + "|");
				}
				sb.Append ("<br>");
			}
			HttpContext.Current.Response.Write (sb.ToString ());
			return;


			bool success = Account.CreateTable ();
			if (success) {
				HttpContext.Current.Response.Write ("Success");
			} else {
				HttpContext.Current.Response.Write ("Failed");
			}
		}
		/// <summary>
		/// 登陆按钮点击
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void btnLogin_Click (object sender, EventArgs args)
		{
			bool success = Account.Login (txtUserName.Text, DesCryptor.EncryptDES(txtPassword.Text, DesCryptor.KeyArray[0], DesCryptor.KeyArray[0]));

			if (success) {
				HttpContext.Current.Response.Write ("Success");
			} else {
				HttpContext.Current.Response.Write ("Failed");
			}
		}

		/// <summary>
		/// 注册按钮点击
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void btnRegister_Click (object sender, EventArgs args)
		{
			bool success = Account.Register (txtUserName.Text, DesCryptor.EncryptDES(txtPassword.Text, DesCryptor.KeyArray[0], DesCryptor.KeyArray[0]));

			if (success) {
				HttpContext.Current.Response.Write ("Success");
			} else {
				HttpContext.Current.Response.Write ("Failed");
			}
		}
		/// <summary>
		/// 把所有已注册用户列出
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void btnList_Click (object sender, EventArgs args)
		{
			string result = Account.List ();
			if (result != null) {
				HttpContext.Current.Response.Write (result);
			}
		}

		public void btnAddCoin_Click (object sender, EventArgs args)
		{
			Dictionary<string, string> dic = Account.AddCoin(1, 3);
			if (dic != null) {
				string name = "";
				string coin = "";
				dic.TryGetValue ("0", out name);
				dic.TryGetValue ("1", out coin);
				HttpContext.Current.Response.Write ("Name : " + name + "<br>" + "Coin : " + coin);
			}
		}

		public void btnReduceCoin_Click (object sender, EventArgs args)
		{

		}
	}
}

