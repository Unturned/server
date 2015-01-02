using Steamworks;
using System;
using UnityEngine;

public class MenuTitle
{
	public static SleekContainer container;

	public static SleekField fieldID;

	public static SleekButton buttonTitle;

	public static SleekLabel labelTitle;

	public static SleekLabel labelNews;

	public static SleekLabel labelAuthor;

	public static SleekButton buttonGold;

	public static SleekLabel labelGold;

	public static SleekButton buttonPlay;

	public static SleekImage iconPlay;

	public static SleekButton buttonCharacter;

	public static SleekImage iconCharacter;

	public static SleekButton buttonConfigure;

	public static SleekImage iconConfigure;

	public static SleekButton buttonQuit;

	public static SleekImage iconQuit;

	public static SleekButton buttonWiki;

	public static SleekImage iconWiki;

	public static SleekButton buttonBugs;

	public static SleekImage iconBugs;

	public static SleekBox marquee_0;

	public MenuTitle()
	{
		MenuTitle.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuRegister.container.addFrame(MenuTitle.container);
		MenuTitle.buttonTitle = new SleekButton()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(-20, 100, 1f, 0f)
		};
		MenuTitle.buttonTitle.onUsed += new SleekDelegate(MenuTitle.usedTitle);
		MenuTitle.container.addFrame(MenuTitle.buttonTitle);
		MenuTitle.marquee_0 = new SleekBox()
		{
			position = new Coord2(0, 110, 0f, 0f),
			size = new Coord2(0, 40, 1f, 0f)
		};
		
        int num = 0;
        //SteamUserStats.GetStat("items", out num);
		int num1 = 0;
		//SteamUserStats.GetStat("resources", out num1);
		int num2 = 0;
		//SteamUserStats.GetStat("animalkills", out num2);
		int num3 = 0;
		//SteamUserStats.GetStat("zombiekills", out num3);
		int num4 = 0;
		//SteamUserStats.GetStat("playerkills", out num4);
		MenuTitle.marquee_0.text = string.Concat(new object[] { num1, " Resources Mined    ", num, " Items Taken    ", num2, " Animals Hunted    ", num3, " Zombies Killed    ", num4, " Players Murdered" });
		MenuTitle.buttonTitle.addFrame(MenuTitle.marquee_0);
		MenuTitle.labelTitle = new SleekLabel()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(300, -40, 0f, 1f),
			text = Texts.TITLE,
			fontSize = 48
		};
		MenuTitle.buttonTitle.addFrame(MenuTitle.labelTitle);
		MenuTitle.labelAuthor = new SleekLabel()
		{
			position = new Coord2(10, 0, 0f, 1f),
			size = new Coord2(-20, 20, 1f, 0f),
			text = Texts.AUTHOR,
			fontSize = 16
		};
		MenuTitle.labelTitle.addFrame(MenuTitle.labelAuthor);
		MenuTitle.labelNews = new SleekLabel()
		{
			position = new Coord2(320, 10, 0f, 0f),
			size = new Coord2(-330, -20, 1f, 1f),
			alignment = TextAnchor.UpperLeft
		};
		MenuTitle.buttonTitle.addFrame(MenuTitle.labelNews);
		if (PlayerSettings.status == 21)
		{
			MenuTitle.buttonTitle.color = Colors.GOLD;
			MenuTitle.labelTitle.paint = Colors.GOLD;
			SleekRender.swapPaid();
			Images.swapPaid();
		}
		if (PlayerSettings.status != 21)
		{
			MenuTitle.buttonGold = new SleekButton()
			{
				position = new Coord2(-200, -210, 0.5f, 1f),
				size = new Coord2(400, 200, 0f, 0f),
				color = Colors.GOLD,
				paint = Colors.GOLD,
				text = Texts.GOLD
			};
			MenuTitle.buttonGold.onUsed += new SleekDelegate(MenuTitle.usedGold);
			MenuTitle.buttonGold.alignment = TextAnchor.LowerCenter;
			MenuTitle.container.addFrame(MenuTitle.buttonGold);
			MenuTitle.labelGold = new SleekLabel()
			{
				position = new Coord2(10, 10, 0f, 0f),
				size = new Coord2(-20, 40, 1f, 0f),
				text = "$5.00 Permanent Gold Upgrade",
				fontSize = 24,
				color = Colors.GOLD,
				paint = Colors.GOLD
			};
			MenuTitle.buttonGold.addFrame(MenuTitle.labelGold);
		}
		MenuTitle.buttonPlay = new SleekButton()
		{
			position = new Coord2(10, -70, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_PLAY
		};
		MenuTitle.buttonPlay.onUsed += new SleekDelegate(MenuTitle.usedPlay);
		MenuTitle.container.addFrame(MenuTitle.buttonPlay);
		MenuTitle.iconPlay = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconPlay.setImage("Textures/Icons/go");
		MenuTitle.buttonPlay.addFrame(MenuTitle.iconPlay);
		MenuTitle.buttonCharacter = new SleekButton()
		{
			position = new Coord2(10, -20, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_CHARACTER
		};
		MenuTitle.buttonCharacter.onUsed += new SleekDelegate(MenuTitle.usedCharacter);
		MenuTitle.container.addFrame(MenuTitle.buttonCharacter);
		MenuTitle.iconCharacter = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconCharacter.setImage("Textures/Icons/character");
		MenuTitle.buttonCharacter.addFrame(MenuTitle.iconCharacter);
		MenuTitle.buttonConfigure = new SleekButton()
		{
			position = new Coord2(10, 30, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_CONFIGURE
		};
		MenuTitle.buttonConfigure.onUsed += new SleekDelegate(MenuTitle.usedConfigure);
		MenuTitle.container.addFrame(MenuTitle.buttonConfigure);
		MenuTitle.iconConfigure = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconConfigure.setImage("Textures/Icons/configure");
		MenuTitle.buttonConfigure.addFrame(MenuTitle.iconConfigure);
		MenuTitle.buttonQuit = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_QUIT
		};
		MenuTitle.buttonQuit.onUsed += new SleekDelegate(MenuTitle.usedQuit);
		MenuTitle.container.addFrame(MenuTitle.buttonQuit);
		MenuTitle.iconQuit = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconQuit.setImage("Textures/Icons/quit");
		MenuTitle.buttonQuit.addFrame(MenuTitle.iconQuit);
		MenuTitle.buttonWiki = new SleekButton()
		{
			position = new Coord2(-210, -50, 1f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_WIKI
		};
		MenuTitle.buttonWiki.onUsed += new SleekDelegate(MenuTitle.usedWiki);
		MenuTitle.container.addFrame(MenuTitle.buttonWiki);
		MenuTitle.iconWiki = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconWiki.setImage("Textures/Icons/wiki");
		MenuTitle.buttonWiki.addFrame(MenuTitle.iconWiki);
		MenuTitle.buttonBugs = new SleekButton()
		{
			position = new Coord2(-210, -100, 1f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BUGS
		};
		MenuTitle.buttonBugs.onUsed += new SleekDelegate(MenuTitle.usedBugs);
		MenuTitle.container.addFrame(MenuTitle.buttonBugs);
		MenuTitle.iconBugs = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuTitle.iconBugs.setImage("Textures/Icons/bugs");
		MenuTitle.buttonBugs.addFrame(MenuTitle.iconBugs);
		MenuPlay menuPlay = new MenuPlay();
		MenuCharacter menuCharacter = new MenuCharacter();
		MenuConfigure menuConfigure = new MenuConfigure();
		MenuTitle.open();
	}

	public static void close()
	{
		MenuTitle.container.position = new Coord2(0, 0, 1f, 0f);
		MenuTitle.container.lerp(new Coord2(0, 0, 2f, 0f), MenuTitle.container.size, 4f);
	}

	public static void open()
	{
		MenuTitle.container.position = new Coord2(0, 0, 2f, 0f);
		MenuTitle.container.lerp(new Coord2(0, 0, 1f, 0f), MenuTitle.container.size, 4f);
	}

	public static void usedBugs(SleekFrame frame)
	{
		//SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/app/304930/discussions/1/");
	}

	public static void usedCharacter(SleekFrame frame)
	{
		MenuTitle.close();
		MenuCharacter.open();
	}

	public static void usedConfigure(SleekFrame frame)
	{
		MenuTitle.close();
		MenuConfigure.open();
	}

	public static void usedGold(SleekFrame frame)
	{
		//SteamFriends.ActivateGameOverlayToStore(Steam.APP_ID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
	}

	public static void usedPlay(SleekFrame frame)
	{
		MenuTitle.close();
		MenuPlay.open();
	}

	public static void usedQuit(SleekFrame frame)
	{
		Application.Quit();
	}

	public static void usedTitle(SleekFrame frame)
	{
		//SteamFriends.ActivateGameOverlayToWebPage("http://steamcommunity.com/app/304930/announcements");
	}

	public static void usedWiki(SleekFrame frame)
	{
		//SteamFriends.ActivateGameOverlayToWebPage("http://unturned-bunker.wikia.com/wiki/");
	}
}