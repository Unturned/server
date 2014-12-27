using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDDedicated : MonoBehaviour
{
	public static SleekWindow window;

	public static SleekContainer container;

	public static SleekBox labelChat_0;

	public static SleekBox labelChat_1;

	public static SleekBox labelChat_2;

	public static SleekBox labelChat_3;

	public static SleekBox labelChat_4;

	public static SleekField chat;

	public static SleekButton send;

	public static SleekButton endButton;

	public static SleekButton kickButton;

	public static SleekButton banButton;

	public static SleekContainer playerList;

	public static int selected;

	private static float lastUpdate;

	static HUDDedicated()
	{
		HUDDedicated.selected = -1;
	}

	public HUDDedicated()
	{
	}

	public void Awake()
	{
		HUDDedicated.window = new SleekWindow();
		HUDDedicated.container = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDDedicated.window.addFrame(HUDDedicated.container);
		HUDDedicated.labelChat_4 = new SleekBox()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(600, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.labelChat_4);
		HUDDedicated.labelChat_3 = new SleekBox()
		{
			position = new Coord2(10, 60, 0f, 0f),
			size = new Coord2(600, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.labelChat_3);
		HUDDedicated.labelChat_2 = new SleekBox()
		{
			position = new Coord2(10, 110, 0f, 0f),
			size = new Coord2(600, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.labelChat_2);
		HUDDedicated.labelChat_1 = new SleekBox()
		{
			position = new Coord2(10, 160, 0f, 0f),
			size = new Coord2(600, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.labelChat_1);
		HUDDedicated.labelChat_0 = new SleekBox()
		{
			position = new Coord2(10, 210, 0f, 0f),
			size = new Coord2(600, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.labelChat_0);
		HUDDedicated.chat = new SleekField()
		{
			position = new Coord2(10, 260, 0f, 0f),
			size = new Coord2(490, 40, 0f, 0f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.chat);
		HUDDedicated.send = new SleekButton()
		{
			position = new Coord2(510, 260, 0f, 0f),
			size = new Coord2(100, 40, 0f, 0f),
			text = "Send"
		};
		HUDDedicated.send.onUsed += new SleekDelegate(HUDDedicated.usedSend);
		HUDDedicated.container.addFrame(HUDDedicated.send);
		HUDDedicated.endButton = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_END
		};
		HUDDedicated.endButton.onUsed += new SleekDelegate(HUDDedicated.usedEnd);
		HUDDedicated.window.addFrame(HUDDedicated.endButton);
		HUDDedicated.kickButton = new SleekButton()
		{
			position = new Coord2(10, -250, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_KICK
		};
		HUDDedicated.kickButton.onUsed += new SleekDelegate(HUDDedicated.usedKick);
		HUDDedicated.kickButton.visible = false;
		HUDDedicated.window.addFrame(HUDDedicated.kickButton);
		HUDDedicated.banButton = new SleekButton()
		{
			position = new Coord2(10, -200, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BAN
		};
		HUDDedicated.banButton.onUsed += new SleekDelegate(HUDDedicated.usedBan);
		HUDDedicated.banButton.visible = false;
		HUDDedicated.window.addFrame(HUDDedicated.banButton);
		HUDDedicated.playerList = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDDedicated.container.addFrame(HUDDedicated.playerList);
		HUDDedicated.refresh();
		AudioListener.volume = 0f;
		NetworkEvents.onPlayersChanged += new NetworkEventDelegate(HUDDedicated.refresh);
	}

	public void OnGUI()
	{
		HUDDedicated.window.drawFrame();
	}

	public static void refresh()
	{
		HUDDedicated.playerList.clearFrames();
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			NetworkUser item = NetworkUserList.users[i];
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(-410, 10 + i * 50, 1f, 0f),
				size = new Coord2(400, 40, 0f, 0f)
			};
			if (!(item.friend == PlayerSettings.friendHash) || !(item.nickname != string.Empty))
			{
				sleekButton.text = item.name;
			}
			else
			{
				sleekButton.text = string.Concat(item.name, " [", item.nickname, "]");
			}
			sleekButton.onUsed += new SleekDelegate(HUDDedicated.usedPlayer);
			HUDDedicated.playerList.addFrame(sleekButton);
			if (item.status == 21)
			{
				sleekButton.color = Colors.GOLD;
			}
			SleekImage sleekImage = new SleekImage()
			{
				position = new Coord2(4, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			int averagePing = Network.GetAveragePing(item.player);
			if (averagePing > 100)
			{
				sleekImage.setImage("Textures/Icons/badConnection");
			}
			else if (averagePing <= 50)
			{
				sleekImage.setImage("Textures/Icons/goodConnection");
			}
			else
			{
				sleekImage.setImage("Textures/Icons/mediumConnection");
			}
			sleekButton.addFrame(sleekImage);
			SleekLabel sleekLabel = new SleekLabel()
			{
				position = new Coord2(10, -20, 1f, 0.5f),
				size = new Coord2(50, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft,
				text = averagePing.ToString()
			};
			sleekImage.addFrame(sleekLabel);
		}
		if (HUDDedicated.selected >= NetworkUserList.users.Count)
		{
			HUDDedicated.selected = -1;
		}
		HUDDedicated.updateSelection();
	}

	public void Start()
	{
		HUDDedicated.updateChat();
	}

	public void Update()
	{
		Screen.lockCursor = false;
		if (Time.realtimeSinceStartup - HUDDedicated.lastUpdate > 1f)
		{
			HUDDedicated.lastUpdate = Time.realtimeSinceStartup;
			HUDDedicated.refresh();
		}
	}

	public static void updateChat()
	{
		if (HUDDedicated.labelChat_0 != null)
		{
			HUDDedicated.labelChat_0.visible = NetworkChat.text_0 != string.Empty;
			HUDDedicated.labelChat_1.visible = NetworkChat.text_1 != string.Empty;
			HUDDedicated.labelChat_2.visible = NetworkChat.text_2 != string.Empty;
			HUDDedicated.labelChat_3.visible = NetworkChat.text_3 != string.Empty;
			HUDDedicated.labelChat_4.visible = NetworkChat.text_4 != string.Empty;
			if (NetworkChat.status_0 == 21)
			{
				HUDDedicated.labelChat_0.color = Colors.GOLD;
			}
			else if (NetworkChat.status_0 != 2147483647)
			{
				HUDDedicated.labelChat_0.color = Color.white;
			}
			else
			{
				HUDDedicated.labelChat_0.color = Color.green;
			}
			if (NetworkChat.status_1 == 21)
			{
				HUDDedicated.labelChat_1.color = Colors.GOLD;
			}
			else if (NetworkChat.status_1 != 2147483647)
			{
				HUDDedicated.labelChat_1.color = Color.white;
			}
			else
			{
				HUDDedicated.labelChat_1.color = Color.green;
			}
			if (NetworkChat.status_2 == 21)
			{
				HUDDedicated.labelChat_2.color = Colors.GOLD;
			}
			else if (NetworkChat.status_2 != 2147483647)
			{
				HUDDedicated.labelChat_2.color = Color.white;
			}
			else
			{
				HUDDedicated.labelChat_2.color = Color.green;
			}
			if (NetworkChat.status_3 == 21)
			{
				HUDDedicated.labelChat_3.color = Colors.GOLD;
			}
			else if (NetworkChat.status_3 != 2147483647)
			{
				HUDDedicated.labelChat_3.color = Color.white;
			}
			else
			{
				HUDDedicated.labelChat_3.color = Color.green;
			}
			if (NetworkChat.status_4 == 21)
			{
				HUDDedicated.labelChat_4.color = Colors.GOLD;
			}
			else if (NetworkChat.status_4 != 2147483647)
			{
				HUDDedicated.labelChat_4.color = Color.white;
			}
			else
			{
				HUDDedicated.labelChat_4.color = Color.green;
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_0 == PlayerSettings.friendHash) || !(NetworkChat.nickname_0 != string.Empty))
			{
				HUDDedicated.labelChat_0.text = string.Concat(NetworkChat.speaker_0, ": ", NetworkChat.text_0);
			}
			else
			{
				HUDDedicated.labelChat_0.text = string.Concat(new string[] { NetworkChat.speaker_0, " [", NetworkChat.nickname_0, "]: ", NetworkChat.text_0 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_1 == PlayerSettings.friendHash) || !(NetworkChat.nickname_1 != string.Empty))
			{
				HUDDedicated.labelChat_1.text = string.Concat(NetworkChat.speaker_1, ": ", NetworkChat.text_1);
			}
			else
			{
				HUDDedicated.labelChat_1.text = string.Concat(new string[] { NetworkChat.speaker_1, " [", NetworkChat.nickname_1, "]: ", NetworkChat.text_1 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_2 == PlayerSettings.friendHash) || !(NetworkChat.nickname_2 != string.Empty))
			{
				HUDDedicated.labelChat_2.text = string.Concat(NetworkChat.speaker_2, ": ", NetworkChat.text_2);
			}
			else
			{
				HUDDedicated.labelChat_2.text = string.Concat(new string[] { NetworkChat.speaker_2, " [", NetworkChat.nickname_2, "]: ", NetworkChat.text_2 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_3 == PlayerSettings.friendHash) || !(NetworkChat.nickname_3 != string.Empty))
			{
				HUDDedicated.labelChat_3.text = string.Concat(NetworkChat.speaker_3, ": ", NetworkChat.text_3);
			}
			else
			{
				HUDDedicated.labelChat_3.text = string.Concat(new string[] { NetworkChat.speaker_3, " [", NetworkChat.nickname_3, "]: ", NetworkChat.text_3 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_4 == PlayerSettings.friendHash) || !(NetworkChat.nickname_4 != string.Empty))
			{
				HUDDedicated.labelChat_4.text = string.Concat(NetworkChat.speaker_4, ": ", NetworkChat.text_4);
			}
			else
			{
				HUDDedicated.labelChat_4.text = string.Concat(new string[] { NetworkChat.speaker_4, " [", NetworkChat.nickname_4, "]: ", NetworkChat.text_4 });
			}
		}
	}

	public static void updateSelection()
	{
		if (HUDDedicated.selected != -1)
		{
			HUDDedicated.kickButton.visible = true;
			HUDDedicated.banButton.visible = true;
		}
		else
		{
			HUDDedicated.kickButton.visible = false;
			HUDDedicated.banButton.visible = false;
		}
	}

	public static void usedBan(SleekFrame frame)
	{
		NetworkTools.ban(NetworkUserList.users[HUDDedicated.selected].player, NetworkUserList.users[HUDDedicated.selected].name, NetworkUserList.users[HUDDedicated.selected].id, string.Concat("Host banning ", NetworkUserList.users[HUDDedicated.selected].name, "."));
	}

	public static void usedEnd(SleekFrame frame)
	{
		NetworkTools.disconnect();
	}

	public static void usedKick(SleekFrame frame)
	{
		NetworkTools.kick(NetworkUserList.users[HUDDedicated.selected].player, string.Concat("Host kicking ", NetworkUserList.users[HUDDedicated.selected].name, "."));
	}

	public static void usedPlayer(SleekFrame frame)
	{
		int offsetY = (frame.position.offset_y - 10) / 50;
		if (HUDDedicated.selected == offsetY || NetworkUserList.users[offsetY].player == Network.player)
		{
			HUDDedicated.selected = -1;
		}
		else
		{
			HUDDedicated.selected = offsetY;
		}
		HUDDedicated.updateSelection();
	}

	public static void usedSend(SleekFrame frame)
	{
		NetworkChat.sendAlert(HUDDedicated.chat.text);
		HUDDedicated.chat.text = string.Empty;
	}
}