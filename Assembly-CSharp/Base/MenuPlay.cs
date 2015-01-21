using System;
using UnityEngine;

public class MenuPlay
{
	public static SleekContainer container;

	public static SleekButton buttonTutorial;

	public static SleekImage iconTutorial;

	public static SleekButton buttonSingleplayer;

	public static SleekImage iconSingleplayer;

	public static SleekButton buttonConnect;

	public static SleekImage iconConnect;

	public static SleekButton buttonServers;

	public static SleekImage iconServers;

	public static SleekButton buttonHost;

	public static SleekImage iconHost;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public MenuPlay()
	{
		MenuPlay.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuPlay.container);
		MenuPlay.buttonTutorial = new SleekButton()
		{
			position = new Coord2(10, -95, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_TUTORIAL
		};
		MenuPlay.buttonTutorial.onUsed += new SleekDelegate(MenuPlay.usedTutorial);
		MenuPlay.container.addFrame(MenuPlay.buttonTutorial);
		MenuPlay.iconTutorial = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuPlay.iconTutorial.setImage("Textures/Icons/wiki");
		MenuPlay.buttonTutorial.addFrame(MenuPlay.iconTutorial);
		MenuPlay.buttonSingleplayer = new SleekButton()
		{
			position = new Coord2(10, -45, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_SINGLEPLAYER
		};
		MenuPlay.buttonSingleplayer.onUsed += new SleekDelegate(MenuPlay.usedSingleplayer);
		MenuPlay.container.addFrame(MenuPlay.buttonSingleplayer);
		MenuPlay.iconSingleplayer = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuPlay.iconSingleplayer.setImage("Textures/Icons/emptyPlayers");
		MenuPlay.buttonSingleplayer.addFrame(MenuPlay.iconSingleplayer);
		MenuPlay.buttonConnect = new SleekButton()
		{
			position = new Coord2(10, 5, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_CONNECT
		};
		MenuPlay.buttonConnect.onUsed += new SleekDelegate(MenuPlay.usedConnect);
		MenuPlay.container.addFrame(MenuPlay.buttonConnect);
		MenuPlay.iconConnect = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuPlay.iconConnect.setImage("Textures/Icons/connect");
		MenuPlay.buttonConnect.addFrame(MenuPlay.iconConnect);
		MenuPlay.buttonHost = new SleekButton()
		{
			position = new Coord2(10, 55, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_HOST
		};
		MenuPlay.buttonHost.onUsed += new SleekDelegate(MenuPlay.usedHost);
		MenuPlay.container.addFrame(MenuPlay.buttonHost);
		MenuPlay.iconHost = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuPlay.iconHost.setImage("Textures/Icons/host");
		MenuPlay.buttonHost.addFrame(MenuPlay.iconHost);
		MenuPlay.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuPlay.buttonBack.onUsed += new SleekDelegate(MenuPlay.usedBack);
		MenuPlay.container.addFrame(MenuPlay.buttonBack);
		MenuPlay.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuPlay.iconBack.setImage("Textures/Icons/back");
		MenuPlay.buttonBack.addFrame(MenuPlay.iconBack);
		MenuSingleplayer menuSingleplayer = new MenuSingleplayer();
		MenuConnect menuConnect = new MenuConnect();
		MenuHost menuHost = new MenuHost();
	}

	public static void close()
	{
		MenuPlay.container.position = new Coord2(0, 0, -1f, 0f);
		MenuPlay.container.lerp(new Coord2(0, 0, 1f, 0f), MenuPlay.container.size, 4f);
	}

	public static void open()
	{
		MenuPlay.container.position = new Coord2(0, 0, 1f, 0f);
		MenuPlay.container.lerp(new Coord2(0, 0, -1f, 0f), MenuPlay.container.size, 4f);
		MenuRegister.lerpTo = Camera.main.transform.parent.FindChild("viewPlay");
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuPlay.close();
		MenuTitle.open();
	}

	public static void usedConnect(SleekFrame frame)
	{
		MenuPlay.close();
		MenuConnect.open();
	}

	public static void usedHost(SleekFrame frame)
	{
		MenuPlay.close();
		MenuHost.open();
	}

	public static void usedServers(SleekFrame frame)
	{
		MenuPlay.close();
		MenuServers.open();
	}

	public static void usedSingleplayer(SleekFrame frame)
	{
		MenuPlay.close();
		MenuSingleplayer.open();
	}

	public static void usedTutorial(SleekFrame frame)
	{
		MenuPlay.close();
		ServerSettings.map = 0;
		ServerSettings.pvp = false;
		ServerSettings.mode = 0;
		ServerSettings.dedicated = false;
		ServerSettings.open = false;
		NetworkTools.host(1, 25565, string.Empty);
	}
}