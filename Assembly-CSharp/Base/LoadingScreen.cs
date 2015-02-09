using System;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
	public static bool loading;

	public LoadingScreen()
	{
	}

	public void Awake()
	{
		if (GameObject.Find("loadingScreen") != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			base.name = "loadingScreen";
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	public void OnGUI()
	{

	}
}