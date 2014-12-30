using System;
using UnityEngine;

public class GameSettings
{
	public static bool metric;

	public static float fov;

	public static bool music;

	public static bool gore;

	public static bool fps;

	public static bool voice;

	public static float volume;

	static GameSettings()
	{
		GameSettings.metric = PlayerPrefs.GetInt("gameSettings_Metric", 1) == 1;
		GameSettings.fov = PlayerPrefs.GetFloat("gameSettings_FOV", 90f);
		GameSettings.music = PlayerPrefs.GetInt("gameSettings_Music", 1) == 1;
		GameSettings.gore = PlayerPrefs.GetInt("gameSettings_Gore", 1) == 1;
		GameSettings.fps = PlayerPrefs.GetInt("gameSettings_FPS", 0) == 1;
		GameSettings.voice = PlayerPrefs.GetInt("gameSettings_Voice", 0) == 1;
		GameSettings.volume = PlayerPrefs.GetFloat("gameSettings_Volume", 1f);
	}

	public GameSettings()
	{
	}

	public static void save()
	{
		PlayerPrefs.SetInt("gameSettings_Metric", (!GameSettings.metric ? 0 : 1));
		PlayerPrefs.SetFloat("gameSettings_FOV", GameSettings.fov);
		PlayerPrefs.SetInt("gameSettings_Music", (!GameSettings.music ? 0 : 1));
		PlayerPrefs.SetInt("gameSettings_Gore", (!GameSettings.gore ? 0 : 1));
		PlayerPrefs.SetInt("gameSettings_FPS", (!GameSettings.fps ? 0 : 1));
		PlayerPrefs.SetInt("gameSettings_Voice", (!GameSettings.voice ? 0 : 1));
		PlayerPrefs.SetFloat("gameSettings_Volume", GameSettings.volume);
	}
}