using System;
using System.Collections.Generic;

public class Packer
{
	public Packer()
	{
	}

	public static string[] unpack(string serial, char delimiter)
	{
		int num = 0;
		List<string> strs = new List<string>();
		for (int i = 0; i < serial.Length; i = num + 1)
		{
			num = serial.IndexOf(delimiter, i);
			strs.Add(serial.Substring(i, num - i));
		}
		return strs.ToArray();
	}
}