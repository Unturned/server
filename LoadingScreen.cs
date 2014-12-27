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
		if (Application.isLoadingLevel)
		{
			SleekRender.image(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), Images.pixel, (PlayerSettings.status != 21 ? Colors.FREE : Colors.PAID));
			SleekRender.box(new Rect(10f, (float)(Screen.height / 2 - 20), (float)((int)(NetworkLoader.load.progress * (float)(Screen.width - 20))), 40f), string.Concat((int)Mathf.Floor(NetworkLoader.load.progress * 100f), "%"), string.Empty, Color.white, Color.white);
			LoadingScreen.loading = true;
			Screen.showCursor = false;
		}
		else if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			SleekRender.image(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), Images.pixel, (PlayerSettings.status != 21 ? Colors.FREE : Colors.PAID));
			SleekRender.box(new Rect(10f, (float)(Screen.height / 2 - 20), (float)(Screen.width - 20), 40f), Texts.ERROR_INTERNET, string.Empty, Color.white, Color.white);
			LoadingScreen.loading = true;
			Screen.showCursor = false;
		}
		else if (Network.peerType == NetworkPeerType.Disconnected || !(Player.model == null) || Network.isServer && ServerSettings.dedicated)
		{
			LoadingScreen.loading = false;
		}
		else
		{
			SleekRender.image(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), Images.pixel, (PlayerSettings.status != 21 ? Colors.FREE : Colors.PAID));
			SleekRender.box(new Rect(10f, (float)(Screen.height / 2 - 20), (float)(Screen.width - 20), 40f), "Awaiting player...", string.Empty, Color.white, Color.white);
			LoadingScreen.loading = true;
			Screen.showCursor = false;
		}
	}
}