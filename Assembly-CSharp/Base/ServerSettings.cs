using System;

public class ServerSettings
{
	public static int map;

	public static string version;

	public static bool pvp;

	public static int mode;

	public static bool dedicated;

	public static int time;

	public static int cycle;

	public static float offset;

	public static bool open;

	public static bool passworded;

	public static string name;

	static ServerSettings()
	{
		ServerSettings.map = 1;
		ServerSettings.version = Texts.VERSION_ID;
		ServerSettings.pvp = true;
		ServerSettings.mode = 0;
		ServerSettings.dedicated = false;
		ServerSettings.time = -1;
		ServerSettings.cycle = -1;
		ServerSettings.offset = 0f;
		ServerSettings.open = false;
		ServerSettings.passworded = false;
		ServerSettings.name = "Unturned Server";
	}

	public ServerSettings()
	{
	}
}