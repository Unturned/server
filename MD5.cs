using System;
using System.Security.Cryptography;
using System.Text;

public class MD5
{
	public MD5()
	{
	}

	public static string hash(string text)
	{
		byte[] numArray = (new MD5CryptoServiceProvider()).ComputeHash((new UTF8Encoding()).GetBytes(text));
		string empty = string.Empty;
		for (int i = 0; i < (int)numArray.Length; i++)
		{
			empty = string.Concat(empty, Convert.ToString(numArray[i], 16).PadLeft(2, '0'));
		}
		return empty.PadLeft(32, '0');
	}
}