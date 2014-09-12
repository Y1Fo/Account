using System;
using System.Web;

namespace Sky.Account
{
	public class UrlHelper
	{
		public UrlHelper ()
		{
		}

		/// <summary> 
		/// 判断当前页面是否接收到了Post请求 
		/// </summary> 
		/// <returns>是否接收到了Post请 求</returns> 
		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		/// <summary> 
		/// 判断当前页面是否接收到了Get请求 
		/// </summary> 
		/// <returns>是否接收到了Get请 求</returns> 
		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		/// <summary> 
		/// 获取当前请求的原始 URL(URL 中域信息之 后的部分,包括查询字符串(如果存在)) 
		/// </summary> 
		/// <returns>原 始 URL</returns> 
		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		/// <summary> 
		/// 获得当前完整Url地址 
		/// </summary> 
		/// <returns>当前完整Url地 址</returns> 
		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		/// <summary> 
		/// 获得指定Url参数的值 get
		/// </summary> 
		/// <param name="strName"& gt;Url参数</param> 
		/// <returns>Url参数的值</returns> 
		public static string GetQueryString(string strName)
		{
			if (HttpContext.Current.Request.QueryString[strName] == null)
			{
				return "";
			}

			return HttpContext.Current.Request.QueryString[strName];
		}

		/// <summary> 
		/// 获得指定表单参数的值 post
		/// </summary> 
		/// <param name="strName"& gt;表单参数</param> 
		/// <returns>表单参数的值</returns> 
		public static string GetFormString(string strName)
		{
			if (HttpContext.Current.Request.Form[strName] == null)
			{
				return "";
			}

			return HttpContext.Current.Request.Form[strName];

		}

		/// <summary>
		/// 获取参数值(QueryString&Form)
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="pramaName">参数名</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns></returns>
		public static T GetPramasValue<T>(string pramaName, T defaultValue)
		{
			T ret = defaultValue;
			try
			{
				string pramaValue = HttpContext.Current.Request[pramaName];

				//				if (!string.IsNullOrEmpty(pramaValue) && StringHelper.CheckStringFilter(pramaValue))
				//				{
				//					pramaValue = StringHelper.FilterHtml(pramaValue);
				//					ret = (T)Convert.ChangeType(pramaValue, typeof(T));
				//				}
				if (!string.IsNullOrEmpty(pramaValue) && !StringHelper.CheckSqlString(pramaValue))
				{
					ret = (T)Convert.ChangeType(pramaValue, typeof(T));
				}
			}
			catch
			{
			}
			return ret;
		}

		/// <summary> 
		/// 获得当前页面客户端的IP 
		/// </summary> 
		/// <returns>当前页面客户端的 IP</returns> 
		public static string GetIP()
		{
			string result = String.Empty;

			result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			if (null == result || result == String.Empty)
			{
				result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}

			if (null == result || result == String.Empty)
			{
				result = HttpContext.Current.Request.UserHostAddress;
			}

			if (null == result || result == String.Empty)
			{
				return "0.0.0.0";
			}

			return result;
		}
	}
}

