using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDPlayers
{
	public static SleekContainer container;

	public static SleekContainer containerPlayers;

	public static SleekContainer containerBans;

	public static bool state;

	public static SleekButton buttonPlayersTab;

	public static SleekButton buttonBansTab;

	public static SleekButton buttonKick;

	public static SleekButton buttonBan;

	public static SleekButton buttonMute;

	public static SleekSlider sliderPlayers;

	public static SleekButton[] buttonPlayers;

	public static SleekButton[] buttonBans;

	public static SleekSlider sliderBans;

	private static int selected;

	private static int offset;

	public HUDPlayers()
	{
		HUDPlayers.selected = -1;
		HUDPlayers.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 1f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.container.addFrame(HUDPlayers.container);
		HUDPlayers.container.visible = false;
		HUDPlayers.containerPlayers = new SleekContainer()
		{
			position = Coord2.ZERO,
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDPlayers.container.addFrame(HUDPlayers.containerPlayers);
		HUDPlayers.containerBans = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDPlayers.container.addFrame(HUDPlayers.containerBans);
		if (Network.isServer)
		{
			HUDPlayers.buttonPlayersTab = new SleekButton()
			{
				position = new Coord2(-405, 10, 0.5f, 0f),
				size = new Coord2(400, 40, 0f, 0f),
				text = "Players"
			};
			HUDPlayers.buttonPlayersTab.onUsed += new SleekDelegate(HUDPlayers.usedPlayersTab);
			HUDPlayers.container.addFrame(HUDPlayers.buttonPlayersTab);
			HUDPlayers.buttonBansTab = new SleekButton()
			{
				position = new Coord2(5, 10, 0.5f, 0f),
				size = new Coord2(400, 40, 0f, 0f),
				text = "Bans"
			};
			HUDPlayers.buttonBansTab.onUsed += new SleekDelegate(HUDPlayers.usedBansTab);
			HUDPlayers.container.addFrame(HUDPlayers.buttonBansTab);
		}
		HUDPlayers.buttonKick = new SleekButton()
		{
			position = new Coord2(-460, -195, 0.5f, 0.5f),
			size = new Coord2(300, 40, 0f, 0f),
			text = Texts.LABEL_KICK
		};
		HUDPlayers.buttonKick.onUsed += new SleekDelegate(HUDPlayers.usedKick);
		HUDPlayers.buttonKick.visible = false;
		HUDPlayers.containerPlayers.addFrame(HUDPlayers.buttonKick);
		HUDPlayers.buttonBan = new SleekButton()
		{
			position = new Coord2(-150, -195, 0.5f, 0.5f),
			size = new Coord2(300, 40, 0f, 0f),
			text = Texts.LABEL_BAN
		};
		HUDPlayers.buttonBan.onUsed += new SleekDelegate(HUDPlayers.usedBan);
		HUDPlayers.buttonBan.visible = false;
		HUDPlayers.containerPlayers.addFrame(HUDPlayers.buttonBan);
		HUDPlayers.buttonMute = new SleekButton()
		{
			position = new Coord2(160, -195, 0.5f, 0.5f),
			size = new Coord2(300, 40, 0f, 0f),
			text = "Mute"
		};
		HUDPlayers.buttonMute.onUsed += new SleekDelegate(HUDPlayers.usedMute);
		HUDPlayers.buttonMute.visible = false;
		HUDPlayers.containerPlayers.addFrame(HUDPlayers.buttonMute);
		HUDPlayers.buttonPlayers = new SleekButton[6];
		for (int i = 0; i < (int)HUDPlayers.buttonPlayers.Length; i++)
		{
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(-200, -140 + i * 50, 0.5f, 0.5f),
				size = new Coord2(400, 40, 0f, 0f)
			};
			sleekButton.onUsed += new SleekDelegate(HUDPlayers.usedPlayer);
			HUDPlayers.buttonPlayers[i] = sleekButton;
			SleekImage sleekImage = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			sleekButton.addFrame(sleekImage);
			HUDPlayers.containerPlayers.addFrame(sleekButton);
		}
		HUDPlayers.sliderPlayers = new SleekSlider()
		{
			position = new Coord2(210, -140, 0.5f, 0.5f),
			size = new Coord2(20, 290, 0f, 0f)
		};
		HUDPlayers.sliderPlayers.onUsed += new SleekDelegate(HUDPlayers.usedSliderPlayers);
		HUDPlayers.sliderPlayers.orientation = Orient2.VERTICAL;
		HUDPlayers.containerPlayers.addFrame(HUDPlayers.sliderPlayers);
		HUDPlayers.buttonBans = new SleekButton[6];
		for (int j = 0; j < (int)HUDPlayers.buttonBans.Length; j++)
		{
			SleekButton sleekButton1 = new SleekButton()
			{
				position = new Coord2(-200, -140 + j * 50, 0.5f, 0.5f),
				size = new Coord2(400, 40, 0f, 0f)
			};
			sleekButton1.onUsed += new SleekDelegate(HUDPlayers.usedBanned);
			HUDPlayers.buttonBans[j] = sleekButton1;
			HUDPlayers.containerBans.addFrame(sleekButton1);
		}
		HUDPlayers.sliderBans = new SleekSlider()
		{
			position = new Coord2(210, -140, 0.5f, 0.5f),
			size = new Coord2(20, 290, 0f, 0f)
		};
		HUDPlayers.sliderBans.onUsed += new SleekDelegate(HUDPlayers.usedSliderBans);
		HUDPlayers.sliderBans.orientation = Orient2.VERTICAL;
		HUDPlayers.containerBans.addFrame(HUDPlayers.sliderBans);
		HUDPlayers.state = false;
	}

	public static void close()
	{
		HUDPlayers.state = false;
		HUDPlayers.container.position = Coord2.ZERO;
		HUDPlayers.container.lerp(new Coord2(0, 0, 0f, 1f), HUDPlayers.container.size, 4f, true);
		HUDPlayers.selected = -1;
		HUDPlayers.buttonKick.visible = false;
		HUDPlayers.buttonBan.visible = false;
	}

	public static void open()
	{
		HUDPlayers.state = true;
		HUDPlayers.container.position = new Coord2(0, 0, 0f, 1f);
		HUDPlayers.container.lerp(Coord2.ZERO, HUDPlayers.container.size, 4f);
		HUDPlayers.container.visible = true;
		HUDPlayers.refreshPlayers();
		HUDPlayers.refreshBans();
	}

	public static void refreshBans()
	{
		// TODO: client side ban managing
		/*if (NetworkBans.GetBannedPlayers().Count >= 6)
		{
			HUDPlayers.offset = (int)Mathf.Ceil((float)(NetworkBans.bans.Count - 6) * HUDPlayers.sliderBans.state);
			HUDPlayers.sliderBans.scale = 6f / (float)NetworkBans.bans.Count;
		}
		else
		{
			HUDPlayers.offset = 0;
			HUDPlayers.sliderBans.scale = 1f;
		}
		for (int i = 0; i < (int)HUDPlayers.buttonBans.Length; i++)
		{
			if (i + HUDPlayers.offset < NetworkBans.GetBannedPlayers().Count)
			{
				HUDPlayers.buttonBans[i].text = string.Concat("Unban ", NetworkBans.bans[i + HUDPlayers.offset].name);
			}
			else if (i != 0)
			{
				HUDPlayers.buttonBans[i].visible = false;
			}
			else
			{
				HUDPlayers.buttonBans[i].text = "Sorry: No Bans";
				HUDPlayers.buttonBans[i].visible = true;
			}
		}*/
	}

	public static void refreshPlayers()
	{
		if (NetworkUserList.users.Count >= 6)
		{
			HUDPlayers.offset = (int)Mathf.Ceil((float)(NetworkUserList.users.Count - 6) * HUDPlayers.sliderPlayers.state);
			HUDPlayers.sliderPlayers.scale = 6f / (float)NetworkUserList.users.Count;
		}
		else
		{
			HUDPlayers.offset = 0;
			HUDPlayers.sliderPlayers.scale = 1f;
		}
		for (int i = 0; i < (int)HUDPlayers.buttonPlayers.Length; i++)
		{
			if (i + HUDPlayers.offset < NetworkUserList.users.Count)
			{
				NetworkUser item = NetworkUserList.users[i + HUDPlayers.offset];
				HUDPlayers.buttonPlayers[i].visible = true;
				if (!(PlayerSettings.friendHash != string.Empty) || !(item.friend == PlayerSettings.friendHash) || !(item.nickname != string.Empty))
				{
					HUDPlayers.buttonPlayers[i].text = item.name;
				}
				else
				{
					HUDPlayers.buttonPlayers[i].text = string.Concat(item.name, " [", item.nickname, "]");
				}
				((SleekImage)HUDPlayers.buttonPlayers[i].children[0]).setImage(Reputation.getIcon(item.reputation));
				if (item.status != 21)
				{
					HUDPlayers.buttonPlayers[i].color = Color.white;
					HUDPlayers.buttonPlayers[i].paint = Color.white;
				}
				else
				{
					HUDPlayers.buttonPlayers[i].color = Colors.GOLD;
					HUDPlayers.buttonPlayers[i].paint = Colors.GOLD;
				}
			}
			else if (i != 0)
			{
				HUDPlayers.buttonPlayers[i].visible = false;
			}
			else
			{
				HUDPlayers.buttonPlayers[i].text = "Sorry: No Players";
				HUDPlayers.buttonPlayers[i].visible = true;
			}
		}
	}

	public static void usedBan(SleekFrame frame)
	{
		if (HUDPlayers.selected != -1 && HUDPlayers.selected < NetworkUserList.users.Count)
		{
			NetworkTools.ban(NetworkUserList.users[HUDPlayers.selected].player, NetworkUserList.users[HUDPlayers.selected].name, NetworkUserList.users[HUDPlayers.selected].id, string.Concat("Host banning ", NetworkUserList.users[HUDPlayers.selected].name, "."));
			HUDPlayers.selected = -1;
			HUDPlayers.buttonKick.visible = false;
			HUDPlayers.buttonBan.visible = false;
			HUDPlayers.refreshPlayers();
		}
	}

	public static void usedBanned(SleekFrame frame) {
		// TODO: client side ban managing
		/*int offsetY = HUDPlayers.offset + (frame.position.offset_y + 140) / 50;
		if (offsetY < NetworkBans.bans.Count)
		{
			NetworkBans.unban(offsetY);
			HUDPlayers.refreshBans();
		}*/
	}

	public static void usedBansTab(SleekFrame frame)
	{
		HUDPlayers.containerPlayers.position = Coord2.ZERO;
		HUDPlayers.containerPlayers.lerp(new Coord2(0, 0, 1f, 0f), HUDPlayers.containerPlayers.size, 4f);
		HUDPlayers.containerBans.position = new Coord2(0, 0, 1f, 0f);
		HUDPlayers.containerBans.lerp(Coord2.ZERO, HUDPlayers.containerPlayers.size, 4f);
		HUDPlayers.refreshBans();
	}

	public static void usedKick(SleekFrame frame)
	{
		if (HUDPlayers.selected != -1 && HUDPlayers.selected < NetworkUserList.users.Count)
		{
			NetworkTools.kick(NetworkUserList.users[HUDPlayers.selected].player, string.Concat("Host kicking ", NetworkUserList.users[HUDPlayers.selected].name, "."));
			HUDPlayers.selected = -1;
			HUDPlayers.buttonKick.visible = false;
			HUDPlayers.buttonBan.visible = false;
			HUDPlayers.refreshPlayers();
		}
	}

	public static void usedMute(SleekFrame frame)
	{
		if (HUDPlayers.selected != -1 && HUDPlayers.selected < NetworkUserList.users.Count)
		{
			NetworkUserList.users[HUDPlayers.selected].toggleMute();
			HUDPlayers.buttonKick.visible = false;
			HUDPlayers.buttonBan.visible = false;
			HUDPlayers.buttonMute.text = string.Concat((!NetworkUserList.users[HUDPlayers.selected].muted ? "Mute " : "Unmute "), NetworkUserList.users[HUDPlayers.selected].name);
		}
	}

	public static void usedPlayer(SleekFrame frame)
	{
		int offsetY = HUDPlayers.offset + (frame.position.offset_y + 140) / 50;
		if (offsetY != HUDPlayers.selected)
		{
			HUDPlayers.selected = offsetY;
			if (HUDPlayers.selected >= NetworkUserList.users.Count)
			{
				HUDPlayers.selected = -1;
				HUDPlayers.buttonKick.visible = false;
				HUDPlayers.buttonBan.visible = false;
				HUDPlayers.buttonMute.visible = false;
			}
			else if (!Network.isServer || !(NetworkUserList.users[HUDPlayers.selected].player.ToString() != "0"))
			{
				HUDPlayers.buttonKick.visible = false;
				HUDPlayers.buttonBan.visible = false;
				HUDPlayers.buttonMute.visible = true;
				HUDPlayers.buttonMute.text = string.Concat((!NetworkUserList.users[HUDPlayers.selected].muted ? "Mute " : "Unmute "), NetworkUserList.users[HUDPlayers.selected].name);
			}
			else
			{
				HUDPlayers.buttonKick.visible = true;
				HUDPlayers.buttonBan.visible = true;
				HUDPlayers.buttonMute.visible = true;
				HUDPlayers.buttonKick.text = string.Concat(Texts.LABEL_KICK, " ", NetworkUserList.users[HUDPlayers.selected].name);
				HUDPlayers.buttonBan.text = string.Concat(Texts.LABEL_BAN, " ", NetworkUserList.users[HUDPlayers.selected].name);
				HUDPlayers.buttonMute.text = string.Concat((!NetworkUserList.users[HUDPlayers.selected].muted ? "Mute " : "Unmute "), NetworkUserList.users[HUDPlayers.selected].name);
			}
		}
		else
		{
			HUDPlayers.selected = -1;
			HUDPlayers.buttonKick.visible = false;
			HUDPlayers.buttonBan.visible = false;
			HUDPlayers.buttonMute.visible = false;
		}
	}

	public static void usedPlayersTab(SleekFrame frame)
	{
		HUDPlayers.containerPlayers.position = new Coord2(0, 0, 1f, 0f);
		HUDPlayers.containerPlayers.lerp(Coord2.ZERO, HUDPlayers.containerPlayers.size, 4f);
		HUDPlayers.containerBans.position = Coord2.ZERO;
		HUDPlayers.containerBans.lerp(new Coord2(0, 0, 1f, 0f), HUDPlayers.containerPlayers.size, 4f);
		HUDPlayers.refreshPlayers();
	}

	public static void usedSliderBans(SleekFrame frame)
	{
		HUDPlayers.refreshBans();
	}

	public static void usedSliderPlayers(SleekFrame frame)
	{
		HUDPlayers.refreshPlayers();
	}
}