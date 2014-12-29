using System;
using UnityEngine;

public class GraphicsSettings
{
	public static bool fullscreen;

	public static float resolution;

	public static float distance;

	public static int shadows;

	public static bool ssao;

	public static bool bloom;

	public static bool blur;

	public static bool streaks;

	public static bool vsync;

	public static int foliage;

	public static int water;

	public static bool dof;

	public static bool hud;

	static GraphicsSettings()
	{
		GraphicsSettings.fullscreen = PlayerPrefs.GetInt("graphicsSettings_Fullscreen", 0) == 1;
		GraphicsSettings.resolution = PlayerPrefs.GetFloat("graphicsSettings_Resolution", 1f);
		GraphicsSettings.distance = PlayerPrefs.GetFloat("graphicsSettings_Distance", 0f);
		GraphicsSettings.shadows = PlayerPrefs.GetInt("graphicsSettings_Shadows", 0);
		GraphicsSettings.ssao = PlayerPrefs.GetInt("graphicsSettings_SSAO", 0) == 1;
		GraphicsSettings.bloom = PlayerPrefs.GetInt("graphicsSettings_Bloom", 0) == 1;
		GraphicsSettings.blur = PlayerPrefs.GetInt("graphicsSettings_Blur", 0) == 1;
		GraphicsSettings.streaks = PlayerPrefs.GetInt("graphicsSettings_Streaks", 0) == 1;
		GraphicsSettings.vsync = PlayerPrefs.GetInt("graphicsSettings_Vsync", 0) == 1;
		GraphicsSettings.foliage = PlayerPrefs.GetInt("graphicsSettings_Foliage", 0);
		GraphicsSettings.water = PlayerPrefs.GetInt("graphicsSettings_Water", 0);
		GraphicsSettings.dof = PlayerPrefs.GetInt("graphicsSettings_Dof", 0) == 1;
		GraphicsSettings.hud = true;
	}

	public GraphicsSettings()
	{
	}

	public static void apply()
	{
		int num = (int)((float)Screen.resolutions[(int)Screen.resolutions.Length - 1].width * GraphicsSettings.resolution);
		if (num < 800)
		{
			num = 800;
		}
		int num1 = (int)((float)Screen.resolutions[(int)Screen.resolutions.Length - 1].height * GraphicsSettings.resolution);
		if (num1 < 600)
		{
			num1 = 600;
		}
		Screen.SetResolution(num, num1, GraphicsSettings.fullscreen);
	}

	public static void dist()
	{
		float[] singleArray = new float[] { Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, 20f + GraphicsSettings.distance * 40f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 75f + GraphicsSettings.distance * 150f, 300f + GraphicsSettings.distance * 600f, 300f + GraphicsSettings.distance * 600f, 75f + GraphicsSettings.distance * 150f, 300f + GraphicsSettings.distance * 600f, 20f + GraphicsSettings.distance * 40f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, Single.MaxValue, 75f + GraphicsSettings.distance * 150f, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue, Single.MaxValue };
		Camera.main.layerCullDistances = singleArray;
		Camera.main.layerCullSpherical = true;
		if (Look.zoom != null)
		{
			Look.zoom.layerCullDistances = singleArray;
		}
	}

	public static void save()
	{
		PlayerPrefs.SetInt("graphicsSettings_Fullscreen", (!GraphicsSettings.fullscreen ? 0 : 1));
		PlayerPrefs.SetFloat("graphicsSettings_Resolution", GraphicsSettings.resolution);
		PlayerPrefs.SetFloat("graphicsSettings_Distance", GraphicsSettings.distance);
		PlayerPrefs.SetInt("graphicsSettings_Shadows", GraphicsSettings.shadows);
		PlayerPrefs.SetInt("graphicsSettings_SSAO", (!GraphicsSettings.ssao ? 0 : 1));
		PlayerPrefs.SetInt("graphicsSettings_Bloom", (!GraphicsSettings.bloom ? 0 : 1));
		PlayerPrefs.SetInt("graphicsSettings_Blur", (!GraphicsSettings.blur ? 0 : 1));
		PlayerPrefs.SetInt("graphicsSettings_Streaks", (!GraphicsSettings.streaks ? 0 : 1));
		PlayerPrefs.SetInt("graphicsSettings_Vsync", (!GraphicsSettings.vsync ? 0 : 1));
		PlayerPrefs.SetInt("graphicsSettings_Foliage", GraphicsSettings.foliage);
		PlayerPrefs.SetInt("graphicsSettings_Water", GraphicsSettings.water);
		PlayerPrefs.SetInt("graphicsSettings_Dof", (!GraphicsSettings.dof ? 0 : 1));
	}
}