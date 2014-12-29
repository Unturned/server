using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuServers
{
	public static SleekContainer container;

	public static SleekContainer servers;

	public static SleekButton[] current;

	public static SleekButton buttonRefresh;

	public static SleekImage iconRefresh;

	public static SleekButton buttonSearch;

	public static SleekImage iconSearch;

	public static SleekField fieldName;

	public static SleekLabel hintName;

	public static SleekField fieldPassword;

	public static SleekLabel passwordHint;

	public static SleekButton buttonMode;

	public static SleekImage iconMode;

	public static SleekButton buttonHost;

	public static SleekImage iconHost;

	public static SleekButton buttonSave;

	public static SleekImage iconSave;

	public static SleekButton buttonPlayers;

	public static SleekImage iconPlayers;

	public static SleekButton buttonType;

	public static SleekImage iconType;

	public static SleekButton buttonMap;

	public static SleekButton buttonNopass;

	public static SleekImage iconNopass;

	public static SleekImage goldIcon;

	public static SleekSlider serverScroll;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static string password;

	public static string searchName;

	public static int searchMode;

	public static int searchHost;

	public static int searchSave;

	public static int searchPlayers;

	public static int searchType;

	public static int searchMap;

	public static bool nopass;

	public static string lastsv;

	public static int offset;

	static MenuServers()
	{
		MenuServers.password = PlayerPrefs.GetString("serversPass", string.Empty);
		MenuServers.searchName = PlayerPrefs.GetString("serversName", string.Empty);
		MenuServers.searchMode = PlayerPrefs.GetInt("serversMode", 0);
		MenuServers.searchHost = PlayerPrefs.GetInt("serversHost", 0);
		MenuServers.searchSave = PlayerPrefs.GetInt("serversSave", 0);
		MenuServers.searchPlayers = PlayerPrefs.GetInt("serversPlayers", 3);
		MenuServers.searchType = PlayerPrefs.GetInt("serversType", -1);
		MenuServers.searchMap = PlayerPrefs.GetInt("serversMap", 0);
		MenuServers.nopass = PlayerPrefs.GetInt("nopass", 1) == 1;
		MenuServers.lastsv = PlayerPrefs.GetString("lastsv");
	}

	public MenuServers()
	{
		MenuServers.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 2f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuServers.container);
		MenuServers.container.visible = false;
		MenuServers.servers = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuServers.container.addFrame(MenuServers.servers);
		MenuServers.buttonRefresh = new SleekButton()
		{
			position = new Coord2(10, -270, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_REFRESH
		};
		MenuServers.buttonRefresh.onUsed += new SleekDelegate(MenuServers.usedRefresh);
		MenuServers.container.addFrame(MenuServers.buttonRefresh);
		MenuServers.iconRefresh = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.iconRefresh.setImage("Textures/Icons/refresh");
		MenuServers.buttonRefresh.addFrame(MenuServers.iconRefresh);
		MenuServers.buttonSearch = new SleekButton()
		{
			position = new Coord2(10, -220, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_SEARCH
		};
		MenuServers.buttonSearch.onUsed += new SleekDelegate(MenuServers.usedSearch);
		MenuServers.container.addFrame(MenuServers.buttonSearch);
		MenuServers.iconSearch = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.iconSearch.setImage("Textures/Icons/search");
		MenuServers.buttonSearch.addFrame(MenuServers.iconSearch);
		MenuServers.fieldName = new SleekField()
		{
			position = new Coord2(10, -170, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuServers.fieldName.setText(MenuServers.searchName);
		MenuServers.fieldName.onUsed += new SleekDelegate(MenuServers.usedName);
		MenuServers.container.addFrame(MenuServers.fieldName);
		MenuServers.hintName = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_SEARCHNAME,
			alignment = TextAnchor.MiddleLeft
		};
		MenuServers.fieldName.addFrame(MenuServers.hintName);
		MenuServers.fieldPassword = new SleekField()
		{
			position = new Coord2(10, -120, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			replace = '#',
			text = MenuServers.password
		};
		MenuServers.fieldPassword.onUsed += new SleekDelegate(MenuServers.usedPassword);
		MenuServers.container.addFrame(MenuServers.fieldPassword);
		MenuServers.passwordHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PASS,
			alignment = TextAnchor.MiddleLeft
		};
		MenuServers.fieldPassword.addFrame(MenuServers.passwordHint);
		MenuServers.buttonMode = new SleekButton()
		{
			position = new Coord2(10, -70, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuServers.iconMode = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.buttonMode.addFrame(MenuServers.iconMode);
		if (MenuServers.searchMode == 0)
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_BOTH;
			MenuServers.iconMode.setImage(string.Empty);
		}
		else if (MenuServers.searchMode != 1)
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_PVE;
			MenuServers.iconMode.setImage("Textures/Icons/pve");
		}
		else
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_PVP;
			MenuServers.iconMode.setImage("Textures/Icons/pvp");
		}
		MenuServers.buttonMode.onUsed += new SleekDelegate(MenuServers.usedMode);
		MenuServers.container.addFrame(MenuServers.buttonMode);
		MenuServers.buttonHost = new SleekButton()
		{
			position = new Coord2(10, 80, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuServers.iconHost = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.buttonHost.addFrame(MenuServers.iconHost);
		if (MenuServers.searchHost == 0)
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_BOTH;
			MenuServers.iconHost.setImage(string.Empty);
		}
		else if (MenuServers.searchHost != 1)
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_NOTDEDICATED;
			MenuServers.iconHost.setImage("Textures/Icons/notDedicated");
		}
		else
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_DEDICATED;
			MenuServers.iconHost.setImage("Textures/Icons/dedicated");
		}
		MenuServers.buttonHost.onUsed += new SleekDelegate(MenuServers.usedHost);
		MenuServers.buttonSave = new SleekButton()
		{
			position = new Coord2(10, 80, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuServers.iconSave = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.buttonSave.addFrame(MenuServers.iconSave);
		if (MenuServers.searchSave == 0)
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_BOTH;
			MenuServers.iconSave.setImage(string.Empty);
		}
		else if (MenuServers.searchSave != 1)
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_GLOBAL;
			MenuServers.iconSave.setImage("Textures/Icons/saveGlobal");
		}
		else
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_LOCAL;
			MenuServers.iconSave.setImage("Textures/Icons/saveLocal");
		}
		MenuServers.buttonSave.onUsed += new SleekDelegate(MenuServers.usedSave);
		MenuServers.container.addFrame(MenuServers.buttonSave);
		MenuServers.buttonPlayers = new SleekButton()
		{
			position = new Coord2(10, 30, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuServers.iconPlayers = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.buttonPlayers.addFrame(MenuServers.iconPlayers);
		if (MenuServers.searchPlayers == 0)
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_SPACE;
			MenuServers.iconPlayers.setImage("Textures/Icons/sparePlayers");
		}
		else if (MenuServers.searchPlayers != 1)
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_ANY;
			MenuServers.iconPlayers.setImage(string.Empty);
		}
		else
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_NONE;
			MenuServers.iconPlayers.setImage("Textures/Icons/emptyPlayers");
		}
		MenuServers.buttonPlayers.onUsed += new SleekDelegate(MenuServers.usedPlayers);
		MenuServers.container.addFrame(MenuServers.buttonPlayers);
		MenuServers.buttonMap = new SleekButton()
		{
			position = new Coord2(10, 130, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		if (MenuServers.searchMap != 0)
		{
			MenuServers.buttonMap.text = Maps.getName(Maps.MAPS[MenuServers.searchMap - 1]);
		}
		else
		{
			MenuServers.buttonMap.text = "Any Map";
		}
		MenuServers.buttonMap.onUsed += new SleekDelegate(MenuServers.usedMap);
		MenuServers.container.addFrame(MenuServers.buttonMap);
		MenuServers.buttonNopass = new SleekButton()
		{
			position = new Coord2(10, 180, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!MenuServers.nopass ? Texts.LABEL_YESPASS : Texts.LABEL_NOPASS)
		};
		MenuServers.buttonNopass.onUsed += new SleekDelegate(MenuServers.usedNopass);
		MenuServers.container.addFrame(MenuServers.buttonNopass);
		MenuServers.iconNopass = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.iconNopass.setImage((!MenuServers.nopass ? "Textures/Icons/password" : "Textures/Icons/nopass"));
		MenuServers.buttonNopass.addFrame(MenuServers.iconNopass);
		MenuServers.buttonType = new SleekButton()
		{
			position = new Coord2(10, -20, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			color = (MenuServers.searchType != 3 ? Color.white : Colors.GOLD),
			paint = (MenuServers.searchType != 3 ? Color.white : Colors.GOLD)
		};
		MenuServers.iconType = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.buttonType.addFrame(MenuServers.iconType);
		if (MenuServers.searchType == -1)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_ALL;
			MenuServers.buttonType.tooltip = string.Empty;
			MenuServers.iconType.setImage(string.Empty);
		}
		else if (MenuServers.searchType == 0)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_NORMAL;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuServers.iconType.setImage("Textures/Icons/normal");
		}
		else if (MenuServers.searchType == 1)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_BAMBI;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuServers.iconType.setImage("Textures/Icons/bambi");
		}
		else if (MenuServers.searchType != 2)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuServers.buttonType.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuServers.iconType.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_HARDCORE;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuServers.iconType.setImage("Textures/Icons/hardcore");
		}
		MenuServers.buttonType.onUsed += new SleekDelegate(MenuServers.usedType);
		MenuServers.container.addFrame(MenuServers.buttonType);
		MenuServers.goldIcon = new SleekImage()
		{
			position = new Coord2(-16, -16, 0.5f, 0.5f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.goldIcon.setImage("Textures/Icons/locked");
		MenuServers.buttonType.addFrame(MenuServers.goldIcon);
		MenuServers.goldIcon.visible = (MenuServers.searchType != 3 ? false : PlayerSettings.status != 21);
		MenuServers.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuServers.buttonBack.onUsed += new SleekDelegate(MenuServers.usedBack);
		MenuServers.container.addFrame(MenuServers.buttonBack);
		MenuServers.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuServers.iconBack.setImage("Textures/Icons/back");
		MenuServers.buttonBack.addFrame(MenuServers.iconBack);
		MenuServers.serverScroll = new SleekSlider()
		{
			position = new Coord2(-30, 10, 1f, 0f),
			size = new Coord2(20, -20, 0f, 1f)
		};
		MenuServers.serverScroll.onUsed += new SleekDelegate(MenuServers.usedScroll);
		MenuServers.serverScroll.orientation = Orient2.VERTICAL;
		MenuServers.container.addFrame(MenuServers.serverScroll);
	}

	public static void close()
	{
		MenuServers.container.position = new Coord2(0, 0, -1f, 0f);
		MenuServers.container.lerp(new Coord2(0, 0, 1f, 0f), MenuServers.container.size, 4f, true);
	}

	public static void load()
	{
		MenuServers.servers.clearFrames();
		if ((int)NetworkTools.cleared.Length != 0)
		{
			MenuServers.current = new SleekButton[(Screen.height - 20) / 50];
			if ((int)NetworkTools.cleared.Length < (int)MenuServers.current.Length || (int)NetworkTools.cleared.Length == 0)
			{
				MenuServers.offset = 0;
				MenuServers.serverScroll.scale = 1f;
			}
			else
			{
				MenuServers.offset = (int)Mathf.Ceil((float)((int)NetworkTools.cleared.Length - (int)MenuServers.current.Length) * MenuServers.serverScroll.state);
				MenuServers.serverScroll.scale = 0.2f;
			}
			for (int i = 0; i < (int)MenuServers.current.Length; i++)
			{
				if (i < (int)NetworkTools.cleared.Length)
				{
					Server server = NetworkTools.cleared[i + MenuServers.offset];
					SleekButton sleekButton = new SleekButton()
					{
						position = new Coord2(-740, 10 + i * 50, 1f, 0f),
						size = new Coord2(700, 40, 0f, 0f),
						text = string.Concat(new object[] { server.name, " with ", server.players, "/", server.max, " players on ", Maps.getName(server.map) }),
						alignment = TextAnchor.MiddleLeft
					};
					sleekButton.onUsed += new SleekDelegate(MenuServers.usedServer);
					SleekImage sleekImage = new SleekImage()
					{
						position = new Coord2(-36, -16, 1f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					if (!server.dedicated)
					{
						sleekImage.setImage("Textures/Icons/notDedicated");
					}
					else
					{
						sleekImage.setImage("Textures/Icons/dedicated");
					}
					SleekImage sleekImage1 = new SleekImage()
					{
						position = new Coord2(-76, -16, 1f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					if (!server.pvp)
					{
						sleekImage1.setImage("Textures/Icons/pve");
					}
					else
					{
						sleekImage1.setImage("Textures/Icons/pvp");
					}
					sleekButton.addFrame(sleekImage1);
					SleekImage sleekImage2 = new SleekImage()
					{
						position = new Coord2(-116, -16, 1f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					if (server.mode == 0)
					{
						sleekImage2.setImage("Textures/Icons/normal");
					}
					else if (server.mode == 1)
					{
						sleekImage2.setImage("Textures/Icons/bambi");
					}
					else if (server.mode != 2)
					{
						sleekImage2.setImage("Textures/Icons/gold");
						sleekButton.color = Colors.GOLD;
						sleekButton.paint = Colors.GOLD;
					}
					else
					{
						sleekImage2.setImage("Textures/Icons/hardcore");
					}
					sleekButton.addFrame(sleekImage2);
					SleekImage sleekImage3 = new SleekImage()
					{
						position = new Coord2(-36, -16, 1f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					if (!server.save)
					{
						sleekImage3.setImage("Textures/Icons/saveLocal");
					}
					else
					{
						sleekImage3.setImage("Textures/Icons/saveGlobal");
					}
					sleekButton.addFrame(sleekImage3);
					if (server.ip == MenuServers.lastsv)
					{
						SleekImage sleekImage4 = new SleekImage()
						{
							position = new Coord2(-44, -16, 0f, 0.5f),
							size = new Coord2(32, 32, 0f, 0f)
						};
						sleekImage4.setImage("Textures/Icons/go");
						sleekButton.addFrame(sleekImage4);
					}
					if (server.passworded)
					{
						SleekImage sleekImage5 = new SleekImage()
						{
							position = new Coord2(94, -16, 0f, 0.5f),
							size = new Coord2(32, 32, 0f, 0f)
						};
						sleekImage5.setImage("Textures/Icons/password");
						sleekButton.addFrame(sleekImage5);
					}
					if (server.mode == 3 && PlayerSettings.status != 21)
					{
						SleekImage sleekImage6 = new SleekImage()
						{
							position = new Coord2(-16, -16, 0.5f, 0.5f),
							size = new Coord2(32, 32, 0f, 0f)
						};
						sleekImage6.setImage("Textures/Icons/locked");
						sleekButton.addFrame(sleekImage6);
						sleekButton.tooltip = Texts.LABEL_GOLD_REQUIRED;
					}
					MenuServers.servers.addFrame(sleekButton);
					MenuServers.current[i] = sleekButton;
				}
			}
		}
		else
		{
			MenuServers.offset = 0;
			MenuServers.serverScroll.scale = 1f;
			SleekButton sleekButton1 = new SleekButton()
			{
				position = new Coord2(-740, 10, 1f, 0f),
				size = new Coord2(700, 40, 0f, 0f),
				text = "Sorry: No servers found."
			};
			SleekImage sleekImage7 = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			sleekImage7.setImage("Textures/Icons/error");
			sleekButton1.addFrame(sleekImage7);
			MenuServers.servers.addFrame(sleekButton1);
		}
	}

	public static void open()
	{
		MenuServers.container.visible = true;
		MenuServers.container.position = new Coord2(0, 0, 1f, 0f);
		MenuServers.container.lerp(new Coord2(0, 0, -1f, 0f), MenuServers.container.size, 4f);
	}

	public static void search()
	{
		NetworkTools.search(MenuServers.searchName, MenuServers.searchMode, MenuServers.searchHost, MenuServers.searchSave, MenuServers.searchPlayers, false, MenuServers.searchType, (MenuServers.searchMap != 0 ? Maps.MAPS[MenuServers.searchMap - 1] : 0), MenuServers.nopass);
	}

	public static void updatePing(int index, int ping)
	{
		index = index - MenuServers.offset;
		if (index >= 0 && index < (int)MenuServers.current.Length)
		{
			if (ping > 300)
			{
				((SleekImage)MenuServers.current[index].children[3]).setImage("Textures/Icons/badConnection");
			}
			else if (ping <= 150)
			{
				((SleekImage)MenuServers.current[index].children[3]).setImage("Textures/Icons/goodConnection");
			}
			else
			{
				((SleekImage)MenuServers.current[index].children[3]).setImage("Textures/Icons/mediumConnection");
			}
			if (ping <= 2000)
			{
				((SleekLabel)MenuServers.current[index].children[3].children[0]).text = ping.ToString();
			}
			else
			{
				((SleekLabel)MenuServers.current[index].children[3].children[0]).text = "???";
			}
		}
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuServers.close();
		MenuPlay.open();
	}

	public static void usedHost(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchHost = MenuServers.searchHost - 1;
			if (MenuServers.searchHost < 0)
			{
				MenuServers.searchHost = 2;
			}
		}
		else
		{
			MenuServers.searchHost = MenuServers.searchHost + 1;
			if (MenuServers.searchHost > 2)
			{
				MenuServers.searchHost = 0;
			}
		}
		PlayerPrefs.SetInt("serversHost", MenuServers.searchHost);
		if (MenuServers.searchHost == 0)
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_BOTH;
			MenuServers.iconHost.setImage(string.Empty);
		}
		else if (MenuServers.searchHost != 1)
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_NOTDEDICATED;
			MenuServers.iconHost.setImage("Textures/Icons/notDedicated");
		}
		else
		{
			MenuServers.buttonHost.text = Texts.LABEL_HOST_DEDICATED;
			MenuServers.iconHost.setImage("Textures/Icons/dedicated");
		}
	}

	public static void usedMap(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchMap = MenuServers.searchMap - 1;
			if (MenuServers.searchMap < 0)
			{
				MenuServers.searchMap = (int)Maps.MAPS.Length;
			}
		}
		else
		{
			MenuServers.searchMap = MenuServers.searchMap + 1;
			if (MenuServers.searchMap > (int)Maps.MAPS.Length)
			{
				MenuServers.searchMap = 0;
			}
		}
		PlayerPrefs.SetInt("serversMap", MenuServers.searchMap);
		if (MenuServers.searchMap != 0)
		{
			MenuServers.buttonMap.text = Maps.getName(Maps.MAPS[MenuServers.searchMap - 1]);
		}
		else
		{
			MenuServers.buttonMap.text = "Any Map";
		}
	}

	public static void usedMode(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchMode = MenuServers.searchMode - 1;
			if (MenuServers.searchMode < 0)
			{
				MenuServers.searchMode = 2;
			}
		}
		else
		{
			MenuServers.searchMode = MenuServers.searchMode + 1;
			if (MenuServers.searchMode > 2)
			{
				MenuServers.searchMode = 0;
			}
		}
		PlayerPrefs.SetInt("serversMode", MenuServers.searchMode);
		if (MenuServers.searchMode == 0)
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_BOTH;
			MenuServers.iconMode.setImage(string.Empty);
		}
		else if (MenuServers.searchMode != 1)
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_PVE;
			MenuServers.iconMode.setImage("Textures/Icons/pve");
		}
		else
		{
			MenuServers.buttonMode.text = Texts.LABEL_MODE_PVP;
			MenuServers.iconMode.setImage("Textures/Icons/pvp");
		}
	}

	public static void usedName(SleekFrame frame)
	{
		MenuServers.searchName = ((SleekField)frame).text;
		PlayerPrefs.SetString("serversName", MenuServers.searchName);
	}

	public static void usedNopass(SleekFrame frame)
	{
		MenuServers.nopass = !MenuServers.nopass;
		PlayerPrefs.SetInt("nopass", (!MenuServers.nopass ? 0 : 1));
		MenuServers.buttonNopass.text = (!MenuServers.nopass ? Texts.LABEL_YESPASS : Texts.LABEL_NOPASS);
		MenuServers.iconNopass.setImage((!MenuServers.nopass ? "Textures/Icons/password" : "Textures/Icons/nopass"));
	}

	public static void usedPassword(SleekFrame frame)
	{
		MenuServers.password = ((SleekField)frame).text;
		PlayerPrefs.SetString("serversPass", MenuServers.password);
	}

	public static void usedPlayers(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchPlayers = MenuServers.searchPlayers - 1;
			if (MenuServers.searchPlayers < 0)
			{
				MenuServers.searchPlayers = 2;
			}
		}
		else
		{
			MenuServers.searchPlayers = MenuServers.searchPlayers + 1;
			if (MenuServers.searchPlayers > 2)
			{
				MenuServers.searchPlayers = 0;
			}
		}
		PlayerPrefs.SetInt("serversPlayers", MenuServers.searchPlayers);
		if (MenuServers.searchPlayers == 0)
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_SPACE;
			MenuServers.iconPlayers.setImage("Textures/Icons/sparePlayers");
		}
		else if (MenuServers.searchPlayers != 1)
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_ANY;
			MenuServers.iconPlayers.setImage(string.Empty);
		}
		else
		{
			MenuServers.buttonPlayers.text = Texts.LABEL_PLAYERS_NONE;
			MenuServers.iconPlayers.setImage("Textures/Icons/emptyPlayers");
		}
	}

	public static void usedRefresh(SleekFrame frame)
	{
		NetworkTools.refresh();
		MenuServers.load();
	}

	public static void usedSave(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchSave = MenuServers.searchSave - 1;
			if (MenuServers.searchSave < 0)
			{
				MenuServers.searchSave = 2;
			}
		}
		else
		{
			MenuServers.searchSave = MenuServers.searchSave + 1;
			if (MenuServers.searchSave > 2)
			{
				MenuServers.searchSave = 0;
			}
		}
		PlayerPrefs.SetInt("serversSave", MenuServers.searchSave);
		if (MenuServers.searchSave == 0)
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_BOTH;
			MenuServers.iconSave.setImage(string.Empty);
		}
		else if (MenuServers.searchSave != 1)
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_GLOBAL;
			MenuServers.iconSave.setImage("Textures/Icons/saveGlobal");
		}
		else
		{
			MenuServers.buttonSave.text = Texts.LABEL_SAVE_LOCAL;
			MenuServers.iconSave.setImage("Textures/Icons/saveLocal");
		}
	}

	public static void usedScroll(SleekFrame frame)
	{
		MenuServers.load();
	}

	public static void usedSearch(SleekFrame frame)
	{
		MenuServers.search();
		MenuServers.load();
	}

	public static void usedServer(SleekFrame frame)
	{
		int offsetY = (frame.position.offset_y - 10) / 50 + MenuServers.offset;
		if (NetworkTools.cleared[offsetY].players >= NetworkTools.cleared[offsetY].max)
		{
			MenuRegister.openError(Texts.ERROR_FULL, "Textures/Icons/fullPlayers");
		}
		else if (NetworkTools.cleared[offsetY].passworded && MenuServers.password == string.Empty)
		{
			MenuRegister.openError(Texts.ERROR_NEEDPASS, "Textures/Icons/password");
		}
		else if (NetworkTools.cleared[offsetY].mode != 3 || PlayerSettings.status == 21)
		{
			MenuServers.close();
			MenuRegister.openInfo(Texts.INFO_CONNECTING, "Textures/Icons/go");
			MenuServers.lastsv = NetworkTools.cleared[offsetY].ip;
			PlayerPrefs.SetString("lastsv", MenuServers.lastsv);
			NetworkTools.connectIP(NetworkTools.cleared[offsetY].ip, NetworkTools.cleared[offsetY].port, (!NetworkTools.cleared[offsetY].passworded ? string.Empty : MenuServers.password));
		}
		else
		{
			MenuRegister.openError(Texts.ERROR_GOLD, "Textures/Icons/gold");
		}
	}

	public static void usedType(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuServers.searchType = MenuServers.searchType - 1;
			if (MenuServers.searchType < -1)
			{
				MenuServers.searchType = 3;
			}
		}
		else
		{
			MenuServers.searchType = MenuServers.searchType + 1;
			if (MenuServers.searchType > 3)
			{
				MenuServers.searchType = -1;
			}
		}
		PlayerPrefs.SetInt("serversType", MenuServers.searchType);
		MenuServers.buttonType.color = (MenuServers.searchType != 3 ? Color.white : Colors.GOLD);
		MenuServers.buttonType.paint = (MenuServers.searchType != 3 ? Color.white : Colors.GOLD);
		if (MenuServers.searchType == -1)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_ALL;
			MenuServers.buttonType.tooltip = string.Empty;
			MenuServers.iconType.setImage(string.Empty);
		}
		else if (MenuServers.searchType == 0)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_NORMAL;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuServers.iconType.setImage("Textures/Icons/normal");
		}
		else if (MenuServers.searchType == 1)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_BAMBI;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuServers.iconType.setImage("Textures/Icons/bambi");
		}
		else if (MenuServers.searchType != 2)
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuServers.buttonType.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuServers.iconType.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuServers.buttonType.text = Texts.LABEL_TYPE_HARDCORE;
			MenuServers.buttonType.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuServers.iconType.setImage("Textures/Icons/hardcore");
		}
		MenuServers.goldIcon.visible = (MenuServers.searchType != 3 ? false : PlayerSettings.status != 21);
	}
}