using System;
using UnityEngine;

public class PlayerSettings
{
	public static string user;

	public static string friend;

	public static string nickname;

	public static string friendHash;

	public static string id;

	public static int status;

	public static int face;

	public static int hair;

	public static int skinColor;

	public static int hairColor;

	public static bool arm;

	static PlayerSettings()
	{
		PlayerSettings.user = string.Empty;
		PlayerSettings.friend = PlayerPrefs.GetString("playerSettings_Clan");
		PlayerSettings.nickname = PlayerPrefs.GetString("playerSettings_Nickname");
		PlayerSettings.friendHash = string.Empty;
		PlayerSettings.id = string.Empty;
		PlayerSettings.status = 0;
	}

	public PlayerSettings()
	{
	}

	public static void hash()
	{
		if (PlayerSettings.friend != string.Empty)
		{
			PlayerSettings.friendHash = MD5.hash(PlayerSettings.friend);
		}
		else
		{
			PlayerSettings.friendHash = string.Empty;
		}
	}

	public static void load()
	{
		PlayerSettings.face = PlayerPrefs.GetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Face"), 6001);
		PlayerSettings.hair = PlayerPrefs.GetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Hair"), 1000);
		PlayerSettings.skinColor = PlayerPrefs.GetInt(string.Concat("playerSettings_", PlayerSettings.id, "_SkinColor"), 1);
		PlayerSettings.hairColor = PlayerPrefs.GetInt(string.Concat("playerSettings_", PlayerSettings.id, "_HairColor"), 2);
		PlayerSettings.arm = PlayerPrefs.GetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Arm"), 0) == 1;
	}

	public static void save()
	{
		PlayerPrefs.SetString("playerSettings_Clan", PlayerSettings.friend);
		PlayerPrefs.SetString("playerSettings_Nickname", PlayerSettings.nickname);
		PlayerPrefs.SetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Face"), PlayerSettings.face);
		PlayerPrefs.SetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Hair"), PlayerSettings.hair);
		PlayerPrefs.SetInt(string.Concat("playerSettings_", PlayerSettings.id, "_SkinColor"), PlayerSettings.skinColor);
		PlayerPrefs.SetInt(string.Concat("playerSettings_", PlayerSettings.id, "_HairColor"), PlayerSettings.hairColor);
		PlayerPrefs.SetInt(string.Concat("playerSettings_", PlayerSettings.id, "_Arm"), (!PlayerSettings.arm ? 0 : 1));
	}
}