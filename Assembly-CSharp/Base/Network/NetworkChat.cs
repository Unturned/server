using System;
using CommandHandler;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class NetworkChat : MonoBehaviour
{
	public readonly static int MAX_CHARACTERS;

	public static NetworkChat tool;

	public static string notification;

	public static string speaker_0;

	public static string nickname_0;

	public static string friend_0;

	public static string text_0;

	public static int status_0;

	public static int type_0;

	public static int reputation_0;

	public static string speaker_1;

	public static string nickname_1;

	public static string friend_1;

	public static string text_1;

	public static int status_1;

	public static int type_1;

	public static int reputation_1;

	public static string speaker_2;

	public static string nickname_2;

	public static string friend_2;

	public static string text_2;

	public static int status_2;

	public static int type_2;

	public static int reputation_2;

	public static string speaker_3;

	public static string nickname_3;

	public static string friend_3;

	public static string text_3;

	public static int status_3;

	public static int type_3;

	public static int reputation_3;

	public static string speaker_4;

	public static string nickname_4;

	public static string friend_4;

	public static string text_4;

	public static int status_4;

	public static int type_4;

	public static int reputation_4;

	public static bool chatting;

	public static int mode;

	static NetworkChat()
	{
		NetworkChat.MAX_CHARACTERS = 75;
		NetworkChat.notification = string.Empty;
		NetworkChat.speaker_0 = string.Empty;
		NetworkChat.nickname_0 = string.Empty;
		NetworkChat.friend_0 = string.Empty;
		NetworkChat.text_0 = string.Empty;
		NetworkChat.speaker_1 = string.Empty;
		NetworkChat.nickname_1 = string.Empty;
		NetworkChat.friend_1 = string.Empty;
		NetworkChat.text_1 = string.Empty;
		NetworkChat.speaker_2 = string.Empty;
		NetworkChat.nickname_2 = string.Empty;
		NetworkChat.friend_2 = string.Empty;
		NetworkChat.text_2 = string.Empty;
		NetworkChat.speaker_3 = string.Empty;
		NetworkChat.nickname_3 = string.Empty;
		NetworkChat.friend_3 = string.Empty;
		NetworkChat.text_3 = string.Empty;
		NetworkChat.speaker_4 = string.Empty;
		NetworkChat.nickname_4 = string.Empty;
		NetworkChat.friend_4 = string.Empty;
		NetworkChat.text_4 = string.Empty;
	}

	public NetworkChat() {
		
	}

	[RPC]
	public void askChat(string text, int type, NetworkPlayer player) {
		
		// Command handler
		if ( !CommandHandlerMain.isCommand(text, type, player) ) {
			NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(player);
			if (userFromPlayer != null) {
				if (text.Length > NetworkChat.MAX_CHARACTERS)
				{
					text = text.Substring(0, NetworkChat.MAX_CHARACTERS);
				}
				if (type == 0)
				{
					base.networkView.RPC("tellChat", RPCMode.All, new object[] { userFromPlayer.name, userFromPlayer.nickname, userFromPlayer.friend, text, userFromPlayer.status, type, userFromPlayer.reputation });
					// TODO: write some better solution!
					using (StreamWriter w = File.AppendText("chat.txt"))
					{
						w.WriteLine(String.Format("{0}||{1}||{2}||{3}", userFromPlayer.status, userFromPlayer.reputation, userFromPlayer.name, text ));
						w.Flush();
						w.Close();
					}
				}
				else if (type == 1)
				{
					for (int i = 0; i < NetworkUserList.users.Count; i++)
					{
						NetworkUser item = NetworkUserList.users[i];
						if (item.model != null && (item.model.transform.position - userFromPlayer.model.transform.position).magnitude < 48f)
						{
							if (item.player != Network.player)
							{
								base.networkView.RPC("tellChat", item.player, new object[] { userFromPlayer.name, userFromPlayer.nickname, userFromPlayer.friend, text, userFromPlayer.status, type, userFromPlayer.reputation });
							}
							else
							{
								this.tellChat_Pizza(userFromPlayer.name, userFromPlayer.nickname, userFromPlayer.friend, text, userFromPlayer.status, type, userFromPlayer.reputation);
							}
						}
					}
				}
				else if (type == 2)
				{
					for (int j = 0; j < NetworkUserList.users.Count; j++)
					{
						NetworkUser networkUser = NetworkUserList.users[j];
						if (userFromPlayer.friend != string.Empty && userFromPlayer.friend == networkUser.friend)
						{
							if (networkUser.player != Network.player)
							{
								base.networkView.RPC("tellChat", networkUser.player, new object[] { userFromPlayer.name, userFromPlayer.nickname, userFromPlayer.friend, text, userFromPlayer.status, type, userFromPlayer.reputation });
							}
							else
							{
								this.tellChat_Pizza(userFromPlayer.name, userFromPlayer.nickname, userFromPlayer.friend, text, userFromPlayer.status, type, userFromPlayer.reputation);
							}
						}
					}
				}
			}
		}
	}

	public static void sendAlert(string text)
	{
		if (Network.isServer)
		{
			NetworkChat.tool.networkView.RPC("tellChat", RPCMode.All, new object[] { "Server", string.Empty, string.Empty, text, 2147483647, 3, -1 });
		}
	}

	public static void sendChat(string message)
	{
		if (message != string.Empty)
		{
			if (!Network.isServer)
			{
				NetworkChat.tool.networkView.RPC("askChat", RPCMode.Server, new object[] { message, NetworkChat.mode, Network.player });
			}
			else
			{
				NetworkChat.tool.askChat(message, NetworkChat.mode, Network.player);
			}
		}
		NetworkChat.chatting = false;
	}

	public static void sendNotification(NetworkPlayer player, string text)
	{
		if (player != Network.player)
		{
			NetworkChat.tool.networkView.RPC("tellNotification", player, new object[] { text });
		}
		else
		{
			NetworkChat.tool.tellNotification(text);
		}
	}

	public void Start()
	{
		NetworkChat.tool = this;
		NetworkChat.chatting = false;
		NetworkChat.mode = 0;

        gameObject.AddComponent<RemovedFunctions>();
	}

	[RPC]
	public void tellChat(string speaker, string nickname, string friend, string text, int status, int type, int reputation, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellChat_Pizza(speaker, nickname, friend, text, status, type, reputation);
		}
	}

	public void tellChat_Pizza(string speaker, string nickname, string friend, string text, int status, int type, int reputation)
	{
		NetworkChat.speaker_4 = NetworkChat.speaker_3;
		NetworkChat.nickname_4 = NetworkChat.nickname_3;
		NetworkChat.friend_4 = NetworkChat.friend_3;
		NetworkChat.text_4 = NetworkChat.text_3;
		NetworkChat.status_4 = NetworkChat.status_3;
		NetworkChat.type_4 = NetworkChat.type_3;
		NetworkChat.reputation_4 = NetworkChat.reputation_3;
		NetworkChat.speaker_3 = NetworkChat.speaker_2;
		NetworkChat.nickname_3 = NetworkChat.nickname_2;
		NetworkChat.friend_3 = NetworkChat.friend_2;
		NetworkChat.text_3 = NetworkChat.text_2;
		NetworkChat.status_3 = NetworkChat.status_2;
		NetworkChat.type_3 = NetworkChat.type_2;
		NetworkChat.reputation_3 = NetworkChat.reputation_2;
		NetworkChat.speaker_2 = NetworkChat.speaker_1;
		NetworkChat.nickname_2 = NetworkChat.nickname_1;
		NetworkChat.friend_2 = NetworkChat.friend_1;
		NetworkChat.text_2 = NetworkChat.text_1;
		NetworkChat.status_2 = NetworkChat.status_1;
		NetworkChat.type_2 = NetworkChat.type_1;
		NetworkChat.reputation_2 = NetworkChat.reputation_1;
		NetworkChat.speaker_1 = NetworkChat.speaker_0;
		NetworkChat.nickname_1 = NetworkChat.nickname_0;
		NetworkChat.friend_1 = NetworkChat.friend_0;
		NetworkChat.text_1 = NetworkChat.text_0;
		NetworkChat.status_1 = NetworkChat.status_0;
		NetworkChat.type_1 = NetworkChat.type_0;
		NetworkChat.reputation_1 = NetworkChat.reputation_0;
		NetworkChat.speaker_0 = speaker;
		NetworkChat.nickname_0 = nickname;
		NetworkChat.friend_0 = friend;
		NetworkChat.text_0 = text;
		NetworkChat.status_0 = status;
		NetworkChat.type_0 = type;
		NetworkChat.reputation_0 = reputation;
	}

	[RPC]
	public void tellNotification(string text) {
		NetworkChat.notification = text;
	}
}