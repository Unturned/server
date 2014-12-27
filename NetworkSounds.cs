using System;
using UnityEngine;

public class NetworkSounds : MonoBehaviour
{
	public static NetworkSounds tool;

	public static GameObject model;

	public NetworkSounds()
	{
	}

	public static void askSound(string path, Vector3 position, float volume, float pitch, float range)
	{
		path = path.Substring(7, path.Length - 7);
		NetworkSounds.tool.networkView.RPC("tellSound", RPCMode.All, new object[] { path, position, volume, pitch, range });
	}

	public static void askSoundMax(string path, Vector3 position, float volume, float pitch, float range, float max)
	{
		path = path.Substring(7, path.Length - 7);
		NetworkSounds.tool.networkView.RPC("tellSoundMax", RPCMode.All, new object[] { path, position, volume, pitch, range, max });
	}

	public void onReady()
	{
		NetworkSounds.tool = this;
		NetworkSounds.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("sounds").gameObject;
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}

	[RPC]
	public void tellSound(string path, Vector3 position, float volume, float pitch, float range)
	{
		if (!ServerSettings.dedicated && Camera.main != null && Mathf.Abs(position.x - Camera.main.transform.position.x) < 32f && Mathf.Abs(position.z - Camera.main.transform.position.z) < 32f)
		{
			path = string.Concat("Sounds/", path);
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Effects/sound"), position, Quaternion.identity);
			gameObject.name = "sound";
			gameObject.transform.parent = NetworkSounds.model.transform;
			gameObject.audio.clip = (AudioClip)Resources.Load(path);
			gameObject.audio.volume = volume;
			gameObject.audio.pitch = pitch;
			gameObject.audio.minDistance = range;
			gameObject.audio.Play();
			if (gameObject.audio.clip == null)
			{
				UnityEngine.Object.Destroy(gameObject, 1f);
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject, gameObject.audio.clip.length);
			}
		}
	}

	[RPC]
	public void tellSoundMax(string path, Vector3 position, float volume, float pitch, float range, float max)
	{
		if (!ServerSettings.dedicated && Camera.main != null && Mathf.Abs(position.x - Camera.main.transform.position.x) < max && Mathf.Abs(position.z - Camera.main.transform.position.z) < max)
		{
			path = string.Concat("Sounds/", path);
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Effects/sound"), position, Quaternion.identity);
			gameObject.name = "sound";
			gameObject.transform.parent = NetworkSounds.model.transform;
			gameObject.audio.clip = (AudioClip)Resources.Load(path);
			gameObject.audio.volume = volume;
			gameObject.audio.pitch = pitch;
			gameObject.audio.minDistance = range;
			gameObject.audio.maxDistance = max;
			gameObject.audio.Play();
			gameObject.audio.priority = 0;
			if (gameObject.audio.clip == null)
			{
				UnityEngine.Object.Destroy(gameObject, 1f);
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject, gameObject.audio.clip.length);
			}
		}
	}
}