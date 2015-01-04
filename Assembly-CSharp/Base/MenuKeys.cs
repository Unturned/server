using System;
using UnityEngine;

public class MenuKeys
{
	public static SleekContainer container;

	public static SleekButton buttonJump;

	public static SleekButton buttonSprint;

	public static SleekButton buttonProne;

	public static SleekButton buttonCrouch;

	public static SleekButton buttonInventory;

	public static SleekButton buttonReload;

	public static SleekButton buttonLeanLeft;

	public static SleekButton buttonLeanRight;

	public static SleekButton buttonEmote;

	public static SleekButton buttonFiremode;

	public static SleekButton buttonAttachment;

	public static SleekButton buttonChat;

	public static SleekButton buttonLocal;

	public static SleekButton buttonClan;

	public static SleekButton buttonInteract;

	public static SleekButton buttonPlayers;

	public static SleekButton buttonVoice;

	public static SleekButton buttonOther;

	public static SleekButton buttonUp;

	public static SleekButton buttonLeft;

	public static SleekButton buttonRight;

	public static SleekButton buttonDown;

	public static SleekButton buttonShoot;

	public static SleekButton buttonAim;

	public static SleekButton buttonHUD;

	public static SleekButton buttonNVG;

	public static SleekButton buttonDrop;

	public static SleekButton buttonItem;

	public static SleekButton buttonLookInvert;

	public static SleekButton buttonBack;

	public static SleekImage iconBack;

	public static int binding;

	static MenuKeys()
	{
		MenuKeys.binding = -1;
	}

	public MenuKeys()
	{
		MenuKeys.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 1f, 0f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		if (Application.loadedLevel == 0)
		{
			MenuTitle.container.addFrame(MenuKeys.container);
		}
		else
		{
			//HUDPause.container.addFrame(MenuKeys.container);
		}
		MenuKeys.container.visible = false;
		MenuKeys.buttonOther = new SleekButton()
		{
			position = new Coord2(-210, -245, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_OTHER, ": ", InputSettings.otherKey)
		};
		MenuKeys.buttonOther.onUsed += new SleekDelegate(MenuKeys.usedOther);
		MenuKeys.container.addFrame(MenuKeys.buttonOther);
		MenuKeys.buttonVoice = new SleekButton()
		{
			position = new Coord2(10, -245, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_VOICE, ": ", InputSettings.voiceKey)
		};
		MenuKeys.buttonVoice.onUsed += new SleekDelegate(MenuKeys.usedVoice);
		MenuKeys.container.addFrame(MenuKeys.buttonVoice);
		MenuKeys.buttonInteract = new SleekButton()
		{
			position = new Coord2(10, -195, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_INTERACT, ": ", InputSettings.interactKey)
		};
		MenuKeys.buttonInteract.onUsed += new SleekDelegate(MenuKeys.usedInteract);
		MenuKeys.container.addFrame(MenuKeys.buttonInteract);
		MenuKeys.buttonEmote = new SleekButton()
		{
			position = new Coord2(10, -145, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_EMOTE, ": ", InputSettings.emoteKey)
		};
		MenuKeys.buttonEmote.onUsed += new SleekDelegate(MenuKeys.usedEmote);
		MenuKeys.container.addFrame(MenuKeys.buttonEmote);
		MenuKeys.buttonInventory = new SleekButton()
		{
			position = new Coord2(10, -95, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_INVENTORY, ": ", InputSettings.inventoryKey)
		};
		MenuKeys.buttonInventory.onUsed += new SleekDelegate(MenuKeys.usedInventory);
		MenuKeys.container.addFrame(MenuKeys.buttonInventory);
		MenuKeys.buttonReload = new SleekButton()
		{
			position = new Coord2(10, -45, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_RELOAD, ": ", InputSettings.reloadKey)
		};
		MenuKeys.buttonReload.onUsed += new SleekDelegate(MenuKeys.usedReload);
		MenuKeys.container.addFrame(MenuKeys.buttonReload);
		MenuKeys.buttonProne = new SleekButton()
		{
			position = new Coord2(10, 5, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_PRONE, ": ", InputSettings.proneKey)
		};
		MenuKeys.buttonProne.onUsed += new SleekDelegate(MenuKeys.usedProne);
		MenuKeys.container.addFrame(MenuKeys.buttonProne);
		MenuKeys.buttonCrouch = new SleekButton()
		{
			position = new Coord2(10, 55, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_CROUCH, ": ", InputSettings.crouchKey)
		};
		MenuKeys.buttonCrouch.onUsed += new SleekDelegate(MenuKeys.usedCrouch);
		MenuKeys.container.addFrame(MenuKeys.buttonCrouch);
		MenuKeys.buttonFiremode = new SleekButton()
		{
			position = new Coord2(10, 105, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_FIREMODE, ": ", InputSettings.firemodeKey)
		};
		MenuKeys.buttonFiremode.onUsed += new SleekDelegate(MenuKeys.usedFiremode);
		MenuKeys.container.addFrame(MenuKeys.buttonFiremode);
		MenuKeys.buttonAttachment = new SleekButton()
		{
			position = new Coord2(10, 155, 0f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_ATTACHMENT, ": ", InputSettings.attachmentKey)
		};
		MenuKeys.buttonAttachment.onUsed += new SleekDelegate(MenuKeys.usedAttachment);
		MenuKeys.container.addFrame(MenuKeys.buttonAttachment);
		MenuKeys.buttonChat = new SleekButton()
		{
			position = new Coord2(-210, -195, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_CHAT, ": ", InputSettings.chatKey)
		};
		MenuKeys.buttonChat.onUsed += new SleekDelegate(MenuKeys.usedChat);
		MenuKeys.container.addFrame(MenuKeys.buttonChat);
		MenuKeys.buttonLocal = new SleekButton()
		{
			position = new Coord2(-210, -145, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_LOCAL, ": ", InputSettings.localKey)
		};
		MenuKeys.buttonLocal.onUsed += new SleekDelegate(MenuKeys.usedLocal);
		MenuKeys.container.addFrame(MenuKeys.buttonLocal);
		MenuKeys.buttonClan = new SleekButton()
		{
			position = new Coord2(-210, -95, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_CLAN, ": ", InputSettings.clanKey)
		};
		MenuKeys.buttonClan.onUsed += new SleekDelegate(MenuKeys.usedClan);
		MenuKeys.container.addFrame(MenuKeys.buttonClan);
		MenuKeys.buttonPlayers = new SleekButton()
		{
			position = new Coord2(-210, -45, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_PLAYERS, ": ", InputSettings.playersKey)
		};
		MenuKeys.buttonPlayers.onUsed += new SleekDelegate(MenuKeys.usedPlayers);
		MenuKeys.container.addFrame(MenuKeys.buttonPlayers);
		MenuKeys.buttonSprint = new SleekButton()
		{
			position = new Coord2(-210, 5, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_SPRINT, ": ", InputSettings.sprintKey)
		};
		MenuKeys.buttonSprint.onUsed += new SleekDelegate(MenuKeys.usedSprint);
		MenuKeys.container.addFrame(MenuKeys.buttonSprint);
		MenuKeys.buttonJump = new SleekButton()
		{
			position = new Coord2(-210, 55, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_JUMP, ": ", InputSettings.jumpKey)
		};
		MenuKeys.buttonJump.onUsed += new SleekDelegate(MenuKeys.usedJump);
		MenuKeys.container.addFrame(MenuKeys.buttonJump);
		MenuKeys.buttonLeanLeft = new SleekButton()
		{
			position = new Coord2(-210, 105, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_LEANLEFT, ": ", InputSettings.leanLeftKey)
		};
		MenuKeys.buttonLeanLeft.onUsed += new SleekDelegate(MenuKeys.usedLeanLeft);
		MenuKeys.container.addFrame(MenuKeys.buttonLeanLeft);
		MenuKeys.buttonLeanRight = new SleekButton()
		{
			position = new Coord2(-210, 155, 1f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_LEANRIGHT, ": ", InputSettings.leanRightKey)
		};
		MenuKeys.buttonLeanRight.onUsed += new SleekDelegate(MenuKeys.usedLeanRight);
		MenuKeys.container.addFrame(MenuKeys.buttonLeanRight);
		MenuKeys.buttonUp = new SleekButton()
		{
			position = new Coord2(-100, -170, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_UP, ": ", InputSettings.upKey)
		};
		MenuKeys.buttonUp.onUsed += new SleekDelegate(MenuKeys.usedUp);
		MenuKeys.container.addFrame(MenuKeys.buttonUp);
		MenuKeys.buttonLeft = new SleekButton()
		{
			position = new Coord2(-100, -120, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_LEFT, ": ", InputSettings.leftKey)
		};
		MenuKeys.buttonLeft.onUsed += new SleekDelegate(MenuKeys.usedLeft);
		MenuKeys.container.addFrame(MenuKeys.buttonLeft);
		MenuKeys.buttonRight = new SleekButton()
		{
			position = new Coord2(-100, -70, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_RIGHT, ": ", InputSettings.rightKey)
		};
		MenuKeys.buttonRight.onUsed += new SleekDelegate(MenuKeys.usedRight);
		MenuKeys.container.addFrame(MenuKeys.buttonRight);
		MenuKeys.buttonDown = new SleekButton()
		{
			position = new Coord2(-100, -20, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_DOWN, ": ", InputSettings.downKey)
		};
		MenuKeys.buttonDown.onUsed += new SleekDelegate(MenuKeys.usedDown);
		MenuKeys.container.addFrame(MenuKeys.buttonDown);
		MenuKeys.buttonShoot = new SleekButton()
		{
			position = new Coord2(-100, 30, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_SHOOT, ": ", InputSettings.shootKey)
		};
		MenuKeys.buttonShoot.onUsed += new SleekDelegate(MenuKeys.usedShoot);
		MenuKeys.container.addFrame(MenuKeys.buttonShoot);
		MenuKeys.buttonAim = new SleekButton()
		{
			position = new Coord2(-100, 80, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_AIM, ": ", InputSettings.aimKey)
		};
		MenuKeys.buttonAim.onUsed += new SleekDelegate(MenuKeys.usedAim);
		MenuKeys.container.addFrame(MenuKeys.buttonAim);
		MenuKeys.buttonHUD = new SleekButton()
		{
			position = new Coord2(-100, 130, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_HUD, ": ", InputSettings.hudKey)
		};
		MenuKeys.buttonHUD.onUsed += new SleekDelegate(MenuKeys.usedHUD);
		MenuKeys.container.addFrame(MenuKeys.buttonHUD);
		MenuKeys.buttonNVG = new SleekButton()
		{
			position = new Coord2(-100, 180, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_NVG, ": ", InputSettings.nvgKey)
		};
		MenuKeys.buttonNVG.onUsed += new SleekDelegate(MenuKeys.usedNVG);
		MenuKeys.container.addFrame(MenuKeys.buttonNVG);
		MenuKeys.buttonDrop = new SleekButton()
		{
			position = new Coord2(-100, 230, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_DROP, ": ", InputSettings.dropKey)
		};
		MenuKeys.buttonDrop.onUsed += new SleekDelegate(MenuKeys.usedDrop);
		MenuKeys.container.addFrame(MenuKeys.buttonDrop);
		MenuKeys.buttonItem = new SleekButton()
		{
			position = new Coord2(-100, -220, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = string.Concat(Texts.LABEL_DEQUIP, ": ", InputSettings.itemKey)
		};
		MenuKeys.buttonItem.onUsed += new SleekDelegate(MenuKeys.usedItem);
		MenuKeys.container.addFrame(MenuKeys.buttonItem);
		MenuKeys.buttonBack = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(200, 40, 0f, 0f),
			text = Texts.LABEL_BACK
		};
		MenuKeys.buttonBack.onUsed += new SleekDelegate(MenuKeys.usedBack);
		MenuKeys.container.addFrame(MenuKeys.buttonBack);
		MenuKeys.iconBack = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		MenuKeys.iconBack.setImage("Textures/Icons/back");
		MenuKeys.buttonBack.addFrame(MenuKeys.iconBack);
		MenuKeys.buttonLookInvert = new SleekButton()
		{
			position = new Coord2(-100, -270, 0.5f, 0.5f),
			size = new Coord2(200, 40, 0f, 0f),
			text = (!InputSettings.lookInvert ? Texts.LABEL_LOOK_INVERT_OFF : Texts.LABEL_LOOK_INVERT_ON)
		};
		MenuKeys.buttonLookInvert.onUsed += new SleekDelegate(MenuKeys.usedLookInvert);
		MenuKeys.buttonLookInvert.tooltip = (!InputSettings.lookInvert ? Texts.TOOLTIP_LOOK_INVERT_OFF : Texts.TOOLTIP_LOOK_INVERT_ON);
		MenuKeys.container.addFrame(MenuKeys.buttonLookInvert);
		MenuKeys.binding = -1;
	}

	public static void bind(KeyCode key)
	{
		if (MenuKeys.binding == 0)
		{
			InputSettings.interactKey = key;
			MenuKeys.buttonInteract.text = string.Concat(Texts.LABEL_INTERACT, ": ", InputSettings.interactKey);
		}
		else if (MenuKeys.binding == 1)
		{
			InputSettings.emoteKey = key;
			MenuKeys.buttonEmote.text = string.Concat(Texts.LABEL_EMOTE, ": ", InputSettings.emoteKey);
		}
		else if (MenuKeys.binding == 2)
		{
			InputSettings.inventoryKey = key;
			MenuKeys.buttonInventory.text = string.Concat(Texts.LABEL_INVENTORY, ": ", InputSettings.inventoryKey);
		}
		else if (MenuKeys.binding == 3)
		{
			InputSettings.reloadKey = key;
			MenuKeys.buttonReload.text = string.Concat(Texts.LABEL_RELOAD, ": ", InputSettings.reloadKey);
		}
		else if (MenuKeys.binding == 4)
		{
			InputSettings.proneKey = key;
			MenuKeys.buttonProne.text = string.Concat(Texts.LABEL_PRONE, ": ", InputSettings.proneKey);
		}
		else if (MenuKeys.binding == 5)
		{
			InputSettings.crouchKey = key;
			MenuKeys.buttonCrouch.text = string.Concat(Texts.LABEL_CROUCH, ": ", InputSettings.crouchKey);
		}
		else if (MenuKeys.binding == 6)
		{
			InputSettings.firemodeKey = key;
			MenuKeys.buttonFiremode.text = string.Concat(Texts.LABEL_FIREMODE, ": ", InputSettings.firemodeKey);
		}
		else if (MenuKeys.binding == 7)
		{
			InputSettings.attachmentKey = key;
			MenuKeys.buttonAttachment.text = string.Concat(Texts.LABEL_ATTACHMENT, ": ", InputSettings.attachmentKey);
		}
		else if (MenuKeys.binding == 8)
		{
			InputSettings.chatKey = key;
			MenuKeys.buttonChat.text = string.Concat(Texts.LABEL_CHAT, ": ", InputSettings.chatKey);
		}
		else if (MenuKeys.binding == 9)
		{
			InputSettings.localKey = key;
			MenuKeys.buttonLocal.text = string.Concat(Texts.LABEL_LOCAL, ": ", InputSettings.localKey);
		}
		else if (MenuKeys.binding == 10)
		{
			InputSettings.clanKey = key;
			MenuKeys.buttonClan.text = string.Concat(Texts.LABEL_CLAN, ": ", InputSettings.clanKey);
		}
		else if (MenuKeys.binding == 11)
		{
			InputSettings.playersKey = key;
			MenuKeys.buttonPlayers.text = string.Concat(Texts.LABEL_PLAYERS, ": ", InputSettings.playersKey);
		}
		else if (MenuKeys.binding == 12)
		{
			InputSettings.sprintKey = key;
			MenuKeys.buttonSprint.text = string.Concat(Texts.LABEL_SPRINT, ": ", InputSettings.sprintKey);
		}
		else if (MenuKeys.binding == 13)
		{
			InputSettings.jumpKey = key;
			MenuKeys.buttonJump.text = string.Concat(Texts.LABEL_JUMP, ": ", InputSettings.jumpKey);
		}
		else if (MenuKeys.binding == 14)
		{
			InputSettings.leanLeftKey = key;
			MenuKeys.buttonLeanLeft.text = string.Concat(Texts.LABEL_LEANLEFT, ": ", InputSettings.leanLeftKey);
		}
		else if (MenuKeys.binding == 15)
		{
			InputSettings.leanRightKey = key;
			MenuKeys.buttonLeanRight.text = string.Concat(Texts.LABEL_LEANRIGHT, ": ", InputSettings.leanRightKey);
		}
		else if (MenuKeys.binding == 16)
		{
			InputSettings.voiceKey = key;
			MenuKeys.buttonVoice.text = string.Concat(Texts.LABEL_VOICE, ": ", InputSettings.voiceKey);
		}
		else if (MenuKeys.binding == 17)
		{
			InputSettings.otherKey = key;
			MenuKeys.buttonOther.text = string.Concat(Texts.LABEL_OTHER, ": ", InputSettings.otherKey);
		}
		else if (MenuKeys.binding == 18)
		{
			InputSettings.upKey = key;
			MenuKeys.buttonUp.text = string.Concat(Texts.LABEL_UP, ": ", InputSettings.upKey);
		}
		else if (MenuKeys.binding == 19)
		{
			InputSettings.leftKey = key;
			MenuKeys.buttonLeft.text = string.Concat(Texts.LABEL_LEFT, ": ", InputSettings.leftKey);
		}
		else if (MenuKeys.binding == 20)
		{
			InputSettings.rightKey = key;
			MenuKeys.buttonRight.text = string.Concat(Texts.LABEL_RIGHT, ": ", InputSettings.rightKey);
		}
		else if (MenuKeys.binding == 21)
		{
			InputSettings.downKey = key;
			MenuKeys.buttonDown.text = string.Concat(Texts.LABEL_DOWN, ": ", InputSettings.downKey);
		}
		else if (MenuKeys.binding == 22)
		{
			InputSettings.shootKey = key;
			MenuKeys.buttonShoot.text = string.Concat(Texts.LABEL_SHOOT, ": ", InputSettings.shootKey);
		}
		else if (MenuKeys.binding == 23)
		{
			InputSettings.aimKey = key;
			MenuKeys.buttonAim.text = string.Concat(Texts.LABEL_AIM, ": ", InputSettings.aimKey);
		}
		else if (MenuKeys.binding == 24)
		{
			InputSettings.hudKey = key;
			MenuKeys.buttonHUD.text = string.Concat(Texts.LABEL_HUD, ": ", InputSettings.hudKey);
		}
		else if (MenuKeys.binding == 25)
		{
			InputSettings.nvgKey = key;
			MenuKeys.buttonNVG.text = string.Concat(Texts.LABEL_NVG, ": ", InputSettings.nvgKey);
		}
		else if (MenuKeys.binding == 26)
		{
			InputSettings.dropKey = key;
			MenuKeys.buttonDrop.text = string.Concat(Texts.LABEL_DROP, ": ", InputSettings.dropKey);
		}
		else if (MenuKeys.binding == 27)
		{
			InputSettings.itemKey = key;
			MenuKeys.buttonItem.text = string.Concat(Texts.LABEL_DEQUIP, ": ", InputSettings.itemKey);
		}
		MenuKeys.binding = -1;
		InputSettings.save();
	}

	public static void close()
	{
		if (Application.loadedLevel == 0)
		{
			MenuKeys.container.position = new Coord2(0, 0, -1f, 0f);
			MenuKeys.container.lerp(new Coord2(0, 0, 1f, 0f), MenuKeys.container.size, 4f, true);
		}
		else
		{
			MenuKeys.container.visible = false;
		}
	}

	public static void open()
	{
		if (Application.loadedLevel == 0)
		{
			MenuKeys.container.visible = true;
			MenuKeys.container.position = new Coord2(0, 0, 1f, 0f);
			MenuKeys.container.lerp(new Coord2(0, 0, -1f, 0f), MenuKeys.container.size, 4f);
		}
		else
		{
			MenuKeys.container.visible = true;
		}
	}

	public static void usedAim(SleekFrame frame)
	{
		if (MenuKeys.binding != 23)
		{
			MenuKeys.binding = 23;
			MenuKeys.buttonAim.text = string.Concat(Texts.LABEL_AIM, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonAim.text = string.Concat(Texts.LABEL_AIM, ": ", InputSettings.aimKey);
		}
	}

	public static void usedAttachment(SleekFrame frame)
	{
		if (MenuKeys.binding != 7)
		{
			MenuKeys.binding = 7;
			MenuKeys.buttonAttachment.text = string.Concat(Texts.LABEL_ATTACHMENT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonAttachment.text = string.Concat(Texts.LABEL_ATTACHMENT, ": ", InputSettings.attachmentKey);
		}
	}

	public static void usedBack(SleekFrame frame)
	{
		if (Application.loadedLevel == 0)
		{
			MenuKeys.close();
			MenuConfigure.open();
		}
		else
		{
			MenuKeys.close();
			//HUDPause.closeMini();
		}
	}

	public static void usedChat(SleekFrame frame)
	{
		if (MenuKeys.binding != 8)
		{
			MenuKeys.binding = 8;
			MenuKeys.buttonChat.text = string.Concat(Texts.LABEL_CHAT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonChat.text = string.Concat(Texts.LABEL_CHAT, ": ", InputSettings.chatKey);
		}
	}

	public static void usedClan(SleekFrame frame)
	{
		if (MenuKeys.binding != 10)
		{
			MenuKeys.binding = 10;
			MenuKeys.buttonClan.text = string.Concat(Texts.LABEL_CLAN, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonClan.text = string.Concat(Texts.LABEL_CLAN, ": ", InputSettings.clanKey);
		}
	}

	public static void usedCrouch(SleekFrame frame)
	{
		if (MenuKeys.binding != 5)
		{
			MenuKeys.binding = 5;
			MenuKeys.buttonCrouch.text = string.Concat(Texts.LABEL_CROUCH, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonCrouch.text = string.Concat(Texts.LABEL_CROUCH, ": ", InputSettings.crouchKey);
		}
	}

	public static void usedDown(SleekFrame frame)
	{
		if (MenuKeys.binding != 21)
		{
			MenuKeys.binding = 21;
			MenuKeys.buttonDown.text = string.Concat(Texts.LABEL_DOWN, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonDown.text = string.Concat(Texts.LABEL_DOWN, ": ", InputSettings.downKey);
		}
	}

	public static void usedDrop(SleekFrame frame)
	{
		if (MenuKeys.binding != 26)
		{
			MenuKeys.binding = 26;
			MenuKeys.buttonDrop.text = string.Concat(Texts.LABEL_DROP, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonDrop.text = string.Concat(Texts.LABEL_DROP, ": ", InputSettings.dropKey);
		}
	}

	public static void usedEmote(SleekFrame frame)
	{
		if (MenuKeys.binding != 1)
		{
			MenuKeys.binding = 1;
			MenuKeys.buttonEmote.text = string.Concat(Texts.LABEL_EMOTE, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonEmote.text = string.Concat(Texts.LABEL_EMOTE, ": ", InputSettings.emoteKey);
		}
	}

	public static void usedFiremode(SleekFrame frame)
	{
		if (MenuKeys.binding != 6)
		{
			MenuKeys.binding = 6;
			MenuKeys.buttonFiremode.text = string.Concat(Texts.LABEL_FIREMODE, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonFiremode.text = string.Concat(Texts.LABEL_FIREMODE, ": ", InputSettings.firemodeKey);
		}
	}

	public static void usedHUD(SleekFrame frame)
	{
		if (MenuKeys.binding != 24)
		{
			MenuKeys.binding = 24;
			MenuKeys.buttonHUD.text = string.Concat(Texts.LABEL_HUD, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonHUD.text = string.Concat(Texts.LABEL_HUD, ": ", InputSettings.hudKey);
		}
	}

	public static void usedInteract(SleekFrame frame)
	{
		if (MenuKeys.binding != 0)
		{
			MenuKeys.binding = 0;
			MenuKeys.buttonInteract.text = string.Concat(Texts.LABEL_INTERACT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonInteract.text = string.Concat(Texts.LABEL_INTERACT, ": ", InputSettings.interactKey);
		}
	}

	public static void usedInventory(SleekFrame frame)
	{
		if (MenuKeys.binding != 2)
		{
			MenuKeys.binding = 2;
			MenuKeys.buttonInventory.text = string.Concat(Texts.LABEL_INVENTORY, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonInventory.text = string.Concat(Texts.LABEL_INVENTORY, ": ", InputSettings.inventoryKey);
		}
	}

	public static void usedItem(SleekFrame frame)
	{
		if (MenuKeys.binding != 27)
		{
			MenuKeys.binding = 27;
			MenuKeys.buttonItem.text = string.Concat(Texts.LABEL_DEQUIP, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonItem.text = string.Concat(Texts.LABEL_DEQUIP, ": ", InputSettings.itemKey);
		}
	}

	public static void usedJump(SleekFrame frame)
	{
		if (MenuKeys.binding != 13)
		{
			MenuKeys.binding = 13;
			MenuKeys.buttonJump.text = string.Concat(Texts.LABEL_JUMP, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonJump.text = string.Concat(Texts.LABEL_JUMP, ": ", InputSettings.jumpKey);
		}
	}

	public static void usedLeanLeft(SleekFrame frame)
	{
		if (MenuKeys.binding != 14)
		{
			MenuKeys.binding = 14;
			MenuKeys.buttonLeanLeft.text = string.Concat(Texts.LABEL_LEANLEFT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonLeanLeft.text = string.Concat(Texts.LABEL_LEANLEFT, ": ", InputSettings.leanLeftKey);
		}
	}

	public static void usedLeanRight(SleekFrame frame)
	{
		if (MenuKeys.binding != 15)
		{
			MenuKeys.binding = 15;
			MenuKeys.buttonLeanRight.text = string.Concat(Texts.LABEL_LEANRIGHT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonLeanRight.text = string.Concat(Texts.LABEL_LEANRIGHT, ": ", InputSettings.leanRightKey);
		}
	}

	public static void usedLeft(SleekFrame frame)
	{
		if (MenuKeys.binding != 19)
		{
			MenuKeys.binding = 19;
			MenuKeys.buttonLeft.text = string.Concat(Texts.LABEL_LEFT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonLeft.text = string.Concat(Texts.LABEL_LEFT, ": ", InputSettings.leftKey);
		}
	}

	public static void usedLocal(SleekFrame frame)
	{
		if (MenuKeys.binding != 9)
		{
			MenuKeys.binding = 9;
			MenuKeys.buttonLocal.text = string.Concat(Texts.LABEL_LOCAL, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonLocal.text = string.Concat(Texts.LABEL_LOCAL, ": ", InputSettings.localKey);
		}
	}

	public static void usedLookInvert(SleekFrame frame)
	{
		InputSettings.lookInvert = !InputSettings.lookInvert;
		MenuKeys.buttonLookInvert.text = (!InputSettings.lookInvert ? Texts.LABEL_LOOK_INVERT_OFF : Texts.LABEL_LOOK_INVERT_ON);
		MenuKeys.buttonLookInvert.tooltip = (!InputSettings.lookInvert ? Texts.TOOLTIP_LOOK_INVERT_OFF : Texts.TOOLTIP_LOOK_INVERT_ON);
		GameSettings.save();
	}

	public static void usedNVG(SleekFrame frame)
	{
		if (MenuKeys.binding != 25)
		{
			MenuKeys.binding = 25;
			MenuKeys.buttonNVG.text = string.Concat(Texts.LABEL_NVG, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonNVG.text = string.Concat(Texts.LABEL_NVG, ": ", InputSettings.nvgKey);
		}
	}

	public static void usedOther(SleekFrame frame)
	{
		if (MenuKeys.binding != 17)
		{
			MenuKeys.binding = 17;
			MenuKeys.buttonOther.text = string.Concat(Texts.LABEL_OTHER, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonOther.text = string.Concat(Texts.LABEL_OTHER, ": ", InputSettings.otherKey);
		}
	}

	public static void usedPlayers(SleekFrame frame)
	{
		if (MenuKeys.binding != 11)
		{
			MenuKeys.binding = 11;
			MenuKeys.buttonPlayers.text = string.Concat(Texts.LABEL_PLAYERS, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonPlayers.text = string.Concat(Texts.LABEL_PLAYERS, ": ", InputSettings.playersKey);
		}
	}

	public static void usedProne(SleekFrame frame)
	{
		if (MenuKeys.binding != 4)
		{
			MenuKeys.binding = 4;
			MenuKeys.buttonProne.text = string.Concat(Texts.LABEL_PRONE, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonReload.text = string.Concat(Texts.LABEL_PRONE, ": ", InputSettings.proneKey);
		}
	}

	public static void usedReload(SleekFrame frame)
	{
		if (MenuKeys.binding != 3)
		{
			MenuKeys.binding = 3;
			MenuKeys.buttonReload.text = string.Concat(Texts.LABEL_RELOAD, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonReload.text = string.Concat(Texts.LABEL_RELOAD, ": ", InputSettings.reloadKey);
		}
	}

	public static void usedRight(SleekFrame frame)
	{
		if (MenuKeys.binding != 20)
		{
			MenuKeys.binding = 20;
			MenuKeys.buttonRight.text = string.Concat(Texts.LABEL_RIGHT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonRight.text = string.Concat(Texts.LABEL_RIGHT, ": ", InputSettings.rightKey);
		}
	}

	public static void usedShoot(SleekFrame frame)
	{
		if (MenuKeys.binding != 22)
		{
			MenuKeys.binding = 22;
			MenuKeys.buttonShoot.text = string.Concat(Texts.LABEL_SHOOT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonShoot.text = string.Concat(Texts.LABEL_SHOOT, ": ", InputSettings.shootKey);
		}
	}

	public static void usedSprint(SleekFrame frame)
	{
		if (MenuKeys.binding != 12)
		{
			MenuKeys.binding = 12;
			MenuKeys.buttonSprint.text = string.Concat(Texts.LABEL_SPRINT, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonSprint.text = string.Concat(Texts.LABEL_SPRINT, ": ", InputSettings.sprintKey);
		}
	}

	public static void usedUp(SleekFrame frame)
	{
		if (MenuKeys.binding != 18)
		{
			MenuKeys.binding = 18;
			MenuKeys.buttonUp.text = string.Concat(Texts.LABEL_UP, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonUp.text = string.Concat(Texts.LABEL_UP, ": ", InputSettings.upKey);
		}
	}

	public static void usedVoice(SleekFrame frame)
	{
		if (MenuKeys.binding != 16)
		{
			MenuKeys.binding = 16;
			MenuKeys.buttonVoice.text = string.Concat(Texts.LABEL_VOICE, ": ?");
		}
		else
		{
			MenuKeys.binding = -1;
			MenuKeys.buttonVoice.text = string.Concat(Texts.LABEL_VOICE, ": ", InputSettings.voiceKey);
		}
	}
}