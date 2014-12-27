using System;
using UnityEngine;

public class Reflector : MonoBehaviour
{
	public static string level;

	public static Transform[] cubemaps;

	static Reflector()
	{
	}

	public Reflector()
	{
	}

	public void Awake()
	{
		Reflector.build();
		UnityEngine.Object.Destroy(this);
	}

	public static void build()
	{
		if (Reflector.level != Application.loadedLevelName || Reflector.cubemaps == null)
		{
			Reflector.level = Application.loadedLevelName;
			Transform transforms = GameObject.Find(Application.loadedLevelName).transform.FindChild("cubemaps");
			Reflector.cubemaps = new Transform[transforms.childCount];
			for (int i = 0; i < (int)Reflector.cubemaps.Length; i++)
			{
				Reflector.cubemaps[i] = transforms.GetChild(i);
			}
		}
	}
}