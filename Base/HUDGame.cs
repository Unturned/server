using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDGame : MonoBehaviour
{
	public static SleekWindow window;

	public static SleekContainer container;

	public static SleekBox healthBox;

	public static SleekBox foodBox;

	public static SleekBox waterBox;

	public static SleekBox sicknessBox;

	public static SleekBox staminaBox;

	public static SleekBox bleedingBox;

	public static SleekBox bonesBox;

	public static SleekImage healthBar;

	public static SleekImage foodBar;

	public static SleekImage waterBar;

	public static SleekImage sicknessBar;

	public static SleekImage staminaBar;

	public static SleekLabel healthLabel;

	public static SleekLabel foodLabel;

	public static SleekLabel waterLabel;

	public static SleekLabel sicknessLabel;

	public static SleekLabel staminaLabel;

	public static SleekImage healthIcon;

	public static SleekImage foodIcon;

	public static SleekImage waterIcon;

	public static SleekImage sicknessIcon;

	public static SleekImage staminaIcon;

	public static SleekImage bleedingIcon;

	public static SleekImage bonesIcon;

	public static SleekBox respawnInfo;

	public static SleekButton bedButton;

	public static SleekButton respawnButton;

	public static SleekImage flash;

	public static SleekLabel labelChat_0;

	public static SleekImage iconChat_0;

	public static SleekImage typeChat_0;

	public static SleekLabel labelChat_1;

	public static SleekImage iconChat_1;

	public static SleekImage typeChat_1;

	public static SleekLabel labelChat_2;

	public static SleekImage iconChat_2;

	public static SleekImage typeChat_2;

	public static SleekLabel labelChat_3;

	public static SleekImage iconChat_3;

	public static SleekImage typeChat_3;

	public static SleekLabel labelChat_4;

	public static SleekImage iconChat_4;

	public static SleekImage typeChat_4;

	public static SleekImage typeChat;

	public static SleekField chatBox;

	public static SleekImage voice;

	public static SleekBox errorBox;

	public static SleekImage iconError;

	public static SleekBox hintBox;

	public static SleekImage hintIcon;

	public static SleekImage dot;

	public static SleekImage crosshairDown;

	public static SleekImage crosshairLeft;

	public static SleekImage crosshairRight;

	public static SleekImage hitmarker;

	public static SleekImage structmarker;

	private static float lastError;

	public static float lastHitmarker;

	public static float lastStructmarker;

	private static string hint;

	private static GameObject hintTarget;

	public static float lastFlash;

	public static bool binoculars;

	public static bool cursor;

	public static bool crosshair;

	public static bool locked;

	public static bool interacting;

	public static float pain;

	public static bool painkilled;

	public static float drug;

	public static bool drugged;

	public static float timer;

	public static bool nvg;

	static HUDGame()
	{
		HUDGame.lastFlash = Single.MinValue;
		HUDGame.nvg = true;
	}

	public HUDGame()
	{
	}

	public void Awake()
	{
		HUDGame.binoculars = false;
		HUDGame.cursor = true;
		HUDGame.crosshair = false;
		HUDGame.locked = false;
		HUDGame.interacting = false;
		HUDGame.lastFlash = Single.MinValue;
		HUDGame.window = new SleekWindow();
		HUDGame.flash = new SleekImage()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.flash.setImage("Textures/Sleek/Pixel");
		HUDGame.flash.visible = false;
		HUDGame.window.addFrame(HUDGame.flash);
		HUDGame.container = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.window.addFrame(HUDGame.container);
		HUDGame.healthBox = new SleekBox()
		{
			position = new Coord2(-395, -100, 0.5f, 1f),
			size = new Coord2(150, 40, 0f, 0f)
		};
		HUDGame.container.addFrame(HUDGame.healthBox);
		HUDGame.foodBox = new SleekBox()
		{
			position = new Coord2(-235, -100, 0.5f, 1f),
			size = new Coord2(150, 40, 0f, 0f)
		};
		HUDGame.container.addFrame(HUDGame.foodBox);
		HUDGame.waterBox = new SleekBox()
		{
			position = new Coord2(-75, -100, 0.5f, 1f),
			size = new Coord2(150, 40, 0f, 0f)
		};
		HUDGame.container.addFrame(HUDGame.waterBox);
		HUDGame.sicknessBox = new SleekBox()
		{
			position = new Coord2(85, -100, 0.5f, 1f),
			size = new Coord2(150, 40, 0f, 0f)
		};
		HUDGame.container.addFrame(HUDGame.sicknessBox);
		HUDGame.staminaBox = new SleekBox()
		{
			position = new Coord2(245, -100, 0.5f, 1f),
			size = new Coord2(150, 40, 0f, 0f)
		};
		HUDGame.container.addFrame(HUDGame.staminaBox);
		HUDGame.bleedingBox = new SleekBox()
		{
			position = new Coord2(-395, -150, 0.5f, 1f),
			size = new Coord2(40, 40, 0f, 0f),
			visible = false
		};
		HUDGame.container.addFrame(HUDGame.bleedingBox);
		HUDGame.bonesBox = new SleekBox()
		{
			position = new Coord2(-345, -150, 0.5f, 1f),
			size = new Coord2(40, 40, 0f, 0f),
			visible = false
		};
		HUDGame.container.addFrame(HUDGame.bonesBox);
		HUDGame.healthBar = new SleekImage()
		{
			position = new Coord2(50, 10, 0f, 0f),
			size = new Coord2(-60, -20, 1f, 1f)
		};
		HUDGame.healthBar.setImage("Textures/Sleek/pixel");
		HUDGame.healthBar.color = new Color(0.627451f, 0.156862751f, 0.156862751f);
		HUDGame.healthBox.addFrame(HUDGame.healthBar);
		HUDGame.foodBar = new SleekImage()
		{
			position = new Coord2(50, 10, 0f, 0f),
			size = new Coord2(-60, -20, 1f, 1f)
		};
		HUDGame.foodBar.setImage("Textures/Sleek/pixel");
		HUDGame.foodBar.color = new Color(0.5882353f, 0.392156869f, 0.05882353f);
		HUDGame.foodBox.addFrame(HUDGame.foodBar);
		HUDGame.waterBar = new SleekImage()
		{
			position = new Coord2(50, 10, 0f, 0f),
			size = new Coord2(-60, -20, 1f, 1f)
		};
		HUDGame.waterBar.setImage("Textures/Sleek/pixel");
		HUDGame.waterBar.color = new Color(0.156862751f, 0.509803951f, 0.6666667f);
		HUDGame.waterBox.addFrame(HUDGame.waterBar);
		HUDGame.sicknessBar = new SleekImage()
		{
			position = new Coord2(50, 10, 0f, 0f),
			size = new Coord2(-60, -20, 1f, 1f)
		};
		HUDGame.sicknessBar.setImage("Textures/Sleek/pixel");
		HUDGame.sicknessBar.color = new Color(0.156862751f, 0.392156869f, 0.196078435f);
		HUDGame.sicknessBox.addFrame(HUDGame.sicknessBar);
		HUDGame.staminaBar = new SleekImage()
		{
			position = new Coord2(50, 10, 0f, 0f),
			size = new Coord2(-60, -20, 1f, 1f)
		};
		HUDGame.staminaBar.setImage("Textures/Sleek/pixel");
		HUDGame.staminaBar.color = new Color(0.9019608f, 0.7058824f, 0.137254909f);
		HUDGame.staminaBox.addFrame(HUDGame.staminaBar);
		if (ServerSettings.mode != 2)
		{
			HUDGame.healthLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, -20, 1f, 1f)
			};
			HUDGame.healthBox.addFrame(HUDGame.healthLabel);
			HUDGame.foodLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, -20, 1f, 1f)
			};
			HUDGame.foodBox.addFrame(HUDGame.foodLabel);
			HUDGame.waterLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, -20, 1f, 1f)
			};
			HUDGame.waterBox.addFrame(HUDGame.waterLabel);
			HUDGame.sicknessLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, -20, 1f, 1f)
			};
			HUDGame.sicknessBox.addFrame(HUDGame.sicknessLabel);
			HUDGame.staminaLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, -20, 1f, 1f)
			};
			HUDGame.staminaBox.addFrame(HUDGame.staminaLabel);
		}
		HUDGame.healthIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.healthIcon.setImage("Textures/Icons/health");
		HUDGame.healthBox.addFrame(HUDGame.healthIcon);
		HUDGame.foodIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.foodIcon.setImage("Textures/Icons/food");
		HUDGame.foodBox.addFrame(HUDGame.foodIcon);
		HUDGame.waterIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.waterIcon.setImage("Textures/Icons/water");
		HUDGame.waterBox.addFrame(HUDGame.waterIcon);
		HUDGame.sicknessIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.sicknessIcon.setImage("Textures/Icons/virus");
		HUDGame.sicknessBox.addFrame(HUDGame.sicknessIcon);
		HUDGame.staminaIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.staminaIcon.setImage("Textures/Icons/stamina");
		HUDGame.staminaBox.addFrame(HUDGame.staminaIcon);
		HUDGame.bleedingIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.bleedingIcon.setImage("Textures/Icons/bleeding");
		HUDGame.bleedingBox.addFrame(HUDGame.bleedingIcon);
		HUDGame.bonesIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.bonesIcon.setImage("Textures/Icons/bones");
		HUDGame.bonesBox.addFrame(HUDGame.bonesIcon);
		HUDGame.respawnInfo = new SleekBox()
		{
			position = new Coord2(10, -45, -1f, 0f),
			size = new Coord2(-20, 40, 1f, 0f),
			text = "Info"
		};
		HUDGame.window.addFrame(HUDGame.respawnInfo);
		HUDGame.bedButton = new SleekButton()
		{
			position = new Coord2(-205, 10, 0.5f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BED
		};
		HUDGame.bedButton.onUsed += new SleekDelegate(HUDGame.usedBed);
		HUDGame.window.addFrame(HUDGame.bedButton);
		HUDGame.respawnButton = new SleekButton()
		{
			position = new Coord2(5, 10, 0.5f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_RESPAWN
		};
		HUDGame.respawnButton.onUsed += new SleekDelegate(HUDGame.usedRespawn);
		HUDGame.window.addFrame(HUDGame.respawnButton);
		if (ServerSettings.mode != 2)
		{
			HUDGame.errorBox = new SleekBox()
			{
				position = new Coord2(-200, -50, 0.5f, 0f),
				size = new Coord2(310, 40, 0f, 0f)
			};
			HUDGame.window.addFrame(HUDGame.errorBox);
			HUDGame.iconError = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.errorBox.addFrame(HUDGame.iconError);
		}
		HUDGame.hintBox = new SleekBox()
		{
			position = new Coord2(-155, -200, 0.5f, 1f),
			size = new Coord2(310, 40, 0f, 0f),
			visible = false
		};
		HUDGame.window.addFrame(HUDGame.hintBox);
		HUDGame.hintIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.hintBox.addFrame(HUDGame.hintIcon);
		if (ServerSettings.mode != 2)
		{
			HUDGame.labelChat_4 = new SleekLabel()
			{
				position = new Coord2(90, 10, 0f, 0f),
				size = new Coord2(600, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.labelChat_4);
			HUDGame.labelChat_4.visible = false;
			HUDGame.iconChat_4 = new SleekImage()
			{
				position = new Coord2(-36, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_4.addFrame(HUDGame.iconChat_4);
			HUDGame.typeChat_4 = new SleekImage()
			{
				position = new Coord2(-76, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_4.addFrame(HUDGame.typeChat_4);
			HUDGame.labelChat_3 = new SleekLabel()
			{
				position = new Coord2(90, 60, 0f, 0f),
				size = new Coord2(600, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.labelChat_3);
			HUDGame.labelChat_3.visible = false;
			HUDGame.iconChat_3 = new SleekImage()
			{
				position = new Coord2(-36, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_3.addFrame(HUDGame.iconChat_3);
			HUDGame.typeChat_3 = new SleekImage()
			{
				position = new Coord2(-76, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_3.addFrame(HUDGame.typeChat_3);
			HUDGame.labelChat_2 = new SleekLabel()
			{
				position = new Coord2(90, 110, 0f, 0f),
				size = new Coord2(600, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.labelChat_2);
			HUDGame.labelChat_2.visible = false;
			HUDGame.iconChat_2 = new SleekImage()
			{
				position = new Coord2(-36, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_2.addFrame(HUDGame.iconChat_2);
			HUDGame.typeChat_2 = new SleekImage()
			{
				position = new Coord2(-76, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_2.addFrame(HUDGame.typeChat_2);
			HUDGame.labelChat_1 = new SleekLabel()
			{
				position = new Coord2(90, 160, 0f, 0f),
				size = new Coord2(600, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.labelChat_1);
			HUDGame.iconChat_1 = new SleekImage()
			{
				position = new Coord2(-36, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_1.addFrame(HUDGame.iconChat_1);
			HUDGame.labelChat_1.visible = false;
			HUDGame.typeChat_1 = new SleekImage()
			{
				position = new Coord2(-76, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_1.addFrame(HUDGame.typeChat_1);
			HUDGame.labelChat_0 = new SleekLabel()
			{
				position = new Coord2(90, 210, 0f, 0f),
				size = new Coord2(600, 40, 0f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.labelChat_0);
			HUDGame.labelChat_0.visible = false;
			HUDGame.iconChat_0 = new SleekImage()
			{
				position = new Coord2(-36, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_0.addFrame(HUDGame.iconChat_0);
			HUDGame.typeChat_0 = new SleekImage()
			{
				position = new Coord2(-76, -16, 0f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.labelChat_0.addFrame(HUDGame.typeChat_0);
			HUDGame.chatBox = new SleekField()
			{
				position = new Coord2(90, -50, 0f, 1f),
				size = new Coord2(-100, 40, 1f, 0f),
				alignment = TextAnchor.MiddleLeft
			};
			HUDGame.container.addFrame(HUDGame.chatBox);
			HUDGame.chatBox.onUsed += new SleekDelegate(HUDGame.usedChatBox);
			HUDGame.typeChat = new SleekImage()
			{
				position = new Coord2(14, -46, 0f, 1f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			HUDGame.typeChat.setImage(ChatType.getIcon(NetworkChat.mode));
			HUDGame.container.addFrame(HUDGame.typeChat);
		}
		HUDGame.voice = new SleekImage()
		{
			position = new Coord2(14, 314, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.voice.setImage("Textures/Icons/voice");
		HUDGame.voice.visible = false;
		HUDGame.container.addFrame(HUDGame.voice);
		HUDGame.dot = new SleekImage()
		{
			position = new Coord2(-2, -2, 0.5f, 0.5f),
			size = new Coord2(4, 4, 0f, 0f)
		};
		HUDGame.dot.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/dotFree" : "Textures/Sleek/dot"));
		HUDGame.container.addFrame(HUDGame.dot);
		HUDGame.crosshairDown = new SleekImage()
		{
			position = new Coord2(-4, 0, 0.5f, 0.5f),
			size = new Coord2(8, 8, 0f, 0f)
		};
		HUDGame.crosshairDown.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/crosshairDownFree" : "Textures/Sleek/crosshairDown"));
		HUDGame.container.addFrame(HUDGame.crosshairDown);
		HUDGame.crosshairLeft = new SleekImage()
		{
			position = new Coord2(-8, -4, 0.5f, 0.5f),
			size = new Coord2(8, 8, 0f, 0f)
		};
		HUDGame.crosshairLeft.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/crosshairLeftFree" : "Textures/Sleek/crosshairLeft"));
		HUDGame.container.addFrame(HUDGame.crosshairLeft);
		HUDGame.crosshairRight = new SleekImage()
		{
			position = new Coord2(0, -4, 0.5f, 0.5f),
			size = new Coord2(8, 8, 0f, 0f)
		};
		HUDGame.crosshairRight.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/crosshairRightFree" : "Textures/Sleek/crosshairRight"));
		HUDGame.container.addFrame(HUDGame.crosshairRight);
		HUDGame.hitmarker = new SleekImage()
		{
			position = new Coord2(-16, -16, 0.5f, 0.5f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.hitmarker.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/hitmarkerFree" : "Textures/Sleek/hitmarker"));
		HUDGame.hitmarker.visible = false;
		HUDGame.container.addFrame(HUDGame.hitmarker);
		HUDGame.structmarker = new SleekImage()
		{
			position = new Coord2(-16, -16, 0.5f, 0.5f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDGame.structmarker.setImage((PlayerSettings.status != 21 ? "Textures/Sleek/structmarkerFree" : "Textures/Sleek/structmarker"));
		HUDGame.structmarker.visible = false;
		HUDGame.container.addFrame(HUDGame.structmarker);
		HUDInventory hUDInventory = new HUDInventory();
		HUDPause hUDPause = new HUDPause();
		HUDEmote hUDEmote = new HUDEmote();
		HUDPlayers hUDPlayer = new HUDPlayers();
		AudioListener.volume = GameSettings.volume;
		HUDGame.updateChat();
		if (Application.loadedLevel != 1)
		{
			Sun.tool.cycle();
		}
		HUDGame.pain = 0f;
		HUDGame.drug = 0f;
		HUDGame.painkilled = false;
	}

	public static void closeError()
	{
		HUDGame.lastError = Single.MaxValue;
		if (HUDGame.errorBox != null)
		{
			HUDGame.errorBox.position = new Coord2(-155, 10, 0.5f, 0f);
			HUDGame.errorBox.lerp(new Coord2(-155, -50, 0.5f, 0f), HUDGame.errorBox.size, 4f);
		}
	}

	public void OnGUI()
	{
		if (!LoadingScreen.loading)
		{
			if (HUDGame.binoculars)
			{
				SleekRender.image(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), Images.binoculars, Color.white);
			}
			else if (GraphicsSettings.hud)
			{
				if (MenuKeys.binding == -1)
				{
					if (PlayerSettings.friend != string.Empty && Player.model != null && Screen.lockCursor && !Gun.aiming)
					{
						for (int i = 0; i < NetworkUserList.users.Count; i++)
						{
							GameObject item = NetworkUserList.users[i].model;
							if (item != null && item != Player.model && item.GetComponent<Player>().owner != null && item.GetComponent<Player>().owner.friend == PlayerSettings.friendHash && (item.transform.position - Player.model.transform.position).magnitude < 500f)
							{
								Vector3 screenPoint = Camera.main.WorldToScreenPoint(item.transform.position + new Vector3(0f, 3f, 0f));
								if (screenPoint.z > 0f)
								{
									SleekRender.image(new Rect(screenPoint.x - 37f, (float)Screen.height - screenPoint.y - 16f, 32f, 32f), (Texture)Resources.Load(Reputation.getIcon(NetworkUserList.users[i].reputation)), Color.white);
									GUI.skin.label.alignment = TextAnchor.MiddleLeft;
									if (NetworkUserList.users[i].nickname == string.Empty)
									{
										SleekRender.label(new Rect(screenPoint.x + 5f, (float)Screen.height - screenPoint.y - 20f, 400f, 40f), item.name, string.Empty, (NetworkUserList.users[i].status != 21 ? Color.white : Colors.GOLD));
									}
									else
									{
										SleekRender.label(new Rect(screenPoint.x + 5f, (float)Screen.height - screenPoint.y - 20f, 400f, 40f), string.Concat(item.name, " [", NetworkUserList.users[i].nickname, "]"), string.Empty, (NetworkUserList.users[i].status != 21 ? Color.white : Colors.GOLD));
									}
									GUI.skin.label.alignment = TextAnchor.MiddleCenter;
								}
							}
						}
					}
					if (Event.current.type == EventType.MouseDown && HUDInventory.state && HUDCharacter.state)
					{
						HUDCharacter.usedCharacter();
					}
					if (Event.current.type == EventType.KeyDown && HUDGame.labelChat_0 != null && Event.current.keyCode == KeyCode.Return)
					{
						if (NetworkChat.chatting)
						{
							NetworkChat.sendChat(HUDGame.chatBox.text);
							HUDGame.chatBox.text = string.Empty;
						}
						else if (Screen.lockCursor)
						{
							NetworkChat.chatting = true;
						}
						HUDGame.updateChat();
					}
				}
				HUDGame.window.drawFrame();
				if (NetworkChat.chatting && GUI.GetNameOfFocusedControl() != "field")
				{
					GUI.FocusControl("field");
				}
				if (MenuKeys.binding != -1)
				{
					if (Event.current.type == EventType.KeyDown)
					{
						if (Event.current.keyCode != KeyCode.Escape)
						{
							MenuKeys.bind(Event.current.keyCode);
						}
						else
						{
							MenuKeys.bind(KeyCode.None);
						}
					}
					else if (Event.current.type == EventType.MouseDown)
					{
						if (Event.current.button == 0)
						{
							MenuKeys.bind(KeyCode.Mouse0);
						}
						if (Event.current.button == 1)
						{
							MenuKeys.bind(KeyCode.Mouse1);
						}
						if (Event.current.button == 2)
						{
							MenuKeys.bind(KeyCode.Mouse2);
						}
						if (Event.current.button == 3)
						{
							MenuKeys.bind(KeyCode.Mouse3);
						}
						if (Event.current.button == 4)
						{
							MenuKeys.bind(KeyCode.Mouse4);
						}
						if (Event.current.button == 5)
						{
							MenuKeys.bind(KeyCode.Mouse5);
						}
						if (Event.current.button == 6)
						{
							MenuKeys.bind(KeyCode.Mouse6);
						}
					}
					else if (Event.current.shift)
					{
						MenuKeys.bind(KeyCode.LeftShift);
					}
				}
			}
		}
	}

	public static void openError(string text, string icon)
	{
		HUDGame.lastError = Time.realtimeSinceStartup;
		if (HUDGame.errorBox != null)
		{
			HUDGame.errorBox.text = text;
			HUDGame.iconError.setImage(icon);
			HUDGame.errorBox.position = new Coord2(-155, -50, 0.5f, 0f);
			HUDGame.errorBox.lerp(new Coord2(-155, 10, 0.5f, 0f), HUDGame.errorBox.size, 4f);
		}
	}

	public static void setHint(string text, GameObject target, Color color, Color paint, string icon)
	{
		if (HUDGame.hint != text || HUDGame.hintTarget != target)
		{
			HUDGame.hint = text;
			HUDGame.hintTarget = target;
			HUDGame.hintBox.text = HUDGame.hint;
			HUDGame.hintIcon.setImage(icon);
			HUDGame.hintBox.color = color;
			HUDGame.hintBox.paint = paint;
		}
	}

	public void Start()
	{
		HUDGame.updateChat();
	}

	public void Update()
	{
		if (Input.GetKeyDown(InputSettings.nvgKey) && Player.clothes != null)
		{
			HUDGame.nvg = !HUDGame.nvg;
			Sun.setVision(Player.clothes.hat, HUDGame.nvg);
			NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
		if (MenuKeys.binding == -1)
		{
			if (Input.GetKeyDown(InputSettings.otherKey))
			{
				HUDInventory.dropButton.text = string.Concat(Texts.LABEL_DROP, " All");
				HUDCrafting.buttonCraft.text = string.Concat(Texts.LABEL_CRAFT, " All");
			}
			else if (Input.GetKeyUp(InputSettings.otherKey))
			{
				HUDInventory.dropButton.text = Texts.LABEL_DROP;
				HUDCrafting.buttonCraft.text = Texts.LABEL_CRAFT;
			}
		}
		if (HUDPause.state)
		{
			if (Time.realtimeSinceStartup - HUDPause.timer > 10f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
			{
				HUDPause.leaveButton.text = Texts.LABEL_LEAVE;
			}
			else
			{
				HUDPause.leaveButton.text = string.Concat(Texts.LABEL_LEAVE, " ", 10 - Mathf.FloorToInt(Time.realtimeSinceStartup - HUDPause.timer));
			}
			if (ServerSettings.mode != 2)
			{
				if (Time.realtimeSinceStartup - HUDPause.timer > 5f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
				{
					HUDPause.resetButton.text = Texts.LABEL_RESET;
				}
				else
				{
					HUDPause.resetButton.text = string.Concat(Texts.LABEL_RESET, " ", 5 - Mathf.FloorToInt(Time.realtimeSinceStartup - HUDPause.timer));
				}
			}
		}
		if (Time.realtimeSinceStartup - HUDGame.timer > 20f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
		{
			HUDGame.bedButton.text = Texts.LABEL_BED;
		}
		else
		{
			HUDGame.bedButton.text = string.Concat(Texts.LABEL_BED, " ", 20 - Mathf.FloorToInt(Time.realtimeSinceStartup - HUDGame.timer));
		}
		HUDGame.pain = HUDGame.pain - Time.deltaTime;
		if (HUDGame.pain < 0f)
		{
			HUDGame.pain = 0f;
			if (HUDGame.painkilled)
			{
				HUDGame.painkilled = false;
				HUDGame.updateHealth();
			}
		}
		else if (!HUDGame.painkilled)
		{
			HUDGame.painkilled = true;
			HUDGame.updateHealth();
		}
		HUDGame.drug = HUDGame.drug - Time.deltaTime;
		
		// TODO: Don't use Drugs :D
		/*
		if (HUDGame.drug >= 0f)
		{
			if (!HUDGame.drugged)
			{
				HUDGame.drugged = true;
				Camera.main.GetComponent<Bloom>().enabled = true;
				Camera.main.GetComponent<TwirlEffect>().enabled = true;
				Camera.main.GetComponent<ContrastEnhance>().enabled = true;
				Look.zoom.GetComponent<Bloom>().enabled = true;
				Look.zoom.GetComponent<TwirlEffect>().enabled = true;
				Look.zoom.GetComponent<ContrastEnhance>().enabled = true;
			}
			Camera.main.GetComponent<TwirlEffect>().angle = Mathf.Lerp(Camera.main.GetComponent<TwirlEffect>().angle, Mathf.Sin(Time.realtimeSinceStartup) * 10f, 4f * Time.deltaTime);
			Look.zoom.GetComponent<TwirlEffect>().angle = Mathf.Lerp(Camera.main.GetComponent<TwirlEffect>().angle, Mathf.Sin(Time.realtimeSinceStartup) * 10f, 4f * Time.deltaTime);
		}
		else
		{
			HUDGame.drug = 0f;
			if (HUDGame.drugged)
			{
				HUDGame.drugged = false;
				Camera.main.GetComponent<Bloom>().enabled = false;
				Camera.main.GetComponent<TwirlEffect>().enabled = false;
				Camera.main.GetComponent<ContrastEnhance>().enabled = false;
				Look.zoom.GetComponent<Bloom>().enabled = false;
				Look.zoom.GetComponent<TwirlEffect>().enabled = false;
				Look.zoom.GetComponent<ContrastEnhance>().enabled = false;
			}
		}
		*/
		
		if (Time.realtimeSinceStartup - HUDGame.lastError > (float)MenuRegister.ERROR_TIMEOUT)
		{
			HUDGame.closeError();
		}
		if (Input.GetKeyDown(InputSettings.hudKey))
		{
			GraphicsSettings.hud = !GraphicsSettings.hud;
		}
		if (MenuKeys.binding == -1 && Input.GetKeyDown(KeyCode.Escape))
		{
			if (HUDPause.state)
			{
				HUDPause.close();
			}
			else if (HUDInventory.state || HUDEmote.state || HUDPlayers.state)
			{
				if (HUDInventory.state)
				{
					HUDInventory.close();
				}
				if (HUDEmote.state)
				{
					HUDEmote.close();
				}
				if (HUDPlayers.state)
				{
					HUDPlayers.close();
				}
				if (HUDGame.interacting)
				{
					Interact.edit = null;
					HUDGame.interacting = false;
				}
			}
			else
			{
				HUDPause.open();
			}
		}
		if (Player.life.dead)
		{
			if (HUDPlayers.state)
			{
				HUDPlayers.close();
			}
		}
		else if (MenuKeys.binding == -1 && Input.GetKeyDown(InputSettings.playersKey) && ServerSettings.mode != 2)
		{
			if (HUDPlayers.state)
			{
				HUDPlayers.close();
			}
			else if (Screen.lockCursor)
			{
				HUDPlayers.open();
				if (HUDInventory.state)
				{
					HUDInventory.close();
				}
				if (HUDPause.state)
				{
					HUDPause.close();
				}
				if (HUDEmote.state)
				{
					HUDEmote.close();
				}
				if (HUDGame.interacting)
				{
					Interact.edit = null;
					HUDGame.interacting = false;
				}
			}
		}
		if (Player.life.dead || Movement.isSwimming || !Movement.isGrounded || Movement.isClimbing)
		{
			if (HUDInventory.state)
			{
				HUDInventory.close();
			}
			if (HUDEmote.state)
			{
				HUDEmote.close();
			}
			if (HUDGame.interacting)
			{
				Interact.edit = null;
				HUDGame.interacting = false;
			}
		}
		else if (MenuKeys.binding == -1)
		{
			if (InputSettings.inventoryToggle)
			{
				if (Input.GetKeyDown(InputSettings.inventoryKey))
				{
					if (HUDInventory.state)
					{
						HUDInventory.close();
						if (HUDGame.interacting)
						{
							Interact.edit = null;
							HUDGame.interacting = false;
						}
					}
					else if (Stance.state != 2 && Screen.lockCursor)
					{
						HUDInventory.open();
						if (HUDPause.state)
						{
							HUDPause.close();
						}
						if (HUDEmote.state)
						{
							HUDEmote.close();
						}
						if (HUDPlayers.state)
						{
							HUDPlayers.close();
						}
					}
				}
			}
			else if (!Input.GetKeyDown(InputSettings.inventoryKey))
			{
				if (!Input.GetKey(InputSettings.inventoryKey) && HUDInventory.state && !HUDGame.interacting)
				{
					HUDInventory.close();
				}
			}
			else if (HUDGame.interacting)
			{
				Interact.edit = null;
				HUDGame.interacting = false;
				HUDInventory.close();
			}
			else if (Stance.state != 2 && Screen.lockCursor)
			{
				HUDInventory.open();
				if (HUDPause.state)
				{
					HUDPause.close();
				}
				if (HUDEmote.state)
				{
					HUDEmote.close();
				}
				if (HUDPlayers.state)
				{
					HUDPlayers.close();
				}
			}
			if (Input.GetKeyDown(InputSettings.emoteKey) && Stance.state == 0 && Equipment.model == null && Screen.lockCursor && !Movement.isSwimming && !Movement.isDriving && !Movement.isClimbing)
			{
				HUDEmote.open();
				if (HUDInventory.state)
				{
					HUDInventory.close();
				}
				if (HUDPause.state)
				{
					HUDPause.close();
				}
				if (HUDPlayers.state)
				{
					HUDPlayers.close();
				}
				if (HUDGame.interacting)
				{
					Interact.edit = null;
					HUDGame.interacting = false;
				}
			}
			else if (!Input.GetKey(InputSettings.emoteKey) && HUDEmote.state)
			{
				HUDEmote.close();
			}
		}
		if (HUDGame.interacting && Interact.edit == null)
		{
			HUDGame.interacting = false;
			HUDInventory.close();
		}
		if (HUDInventory.state && HUDInventory.dragging.x != -1)
		{
			Vector3 vector3 = Input.mousePosition;
			HUDInventory.drag.position.offset_x = (int)(vector3.x - 16f);
			float single = (float)Screen.height;
			Vector3 vector31 = Input.mousePosition;
			HUDInventory.drag.position.offset_y = (int)(single - vector31.y - 48f);
			if (ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) != 10)
			{
				HUDInventory.dragAmount.text = string.Concat("x", Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].amount);
			}
			else
			{
				HUDInventory.dragAmount.text = string.Concat("x", Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].amount - 1);
			}
		}
		if (HUDInventory.state || HUDPause.state || HUDEmote.state || HUDPlayers.state || Player.life.dead || NetworkChat.chatting || HUDGame.locked)
		{
			Screen.lockCursor = false;
		}
		else
		{
			Screen.lockCursor = true;
		}
		HUDGame.dot.visible = (!Screen.lockCursor || !HUDGame.cursor ? false : ServerSettings.mode != 2);
		if (!Screen.lockCursor || HUDGame.cursor || !HUDGame.crosshair || ServerSettings.mode == 2)
		{
			HUDGame.crosshairDown.visible = false;
			HUDGame.crosshairLeft.visible = false;
			HUDGame.crosshairRight.visible = false;
		}
		else
		{
			HUDGame.crosshairDown.visible = true;
			HUDGame.crosshairLeft.visible = true;
			HUDGame.crosshairRight.visible = true;
			HUDGame.crosshairDown.position.offset_y = (int)(Stance.cross * 30f + Gun.spread * 10f);
			HUDGame.crosshairLeft.position.offset_x = -8 - (int)(Stance.cross * 30f + Gun.spread * 10f);
			HUDGame.crosshairRight.position.offset_x = (int)(Stance.cross * 30f + Gun.spread * 10f);
		}
		HUDGame.hitmarker.visible = (Time.realtimeSinceStartup - HUDGame.lastHitmarker >= 0.5f ? false : ServerSettings.mode != 2);
		HUDGame.structmarker.visible = (Time.realtimeSinceStartup - HUDGame.lastStructmarker >= 0.5f ? false : ServerSettings.mode != 2);
		if (!Screen.lockCursor || !(HUDGame.hint != string.Empty) || ServerSettings.mode == 2)
		{
			HUDGame.hintBox.visible = false;
		}
		else
		{
			HUDGame.hintBox.visible = true;
		}
		if ((double)(Time.realtimeSinceStartup - HUDGame.lastFlash) >= 0.5 || HUDGame.painkilled)
		{
			HUDGame.flash.visible = false;
		}
		else
		{
			HUDGame.flash.color = new Color(0.392156869f, 0f, 0f, 0.5f - (Time.realtimeSinceStartup - HUDGame.lastFlash));
			HUDGame.flash.visible = true;
		}
		HUDGame.voice.visible = Voice.sending;
	}

	public static void updateBleeding()
	{
		HUDGame.bleedingBox.visible = Player.life.bleeding;
	}

	public static void updateBones()
	{
		HUDGame.bonesBox.visible = Player.life.bones;
	}

	public static void updateChat()
	{
		if (HUDGame.labelChat_0 != null)
		{
			HUDGame.labelChat_0.visible = NetworkChat.text_0 != string.Empty;
			HUDGame.labelChat_1.visible = NetworkChat.text_1 != string.Empty;
			HUDGame.labelChat_2.visible = NetworkChat.text_2 != string.Empty;
			HUDGame.labelChat_3.visible = NetworkChat.text_3 != string.Empty;
			HUDGame.labelChat_4.visible = NetworkChat.text_4 != string.Empty;
			if (NetworkChat.status_0 == 21)
			{
				HUDGame.labelChat_0.paint = Colors.GOLD;
			}
			else if (NetworkChat.status_0 != 2147483647)
			{
				HUDGame.labelChat_0.paint = Color.white;
			}
			else
			{
				HUDGame.labelChat_0.paint = Color.green;
			}
			if (NetworkChat.status_1 == 21)
			{
				HUDGame.labelChat_1.paint = Colors.GOLD;
			}
			else if (NetworkChat.status_1 != 2147483647)
			{
				HUDGame.labelChat_1.paint = Color.white;
			}
			else
			{
				HUDGame.labelChat_1.paint = Color.green;
			}
			if (NetworkChat.status_2 == 21)
			{
				HUDGame.labelChat_2.paint = Colors.GOLD;
			}
			else if (NetworkChat.status_2 != 2147483647)
			{
				HUDGame.labelChat_2.paint = Color.white;
			}
			else
			{
				HUDGame.labelChat_2.paint = Color.green;
			}
			if (NetworkChat.status_3 == 21)
			{
				HUDGame.labelChat_3.paint = Colors.GOLD;
			}
			else if (NetworkChat.status_3 != 2147483647)
			{
				HUDGame.labelChat_3.paint = Color.white;
			}
			else
			{
				HUDGame.labelChat_3.paint = Color.green;
			}
			if (NetworkChat.status_4 == 21)
			{
				HUDGame.labelChat_4.paint = Colors.GOLD;
			}
			else if (NetworkChat.status_4 != 2147483647)
			{
				HUDGame.labelChat_4.paint = Color.white;
			}
			else
			{
				HUDGame.labelChat_4.paint = Color.green;
			}
			HUDGame.iconChat_0.visible = NetworkChat.type_0 != 3;
			HUDGame.iconChat_1.visible = NetworkChat.type_1 != 3;
			HUDGame.iconChat_2.visible = NetworkChat.type_2 != 3;
			HUDGame.iconChat_3.visible = NetworkChat.type_3 != 3;
			HUDGame.iconChat_4.visible = NetworkChat.type_4 != 3;
			HUDGame.iconChat_0.setImage(Reputation.getIcon(NetworkChat.reputation_0));
			HUDGame.iconChat_1.setImage(Reputation.getIcon(NetworkChat.reputation_1));
			HUDGame.iconChat_2.setImage(Reputation.getIcon(NetworkChat.reputation_2));
			HUDGame.iconChat_3.setImage(Reputation.getIcon(NetworkChat.reputation_3));
			HUDGame.iconChat_4.setImage(Reputation.getIcon(NetworkChat.reputation_4));
			HUDGame.typeChat_0.setImage(ChatType.getIcon(NetworkChat.type_0));
			HUDGame.typeChat_1.setImage(ChatType.getIcon(NetworkChat.type_1));
			HUDGame.typeChat_2.setImage(ChatType.getIcon(NetworkChat.type_2));
			HUDGame.typeChat_3.setImage(ChatType.getIcon(NetworkChat.type_3));
			HUDGame.typeChat_4.setImage(ChatType.getIcon(NetworkChat.type_4));
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_0 == PlayerSettings.friendHash) || !(NetworkChat.nickname_0 != string.Empty))
			{
				HUDGame.labelChat_0.text = string.Concat(NetworkChat.speaker_0, ":\n", NetworkChat.text_0);
			}
			else
			{
				HUDGame.labelChat_0.text = string.Concat(new string[] { NetworkChat.speaker_0, " [", NetworkChat.nickname_0, "]:\n", NetworkChat.text_0 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_1 == PlayerSettings.friendHash) || !(NetworkChat.nickname_1 != string.Empty))
			{
				HUDGame.labelChat_1.text = string.Concat(NetworkChat.speaker_1, ":\n", NetworkChat.text_1);
			}
			else
			{
				HUDGame.labelChat_1.text = string.Concat(new string[] { NetworkChat.speaker_1, " [", NetworkChat.nickname_1, "]:\n", NetworkChat.text_1 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_2 == PlayerSettings.friendHash) || !(NetworkChat.nickname_2 != string.Empty))
			{
				HUDGame.labelChat_2.text = string.Concat(NetworkChat.speaker_2, ":\n", NetworkChat.text_2);
			}
			else
			{
				HUDGame.labelChat_2.text = string.Concat(new string[] { NetworkChat.speaker_2, " [", NetworkChat.nickname_2, "]:\n", NetworkChat.text_2 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_3 == PlayerSettings.friendHash) || !(NetworkChat.nickname_3 != string.Empty))
			{
				HUDGame.labelChat_3.text = string.Concat(NetworkChat.speaker_3, ":\n", NetworkChat.text_3);
			}
			else
			{
				HUDGame.labelChat_3.text = string.Concat(new string[] { NetworkChat.speaker_3, " [", NetworkChat.nickname_3, "]:\n", NetworkChat.text_3 });
			}
			if (!(PlayerSettings.friendHash != string.Empty) || !(NetworkChat.friend_4 == PlayerSettings.friendHash) || !(NetworkChat.nickname_4 != string.Empty))
			{
				HUDGame.labelChat_4.text = string.Concat(NetworkChat.speaker_4, ":\n", NetworkChat.text_4);
			}
			else
			{
				HUDGame.labelChat_4.text = string.Concat(new string[] { NetworkChat.speaker_4, " [", NetworkChat.nickname_4, "]:\n", NetworkChat.text_4 });
			}
			if (!NetworkChat.chatting)
			{
				HUDGame.chatBox.visible = false;
			}
			else
			{
				HUDGame.chatBox.visible = true;
			}
			HUDGame.typeChat.setImage(ChatType.getIcon(NetworkChat.mode));
		}
	}

	public static void updateDead()
	{
		HUDGame.timer = Time.realtimeSinceStartup;
		HUDGame.respawnInfo.text = Player.life.death;
		if (Player.life.dead)
		{
			if (HUDGame.respawnInfo.position.scale_x != 0f)
			{
				HUDGame.respawnInfo.position = new Coord2(10, -200, 1f, 1f);
				HUDGame.respawnInfo.lerp(new Coord2(10, -200, 0f, 1f), HUDGame.respawnInfo.size, 4f);
				HUDGame.bedButton.position = new Coord2(-205, 10, 0.5f, 1f);
				HUDGame.bedButton.lerp(new Coord2(-205, -150, 0.5f, 1f), HUDGame.bedButton.size, 4f);
				HUDGame.respawnButton.position = new Coord2(5, 10, 0.5f, 1f);
				HUDGame.respawnButton.lerp(new Coord2(5, -150, 0.5f, 1f), HUDGame.respawnButton.size, 4f);
				HUDGame.container.position = Coord2.ZERO;
				HUDGame.container.lerp(new Coord2(0, 0, 0f, 1f), HUDGame.container.size, 4f);
				if (HUDInventory.state)
				{
					HUDInventory.close();
				}
			}
		}
		else if (HUDGame.respawnInfo.position.scale_x == 0f)
		{
			HUDGame.respawnInfo.position = new Coord2(10, -200, 0f, 1f);
			HUDGame.respawnInfo.lerp(new Coord2(10, -200, -1f, 1f), HUDGame.respawnInfo.size, 4f);
			HUDGame.bedButton.position = new Coord2(-205, -150, 0.5f, 1f);
			HUDGame.bedButton.lerp(new Coord2(-205, 10, 0.5f, 1f), HUDGame.bedButton.size, 4f);
			HUDGame.respawnButton.position = new Coord2(5, -150, 0.5f, 1f);
			HUDGame.respawnButton.lerp(new Coord2(5, 10, 0.5f, 1f), HUDGame.respawnButton.size, 4f);
			HUDGame.container.position = new Coord2(0, 0, 0f, 1f);
			HUDGame.container.lerp(Coord2.ZERO, HUDGame.container.size, 4f);
		}
	}

	public static void updateFood()
	{
		HUDGame.foodBar.size = new Coord2((int)((float)(100 - Player.life.food) * 0.9f), 20, 0f, 0f);
		if (HUDGame.foodLabel != null)
		{
			HUDGame.foodLabel.text = string.Concat(100 - Player.life.food, "%");
		}
	}

	public static void updateHealth()
	{
		HUDGame.healthBar.size = new Coord2((int)((float)Player.life.health * 0.9f), 20, 0f, 0f);
		if (HUDGame.healthLabel != null)
		{
			HUDGame.healthLabel.text = string.Concat(Player.life.health, "%");
		}
		
		// TODO: cure
		/*
		if (Player.life.health >= 50 || HUDGame.painkilled)
		{
			Camera.main.GetComponent<ColorCorrectionCurves>().saturation = 1f;
			Look.view.GetComponent<ColorCorrectionCurves>().saturation = 1f;
			Look.zoom.GetComponent<ColorCorrectionCurves>().saturation = 1f;
		}
		else
		{
			Camera.main.GetComponent<ColorCorrectionCurves>().saturation = (float)Player.life.health / 50f;
			Look.view.GetComponent<ColorCorrectionCurves>().saturation = (float)Player.life.health / 50f;
			Look.zoom.GetComponent<ColorCorrectionCurves>().saturation = (float)Player.life.health / 50f;
		}
		*/
	}

	public static void updateSickness()
	{
		HUDGame.sicknessBar.size = new Coord2((int)((float)(100 - Player.life.sickness) * 0.9f), 20, 0f, 0f);
		if (HUDGame.sicknessLabel != null)
		{
			HUDGame.sicknessLabel.text = string.Concat(100 - Player.life.sickness, "%");
		}
	}

	public static void updateStamina()
	{
		HUDGame.staminaBar.size = new Coord2((int)((float)Player.life.stamina * 0.9f), 20, 0f, 0f);
		if (HUDGame.staminaLabel != null)
		{
			HUDGame.staminaLabel.text = string.Concat(Player.life.stamina, "%");
		}
	}

	public static void updateWater()
	{
		HUDGame.waterBar.size = new Coord2((int)((float)(100 - Player.life.water) * 0.9f), 20, 0f, 0f);
		if (HUDGame.waterLabel != null)
		{
			HUDGame.waterLabel.text = string.Concat(100 - Player.life.water, "%");
		}
	}

	public static void usedBed(SleekFrame frame)
	{
		if (Time.realtimeSinceStartup - HUDGame.timer > 20f || NetworkUserList.users.Count == 1 || !ServerSettings.pvp)
		{
			Stance.change(0);
			Look.pitch = 0f;
			Equipment.equipped = Point2.NONE;
			Equipment.id = -1;
			Equipment.busy = false;
			Equipment.setup = true;
			Equipment.ticking = false;
			HUDGame.pain = 0f;
			HUDGame.drug = 0f;
			Player.life.respawn(true);
			Player.spawned = Time.realtimeSinceStartup;
		}
	}

	public static void usedChatBox(SleekFrame frame)
	{
		if (HUDGame.chatBox.text.Length > NetworkChat.MAX_CHARACTERS)
		{
			HUDGame.chatBox.text = HUDGame.chatBox.text.Substring(0, NetworkChat.MAX_CHARACTERS);
		}
	}

	public static void usedRespawn(SleekFrame frame)
	{
		Stance.change(0);
		Look.pitch = 0f;
		Equipment.equipped = Point2.NONE;
		Equipment.id = -1;
		Equipment.busy = false;
		Equipment.setup = true;
		Equipment.ticking = false;
		HUDGame.pain = 0f;
		HUDGame.drug = 0f;
		Player.life.respawn(false);
		Player.spawned = Time.realtimeSinceStartup;
	}
}