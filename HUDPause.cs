using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDPause
{
	public static SleekContainer container;

	public static bool state;

	public static SleekButton buttonKeys;

	public static SleekImage iconKeys;

	public static SleekButton buttonGraphics;

	public static SleekImage iconGraphics;

	public static SleekButton buttonSettings;

	public static SleekImage iconSettings;

	public static SleekButton leaveButton;

	public static SleekImage iconLeave;

	public static SleekButton resetButton;

	public static SleekImage iconReset;

	public static SleekButton noButton;

	public static SleekImage iconNo;

	public static SleekButton yesButton;

	public static SleekImage iconYes;

	public static float timer;

	public HUDPause()
	{
		HUDPause.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 1f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.window.addFrame(HUDPause.container);
		HUDPause.container.visible = false;
		HUDPause.buttonKeys = new SleekButton()
		{
			position = new Coord2(-200, -120, 0.5f, 0.5f),
			size = new Coord2(400, 40, 0f, 0f),
			text = Texts.LABEL_KEYS
		};
		HUDPause.buttonKeys.onUsed += new SleekDelegate(HUDPause.usedKeys);
		HUDPause.container.addFrame(HUDPause.buttonKeys);
		HUDPause.iconKeys = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDPause.iconKeys.setImage("Textures/Icons/keys");
		HUDPause.buttonKeys.addFrame(HUDPause.iconKeys);
		HUDPause.buttonGraphics = new SleekButton()
		{
			position = new Coord2(-200, -70, 0.5f, 0.5f),
			size = new Coord2(400, 40, 0f, 0f),
			text = Texts.LABEL_GRAPHICS
		};
		HUDPause.buttonGraphics.onUsed += new SleekDelegate(HUDPause.usedGraphics);
		HUDPause.container.addFrame(HUDPause.buttonGraphics);
		HUDPause.iconGraphics = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDPause.iconGraphics.setImage("Textures/Icons/graphics");
		HUDPause.buttonGraphics.addFrame(HUDPause.iconGraphics);
		HUDPause.buttonSettings = new SleekButton()
		{
			position = new Coord2(-200, -20, 0.5f, 0.5f),
			size = new Coord2(400, 40, 0f, 0f),
			text = Texts.LABEL_SETTINGS
		};
		HUDPause.buttonSettings.onUsed += new SleekDelegate(HUDPause.usedSettings);
		HUDPause.container.addFrame(HUDPause.buttonSettings);
		HUDPause.iconSettings = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDPause.iconSettings.setImage("Textures/Icons/settings");
		HUDPause.buttonSettings.addFrame(HUDPause.iconSettings);
		HUDPause.leaveButton = new SleekButton()
		{
			position = new Coord2(-200, 30, 0.5f, 0.5f),
			size = new Coord2(400, 40, 0f, 0f)
		};
		HUDPause.leaveButton.onUsed += new SleekDelegate(HUDPause.usedLeave);
		HUDPause.leaveButton.text = Texts.LABEL_LEAVE;
		HUDPause.container.addFrame(HUDPause.leaveButton);
		HUDPause.iconLeave = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDPause.iconLeave.setImage("Textures/Icons/back");
		HUDPause.leaveButton.addFrame(HUDPause.iconLeave);
		if (ServerSettings.mode != 2)
		{
			HUDPause.resetButton = new SleekButton()
			{
				position = new Coord2(-200, 80, 0.5f, 0.5f),
				size = new Coord2(400, 40, 0f, 0f)
			};
			HUDPause.resetButton.onUsed += new SleekDelegate(HUDPause.usedReset);
			HUDPause.resetButton.text = Texts.LABEL_RESET;
			HUDPause.container.addFrame(HUDPause.resetButton);
			HUDPause.iconReset = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDPause.iconReset.setImage("Textures/Icons/refresh");
			HUDPause.resetButton.addFrame(HUDPause.iconReset);
			HUDPause.noButton = new SleekButton()
			{
				position = new Coord2(-200, 80, 0.5f, 0.5f),
				size = new Coord2(195, 40, 0f, 0f)
			};
			HUDPause.noButton.onUsed += new SleekDelegate(HUDPause.usedNo);
			HUDPause.noButton.text = Texts.LABEL_NO;
			HUDPause.container.addFrame(HUDPause.noButton);
			HUDPause.container.visible = false;
			HUDPause.iconNo = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDPause.iconNo.setImage("Textures/Icons/quit");
			HUDPause.noButton.addFrame(HUDPause.iconNo);
			HUDPause.yesButton = new SleekButton()
			{
				position = new Coord2(5, 80, 0.5f, 0.5f),
				size = new Coord2(195, 40, 0f, 0f)
			};
			HUDPause.yesButton.onUsed += new SleekDelegate(HUDPause.usedYes);
			HUDPause.yesButton.text = Texts.LABEL_YES;
			HUDPause.container.addFrame(HUDPause.yesButton);
			HUDPause.container.visible = false;
			HUDPause.iconYes = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDPause.iconYes.setImage("Textures/Icons/yes");
			HUDPause.yesButton.addFrame(HUDPause.iconYes);
		}
		MenuKeys menuKey = new MenuKeys();
		MenuGraphics menuGraphic = new MenuGraphics();
		MenuSettings menuSetting = new MenuSettings();
		HUDPause.state = false;
	}

	public static void close()
	{
		HUDPause.closeMini();
		HUDPause.state = false;
		HUDPause.container.position = Coord2.ZERO;
		HUDPause.container.lerp(new Coord2(0, 0, 0f, 1f), HUDPause.container.size, 4f, true);
		HUDGame.container.visible = true;
	}

	public static void closeMini()
	{
		MenuKeys.container.visible = false;
		MenuSettings.container.visible = false;
		MenuGraphics.container.visible = false;
		MenuKeys.binding = -1;
		HUDPause.container.position = new Coord2(0, 0, -1f, 0f);
		HUDPause.container.lerp(Coord2.ZERO, HUDPause.container.size, 4f);
	}

	public static void open()
	{
		HUDPause.state = true;
		HUDPause.container.position = new Coord2(0, 0, 0f, 1f);
		HUDPause.container.lerp(Coord2.ZERO, HUDPause.container.size, 4f);
		HUDPause.container.visible = true;
		HUDPause.resetButton.visible = true;
		HUDPause.noButton.visible = false;
		HUDPause.yesButton.visible = false;
		HUDPause.timer = Time.realtimeSinceStartup;
		HUDGame.container.visible = false;
	}

	public static void openMini()
	{
		HUDPause.container.position = Coord2.ZERO;
		HUDPause.container.lerp(new Coord2(0, 0, -1f, 0f), HUDPause.container.size, 4f);
	}

	public static void usedGraphics(SleekFrame frame)
	{
		HUDPause.openMini();
		MenuGraphics.open();
	}

	public static void usedKeys(SleekFrame frame)
	{
		HUDPause.openMini();
		MenuKeys.open();
	}

	public static void usedLeave(SleekFrame frame)
	{
		if (Time.realtimeSinceStartup - HUDPause.timer > 10f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
		{
			HUDPause.close();
			NetworkTools.disconnect();
		}
	}

	public static void usedNo(SleekFrame frame)
	{
		HUDPause.resetButton.visible = true;
		HUDPause.noButton.visible = false;
		HUDPause.yesButton.visible = false;
	}

	public static void usedReset(SleekFrame frame)
	{
		if (Time.realtimeSinceStartup - HUDPause.timer > 5f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
		{
			HUDPause.resetButton.visible = false;
			HUDPause.noButton.visible = true;
			HUDPause.yesButton.visible = true;
		}
	}

	public static void usedSettings(SleekFrame frame)
	{
		HUDPause.openMini();
		MenuSettings.open();
	}

	public static void usedYes(SleekFrame frame)
	{
		HUDPause.close();
		SpawnPlayers.reset();
	}
}