using System;
using UnityEngine;

public class NetworkEffects : MonoBehaviour
{
	public static NetworkEffects tool;

	public static GameObject model;

	public NetworkEffects()
	{
	}

	public static void askEffect(string path, Vector3 position, Quaternion rotation, float delay)
	{
		path = path.Substring(8, path.Length - 8);
		NetworkEffects.tool.networkView.RPC("tellEffect", RPCMode.All, new object[] { path, position, rotation, delay });
	}

	public void onReady()
	{
		NetworkEffects.tool = this;
		NetworkEffects.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("effects").gameObject;
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}

	[RPC]
	public void tellEffect(string path, Vector3 position, Quaternion rotation, float delay)
	{
		if (!ServerSettings.dedicated)
		{
			path = string.Concat("Effects/", path);
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(path), position, rotation);
			gameObject.name = "effect";
			gameObject.transform.parent = NetworkEffects.model.transform;
			if (delay != -1f)
			{
				UnityEngine.Object.Destroy(gameObject, delay);
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.startLifetime);
			}
		}
	}
}