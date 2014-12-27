using System;
using UnityEngine;

public class MenuConfigure
{
	public static SleekContainer container;

	public static SleekButton buttonKeys;

	public static SleekImage iconKeys;

	public static SleekButton buttonGraphics;

	public static SleekImage iconGraphics;

	public static SleekButton buttonSettings;

	public static SleekImage iconSettings;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public MenuConfigure()
	{
		MenuConfigure.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuConfigure.container);
		MenuConfigure.buttonKeys = new SleekButton()
		{
			position = new Coord2(10, -70, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_KEYS
		};
		MenuConfigure.buttonKeys.onUsed += new SleekDelegate(MenuConfigure.usedKeys);
		MenuConfigure.container.addFrame(MenuConfigure.buttonKeys);
		MenuConfigure.iconKeys = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConfigure.iconKeys.setImage("Textures/Icons/keys");
		MenuConfigure.buttonKeys.addFrame(MenuConfigure.iconKeys);
		MenuConfigure.buttonGraphics = new SleekButton()
		{
			position = new Coord2(10, -20, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_GRAPHICS
		};
		MenuConfigure.buttonGraphics.onUsed += new SleekDelegate(MenuConfigure.usedGraphics);
		MenuConfigure.container.addFrame(MenuConfigure.buttonGraphics);
		MenuConfigure.iconGraphics = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConfigure.iconGraphics.setImage("Textures/Icons/graphics");
		MenuConfigure.buttonGraphics.addFrame(MenuConfigure.iconGraphics);
		MenuConfigure.buttonSettings = new SleekButton()
		{
			position = new Coord2(10, 30, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_SETTINGS
		};
		MenuConfigure.buttonSettings.onUsed += new SleekDelegate(MenuConfigure.usedSettings);
		MenuConfigure.container.addFrame(MenuConfigure.buttonSettings);
		MenuConfigure.iconSettings = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConfigure.iconSettings.setImage("Textures/Icons/settings");
		MenuConfigure.buttonSettings.addFrame(MenuConfigure.iconSettings);
		MenuConfigure.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuConfigure.buttonBack.onUsed += new SleekDelegate(MenuConfigure.usedBack);
		MenuConfigure.container.addFrame(MenuConfigure.buttonBack);
		MenuConfigure.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConfigure.iconBack.setImage("Textures/Icons/back");
		MenuConfigure.buttonBack.addFrame(MenuConfigure.iconBack);
		MenuKeys menuKey = new MenuKeys();
		MenuSettings menuSetting = new MenuSettings();
		MenuGraphics menuGraphic = new MenuGraphics();
	}

	public static void close()
	{
		MenuConfigure.container.position = new Coord2(0, 0, -1f, 0f);
		MenuConfigure.container.lerp(new Coord2(0, 0, 1f, 0f), MenuConfigure.container.size, 4f);
	}

	public static void open()
	{
		MenuConfigure.container.position = new Coord2(0, 0, 1f, 0f);
		MenuConfigure.container.lerp(new Coord2(0, 0, -1f, 0f), MenuConfigure.container.size, 4f);
		MenuRegister.lerpTo = Camera.main.transform.parent.FindChild("viewConfigure");
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuConfigure.close();
		MenuTitle.open();
	}

	public static void usedGraphics(SleekFrame frame)
	{
		MenuConfigure.close();
		MenuGraphics.open();
	}

	public static void usedKeys(SleekFrame frame)
	{
		MenuConfigure.close();
		MenuKeys.open();
	}

	public static void usedSettings(SleekFrame frame)
	{
		MenuConfigure.close();
		MenuSettings.open();
	}
}