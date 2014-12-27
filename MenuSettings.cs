using System;
using UnityEngine;

public class MenuSettings
{
	public static SleekContainer container;

	public static SleekButton buttonMetric;

	public static SleekSlider sliderVolume;

	public static SleekLabel volumeHint;

	public static SleekSlider sliderFOV;

	public static SleekLabel fovHint;

	public static SleekSlider sliderSensitivity;

	public static SleekLabel sensitivityHint;

	public static SleekButton buttonMusic;

	public static SleekButton buttonInventory;

	public static SleekButton buttonProne;

	public static SleekButton buttonCrouch;

	public static SleekButton buttonAim;

	public static SleekButton buttonGore;

	public static SleekButton buttonFPS;

	public static SleekButton buttonVoice;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public MenuSettings()
	{
		MenuSettings.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		if (Application.loadedLevel == 0)
		{
			MenuTitle.container.addFrame(MenuSettings.container);
		}
		else
		{
			HUDPause.container.addFrame(MenuSettings.container);
		}
		MenuSettings.container.visible = false;
		MenuSettings.buttonFPS = new SleekButton()
		{
			position = new Coord2(10, -230, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GameSettings.fps ? Texts.LABEL_FPS_OFF : Texts.LABEL_FPS_ON)
		};
		MenuSettings.buttonFPS.onUsed += new SleekDelegate(MenuSettings.usedFPS);
		MenuSettings.buttonFPS.tooltip = (!GameSettings.fps ? Texts.TOOLTIP_FPS_OFF : Texts.TOOLTIP_FPS_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonFPS);
		MenuSettings.buttonMetric = new SleekButton()
		{
			position = new Coord2(10, -180, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GameSettings.metric ? Texts.LABEL_METRIC_OFF : Texts.LABEL_METRIC_ON)
		};
		MenuSettings.buttonMetric.onUsed += new SleekDelegate(MenuSettings.usedMetric);
		MenuSettings.buttonMetric.tooltip = (!GameSettings.metric ? Texts.TOOLTIP_METRIC_OFF : Texts.TOOLTIP_METRIC_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonMetric);
		MenuSettings.sliderFOV = new SleekSlider()
		{
			position = new Coord2(10, -130, 0f, 0.5f),
			size = new Coord2(200, 20, 0f, 0f),
			orientation = Orient2.HORIZONTAL
		};
		MenuSettings.sliderFOV.onUsed += new SleekDelegate(MenuSettings.usedFOV);
		MenuSettings.sliderFOV.setState((GameSettings.fov - 60f) / 40f);
		MenuSettings.sliderFOV.scale = 0.2f;
		MenuSettings.container.addFrame(MenuSettings.sliderFOV);
		MenuSettings.fovHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(new object[] { Texts.HINT_FOV, ": ", Mathf.FloorToInt(GameSettings.fov), "°" }),
			alignment = TextAnchor.MiddleLeft
		};
		MenuSettings.sliderFOV.addFrame(MenuSettings.fovHint);
		MenuSettings.sliderSensitivity = new SleekSlider()
		{
			position = new Coord2(10, -100, 0f, 0.5f),
			size = new Coord2(200, 20, 0f, 0f),
			orientation = Orient2.HORIZONTAL
		};
		MenuSettings.sliderSensitivity.onUsed += new SleekDelegate(MenuSettings.usedSensitivity);
		MenuSettings.sliderSensitivity.setState(InputSettings.mouseSensitivity / 10f);
		MenuSettings.sliderSensitivity.scale = 0.2f;
		MenuSettings.container.addFrame(MenuSettings.sliderSensitivity);
		MenuSettings.sensitivityHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.HINT_SENSITIVITY, ": ", Mathf.FloorToInt(InputSettings.mouseSensitivity)),
			alignment = TextAnchor.MiddleLeft
		};
		MenuSettings.sliderSensitivity.addFrame(MenuSettings.sensitivityHint);
		MenuSettings.sliderVolume = new SleekSlider()
		{
			position = new Coord2(10, -260, 0f, 0.5f),
			size = new Coord2(200, 20, 0f, 0f),
			orientation = Orient2.HORIZONTAL
		};
		MenuSettings.sliderVolume.onUsed += new SleekDelegate(MenuSettings.usedVolume);
		MenuSettings.sliderVolume.setState(GameSettings.volume);
		MenuSettings.sliderVolume.scale = 0.2f;
		MenuSettings.container.addFrame(MenuSettings.sliderVolume);
		MenuSettings.volumeHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(new object[] { Texts.HINT_VOLUME, ": ", Mathf.FloorToInt(GameSettings.volume * 100f), "%" }),
			alignment = TextAnchor.MiddleLeft
		};
		MenuSettings.sliderVolume.addFrame(MenuSettings.volumeHint);
		MenuSettings.buttonMusic = new SleekButton()
		{
			position = new Coord2(10, -70, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GameSettings.music ? Texts.LABEL_MUSIC_OFF : Texts.LABEL_MUSIC_ON)
		};
		MenuSettings.buttonMusic.onUsed += new SleekDelegate(MenuSettings.usedMusic);
		MenuSettings.buttonMusic.tooltip = (!GameSettings.music ? Texts.TOOLTIP_MUSIC_OFF : Texts.TOOLTIP_MUSIC_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonMusic);
		MenuSettings.buttonInventory = new SleekButton()
		{
			position = new Coord2(10, -20, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!InputSettings.inventoryToggle ? Texts.LABEL_INVENTORY_HOLD : Texts.LABEL_INVENTORY_TOGGLE)
		};
		MenuSettings.buttonInventory.onUsed += new SleekDelegate(MenuSettings.usedInventory);
		MenuSettings.buttonInventory.tooltip = (!InputSettings.inventoryToggle ? Texts.TOOLTIP_INVENTORYTOGGLE_OFF : Texts.TOOLTIP_INVENTORYTOGGLE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonInventory);
		MenuSettings.buttonProne = new SleekButton()
		{
			position = new Coord2(10, 30, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!InputSettings.proneToggle ? Texts.LABEL_PRONE_HOLD : Texts.LABEL_PRONE_TOGGLE)
		};
		MenuSettings.buttonProne.onUsed += new SleekDelegate(MenuSettings.usedProne);
		MenuSettings.buttonProne.tooltip = (!InputSettings.proneToggle ? Texts.TOOLTIP_PRONETOGGLE_OFF : Texts.TOOLTIP_PRONETOGGLE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonProne);
		MenuSettings.buttonCrouch = new SleekButton()
		{
			position = new Coord2(10, 80, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!InputSettings.crouchToggle ? Texts.LABEL_CROUCH_HOLD : Texts.LABEL_CROUCH_TOGGLE)
		};
		MenuSettings.buttonCrouch.onUsed += new SleekDelegate(MenuSettings.usedCrouch);
		MenuSettings.buttonCrouch.tooltip = (!InputSettings.crouchToggle ? Texts.TOOLTIP_CROUCHTOGGLE_OFF : Texts.TOOLTIP_CROUCHTOGGLE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonCrouch);
		MenuSettings.buttonAim = new SleekButton()
		{
			position = new Coord2(10, 130, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!InputSettings.aimToggle ? Texts.LABEL_AIM_HOLD : Texts.LABEL_AIM_TOGGLE)
		};
		MenuSettings.buttonAim.onUsed += new SleekDelegate(MenuSettings.usedAim);
		MenuSettings.buttonAim.tooltip = (!InputSettings.aimToggle ? Texts.TOOLTIP_AIMTOGGLE_OFF : Texts.TOOLTIP_AIMTOGGLE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonAim);
		MenuSettings.buttonGore = new SleekButton()
		{
			position = new Coord2(10, 180, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GameSettings.gore ? Texts.LABEL_GORE_OFF : Texts.LABEL_GORE_ON)
		};
		MenuSettings.buttonGore.onUsed += new SleekDelegate(MenuSettings.usedGore);
		MenuSettings.buttonGore.tooltip = (!GameSettings.gore ? Texts.TOOLTIP_GORE_OFF : Texts.TOOLTIP_GORE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonGore);
		MenuSettings.buttonVoice = new SleekButton()
		{
			position = new Coord2(10, 230, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!GameSettings.voice ? Texts.LABEL_VOICE_OFF : Texts.LABEL_VOICE_ON)
		};
		MenuSettings.buttonVoice.onUsed += new SleekDelegate(MenuSettings.usedVoice);
		MenuSettings.buttonVoice.tooltip = (!GameSettings.voice ? Texts.TOOLTIP_VOICE_OFF : Texts.TOOLTIP_VOICE_ON);
		MenuSettings.container.addFrame(MenuSettings.buttonVoice);
		MenuSettings.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuSettings.buttonBack.onUsed += new SleekDelegate(MenuSettings.usedBack);
		MenuSettings.container.addFrame(MenuSettings.buttonBack);
		MenuSettings.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuSettings.iconBack.setImage("Textures/Icons/back");
		MenuSettings.buttonBack.addFrame(MenuSettings.iconBack);
		Camera.main.fieldOfView = GameSettings.fov;
	}

	public static void close()
	{
		if (Application.loadedLevel == 0)
		{
			MenuSettings.container.position = new Coord2(0, 0, -1f, 0f);
			MenuSettings.container.lerp(new Coord2(0, 0, 1f, 0f), MenuSettings.container.size, 4f, true);
		}
		else
		{
			MenuSettings.container.visible = false;
		}
	}

	public static void open()
	{
		if (Application.loadedLevel == 0)
		{
			MenuSettings.container.visible = true;
			MenuSettings.container.position = new Coord2(0, 0, 1f, 0f);
			MenuSettings.container.lerp(new Coord2(0, 0, -1f, 0f), MenuSettings.container.size, 4f);
		}
		else
		{
			MenuSettings.container.visible = true;
		}
	}

	public static void usedAim(SleekFrame frame)
	{
		InputSettings.aimToggle = !InputSettings.aimToggle;
		MenuSettings.buttonAim.text = (!InputSettings.aimToggle ? Texts.LABEL_AIM_HOLD : Texts.LABEL_AIM_TOGGLE);
		MenuSettings.buttonAim.tooltip = (!InputSettings.aimToggle ? Texts.TOOLTIP_AIMTOGGLE_OFF : Texts.TOOLTIP_AIMTOGGLE_ON);
		InputSettings.save();
	}

	public static void usedBack(SleekFrame frame)
	{
		if (Application.loadedLevel == 0)
		{
			MenuSettings.close();
			MenuConfigure.open();
		}
		else
		{
			MenuSettings.close();
			HUDPause.closeMini();
		}
	}

	public static void usedCrouch(SleekFrame frame)
	{
		InputSettings.crouchToggle = !InputSettings.crouchToggle;
		MenuSettings.buttonCrouch.text = (!InputSettings.crouchToggle ? Texts.LABEL_CROUCH_HOLD : Texts.LABEL_CROUCH_TOGGLE);
		MenuSettings.buttonCrouch.tooltip = (!InputSettings.crouchToggle ? Texts.TOOLTIP_CROUCHTOGGLE_OFF : Texts.TOOLTIP_CROUCHTOGGLE_ON);
		InputSettings.save();
	}

	public static void usedFOV(SleekFrame frame)
	{
		GameSettings.fov = 60f + ((SleekSlider)frame).state * 40f;
		MenuSettings.fovHint.text = string.Concat(new object[] { Texts.HINT_FOV, ": ", Mathf.FloorToInt(GameSettings.fov), "°" });
		GameSettings.save();
		Camera.main.fieldOfView = GameSettings.fov;
	}

	public static void usedFPS(SleekFrame frame)
	{
		GameSettings.fps = !GameSettings.fps;
		MenuSettings.buttonFPS.text = (!GameSettings.fps ? Texts.LABEL_FPS_OFF : Texts.LABEL_FPS_ON);
		MenuSettings.buttonFPS.tooltip = (!GameSettings.fps ? Texts.TOOLTIP_FPS_OFF : Texts.TOOLTIP_FPS_ON);
		GameSettings.save();
	}

	public static void usedGore(SleekFrame frame)
	{
		GameSettings.gore = !GameSettings.gore;
		MenuSettings.buttonGore.text = (!GameSettings.gore ? Texts.LABEL_GORE_OFF : Texts.LABEL_GORE_ON);
		MenuSettings.buttonGore.tooltip = (!GameSettings.gore ? Texts.TOOLTIP_GORE_OFF : Texts.TOOLTIP_GORE_ON);
		GameSettings.save();
	}

	public static void usedInventory(SleekFrame frame)
	{
		InputSettings.inventoryToggle = !InputSettings.inventoryToggle;
		MenuSettings.buttonInventory.text = (!InputSettings.inventoryToggle ? Texts.LABEL_INVENTORY_HOLD : Texts.LABEL_INVENTORY_TOGGLE);
		MenuSettings.buttonInventory.tooltip = (!InputSettings.inventoryToggle ? Texts.TOOLTIP_INVENTORYTOGGLE_OFF : Texts.TOOLTIP_INVENTORYTOGGLE_ON);
		InputSettings.save();
	}

	public static void usedMetric(SleekFrame frame)
	{
		GameSettings.metric = !GameSettings.metric;
		MenuSettings.buttonMetric.text = (!GameSettings.metric ? Texts.LABEL_METRIC_OFF : Texts.LABEL_METRIC_ON);
		MenuSettings.buttonMetric.tooltip = (!GameSettings.metric ? Texts.TOOLTIP_METRIC_OFF : Texts.TOOLTIP_METRIC_ON);
		GameSettings.save();
	}

	public static void usedMusic(SleekFrame frame)
	{
		GameSettings.music = !GameSettings.music;
		MenuSettings.buttonMusic.text = (!GameSettings.music ? Texts.LABEL_MUSIC_OFF : Texts.LABEL_MUSIC_ON);
		MenuSettings.buttonMusic.tooltip = (!GameSettings.music ? Texts.TOOLTIP_MUSIC_OFF : Texts.TOOLTIP_MUSIC_ON);
		if (!GameSettings.music)
		{
			Camera.main.audio.Stop();
		}
		else
		{
			Camera.main.audio.Play();
		}
		GameSettings.save();
	}

	public static void usedProne(SleekFrame frame)
	{
		InputSettings.proneToggle = !InputSettings.proneToggle;
		MenuSettings.buttonProne.text = (!InputSettings.proneToggle ? Texts.LABEL_PRONE_HOLD : Texts.LABEL_PRONE_TOGGLE);
		MenuSettings.buttonProne.tooltip = (!InputSettings.proneToggle ? Texts.TOOLTIP_PRONETOGGLE_OFF : Texts.TOOLTIP_PRONETOGGLE_ON);
		InputSettings.save();
	}

	public static void usedSensitivity(SleekFrame frame)
	{
		InputSettings.mouseSensitivity = 1f + ((SleekSlider)frame).state * 9f;
		MenuSettings.sensitivityHint.text = string.Concat(Texts.HINT_SENSITIVITY, ": ", Mathf.FloorToInt(InputSettings.mouseSensitivity));
		InputSettings.save();
	}

	public static void usedVoice(SleekFrame frame)
	{
		GameSettings.voice = !GameSettings.voice;
		MenuSettings.buttonVoice.text = (!GameSettings.voice ? Texts.LABEL_VOICE_OFF : Texts.LABEL_VOICE_ON);
		MenuSettings.buttonVoice.tooltip = (!GameSettings.voice ? Texts.TOOLTIP_VOICE_OFF : Texts.TOOLTIP_VOICE_ON);
		GameSettings.save();
	}

	public static void usedVolume(SleekFrame frame)
	{
		GameSettings.volume = ((SleekSlider)frame).state;
		MenuSettings.volumeHint.text = string.Concat(new object[] { Texts.HINT_VOLUME, ": ", Mathf.FloorToInt(GameSettings.volume * 100f), "%" });
		GameSettings.save();
		AudioListener.volume = GameSettings.volume;
	}
}