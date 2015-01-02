using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ModLoader;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
	public static NetworkManager tool;

	public static bool dedicated;

	public NetworkManager() {
	}

	public static void error(string text, string icon, NetworkPlayer player) {
		if (player != Network.player) {
			NetworkManager.tool.networkView.RPC("openError", player, new object[] { text, icon });
		} else {
			NetworkManager.tool.openError(text, icon);
		}
	}

	public void hostDedicated() {
		int maxPlayers = 12;
		int port = 25444;
		string password = string.Empty;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < (int)commandLineArgs.Length; i++) {
			if (commandLineArgs[i] == "-pei") {
				ServerSettings.map = 1;
			}
			else if (commandLineArgs[i] == "-arena") {
				ServerSettings.map = 2;
			}
			else if (commandLineArgs[i] == "-normal") {
				ServerSettings.mode = 0;
			}
			else if (commandLineArgs[i] == "-bambi") {
				ServerSettings.mode = 1;
			}
			else if (commandLineArgs[i] == "-hardcore")	{
				ServerSettings.mode = 2;
			}
			else if (commandLineArgs[i] == "-gold")	{
				ServerSettings.mode = 3;
			}
			else if (commandLineArgs[i] == "-sync") {
				ServerSettings.save = true;
			}
			else if (commandLineArgs[i] == "-nosync") {
				ServerSettings.save = false;
			}
			else if (commandLineArgs[i] == "-pvp") {
				ServerSettings.pvp = true;
			}
			else if (commandLineArgs[i] == "-pve") {
				ServerSettings.pvp = false;
			}
			else if (commandLineArgs[i] == "-sv") {
				ServerSettings.dedicated = true;
				ServerSettings.open = false;
				NetworkTools.host(maxPlayers, port, password);
			} else if (commandLineArgs[i].Length > 6 && commandLineArgs[i].Substring(0, 6) == "-pass:") {
				ServerSettings.passworded = true;
				password = commandLineArgs[i].Substring(6, commandLineArgs[i].Length - 6);
			} else if (commandLineArgs[i].Length > 6 && commandLineArgs[i].Substring(0, 6) == "-port:") {
				ServerSettings.passworded = true;
				int.TryParse(commandLineArgs[i].Substring(6, commandLineArgs[i].Length - 6), out port);
			} else if (commandLineArgs[i].Length > 9 && commandLineArgs[i].Substring(0, 9) == "-players:") {
				ServerSettings.passworded = true;
				int.TryParse(commandLineArgs[i].Substring(9, commandLineArgs[i].Length - 9), out maxPlayers);
			}
			
			Loader.hook();
		}
	}

	public void OnApplicationQuit() {
		if (Network.peerType != NetworkPeerType.Disconnected) {
			NetworkTools.disconnect();
		}
	}

	public void OnConnectedToServer() {
		NetworkEvents.triggerOnConnected();
	}

	public void OnDisconnectedFromServer() {
		NetworkEvents.triggerOnDisconnected();
	}

	public void OnFailedToConnect(NetworkConnectionError data) {
		string lower = data.ToString().ToLower();
		if (lower == "toomanyconnectedplayers") {
			NetworkEvents.triggerOnFailed(2);
		} else if (lower != "invalidpassword") {
			NetworkEvents.triggerOnFailed(0);
		}
		else {
			NetworkEvents.triggerOnFailed(1);
		}
		
		NetworkTools.disconnect();
	}

	public void OnLevelWasLoaded() {
		if (base.name == "network" && Application.loadedLevel == 0) {
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void OnPlayerConnected(NetworkPlayer player) {
		NetworkEvents.triggerOnPlayerConnected(player);
	}

	public void OnPlayerDisconnected(NetworkPlayer player) {
		NetworkEvents.triggerOnPlayerDisconnected(player);
	}

	public void OnServerInitialized() {
		NetworkEvents.triggerOnHosted();
		Console.WriteLine("Server initialized...");
	}

	[RPC]
	public void openError(string text, string icon) {
		HUDGame.openError(text, icon);
	}

	public void Start() {
		Network.minimumAllocatableViewIDs = 512;
		NetworkManager.tool = this;
		
		base.name = "network";
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!NetworkManager.dedicated) {
			NetworkManager.dedicated = true;
			base.Invoke("hostDedicated", 3f);
		}
	}
}