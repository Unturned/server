using System;
using UnityEngine;

public class MenuGraphics
{
	public static SleekContainer container;

	public static SleekButton buttonFullscreen;

	public static SleekSlider sliderResolution;

	public static SleekLabel resolutionHint;

	public static SleekSlider sliderDistance;

	public static SleekLabel distanceHint;

	public static SleekButton buttonShadows;

	public static SleekButton buttonSSAO;

	public static SleekButton buttonBloom;

	public static SleekButton buttonBlur;

	public static SleekButton buttonStreaks;

	public static SleekButton buttonVsync;

	public static SleekButton buttonFoliage;

	public static SleekButton buttonWater;

	public static SleekButton buttonDof;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static float lastRes;

	static MenuGraphics()
	{
		MenuGraphics.lastRes = Single.MaxValue;
	}

	public MenuGraphics()
	{
		MenuGraphics.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		if (Application.loadedLevel == 0)
		{
			MenuTitle.container.addFrame(MenuGraphics.container);
		}
		else
		{
			HUDPause.container.addFrame(MenuGraphics.container);
		}
		MenuGraphics.container.visible = false;
		MenuGraphics.buttonFullscreen = new SleekButton()
		{
			position = new Coord2(10, -280, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.fullscreen ? Texts.LABEL_FULLSCREEN_OFF : Texts.LABEL_FULLSCREEN_ON),
			tooltip = (!GraphicsSettings.fullscreen ? Texts.TOOLTIP_LAG_1 : Texts.TOOLTIP_LAG_0)
		};
		MenuGraphics.buttonFullscreen.onUsed += new SleekDelegate(MenuGraphics.usedFullscreen);
		MenuGraphics.container.addFrame(MenuGraphics.buttonFullscreen);
		MenuGraphics.sliderResolution = new SleekSlider()
		{
			position = new Coord2(10, -230, 0f, 0.5f),
			size = new Coord2(200, 10, 0f, 0f)
		};
		MenuGraphics.sliderResolution.setState(GraphicsSettings.resolution);
		MenuGraphics.sliderResolution.onUsed += new SleekDelegate(MenuGraphics.usedResolution);
		MenuGraphics.sliderResolution.scale = 0.2f;
		MenuGraphics.container.addFrame(MenuGraphics.sliderResolution);
		MenuGraphics.resolutionHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(new object[] { Texts.HINT_RESOLUTION, ": ", Mathf.FloorToInt(GraphicsSettings.resolution * 100f), "%" }),
			alignment = TextAnchor.MiddleLeft
		};
		MenuGraphics.sliderResolution.addFrame(MenuGraphics.resolutionHint);
		MenuGraphics.sliderDistance = new SleekSlider()
		{
			position = new Coord2(10, -200, 0f, 0.5f),
			size = new Coord2(200, 10, 0f, 0f)
		};
		MenuGraphics.sliderDistance.setState(GraphicsSettings.distance);
		MenuGraphics.sliderDistance.onUsed += new SleekDelegate(MenuGraphics.usedDistance);
		MenuGraphics.sliderDistance.scale = 0.2f;
		MenuGraphics.container.addFrame(MenuGraphics.sliderDistance);
		MenuGraphics.distanceHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(new object[] { Texts.HINT_DISTANCE, ": ", 50 + Mathf.FloorToInt(GraphicsSettings.distance * 100f), "%" }),
			alignment = TextAnchor.MiddleLeft
		};
		MenuGraphics.sliderDistance.addFrame(MenuGraphics.distanceHint);
		MenuGraphics.buttonShadows = new SleekButton()
		{
			position = new Coord2(10, -170, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		if (GraphicsSettings.shadows == 0)
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_LOW;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.shadows != 1)
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_HIGH;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_4;
		}
		else
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_MEDIUM;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_2;
		}
		MenuGraphics.buttonShadows.onUsed += new SleekDelegate(MenuGraphics.usedShadows);
		MenuGraphics.container.addFrame(MenuGraphics.buttonShadows);
		MenuGraphics.buttonSSAO = new SleekButton()
		{
			position = new Coord2(10, -120, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.ssao ? Texts.LABEL_SSAO_OFF : Texts.LABEL_SSAO_ON),
			tooltip = (!GraphicsSettings.ssao ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_4)
		};
		MenuGraphics.buttonSSAO.onUsed += new SleekDelegate(MenuGraphics.usedSSAO);
		MenuGraphics.container.addFrame(MenuGraphics.buttonSSAO);
		MenuGraphics.buttonBloom = new SleekButton()
		{
			position = new Coord2(10, -70, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.bloom ? Texts.LABEL_BLOOM_OFF : Texts.LABEL_BLOOM_ON),
			tooltip = (!GraphicsSettings.bloom ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3)
		};
		MenuGraphics.buttonBloom.onUsed += new SleekDelegate(MenuGraphics.usedBloom);
		MenuGraphics.container.addFrame(MenuGraphics.buttonBloom);
		MenuGraphics.buttonBlur = new SleekButton()
		{
			position = new Coord2(10, -20, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.blur ? Texts.LABEL_BLUR_OFF : Texts.LABEL_BLUR_ON),
			tooltip = (!GraphicsSettings.blur ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3)
		};
		MenuGraphics.buttonBlur.onUsed += new SleekDelegate(MenuGraphics.usedBlur);
		MenuGraphics.container.addFrame(MenuGraphics.buttonBlur);
		MenuGraphics.buttonStreaks = new SleekButton()
		{
			position = new Coord2(10, 30, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.streaks ? Texts.LABEL_STREAKS_OFF : Texts.LABEL_STREAKS_ON),
			tooltip = (!GraphicsSettings.streaks ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_2)
		};
		MenuGraphics.buttonStreaks.onUsed += new SleekDelegate(MenuGraphics.usedStreaks);
		MenuGraphics.container.addFrame(MenuGraphics.buttonStreaks);
		MenuGraphics.buttonVsync = new SleekButton()
		{
			position = new Coord2(10, 80, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.vsync ? Texts.LABEL_VSYNC_OFF : Texts.LABEL_VSYNC_ON),
			tooltip = (!GraphicsSettings.vsync ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3)
		};
		MenuGraphics.buttonVsync.onUsed += new SleekDelegate(MenuGraphics.usedVsync);
		MenuGraphics.container.addFrame(MenuGraphics.buttonVsync);
		MenuGraphics.buttonFoliage = new SleekButton()
		{
			position = new Coord2(10, 130, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		if (GraphicsSettings.foliage == 0)
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_LOW;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.foliage != 1)
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_HIGH;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_3;
		}
		else
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_MEDIUM;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_2;
		}
		MenuGraphics.buttonFoliage.onUsed += new SleekDelegate(MenuGraphics.usedFoliage);
		MenuGraphics.container.addFrame(MenuGraphics.buttonFoliage);
		MenuGraphics.buttonWater = new SleekButton()
		{
			position = new Coord2(10, 180, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		if (GraphicsSettings.water == 0)
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_LOW;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.water != 1)
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_HIGH;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_4;
		}
		else
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_MEDIUM;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_2;
		}
		MenuGraphics.buttonWater.onUsed += new SleekDelegate(MenuGraphics.usedWater);
		MenuGraphics.container.addFrame(MenuGraphics.buttonWater);
		MenuGraphics.buttonDof = new SleekButton()
		{
			position = new Coord2(10, 230, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GraphicsSettings.dof ? Texts.LABEL_DOF_OFF : Texts.LABEL_DOF_ON),
			tooltip = (!GraphicsSettings.dof ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3)
		};
		MenuGraphics.buttonDof.onUsed += new SleekDelegate(MenuGraphics.usedDof);
		MenuGraphics.container.addFrame(MenuGraphics.buttonDof);
		MenuGraphics.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuGraphics.buttonBack.onUsed += new SleekDelegate(MenuGraphics.usedBack);
		MenuGraphics.container.addFrame(MenuGraphics.buttonBack);
		MenuGraphics.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuGraphics.iconBack.setImage("Textures/Icons/back");
		MenuGraphics.buttonBack.addFrame(MenuGraphics.iconBack);
	}

	public static void close()
	{
		if (Application.loadedLevel == 0)
		{
			MenuGraphics.container.position = new Coord2(0, 0, -1f, 0f);
			MenuGraphics.container.lerp(new Coord2(0, 0, 1f, 0f), MenuGraphics.container.size, 4f, true);
		}
		else
		{
			MenuGraphics.container.visible = false;
		}
	}

	public static void load()
	{
		GraphicsSettings.apply();
		GraphicsSettings.dist();
		if (GraphicsSettings.shadows == 0)
		{
			QualitySettings.SetQualityLevel(0);
		}
		else if (GraphicsSettings.shadows != 1)
		{
			QualitySettings.SetQualityLevel(2);
		}
		else
		{
			QualitySettings.SetQualityLevel(1);
		}
		Camera.main.GetComponent<SSAOEffect>().enabled = GraphicsSettings.ssao;
		Camera.main.GetComponent<BloomAndLensFlares>().enabled = GraphicsSettings.bloom;
		Camera.main.GetComponent<AntialiasingAsPostEffect>().enabled = GraphicsSettings.blur;
		Camera.main.GetComponent<SunShafts>().enabled = GraphicsSettings.streaks;
		Camera.main.GetComponent<DepthOfField34>().enabled = GraphicsSettings.dof;
		//Camera.main.GetComponent<CameraMotionBlur>().enabled = GraphicsSettings.dof;
		QualitySettings.vSyncCount = (!GraphicsSettings.vsync ? 0 : 1);
		if (GameObject.Find("ground") != null)
		{
			Terrain component = GameObject.Find("ground").GetComponent<Terrain>();
			if (GraphicsSettings.foliage == 0)
			{
				component.detailObjectDensity = 0.1f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 50f;
			}
			else if (GraphicsSettings.foliage != 1)
			{
				component.detailObjectDensity = 1f;
				component.terrainData.wavingGrassAmount = 0.3f;
				component.detailObjectDistance = 250f;
			}
			else
			{
				component.detailObjectDensity = 0.5f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 100f;
			}
		}
		if (GameObject.Find("water") != null)
		{
			GameObject.Find("water").GetComponent<Ocean>().apply();
		}
	}

	public static void open()
	{
		if (Application.loadedLevel == 0)
		{
			MenuGraphics.container.visible = true;
			MenuGraphics.container.position = new Coord2(0, 0, 1f, 0f);
			MenuGraphics.container.lerp(new Coord2(0, 0, -1f, 0f), MenuGraphics.container.size, 4f);
		}
		else
		{
			MenuGraphics.container.visible = true;
		}
	}

	public static void usedBack(SleekFrame frame)
	{
		if (Application.loadedLevel == 0)
		{
			MenuGraphics.close();
			MenuConfigure.open();
		}
		else
		{
			MenuGraphics.close();
			HUDPause.closeMini();
		}
	}

	public static void usedBloom(SleekFrame frame)
	{
		GraphicsSettings.bloom = !GraphicsSettings.bloom;
		MenuGraphics.buttonBloom.text = (!GraphicsSettings.bloom ? Texts.LABEL_BLOOM_OFF : Texts.LABEL_BLOOM_ON);
		MenuGraphics.buttonBloom.tooltip = (!GraphicsSettings.bloom ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3);
		Camera.main.GetComponent<BloomAndLensFlares>().enabled = GraphicsSettings.bloom;
		GraphicsSettings.save();
	}

	public static void usedBlur(SleekFrame frame)
	{
		GraphicsSettings.blur = !GraphicsSettings.blur;
		MenuGraphics.buttonBlur.text = (!GraphicsSettings.blur ? Texts.LABEL_BLUR_OFF : Texts.LABEL_BLUR_ON);
		MenuGraphics.buttonBlur.tooltip = (!GraphicsSettings.blur ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3);
		Camera.main.GetComponent<AntialiasingAsPostEffect>().enabled = GraphicsSettings.blur;
		GraphicsSettings.save();
	}

	public static void usedDistance(SleekFrame frame)
	{
		GraphicsSettings.distance = ((SleekSlider)frame).state;
		MenuGraphics.distanceHint.text = string.Concat(new object[] { Texts.HINT_DISTANCE, ": ", 50 + Mathf.FloorToInt(GraphicsSettings.distance * 100f), "%" });
		GraphicsSettings.save();
		GraphicsSettings.dist();
	}

	public static void usedDof(SleekFrame frame)
	{
		GraphicsSettings.dof = !GraphicsSettings.dof;
		MenuGraphics.buttonDof.text = (!GraphicsSettings.dof ? Texts.LABEL_DOF_OFF : Texts.LABEL_DOF_ON);
		MenuGraphics.buttonDof.tooltip = (!GraphicsSettings.dof ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3);
		Camera.main.GetComponent<DepthOfField34>().enabled = GraphicsSettings.dof;
		//Camera.main.GetComponent<CameraMotionBlur>().enabled = GraphicsSettings.dof;
		GraphicsSettings.save();
	}

	public static void usedFoliage(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			GraphicsSettings.foliage = GraphicsSettings.foliage - 1;
			if (GraphicsSettings.foliage < 0)
			{
				GraphicsSettings.foliage = 2;
			}
		}
		else
		{
			GraphicsSettings.foliage = GraphicsSettings.foliage + 1;
			if (GraphicsSettings.foliage > 2)
			{
				GraphicsSettings.foliage = 0;
			}
		}
		if (GraphicsSettings.foliage == 0)
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_LOW;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.foliage != 1)
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_HIGH;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_3;
		}
		else
		{
			MenuGraphics.buttonFoliage.text = Texts.LABEL_FOLIAGE_MEDIUM;
			MenuGraphics.buttonFoliage.tooltip = Texts.TOOLTIP_LAG_2;
		}
		if (GameObject.Find("ground") != null)
		{
			Terrain component = GameObject.Find("ground").GetComponent<Terrain>();
			if (GraphicsSettings.foliage == 0)
			{
				component.detailObjectDensity = 0.1f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 50f;
			}
			else if (GraphicsSettings.foliage != 1)
			{
				component.detailObjectDensity = 1f;
				component.terrainData.wavingGrassAmount = 0.3f;
				component.detailObjectDistance = 250f;
			}
			else
			{
				component.detailObjectDensity = 0.5f;
				component.terrainData.wavingGrassAmount = 0f;
				component.detailObjectDistance = 100f;
			}
		}
		GraphicsSettings.save();
	}

	public static void usedFullscreen(SleekFrame frame)
	{
		GraphicsSettings.fullscreen = !GraphicsSettings.fullscreen;
		MenuGraphics.buttonFullscreen.text = (!GraphicsSettings.fullscreen ? Texts.LABEL_FULLSCREEN_OFF : Texts.LABEL_FULLSCREEN_ON);
		MenuGraphics.buttonFullscreen.tooltip = (!GraphicsSettings.fullscreen ? Texts.TOOLTIP_LAG_1 : Texts.TOOLTIP_LAG_0);
		GraphicsSettings.apply();
		GraphicsSettings.save();
	}

	public static void usedResolution(SleekFrame frame)
	{
		GraphicsSettings.resolution = ((SleekSlider)frame).state;
		MenuGraphics.resolutionHint.text = string.Concat(new object[] { Texts.HINT_RESOLUTION, ": ", Mathf.FloorToInt(GraphicsSettings.resolution * 100f), "%" });
		MenuGraphics.lastRes = Time.realtimeSinceStartup;
		GraphicsSettings.save();
	}

	public static void usedShadows(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			GraphicsSettings.shadows = GraphicsSettings.shadows - 1;
			if (GraphicsSettings.shadows < 0)
			{
				GraphicsSettings.shadows = 2;
			}
		}
		else
		{
			GraphicsSettings.shadows = GraphicsSettings.shadows + 1;
			if (GraphicsSettings.shadows > 2)
			{
				GraphicsSettings.shadows = 0;
			}
		}
		if (GraphicsSettings.shadows == 0)
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_LOW;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.shadows != 1)
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_HIGH;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_4;
		}
		else
		{
			MenuGraphics.buttonShadows.text = Texts.LABEL_SHADOWS_MEDIUM;
			MenuGraphics.buttonShadows.tooltip = Texts.TOOLTIP_LAG_2;
		}
		if (GraphicsSettings.shadows == 0)
		{
			QualitySettings.SetQualityLevel(0);
		}
		else if (GraphicsSettings.shadows != 1)
		{
			QualitySettings.SetQualityLevel(2);
		}
		else
		{
			QualitySettings.SetQualityLevel(1);
		}
		GraphicsSettings.save();
	}

	public static void usedSSAO(SleekFrame frame)
	{
		GraphicsSettings.ssao = !GraphicsSettings.ssao;
		MenuGraphics.buttonSSAO.text = (!GraphicsSettings.ssao ? Texts.LABEL_SSAO_OFF : Texts.LABEL_SSAO_ON);
		MenuGraphics.buttonSSAO.tooltip = (!GraphicsSettings.ssao ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_4);
		Camera.main.GetComponent<SSAOEffect>().enabled = GraphicsSettings.ssao;
		GraphicsSettings.save();
	}

	public static void usedStreaks(SleekFrame frame)
	{
		GraphicsSettings.streaks = !GraphicsSettings.streaks;
		MenuGraphics.buttonStreaks.text = (!GraphicsSettings.streaks ? Texts.LABEL_STREAKS_OFF : Texts.LABEL_STREAKS_ON);
		MenuGraphics.buttonStreaks.tooltip = (!GraphicsSettings.streaks ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_2);
		Camera.main.GetComponent<SunShafts>().enabled = GraphicsSettings.streaks;
		GraphicsSettings.save();
	}

	public static void usedVsync(SleekFrame frame)
	{
		GraphicsSettings.vsync = !GraphicsSettings.vsync;
		MenuGraphics.buttonVsync.text = (!GraphicsSettings.vsync ? Texts.LABEL_VSYNC_OFF : Texts.LABEL_VSYNC_ON);
		MenuGraphics.buttonVsync.tooltip = (!GraphicsSettings.vsync ? Texts.TOOLTIP_LAG_0 : Texts.TOOLTIP_LAG_3);
		QualitySettings.vSyncCount = (!GraphicsSettings.vsync ? 0 : 1);
		GraphicsSettings.save();
	}

	public static void usedWater(SleekFrame frame)
	{
		if (Event.current.button != 0)
		{
			GraphicsSettings.water = GraphicsSettings.water - 1;
			if (GraphicsSettings.water < 0)
			{
				GraphicsSettings.water = 2;
			}
		}
		else
		{
			GraphicsSettings.water = GraphicsSettings.water + 1;
			if (GraphicsSettings.water > 2)
			{
				GraphicsSettings.water = 0;
			}
		}
		if (GraphicsSettings.water == 0)
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_LOW;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_0;
		}
		else if (GraphicsSettings.water != 1)
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_HIGH;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_4;
		}
		else
		{
			MenuGraphics.buttonWater.text = Texts.LABEL_WATER_MEDIUM;
			MenuGraphics.buttonWater.tooltip = Texts.TOOLTIP_LAG_2;
		}
		if (GameObject.Find("water") != null)
		{
			GameObject.Find("water").GetComponent<Ocean>().apply();
		}
		GraphicsSettings.save();
	}
}