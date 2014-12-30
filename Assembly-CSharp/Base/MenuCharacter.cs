using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter
{
	public static SleekContainer container;

	public static SleekButton buttonHairTab;

	public static SleekButton buttonSkinTab;

	public static SleekButton buttonClanTab;

	public static SleekBox boxFace;

	public static SleekBox boxHair;

	public static SleekBox boxSkinColor;

	public static SleekBox boxHairColor;

	public static SleekButton buttonArm;

	public static SleekButton[] buttonFace;

	public static SleekButton[] buttonHair;

	public static CSteamID[] clans;

	public static SleekSlider sliderClans;

	public static SleekField fieldNickname;

	public static SleekLabel nicknameHint;

	public static SleekBox boxClan;

	public static SleekButton buttonNoclan;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static SleekSlider sliderRotation;

	public static SleekLabel rotationHint;

	public static float rotation;

	private static int offset;

	public MenuCharacter()
	{
		MenuCharacter.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		MenuTitle.container.addFrame(MenuCharacter.container);
		MenuCharacter.container.visible = false;
		MenuCharacter.buttonHairTab = new SleekButton()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(170, 40, 0f, 0f),
			text = Texts.LABEL_HAIR_TAB
		};
		MenuCharacter.buttonHairTab.onUsed += new SleekDelegate(MenuCharacter.usedHairTab);
		MenuCharacter.container.addFrame(MenuCharacter.buttonHairTab);
		MenuCharacter.buttonSkinTab = new SleekButton()
		{
			position = new Coord2(190, 10, 0f, 0f),
			size = new Coord2(170, 40, 0f, 0f),
			text = Texts.LABEL_SKIN_TAB
		};
		MenuCharacter.buttonSkinTab.onUsed += new SleekDelegate(MenuCharacter.usedSkinTab);
		MenuCharacter.container.addFrame(MenuCharacter.buttonSkinTab);
		MenuCharacter.buttonClanTab = new SleekButton()
		{
			position = new Coord2(370, 10, 0f, 0f),
			size = new Coord2(170, 40, 0f, 0f),
			text = Texts.LABEL_CLAN_TAB
		};
		MenuCharacter.buttonClanTab.onUsed += new SleekDelegate(MenuCharacter.usedClanTab);
		MenuCharacter.container.addFrame(MenuCharacter.buttonClanTab);
		MenuCharacter.boxFace = new SleekBox()
		{
			position = new Coord2(10, 60, 1f, 0f),
			size = new Coord2(530, 40, 0f, 0f),
			text = Texts.LABEL_FACE
		};
		MenuCharacter.container.addFrame(MenuCharacter.boxFace);
		MenuCharacter.buttonFace = new SleekButton[18];
		for (int i = 0; i < (int)MenuCharacter.buttonFace.Length; i++)
		{
			int num = i % 9;
			int num1 = Mathf.FloorToInt((float)i / 9f);
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(num * 60, 50 + num1 * 60, 0f, 0f),
				size = new Coord2(50, 50, 0f, 0f)
			};
			MenuCharacter.boxFace.addFrame(sleekButton);
			SleekImage sleekImage = new SleekImage()
			{
				position = new Coord2(-16, -16, 0.5f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			sleekImage.setImage(string.Concat("Textures/Faces/", FaceStyles.FACES[i]));
			sleekImage.color = SkinColor.getColor(PlayerSettings.skinColor);
			sleekButton.addFrame(sleekImage);
			if (num1 <= 0)
			{
				sleekButton.onUsed += new SleekDelegate(MenuCharacter.usedFace);
			}
			else
			{
				sleekButton.color = Colors.GOLD;
				if (PlayerSettings.status == 21)
				{
					sleekButton.onUsed += new SleekDelegate(MenuCharacter.usedFace);
				}
				else
				{
					SleekImage sleekImage1 = new SleekImage()
					{
						position = new Coord2(-16, -16, 0.5f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					sleekImage1.setImage("Textures/Icons/locked");
					sleekButton.addFrame(sleekImage1);
					sleekButton.tooltip = Texts.LABEL_GOLD_REQUIRED;
				}
			}
			MenuCharacter.buttonFace[i] = sleekButton;
		}
		MenuCharacter.boxHair = new SleekBox()
		{
			position = new Coord2(10, 60, 0f, 0f),
			size = new Coord2(530, 40, 0f, 0f),
			text = Texts.LABEL_HAIR
		};
		MenuCharacter.container.addFrame(MenuCharacter.boxHair);
		MenuCharacter.buttonHair = new SleekButton[18];
		for (int j = 0; j < (int)MenuCharacter.buttonHair.Length; j++)
		{
			int num2 = j % 9;
			int num3 = Mathf.FloorToInt((float)j / 9f);
			SleekButton gOLD = new SleekButton()
			{
				position = new Coord2(num2 * 60, 50 + num3 * 60, 0f, 0f),
				size = new Coord2(50, 50, 0f, 0f)
			};
			MenuCharacter.boxHair.addFrame(gOLD);
			SleekImage color = new SleekImage()
			{
				position = new Coord2(-16, -16, 0.5f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			if (j != 0)
			{
				color.setImage(string.Concat("Textures/Items/", HairStyles.HAIRS[j - 1]));
				color.color = HairColor.getColor(PlayerSettings.hairColor);
			}
			else
			{
				color.setImage("Textures/Icons/quit");
			}
			gOLD.addFrame(color);
			if (num3 <= 0)
			{
				gOLD.onUsed += new SleekDelegate(MenuCharacter.usedHair);
			}
			else
			{
				gOLD.color = Colors.GOLD;
				if (PlayerSettings.status == 21)
				{
					gOLD.onUsed += new SleekDelegate(MenuCharacter.usedHair);
				}
				else
				{
					SleekImage sleekImage2 = new SleekImage()
					{
						position = new Coord2(-16, -16, 0.5f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					sleekImage2.setImage("Textures/Icons/locked");
					gOLD.addFrame(sleekImage2);
					gOLD.tooltip = Texts.LABEL_GOLD_REQUIRED;
				}
			}
			MenuCharacter.buttonHair[j] = gOLD;
		}
		MenuCharacter.boxSkinColor = new SleekBox()
		{
			position = new Coord2(10, 230, 1f, 0f),
			size = new Coord2(530, 40, 0f, 0f),
			text = Texts.LABEL_SKIN_COLOR
		};
		MenuCharacter.container.addFrame(MenuCharacter.boxSkinColor);
		for (int k = 0; k < (int)SkinColor.COLORS.Length; k++)
		{
			int num4 = k % 9;
			int num5 = Mathf.FloorToInt((float)k / 9f);
			SleekButton lABELGOLDREQUIRED = new SleekButton()
			{
				position = new Coord2(num4 * 60, 50 + num5 * 60, 0f, 0f),
				size = new Coord2(50, 50, 0f, 0f)
			};
			MenuCharacter.boxSkinColor.addFrame(lABELGOLDREQUIRED);
			SleekImage color1 = new SleekImage()
			{
				position = new Coord2(10, 10, 0f, 0f),
				size = new Coord2(-20, -20, 1f, 1f)
			};
			color1.setImage("Textures/Sleek/pixel");
			color1.color = SkinColor.getColor(k);
			lABELGOLDREQUIRED.addFrame(color1);
			if (num5 <= 0)
			{
				lABELGOLDREQUIRED.onUsed += new SleekDelegate(MenuCharacter.usedSkinColor);
			}
			else
			{
				lABELGOLDREQUIRED.color = Colors.GOLD;
				if (PlayerSettings.status == 21)
				{
					lABELGOLDREQUIRED.onUsed += new SleekDelegate(MenuCharacter.usedSkinColor);
				}
				else
				{
					SleekImage sleekImage3 = new SleekImage()
					{
						position = new Coord2(-16, -16, 0.5f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					sleekImage3.setImage("Textures/Icons/locked");
					lABELGOLDREQUIRED.addFrame(sleekImage3);
					lABELGOLDREQUIRED.tooltip = Texts.LABEL_GOLD_REQUIRED;
				}
			}
		}
		MenuCharacter.boxHairColor = new SleekBox()
		{
			position = new Coord2(10, 230, 0f, 0f),
			size = new Coord2(530, 40, 0f, 0f),
			text = Texts.LABEL_HAIR_COLOR
		};
		MenuCharacter.container.addFrame(MenuCharacter.boxHairColor);
		for (int l = 0; l < (int)HairColor.COLORS.Length; l++)
		{
			int num6 = l % 9;
			int num7 = Mathf.FloorToInt((float)l / 9f);
			SleekButton sleekButton1 = new SleekButton()
			{
				position = new Coord2(num6 * 60, 50 + num7 * 60, 0f, 0f),
				size = new Coord2(50, 50, 0f, 0f)
			};
			MenuCharacter.boxHairColor.addFrame(sleekButton1);
			SleekImage color2 = new SleekImage()
			{
				position = new Coord2(10, 10, 0f, 0f),
				size = new Coord2(-20, -20, 1f, 1f)
			};
			color2.setImage("Textures/Sleek/pixel");
			color2.color = HairColor.getColor(l);
			sleekButton1.addFrame(color2);
			if (num7 <= 0)
			{
				sleekButton1.onUsed += new SleekDelegate(MenuCharacter.usedHairColor);
			}
			else
			{
				sleekButton1.color = Colors.GOLD;
				if (PlayerSettings.status == 21)
				{
					sleekButton1.onUsed += new SleekDelegate(MenuCharacter.usedHairColor);
				}
				else
				{
					SleekImage sleekImage4 = new SleekImage()
					{
						position = new Coord2(-16, -16, 0.5f, 0.5f),
						size = new Coord2(32, 32, 0f, 0f)
					};
					sleekImage4.setImage("Textures/Icons/locked");
					sleekButton1.addFrame(sleekImage4);
					sleekButton1.tooltip = Texts.LABEL_GOLD_REQUIRED;
				}
			}
		}
		MenuCharacter.buttonArm = new SleekButton()
		{
			position = new Coord2(10, -100, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f)
		};
		MenuCharacter.buttonArm.onUsed += new SleekDelegate(MenuCharacter.usedArm);
		MenuCharacter.container.addFrame(MenuCharacter.buttonArm);
		MenuCharacter.fieldNickname = new SleekField()
		{
			position = new Coord2(10, 60, 1f, 0f),
			size = new Coord2(530, 40, 0f, 0f),
			text = PlayerSettings.nickname
		};
		MenuCharacter.fieldNickname.onUsed += new SleekDelegate(MenuCharacter.usedNickname);
		MenuCharacter.container.addFrame(MenuCharacter.fieldNickname);
		MenuCharacter.nicknameHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.HINT_NICKNAME,
			alignment = TextAnchor.MiddleLeft
		};
		MenuCharacter.fieldNickname.addFrame(MenuCharacter.nicknameHint);
		PlayerSettings.hash();
		MenuCharacter.clans = new CSteamID[SteamFriends.GetClanCount()];
		for (int m = 0; m < (int)MenuCharacter.clans.Length; m++)
		{
			MenuCharacter.clans[m] = SteamFriends.GetClanByIndex(m);
		}
		ulong num8 = (ulong)0;
		if (PlayerSettings.friend != string.Empty)
		{
			num8 = ulong.Parse(PlayerSettings.friend);
			bool flag = false;
			int num9 = 0;
			while (num9 < (int)MenuCharacter.clans.Length)
			{
				// TODO: steam ID
				/*if (MenuCharacter.clans[num9].m_SteamID != num8)
				{
					num9++;
				}
				else
				{
					flag = true;
					break;
				}*/
			}
			if (!flag)
			{
				num8 = (ulong)0;
				PlayerSettings.friend = string.Empty;
				PlayerSettings.hash();
				PlayerSettings.save();
			}
		}
		MenuCharacter.boxClan = new SleekBox()
		{
			position = new Coord2(0, 50, 0f, 0f),
			size = new Coord2(-5, 40, 0.5f, 0f)
		};
		if (num8 == 0)
		{
			MenuCharacter.boxClan.text = string.Empty;
		}
		else
		{
			MenuCharacter.boxClan.text = SteamFriends.GetClanName(new CSteamID(num8));
		}
		MenuCharacter.fieldNickname.addFrame(MenuCharacter.boxClan);
		MenuCharacter.buttonNoclan = new SleekButton()
		{
			position = new Coord2(5, 50, 0.5f, 0f),
			size = new Coord2(-5, 40, 0.5f, 0f),
			text = Texts.LABEL_NOCLAN
		};
		MenuCharacter.buttonNoclan.onUsed += new SleekDelegate(MenuCharacter.usedNoclan);
		MenuCharacter.fieldNickname.addFrame(MenuCharacter.buttonNoclan);
		if ((int)MenuCharacter.clans.Length != 0)
		{
			for (int n = 0; n < 7; n++)
			{
				SleekButton clanName = new SleekButton()
				{
					position = new Coord2(0, 100 + n * 50, 0f, 0f),
					size = new Coord2(0, 40, 1f, 0f)
				};
				if (n >= (int)MenuCharacter.clans.Length)
				{
					clanName.visible = false;
				}
				else
				{
					clanName.text = SteamFriends.GetClanName(MenuCharacter.clans[n]);
				}
				clanName.onUsed += new SleekDelegate(MenuCharacter.usedClan);
				MenuCharacter.fieldNickname.addFrame(clanName);
			}
			MenuCharacter.sliderClans = new SleekSlider()
			{
				position = new Coord2(10, 100, 1f, 0f),
				size = new Coord2(10, 340, 0f, 0f),
				orientation = Orient2.VERTICAL
			};
			MenuCharacter.sliderClans.onUsed += new SleekDelegate(MenuCharacter.usedSliderClans);
			if ((int)MenuCharacter.clans.Length >= 7)
			{
				MenuCharacter.offset = (int)Mathf.Ceil((float)((int)MenuCharacter.clans.Length - 7) * MenuCharacter.sliderClans.state);
				MenuCharacter.sliderClans.scale = 7f / (float)((int)MenuCharacter.clans.Length);
			}
			else
			{
				MenuCharacter.offset = 0;
				MenuCharacter.sliderClans.scale = 1f;
			}
			MenuCharacter.fieldNickname.addFrame(MenuCharacter.sliderClans);
		}
		else
		{
			SleekButton sleekButton2 = new SleekButton()
			{
				position = new Coord2(0, 100, 0f, 0f),
				size = new Coord2(0, 40, 1f, 0f),
				text = "Sorry: No Groups"
			};
			MenuCharacter.fieldNickname.addFrame(sleekButton2);
		}
		MenuCharacter.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuCharacter.buttonBack.onUsed += new SleekDelegate(MenuCharacter.usedBack);
		MenuCharacter.container.addFrame(MenuCharacter.buttonBack);
		MenuCharacter.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuCharacter.iconBack.setImage("Textures/Icons/back");
		MenuCharacter.buttonBack.addFrame(MenuCharacter.iconBack);
		MenuCharacter.sliderRotation = new SleekSlider()
		{
			position = new Coord2(340, -90, 0f, 1f),
			size = new Coord2(200, 20, 0f, 0f),
			orientation = Orient2.HORIZONTAL
		};
		MenuCharacter.sliderRotation.onUsed += new SleekDelegate(MenuCharacter.usedRotation);
		MenuCharacter.sliderRotation.state = PlayerPrefs.GetFloat("mRot");
		MenuCharacter.sliderRotation.scale = 0.2f;
		MenuCharacter.container.addFrame(MenuCharacter.sliderRotation);
		MenuCharacter.rotationHint = new SleekLabel()
		{
			position = new Coord2(10, -20, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat("Rotation: ", Mathf.FloorToInt(MenuCharacter.rotation), "°"),
			alignment = TextAnchor.MiddleLeft
		};
		MenuCharacter.sliderRotation.addFrame(MenuCharacter.rotationHint);
		MenuCharacter.usedRotation(MenuCharacter.sliderRotation);
		PlayerSettings.load();
		MenuCharacter.wear();
		MenuCharacter.buttonArm.text = (!PlayerSettings.arm ? Texts.LABEL_ARM_RIGHT : Texts.LABEL_ARM_LEFT);
	}

	public static void close()
	{
		MenuCharacter.container.position = new Coord2(0, 0, -1f, 0f);
		MenuCharacter.container.lerp(new Coord2(0, 0, 1f, 0f), MenuCharacter.container.size, 4f, true);
	}

	public static void open()
	{
		MenuCharacter.container.visible = true;
		MenuCharacter.container.position = new Coord2(0, 0, 1f, 0f);
		MenuCharacter.container.lerp(new Coord2(0, 0, -1f, 0f), MenuCharacter.container.size, 4f);
		MenuRegister.lerpTo = Camera.main.transform.parent.FindChild("viewCharacter");
	}

	public static void usedArm(SleekFrame frame)
	{
		PlayerSettings.arm = !PlayerSettings.arm;
		MenuCharacter.buttonArm.text = (!PlayerSettings.arm ? Texts.LABEL_ARM_RIGHT : Texts.LABEL_ARM_LEFT);
		PlayerSettings.save();
		MenuCharacter.wear();
	}

	public static void usedBack(SleekFrame frame)
	{
		MenuCharacter.close();
		MenuTitle.open();
	}

	public static void usedClan(SleekFrame frame)
	{
		int offsetY = (frame.position.offset_y - 100) / 50 + MenuCharacter.offset;
		if (offsetY < (int)MenuCharacter.clans.Length)
		{
			// TODO: Steam - NO Steam clans
			//PlayerSettings.friend = MenuCharacter.clans[offsetY].m_SteamID.ToString();
			PlayerSettings.friend = MenuCharacter.clans[offsetY].GetAccountID().ToString();
			PlayerSettings.hash();
			PlayerSettings.save();
			MenuCharacter.boxClan.text = SteamFriends.GetClanName(MenuCharacter.clans[offsetY]);
		}
	}

	public static void usedClanTab(SleekFrame frame)
	{
		MenuCharacter.boxHair.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.boxHair.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.boxHair.size, 4f, true);
		MenuCharacter.boxHairColor.position = new Coord2(10, 230, 0f, 0f);
		MenuCharacter.boxHairColor.lerp(new Coord2(10, 230, 1f, 0f), MenuCharacter.boxHairColor.size, 4f, true);
		MenuCharacter.boxFace.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.boxFace.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.boxFace.size, 4f, true);
		MenuCharacter.boxSkinColor.position = new Coord2(10, 230, 0f, 0f);
		MenuCharacter.boxSkinColor.lerp(new Coord2(10, 230, 1f, 0f), MenuCharacter.boxSkinColor.size, 4f, true);
		MenuCharacter.fieldNickname.visible = true;
		MenuCharacter.fieldNickname.position = new Coord2(10, 60, 1f, 0f);
		MenuCharacter.fieldNickname.lerp(new Coord2(10, 60, 0f, 0f), MenuCharacter.fieldNickname.size, 4f);
	}

	public static void usedFace(SleekFrame frame)
	{
		int offsetX = frame.position.offset_x / 60 + (frame.position.offset_y - 50) / 60 * 9;
		PlayerSettings.face = FaceStyles.FACES[offsetX];
		PlayerSettings.save();
		MenuCharacter.wear();
	}

	public static void usedHair(SleekFrame frame)
	{
		int offsetX = frame.position.offset_x / 60 + (frame.position.offset_y - 50) / 60 * 9;
		if (offsetX != 0)
		{
			PlayerSettings.hair = HairStyles.HAIRS[offsetX - 1];
		}
		else
		{
			PlayerSettings.hair = -1;
		}
		PlayerSettings.save();
		MenuCharacter.wear();
	}

	public static void usedHairColor(SleekFrame frame)
	{
		int offsetX = frame.position.offset_x / 60 + (frame.position.offset_y - 50) / 60 * 9;
		PlayerSettings.hairColor = offsetX;
		PlayerSettings.save();
		MenuCharacter.wear();
		for (int i = 1; i < (int)MenuCharacter.buttonHair.Length; i++)
		{
			MenuCharacter.buttonHair[i].children[0].color = HairColor.getColor(PlayerSettings.hairColor);
		}
	}

	public static void usedHairTab(SleekFrame frame)
	{
		MenuCharacter.boxHair.visible = true;
		MenuCharacter.boxHair.position = new Coord2(10, 60, 1f, 0f);
		MenuCharacter.boxHair.lerp(new Coord2(10, 60, 0f, 0f), MenuCharacter.boxHair.size, 4f);
		MenuCharacter.boxHairColor.visible = true;
		MenuCharacter.boxHairColor.position = new Coord2(10, 230, 1f, 0f);
		MenuCharacter.boxHairColor.lerp(new Coord2(10, 230, 0f, 0f), MenuCharacter.boxHairColor.size, 4f);
		MenuCharacter.boxFace.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.boxFace.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.boxFace.size, 4f, true);
		MenuCharacter.boxSkinColor.position = new Coord2(10, 230, 0f, 0f);
		MenuCharacter.boxSkinColor.lerp(new Coord2(10, 230, 1f, 0f), MenuCharacter.boxSkinColor.size, 4f, true);
		MenuCharacter.fieldNickname.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.fieldNickname.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.fieldNickname.size, 4f, true);
	}

	public static void usedNickname(SleekFrame frame)
	{
		PlayerSettings.nickname = ((SleekField)frame).text;
		PlayerSettings.save();
	}

	public static void usedNoclan(SleekFrame frame)
	{
		PlayerSettings.friend = string.Empty;
		PlayerSettings.hash();
		PlayerSettings.save();
		MenuCharacter.boxClan.text = string.Empty;
	}

	public static void usedRotation(SleekFrame frame)
	{
		PlayerPrefs.SetFloat("mRot", ((SleekSlider)frame).state);
		MenuCharacter.rotation = ((SleekSlider)frame).state * 360f;
		MenuCharacter.rotationHint.text = string.Concat("Rotation: ", Mathf.FloorToInt(MenuCharacter.rotation), "°");
		Camera.main.transform.parent.FindChild("player").rotation = Quaternion.Euler(0f, 282f - MenuCharacter.rotation, 0f);
	}

	public static void usedSkinColor(SleekFrame frame)
	{
		int offsetX = frame.position.offset_x / 60 + (frame.position.offset_y - 50) / 60 * 9;
		PlayerSettings.skinColor = offsetX;
		PlayerSettings.save();
		MenuCharacter.wear();
		for (int i = 0; i < (int)MenuCharacter.buttonFace.Length; i++)
		{
			MenuCharacter.buttonFace[i].children[0].color = SkinColor.getColor(PlayerSettings.skinColor);
		}
	}

	public static void usedSkinTab(SleekFrame frame)
	{
		MenuCharacter.boxHair.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.boxHair.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.boxHair.size, 4f, true);
		MenuCharacter.boxHairColor.position = new Coord2(10, 230, 0f, 0f);
		MenuCharacter.boxHairColor.lerp(new Coord2(10, 230, 1f, 0f), MenuCharacter.boxHairColor.size, 4f, true);
		MenuCharacter.boxFace.visible = true;
		MenuCharacter.boxFace.position = new Coord2(10, 60, 1f, 0f);
		MenuCharacter.boxFace.lerp(new Coord2(10, 60, 0f, 0f), MenuCharacter.boxFace.size, 4f);
		MenuCharacter.boxSkinColor.visible = true;
		MenuCharacter.boxSkinColor.position = new Coord2(10, 230, 1f, 0f);
		MenuCharacter.boxSkinColor.lerp(new Coord2(10, 230, 0f, 0f), MenuCharacter.boxSkinColor.size, 4f);
		MenuCharacter.fieldNickname.position = new Coord2(10, 60, 0f, 0f);
		MenuCharacter.fieldNickname.lerp(new Coord2(10, 60, 1f, 0f), MenuCharacter.fieldNickname.size, 4f, true);
	}

	public static void usedSliderClans(SleekFrame frame)
	{
		if ((int)MenuCharacter.clans.Length >= 7)
		{
			MenuCharacter.offset = (int)Mathf.Ceil((float)((int)MenuCharacter.clans.Length - 7) * MenuCharacter.sliderClans.state);
			MenuCharacter.sliderClans.scale = 7f / (float)((int)MenuCharacter.clans.Length);
		}
		else
		{
			MenuCharacter.offset = 0;
			MenuCharacter.sliderClans.scale = 1f;
		}
		for (int i = 0; i < 7; i++)
		{
			if (i + MenuCharacter.offset < (int)MenuCharacter.clans.Length)
			{
				((SleekButton)MenuCharacter.fieldNickname.children[3 + i]).text = SteamFriends.GetClanName(MenuCharacter.clans[i + MenuCharacter.offset]);
			}
		}
	}

	public static void wear()
	{
		Character component = Camera.main.transform.parent.FindChild("player").FindChild("character").GetComponent<Character>();
		component.face = PlayerSettings.face;
		component.hair = PlayerSettings.hair;
		component.skinColor = PlayerSettings.skinColor;
		component.hairColor = PlayerSettings.hairColor;
		component.arm = PlayerSettings.arm;
		component.shirt = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastShirt_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.pants = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastPants_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.hat = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastHat_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.backpack = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastBackpack_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.vest = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastVest_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.item = Sneaky.expose(PlayerPrefs.GetInt(string.Concat("lastItem_", PlayerSettings.id), Sneaky.sneak(-1)));
		component.state = Sneaky.expose(PlayerPrefs.GetString(string.Concat("lastState_", PlayerSettings.id), Sneaky.sneak(string.Empty)));
		if (component.item == -1)
		{
			component.transform.parent.GetComponent<Animifier>().stance("standIdleBasic");
		}
		else if (component.item == 7004 || component.item == 7014)
		{
			component.transform.parent.GetComponent<Animifier>().stance("standFriendlyBow");
		}
		else if (ItemType.getType(component.item) == 7)
		{
			component.transform.parent.GetComponent<Animifier>().stance("standFriendlyGun");
		}
		else if (component.item == 8001 || component.item == 8008)
		{
			component.transform.parent.GetComponent<Animifier>().stance("standIdleFlashlight");
		}
		else
		{
			component.transform.parent.GetComponent<Animifier>().stance("standIdleItem");
		}
		component.wear();
	}
}