using System;
using UnityEngine;

public class MenuHost
{
	public static SleekContainer container;

	public static SleekBox mapLabel;

	public static SleekBox descriptionLabel;

	public static SleekBox mapBox;

	public static SleekImage mapIcon;

	public static SleekButton[] buttonMap;

	public static SleekButton buttonHost;

	public static SleekImage iconHost;

	public static SleekButton buttonPVP;

	public static SleekImage iconPVP;

	public static SleekButton buttonMode;

	public static SleekImage iconMode;

	public static SleekButton buttonDedicated;

	public static SleekImage iconDedicated;

	public static SleekButton buttonSave;

	public static SleekImage iconSave;

	public static SleekButton buttonPublic;

	public static SleekImage iconPublic;

	public static SleekField fieldPlayers;

	public static SleekLabel playersHint;

	public static SleekField fieldPort;

	public static SleekLabel portHint;

	public static SleekField fieldName;

	public static SleekLabel nameHint;

	public static SleekField fieldPassword;

	public static SleekLabel passwordHint;

	public static SleekImage goldIcon;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static string name;

	public static string password;

	public static int players;

	public static int port;

	public static int map;

	public static bool pvp;

	public static int mode;

	public static bool dedicated;

	public static bool save;

	public static bool opened;

	static MenuHost()
	{
		MenuHost.name = PlayerPrefs.GetString("multiplayerName", "Unturned Server");
		MenuHost.password = PlayerPrefs.GetString("multiplayerPass", string.Empty);
		MenuHost.players = PlayerPrefs.GetInt("multiplayerPlayers", 8);
		MenuHost.port = PlayerPrefs.GetInt("multiplayerPort", 25444);
		MenuHost.map = PlayerPrefs.GetInt("multiplayerMap", 1);
		MenuHost.pvp = PlayerPrefs.GetInt("multiplayerPvp", 1) == 1;
		MenuHost.mode = PlayerPrefs.GetInt("multiplayerMode", 0);
		MenuHost.dedicated = PlayerPrefs.GetInt("multiplayerDedicated", 0) == 1;
		MenuHost.save = PlayerPrefs.GetInt("multiplayerSave", 0) == 1;
		MenuHost.opened = PlayerPrefs.GetInt("multiplayerOpen", 0) == 1;
	}

	public MenuHost()
	{
		MenuHost.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 2f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuHost.container);
		MenuHost.container.visible = false;
		MenuHost.mapLabel = new SleekBox()
		{
			position = new Coord2(-205, -255, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuHost.container.addFrame(MenuHost.mapLabel);
		MenuHost.descriptionLabel = new SleekBox()
		{
			position = new Coord2(-205, -205, 0.5f, 0.5f),
			size = new Coord2(200, 150, 0f, 0f)
		};
		MenuHost.container.addFrame(MenuHost.descriptionLabel);
		MenuHost.mapBox = new SleekBox()
		{
			position = new Coord2(5, -255, 0.5f, 0.5f),
			size = new Coord2(340, 200, 0f, 0f)
		};
		MenuHost.container.addFrame(MenuHost.mapBox);
		MenuHost.mapIcon = new SleekImage()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(320, 180, 0f, 0f)
		};
		MenuHost.mapBox.addFrame(MenuHost.mapIcon);
		MenuHost.buttonMap = new SleekButton[(int)Maps.MAPS.Length];
		for (int i = 0; i < (int)Maps.MAPS.Length; i++)
		{
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(-205, -45 + i * 50, 0.5f, 0.5f),
				size = new Coord2(200, 40, 0f, 0f),
				text = Maps.getName(Maps.MAPS[i])
			};
			sleekButton.onUsed += new SleekDelegate(MenuHost.usedMap);
			MenuHost.container.addFrame(sleekButton);
		}
		MenuHost.buttonHost = new SleekButton()
		{
			position = new Coord2(5, -45, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			text = Texts.LABEL_HOSTING
		};
		MenuHost.buttonHost.onUsed += new SleekDelegate(MenuHost.usedHost);
		MenuHost.container.addFrame(MenuHost.buttonHost);
		MenuHost.iconHost = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.iconHost.setImage("Textures/Icons/go");
		MenuHost.buttonHost.addFrame(MenuHost.iconHost);
		MenuHost.buttonPVP = new SleekButton()
		{
			position = new Coord2(5, 5, 0.5f, 0.5f),
			size = new Coord2(165, 40, 0f, 0f),
			text = (!MenuHost.pvp ? Texts.LABEL_PVP_OFF : Texts.LABEL_PVP_ON),
			tooltip = (!MenuHost.pvp ? Texts.TOOLTIP_MODE_PVE : Texts.TOOLTIP_MODE_PVP)
		};
		MenuHost.buttonPVP.onUsed += new SleekDelegate(MenuHost.usedPVP);
		MenuHost.container.addFrame(MenuHost.buttonPVP);
		MenuHost.iconPVP = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.iconPVP.setImage((!MenuHost.pvp ? "Textures/Icons/pve" : "Textures/Icons/pvp"));
		MenuHost.buttonPVP.addFrame(MenuHost.iconPVP);
		MenuHost.buttonMode = new SleekButton()
		{
			position = new Coord2(180, 5, 0.5f, 0.5f),
			size = new Coord2(165, 40, 0f, 0f),
			color = (MenuHost.mode != 3 ? Color.white : Colors.GOLD),
			paint = (MenuHost.mode != 3 ? Color.white : Colors.GOLD)
		};
		MenuHost.iconMode = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.buttonMode.addFrame(MenuHost.iconMode);
		if (MenuHost.mode == 0)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_NORMAL;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuHost.iconMode.setImage("Textures/Icons/normal");
		}
		else if (MenuHost.mode == 1)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_BAMBI;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuHost.iconMode.setImage("Textures/Icons/bambi");
		}
		else if (MenuHost.mode != 2)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuHost.buttonMode.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuHost.iconMode.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_HARD;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuHost.iconMode.setImage("Textures/Icons/hardcore");
		}
		MenuHost.buttonMode.onUsed += new SleekDelegate(MenuHost.usedMode);
		MenuHost.container.addFrame(MenuHost.buttonMode);
		MenuHost.goldIcon = new SleekImage()
		{
			position = new Coord2(-16, -16, 0.5f, 0.5f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.goldIcon.setImage("Textures/Icons/locked");
		MenuHost.buttonMode.addFrame(MenuHost.goldIcon);
		MenuHost.goldIcon.visible = (MenuHost.mode != 3 ? false : PlayerSettings.status != 21);
		MenuHost.buttonDedicated = new SleekButton()
		{
			position = new Coord2(5, 55, 0.5f, 0.5f),
			size = new Coord2(165, 40, 0f, 0f),
			text = (!MenuHost.dedicated ? Texts.LABEL_DEDICATED_OFF : Texts.LABEL_DEDICATED_ON),
			tooltip = (!MenuHost.dedicated ? Texts.TOOLTIP_NOTDEDICATED : Texts.TOOLTIP_DEDICATED)
		};
		MenuHost.buttonDedicated.onUsed += new SleekDelegate(MenuHost.usedDedicated);
		MenuHost.iconDedicated = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.iconDedicated.setImage((!MenuHost.dedicated ? "Textures/Icons/notDedicated" : "Textures/Icons/dedicated"));
		MenuHost.buttonDedicated.addFrame(MenuHost.iconDedicated);
		MenuHost.buttonSave = new SleekButton()
		{
			position = new Coord2(5, 55, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			text = (!MenuHost.save ? Texts.LABEL_LOCALSAVE : Texts.LABEL_GLOBALSAVE),
			tooltip = (!MenuHost.save ? Texts.TOOLTIP_LOCALSAVE : Texts.TOOLTIP_GLOBALSAVE)
		};
		MenuHost.buttonSave.onUsed += new SleekDelegate(MenuHost.usedSave);
		MenuHost.container.addFrame(MenuHost.buttonSave);
		MenuHost.iconSave = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.iconSave.setImage((!MenuHost.save ? "Textures/Icons/saveLocal" : "Textures/Icons/saveGlobal"));
		MenuHost.buttonSave.addFrame(MenuHost.iconSave);
		MenuHost.fieldPlayers = new SleekField()
		{
			position = new Coord2(5, 155, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			text = MenuHost.players.ToString()
		};
		MenuHost.fieldPlayers.onUsed += new SleekDelegate(MenuHost.usedPlayers);
		MenuHost.container.addFrame(MenuHost.fieldPlayers);
		MenuHost.playersHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PLAYERS,
			alignment = TextAnchor.MiddleLeft
		};
		MenuHost.fieldPlayers.addFrame(MenuHost.playersHint);
		MenuHost.fieldPort = new SleekField()
		{
			position = new Coord2(5, 205, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			text = MenuHost.port.ToString()
		};
		MenuHost.fieldPort.onUsed += new SleekDelegate(MenuHost.usedPort);
		MenuHost.container.addFrame(MenuHost.fieldPort);
		MenuHost.portHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PORT,
			alignment = TextAnchor.MiddleLeft
		};
		MenuHost.fieldPort.addFrame(MenuHost.portHint);
		MenuHost.fieldPassword = new SleekField()
		{
			position = new Coord2(5, 105, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			replace = '#',
			text = MenuHost.password
		};
		MenuHost.fieldPassword.onUsed += new SleekDelegate(MenuHost.usedPassword);
		MenuHost.container.addFrame(MenuHost.fieldPassword);
		MenuHost.passwordHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PASS,
			alignment = TextAnchor.MiddleLeft
		};
		MenuHost.fieldPassword.addFrame(MenuHost.passwordHint);
		MenuHost.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuHost.buttonBack.onUsed += new SleekDelegate(MenuHost.usedBack);
		MenuHost.container.addFrame(MenuHost.buttonBack);
		MenuHost.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuHost.iconBack.setImage("Textures/Icons/back");
		MenuHost.buttonBack.addFrame(MenuHost.iconBack);
		MenuHost.updateInfo();
	}

	public static void close()
	{
		MenuHost.container.position = new Coord2(0, 0, -1f, 0f);
		MenuHost.container.lerp(new Coord2(0, 0, 1f, 0f), MenuHost.container.size, 4f, true);
	}

	public static void open()
	{
		MenuHost.container.visible = true;
		MenuHost.container.position = new Coord2(0, 0, 1f, 0f);
		MenuHost.container.lerp(new Coord2(0, 0, -1f, 0f), MenuHost.container.size, 4f);
		MenuHost.updateInfo();
	}

	public static void updateInfo()
	{
		MenuHost.mapLabel.text = string.Concat(Texts.LABEL_MAP, Maps.getName(MenuHost.map));
		MenuHost.descriptionLabel.text = Maps.getDescription(MenuHost.map);
		MenuHost.mapIcon.setImage(string.Concat("Textures/Maps/", Maps.getFile(MenuHost.map)));
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuHost.close();
		MenuPlay.open();
	}

	public static void usedDedicated(SleekFrame frame)
	{
		MenuHost.dedicated = !MenuHost.dedicated;
		PlayerPrefs.SetInt("multiplayerDedicated", (!MenuHost.dedicated ? 0 : 1));
		MenuHost.buttonDedicated.text = (!MenuHost.dedicated ? Texts.LABEL_DEDICATED_OFF : Texts.LABEL_DEDICATED_ON);
		MenuHost.buttonDedicated.tooltip = (!MenuHost.dedicated ? Texts.TOOLTIP_NOTDEDICATED : Texts.TOOLTIP_DEDICATED);
		MenuHost.iconDedicated.setImage((!MenuHost.dedicated ? "Textures/Icons/notDedicated" : "Textures/Icons/dedicated"));
	}

	public static void usedHost(SleekFrame frame)
	{
		if ((MenuHost.mode != 3 || PlayerSettings.status == 21) && MenuHost.name != string.Empty)
		{
			MenuHost.close();
			ServerSettings.name = MenuHost.name;
			ServerSettings.map = MenuHost.map;
			ServerSettings.pvp = MenuHost.pvp;
			ServerSettings.mode = MenuHost.mode;
			ServerSettings.dedicated = MenuHost.dedicated;
			ServerSettings.save = MenuHost.save;
			ServerSettings.open = MenuHost.opened;
			NetworkTools.host(MenuHost.players, MenuHost.port, MenuHost.password);
		}
	}

	public static void usedMap(SleekFrame frame)
	{
		MenuHost.map = Maps.MAPS[(frame.position.offset_y + 45) / 50];
		PlayerPrefs.SetInt("multiplayerMap", MenuHost.map);
		MenuHost.updateInfo();
	}

	public static void usedMode(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuHost.mode = MenuHost.mode - 1;
			if (MenuHost.mode < 0)
			{
				MenuHost.mode = 3;
			}
		}
		else
		{
			MenuHost.mode = MenuHost.mode + 1;
			if (MenuHost.mode > 3)
			{
				MenuHost.mode = 0;
			}
		}
		PlayerPrefs.SetInt("multiplayerMode", MenuHost.mode);
		MenuHost.buttonMode.color = (MenuHost.mode != 3 ? Color.white : Colors.GOLD);
		MenuHost.buttonMode.paint = (MenuHost.mode != 3 ? Color.white : Colors.GOLD);
		if (MenuHost.mode == 0)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_NORMAL;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuHost.iconMode.setImage("Textures/Icons/normal");
		}
		else if (MenuHost.mode == 1)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_BAMBI;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuHost.iconMode.setImage("Textures/Icons/bambi");
		}
		else if (MenuHost.mode != 2)
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuHost.buttonMode.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuHost.iconMode.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuHost.buttonMode.text = Texts.LABEL_MODE_HARD;
			MenuHost.buttonMode.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuHost.iconMode.setImage("Textures/Icons/hardcore");
		}
		MenuHost.goldIcon.visible = (MenuHost.mode != 3 ? false : PlayerSettings.status != 21);
	}

	public static void usedName(SleekFrame frame)
	{
		MenuHost.name = ((SleekField)frame).text;
		PlayerPrefs.SetString("multiplayerName", MenuHost.name);
	}

	public static void usedPassword(SleekFrame frame)
	{
		MenuHost.password = ((SleekField)frame).text;
		PlayerPrefs.SetString("multiplayerPass", MenuHost.password);
	}

	public static void usedPlayers(SleekFrame frame)
	{
		SleekField str = (SleekField)frame;
		if (str.text == string.Empty)
		{
			MenuHost.players = 8;
		}
		else if (!int.TryParse(str.text, out MenuHost.players))
		{
			MenuHost.players = 8;
			str.text = MenuHost.players.ToString();
		}
		else if (MenuHost.players > 12)
		{
			MenuHost.players = 12;
			str.text = MenuHost.players.ToString();
		}
		else if (MenuHost.players < 1)
		{
			MenuHost.players = 1;
			str.text = MenuHost.players.ToString();
		}
		PlayerPrefs.SetInt("multiplayerPlayers", MenuHost.players);
	}

	public static void usedPort(SleekFrame frame)
	{
		SleekField sleekField = (SleekField)frame;
		if (!int.TryParse(sleekField.text, out MenuHost.port))
		{
			MenuHost.port = 25444;
			if (sleekField.text != string.Empty)
			{
				sleekField.text = "25444";
			}
		}
		PlayerPrefs.SetInt("multiplayerPort", MenuHost.port);
	}

	public static void usedPublic(SleekFrame frame)
	{
		MenuHost.opened = !MenuHost.opened;
		PlayerPrefs.SetInt("multiplayerOpen", (!MenuHost.opened ? 0 : 1));
		MenuHost.buttonPublic.text = (!MenuHost.opened ? Texts.LABEL_PUBLIC_OFF : Texts.LABEL_PUBLIC_ON);
		MenuHost.buttonPublic.tooltip = (!MenuHost.opened ? Texts.TOOLTIP_PRIVATE : Texts.TOOLTIP_PUBLIC);
		MenuHost.iconPublic.setImage((!MenuHost.opened ? "Textures/Icons/private" : "Textures/Icons/public"));
	}

	public static void usedPVP(SleekFrame frame)
	{
		MenuHost.pvp = !MenuHost.pvp;
		PlayerPrefs.SetInt("multiplayerPvp", (!MenuHost.pvp ? 0 : 1));
		MenuHost.buttonPVP.text = (!MenuHost.pvp ? Texts.LABEL_PVP_OFF : Texts.LABEL_PVP_ON);
		MenuHost.buttonPVP.tooltip = (!MenuHost.pvp ? Texts.TOOLTIP_MODE_PVE : Texts.TOOLTIP_MODE_PVP);
		MenuHost.iconPVP.setImage((!MenuHost.pvp ? "Textures/Icons/pve" : "Textures/Icons/pvp"));
	}

	public static void usedSave(SleekFrame frame)
	{
		MenuHost.save = !MenuHost.save;
		PlayerPrefs.SetInt("multiplayerSave", (!MenuHost.save ? 0 : 1));
		MenuHost.buttonSave.text = (!MenuHost.save ? Texts.LABEL_LOCALSAVE : Texts.LABEL_GLOBALSAVE);
		MenuHost.buttonSave.tooltip = (!MenuHost.save ? Texts.TOOLTIP_LOCALSAVE : Texts.TOOLTIP_GLOBALSAVE);
		MenuHost.iconSave.setImage((!MenuHost.save ? "Textures/Icons/saveLocal" : "Textures/Icons/saveGlobal"));
	}
}