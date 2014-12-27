using System;
using UnityEngine;

public class MenuConnect
{
	public static SleekContainer container;

	public static SleekField fieldPassword;

	public static SleekField fieldIP;

	public static SleekField fieldPort;

	public static SleekLabel passwordHint;

	public static SleekLabel ipHint;

	public static SleekLabel portHint;

	public static SleekButton buttonConnect;

	public static SleekImage iconConnect;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static string password;

	public static string ip;

	public static int port;

	static MenuConnect()
	{
		MenuConnect.password = PlayerPrefs.GetString("connectPass", string.Empty);
		MenuConnect.ip = PlayerPrefs.GetString("connectIP", "localhost");
		MenuConnect.port = PlayerPrefs.GetInt("connectPort", 25444);
	}

	public MenuConnect()
	{
		MenuConnect.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 2f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuConnect.container);
		MenuConnect.container.visible = false;
		MenuConnect.fieldPassword = new SleekField()
		{
			position = new Coord2(10, -45, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			replace = '#',
			text = MenuConnect.password
		};
		MenuConnect.fieldPassword.onUsed += new SleekDelegate(MenuConnect.usedPassword);
		MenuConnect.container.addFrame(MenuConnect.fieldPassword);
		MenuConnect.passwordHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PASS,
			alignment = TextAnchor.MiddleLeft
		};
		MenuConnect.fieldPassword.addFrame(MenuConnect.passwordHint);
		MenuConnect.fieldIP = new SleekField()
		{
			position = new Coord2(10, 5, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = MenuConnect.ip
		};
		MenuConnect.fieldIP.onUsed += new SleekDelegate(MenuConnect.usedIP);
		MenuConnect.container.addFrame(MenuConnect.fieldIP);
		MenuConnect.ipHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_IP,
			alignment = TextAnchor.MiddleLeft
		};
		MenuConnect.fieldIP.addFrame(MenuConnect.ipHint);
		MenuConnect.fieldPort = new SleekField()
		{
			position = new Coord2(10, 55, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = MenuConnect.port.ToString()
		};
		MenuConnect.fieldPort.onUsed += new SleekDelegate(MenuConnect.usedPort);
		MenuConnect.container.addFrame(MenuConnect.fieldPort);
		MenuConnect.portHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_PORT,
			alignment = TextAnchor.MiddleLeft
		};
		MenuConnect.fieldPort.addFrame(MenuConnect.portHint);
		MenuConnect.buttonConnect = new SleekButton()
		{
			position = new Coord2(10, -95, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_CONNECTING
		};
		MenuConnect.buttonConnect.onUsed += new SleekDelegate(MenuConnect.usedConnect);
		MenuConnect.container.addFrame(MenuConnect.buttonConnect);
		MenuConnect.iconConnect = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConnect.iconConnect.setImage("Textures/Icons/go");
		MenuConnect.buttonConnect.addFrame(MenuConnect.iconConnect);
		MenuConnect.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuConnect.buttonBack.onUsed += new SleekDelegate(MenuConnect.usedBack);
		MenuConnect.container.addFrame(MenuConnect.buttonBack);
		MenuConnect.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuConnect.iconBack.setImage("Textures/Icons/back");
		MenuConnect.buttonBack.addFrame(MenuConnect.iconBack);
		NetworkEvents.onFailed += new NetworkErrorDelegate(this.onFailed);
	}

	public static void close()
	{
		MenuConnect.container.position = new Coord2(0, 0, -1f, 0f);
		MenuConnect.container.lerp(new Coord2(0, 0, 1f, 0f), MenuConnect.container.size, 4f, true);
	}

	public void onFailed(int id)
	{
		MenuPlay.open();
		if (id == 2)
		{
			MenuRegister.openError(Texts.ERROR_MAXP, "Textures/Icons/error");
		}
		else if (id == 1)
		{
			MenuRegister.openError(Texts.ERROR_PASSWORD, "Textures/Icons/error");
		}
		else if (id == 0)
		{
			MenuRegister.openError(Texts.ERROR_TIMED_OUT, "Textures/Icons/error");
		}
	}

	public static void open()
	{
		MenuConnect.container.visible = true;
		MenuConnect.container.position = new Coord2(0, 0, 1f, 0f);
		MenuConnect.container.lerp(new Coord2(0, 0, -1f, 0f), MenuConnect.container.size, 4f);
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuConnect.close();
		MenuPlay.open();
	}

	public static void usedConnect(SleekFrame frame)
	{
		MenuConnect.close();
		MenuRegister.openInfo(Texts.INFO_CONNECTING, "Textures/Icons/go");
		NetworkTools.connectIP(MenuConnect.ip, MenuConnect.port, MenuConnect.password);
	}

	public static void usedIP(SleekFrame frame)
	{
		MenuConnect.ip = ((SleekField)frame).text;
		PlayerPrefs.SetString("connectIP", MenuConnect.ip);
	}

	public static void usedPassword(SleekFrame frame)
	{
		MenuConnect.password = ((SleekField)frame).text;
		PlayerPrefs.SetString("connectPass", MenuConnect.password);
	}

	public static void usedPort(SleekFrame frame)
	{
		SleekField sleekField = (SleekField)frame;
		if (!int.TryParse(sleekField.text, out MenuConnect.port))
		{
			MenuConnect.port = 25444;
			if (sleekField.text != string.Empty)
			{
				sleekField.text = "25444";
			}
		}
		PlayerPrefs.SetInt("connectPort", MenuConnect.port);
	}
}