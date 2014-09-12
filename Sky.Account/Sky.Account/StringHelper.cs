using System;
using System.Web;

namespace Sky.Account
{
	public class StringHelper
	{
		public StringHelper ()
		{
		}

		/// <summary>
		/// 检查是否含sql字符串，防止sql注入
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="paramStr">Parameter string.</param>
		public static bool CheckSqlString(string paramStr)
		{
			string lowstring = paramStr.ToLower();
			string[] sqlArray = {"select ", "drop ", "delete ", ";"};              
			for (int x = 0; x < sqlArray.Length; x++) 
			{   
				if (lowstring.Contains(sqlArray[x])) {
					return true;
				}  
			} 

			return false;
		}
			
		/// <summary>
		/// 检查安全字符长度
		/// </summary>
		/// <returns><c>true</c>, if safe length string was checked, <c>false</c> otherwise.</returns>
		/// <param name="paramStr">Parameter string.</param>
		/// <param name="safe">Safe.</param>
		public static bool CheckSafeLengthString(string paramStr, int safe)
		{
			if (paramStr.Length > safe) {
				return false;
			}

			return true;
		}
	}
}

