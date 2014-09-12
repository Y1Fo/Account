using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Sky.Account
{
	public class DesCryptor
	{
		public DesCryptor ()
		{
		}

		public static string[] KeyArray
		{
			get { return new string[] { "ac68!3#1", "55%g7z!@", "(^g&vd+1", "FpV94n&3", "5*cKem&2", "&35%383@", "c@di*4#!", "(&j1k9Bv", "{[d8j*c6", "O~i2&8)8" }; }
		}

		public static int KeyToken
		{
			get { return (new Random()).Next(10); }
		}

		/// <summary>
		/// 加密字符串(参数编码:UTF8)
		/// </summary>
		/// <param name="encryptString">源字符串</param>
		/// <param name="pKey">密钥</param>
		/// <param name="iKey">初始化向量</param>
		/// <returns>加密后的字符串</returns>
		public static string EncryptDES(string encryptString, string pKey, string iKey)
		{
			try
			{
				byte[] private_key = Encoding.UTF8.GetBytes(pKey.PadRight(8).Substring(0, 8));
				byte[] public_key = Encoding.UTF8.GetBytes(iKey);
				byte[] buffer = Encoding.UTF8.GetBytes(encryptString);

				DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
				MemoryStream mStream = new MemoryStream();
				CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(private_key, public_key), CryptoStreamMode.Write);
				cStream.Write(buffer, 0, buffer.Length);
				cStream.FlushFinalBlock();

				return Convert.ToBase64String(mStream.ToArray());
			}
			catch
			{
				return encryptString;
			}
		}

		/// <summary>
		/// 解密字符串(参数编码:UTF8)
		/// </summary>
		/// <param name="decryptString">加密过的字符串</param>
		/// <param name="pKey">密钥</param>
		/// <param name="iKey">初始化向量</param>
		/// <returns>解密后的字符串</returns>
		public static string DecryptDES(string decryptString, string pKey, string iKey)
		{
			try
			{
				byte[] private_key = Encoding.UTF8.GetBytes(pKey.PadRight(8).Substring(0, 8));
				byte[] public_key = Encoding.UTF8.GetBytes(iKey);
				byte[] inputByteArray = Convert.FromBase64String(decryptString);

				DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
				MemoryStream mStream = new MemoryStream();
				CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(private_key, public_key), CryptoStreamMode.Write);
				cStream.Write(inputByteArray, 0, inputByteArray.Length);
				cStream.FlushFinalBlock();

				return Encoding.UTF8.GetString(mStream.ToArray());
			}
			catch
			{
				return decryptString;
			}
		}

	}
}

