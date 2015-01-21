using System;
using UnityEngine;

public class MenuSingleplayer
{
	public static SleekContainer container;

	public static SleekBox mapLabel;

	public static SleekBox descriptionLabel;

	public static SleekBox mapBox;

	public static SleekImage mapIcon;

	public static SleekButton[] buttonMap;

	public static SleekButton buttonHost;

	public static SleekImage iconHost;

	public static SleekButton buttonMode;

	public static SleekImage iconMode;

	public static SleekImage goldIcon;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static int map;

	public static int mode;

	static MenuSingleplayer()
	{
		MenuSingleplayer.map = PlayerPrefs.GetInt("singleplayerMap", 1);
		MenuSingleplayer.mode = PlayerPrefs.GetInt("singleplayerMode", 0);
	}

	public MenuSingleplayer()
	{
		MenuSingleplayer.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 2f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuSingleplayer.container);
		MenuSingleplayer.container.visible = false;
		MenuSingleplayer.mapLabel = new SleekBox()
		{
			position = new Coord2(-205, -255, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuSingleplayer.container.addFrame(MenuSingleplayer.mapLabel);
		MenuSingleplayer.descriptionLabel = new SleekBox()
		{
			position = new Coord2(-205, -205, 0.5f, 0.5f),
			size = new Coord2(200, 150, 0f, 0f)
		};
		MenuSingleplayer.container.addFrame(MenuSingleplayer.descriptionLabel);
		MenuSingleplayer.mapBox = new SleekBox()
		{
			position = new Coord2(5, -255, 0.5f, 0.5f),
			size = new Coord2(340, 200, 0f, 0f)
		};
		MenuSingleplayer.container.addFrame(MenuSingleplayer.mapBox);
		MenuSingleplayer.mapIcon = new SleekImage()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(320, 180, 0f, 0f)
		};
		MenuSingleplayer.mapBox.addFrame(MenuSingleplayer.mapIcon);
		MenuSingleplayer.buttonMap = new SleekButton[(int)Maps.MAPS.Length];
		for (int i = 0; i < (int)Maps.MAPS.Length; i++)
		{
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(-205, -45 + i * 50, 0.5f, 0.5f),
				size = new Coord2(200, 40, 0f, 0f),
				text = Maps.getName(Maps.MAPS[i])
			};
			sleekButton.onUsed += new SleekDelegate(MenuSingleplayer.usedMap);
			MenuSingleplayer.container.addFrame(sleekButton);
		}
		MenuSingleplayer.buttonHost = new SleekButton()
		{
			position = new Coord2(5, -45, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			text = Texts.LABEL_PLAYSINGLE
		};
		MenuSingleplayer.buttonHost.onUsed += new SleekDelegate(MenuSingleplayer.usedHost);
		MenuSingleplayer.container.addFrame(MenuSingleplayer.buttonHost);
		MenuSingleplayer.iconHost = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuSingleplayer.iconHost.setImage("Textures/Icons/go");
		MenuSingleplayer.buttonHost.addFrame(MenuSingleplayer.iconHost);
		MenuSingleplayer.buttonMode = new SleekButton()
		{
			position = new Coord2(5, 5, 0.5f, 0.5f),
			size = new Coord2(340, 40, 0f, 0f),
			color = (MenuSingleplayer.mode != 3 ? Color.white : Colors.GOLD),
			paint = (MenuSingleplayer.mode != 3 ? Color.white : Colors.GOLD)
		};
		MenuSingleplayer.iconMode = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuSingleplayer.buttonMode.addFrame(MenuSingleplayer.iconMode);
		if (MenuSingleplayer.mode == 0)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_NORMAL;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/normal");
		}
		else if (MenuSingleplayer.mode == 1)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_BAMBI;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/bambi");
		}
		else if (MenuSingleplayer.mode != 2)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuSingleplayer.buttonMode.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuSingleplayer.iconMode.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_HARD;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/hardcore");
		}
		MenuSingleplayer.buttonMode.onUsed += new SleekDelegate(MenuSingleplayer.usedMode);
		MenuSingleplayer.container.addFrame(MenuSingleplayer.buttonMode);
		MenuSingleplayer.goldIcon = new SleekImage()
		{
			position = new Coord2(-16, -16, 0.5f, 0.5f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuSingleplayer.goldIcon.setImage("Textures/Icons/locked");
		MenuSingleplayer.buttonMode.addFrame(MenuSingleplayer.goldIcon);
		MenuSingleplayer.goldIcon.visible = (MenuSingleplayer.mode != 3 ? false : PlayerSettings.status != 21);
		MenuSingleplayer.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuSingleplayer.buttonBack.onUsed += new SleekDelegate(MenuSingleplayer.usedBack);
		MenuSingleplayer.container.addFrame(MenuSingleplayer.buttonBack);
		MenuSingleplayer.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuSingleplayer.iconBack.setImage("Textures/Icons/back");
		MenuSingleplayer.buttonBack.addFrame(MenuSingleplayer.iconBack);
		MenuSingleplayer.updateInfo();
	}

	public static void close()
	{
		MenuSingleplayer.container.position = new Coord2(0, 0, -1f, 0f);
		MenuSingleplayer.container.lerp(new Coord2(0, 0, 1f, 0f), MenuSingleplayer.container.size, 4f, true);
	}

	public static void open()
	{
		MenuSingleplayer.container.visible = true;
		MenuSingleplayer.container.position = new Coord2(0, 0, 1f, 0f);
		MenuSingleplayer.container.lerp(new Coord2(0, 0, -1f, 0f), MenuSingleplayer.container.size, 4f);
	}

	public static void updateInfo()
	{
		MenuSingleplayer.mapLabel.text = string.Concat(Texts.LABEL_MAP, Maps.getName(MenuSingleplayer.map));
		MenuSingleplayer.descriptionLabel.text = Maps.getDescription(MenuSingleplayer.map);
		MenuSingleplayer.mapIcon.setImage(string.Concat("Textures/Maps/", Maps.getFile(MenuSingleplayer.map)));
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuSingleplayer.close();
		MenuPlay.open();
	}

	public static void usedHost(SleekFrame frame)
	{
		if (MenuSingleplayer.mode != 3 || PlayerSettings.status == 21)
		{
			MenuSingleplayer.close();
			ServerSettings.name = "singleplayer";
			ServerSettings.map = MenuSingleplayer.map;
			ServerSettings.pvp = true;
			ServerSettings.mode = MenuSingleplayer.mode;
			ServerSettings.dedicated = false;
			ServerSettings.open = false;
			NetworkTools.host(1, 25565, string.Empty);
		}
	}

	public static void usedMap(SleekFrame frame)
	{
		MenuSingleplayer.map = Maps.MAPS[(frame.position.offset_y + 45) / 50];
		PlayerPrefs.SetInt("singleplayerMap", MenuSingleplayer.map);
		MenuSingleplayer.updateInfo();
	}

	public static void usedMode(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			MenuSingleplayer.mode = MenuSingleplayer.mode - 1;
			if (MenuSingleplayer.mode < 0)
			{
				MenuSingleplayer.mode = 3;
			}
		}
		else
		{
			MenuSingleplayer.mode = MenuSingleplayer.mode + 1;
			if (MenuSingleplayer.mode > 3)
			{
				MenuSingleplayer.mode = 0;
			}
		}
		PlayerPrefs.SetInt("singleplayerMode", MenuSingleplayer.mode);
		MenuSingleplayer.buttonMode.color = (MenuSingleplayer.mode != 3 ? Color.white : Colors.GOLD);
		MenuSingleplayer.buttonMode.paint = (MenuSingleplayer.mode != 3 ? Color.white : Colors.GOLD);
		if (MenuSingleplayer.mode == 0)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_NORMAL;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_NORMAL;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/normal");
		}
		else if (MenuSingleplayer.mode == 1)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_BAMBI;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_BAMBI;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/bambi");
		}
		else if (MenuSingleplayer.mode != 2)
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_GOLD;
			if (PlayerSettings.status == 21)
			{
				MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_GOLD;
			}
			else
			{
				MenuSingleplayer.buttonMode.tooltip = Texts.LABEL_GOLD_REQUIRED;
			}
			MenuSingleplayer.iconMode.setImage("Textures/Icons/gold");
		}
		else
		{
			MenuSingleplayer.buttonMode.text = Texts.LABEL_MODE_HARD;
			MenuSingleplayer.buttonMode.tooltip = Texts.TOOLTIP_MODE_HARDCORE;
			MenuSingleplayer.iconMode.setImage("Textures/Icons/hardcore");
		}
		MenuSingleplayer.goldIcon.visible = (MenuSingleplayer.mode != 3 ? false : PlayerSettings.status != 21);
	}
}