using System;
using UnityEngine;

public class Epoch
{
	public static int serverTime = -1;

	public Epoch()
	{
	}

	public static int getSeconds()
	{
		return Epoch.serverTime + (int)Time.realtimeSinceStartup;
	}
}