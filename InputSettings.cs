using System;
using UnityEngine;

public class InputSettings
{
	public static KeyCode jumpKey;

	public static KeyCode sprintKey;

	public static KeyCode proneKey;

	public static KeyCode crouchKey;

	public static KeyCode inventoryKey;

	public static KeyCode reloadKey;

	public static KeyCode leanLeftKey;

	public static KeyCode leanRightKey;

	public static KeyCode emoteKey;

	public static KeyCode firemodeKey;

	public static KeyCode attachmentKey;

	public static KeyCode chatKey;

	public static KeyCode localKey;

	public static KeyCode clanKey;

	public static KeyCode interactKey;

	public static KeyCode playersKey;

	public static KeyCode voiceKey;

	public static KeyCode otherKey;

	public static KeyCode upKey;

	public static KeyCode leftKey;

	public static KeyCode rightKey;

	public static KeyCode downKey;

	public static KeyCode shootKey;

	public static KeyCode aimKey;

	public static KeyCode hudKey;

	public static KeyCode nvgKey;

	public static KeyCode dropKey;

	public static KeyCode itemKey;

	public static bool lookInvert;

	public static bool inventoryToggle;

	public static bool proneToggle;

	public static bool crouchToggle;

	public static bool aimToggle;

	public static float mouseSensitivity;

	static InputSettings()
	{
		InputSettings.jumpKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Space", 32);
		InputSettings.sprintKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Sprint", 304);
		InputSettings.proneKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Prone", 122);
		InputSettings.crouchKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Crouch", 120);
		InputSettings.inventoryKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Inventory", 9);
		InputSettings.reloadKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Reload", 114);
		InputSettings.leanLeftKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_LeanLeft", 113);
		InputSettings.leanRightKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_LeanRight", 101);
		InputSettings.emoteKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Emote", 103);
		InputSettings.firemodeKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Firemode", 118);
		InputSettings.attachmentKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Attachment", 116);
		InputSettings.chatKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Global", 106);
		InputSettings.localKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Local", 107);
		InputSettings.clanKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Clan", 108);
		InputSettings.interactKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Interact", 102);
		InputSettings.playersKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Players", 112);
		InputSettings.voiceKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Voice", 308);
		InputSettings.otherKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Other", 306);
		InputSettings.upKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Up", 119);
		InputSettings.leftKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Left", 97);
		InputSettings.rightKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Right", 100);
		InputSettings.downKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Down", 115);
		InputSettings.shootKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Shoot", 323);
		InputSettings.aimKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Aim", 324);
		InputSettings.hudKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_HUD", 278);
		InputSettings.nvgKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_NVG", 110);
		InputSettings.dropKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Drop", 43);
		InputSettings.itemKey = (KeyCode)PlayerPrefs.GetInt("inputSettings_Item", 8);
		InputSettings.lookInvert = PlayerPrefs.GetInt("inputSettings_LookInvert", 0) == 1;
		InputSettings.inventoryToggle = PlayerPrefs.GetInt("inputSettings_InventoryToggle", 1) == 1;
		InputSettings.proneToggle = PlayerPrefs.GetInt("inputSettings_ProneToggle", 1) == 1;
		InputSettings.crouchToggle = PlayerPrefs.GetInt("inputSettings_CrouchToggle", 1) == 1;
		InputSettings.aimToggle = PlayerPrefs.GetInt("inputSettings_AimToggle", 0) == 1;
		InputSettings.mouseSensitivity = PlayerPrefs.GetFloat("inputSettings_MouseSensitivity", 4f);
	}

	public InputSettings()
	{
	}

	public static float getX()
	{
		if (Input.GetKey(InputSettings.leftKey))
		{
			return -1f;
		}
		if (Input.GetKey(InputSettings.rightKey))
		{
			return 1f;
		}
		return 0f;
	}

	public static float getY()
	{
		if (Input.GetKey(InputSettings.downKey))
		{
			return -1f;
		}
		if (Input.GetKey(InputSettings.upKey))
		{
			return 1f;
		}
		return 0f;
	}

	public static void save()
	{
		PlayerPrefs.SetInt("inputSettings_Space", (int)InputSettings.jumpKey);
		PlayerPrefs.SetInt("inputSettings_Sprint", (int)InputSettings.sprintKey);
		PlayerPrefs.SetInt("inputSettings_Prone", (int)InputSettings.proneKey);
		PlayerPrefs.SetInt("inputSettings_Crouch", (int)InputSettings.crouchKey);
		PlayerPrefs.SetInt("inputSettings_Inventory", (int)InputSettings.inventoryKey);
		PlayerPrefs.SetInt("inputSettings_Reload", (int)InputSettings.reloadKey);
		PlayerPrefs.SetInt("inputSettings_LeanLeft", (int)InputSettings.leanLeftKey);
		PlayerPrefs.SetInt("inputSettings_LeanRight", (int)InputSettings.leanRightKey);
		PlayerPrefs.SetInt("inputSettings_Emote", (int)InputSettings.emoteKey);
		PlayerPrefs.SetInt("inputSettings_Firemode", (int)InputSettings.firemodeKey);
		PlayerPrefs.SetInt("inputSettings_Attachment", (int)InputSettings.attachmentKey);
		PlayerPrefs.SetInt("inputSettings_Global", (int)InputSettings.chatKey);
		PlayerPrefs.SetInt("inputSettings_Local", (int)InputSettings.localKey);
		PlayerPrefs.SetInt("inputSettings_Clan", (int)InputSettings.clanKey);
		PlayerPrefs.SetInt("inputSettings_Interact", (int)InputSettings.interactKey);
		PlayerPrefs.SetInt("inputSettings_Players", (int)InputSettings.playersKey);
		PlayerPrefs.SetInt("inputSettings_Voice", (int)InputSettings.voiceKey);
		PlayerPrefs.SetInt("inputSettings_Other", (int)InputSettings.otherKey);
		PlayerPrefs.SetInt("inputSettings_Up", (int)InputSettings.upKey);
		PlayerPrefs.SetInt("inputSettings_Left", (int)InputSettings.leftKey);
		PlayerPrefs.SetInt("inputSettings_Right", (int)InputSettings.rightKey);
		PlayerPrefs.SetInt("inputSettings_Down", (int)InputSettings.downKey);
		PlayerPrefs.SetInt("inputSettings_Shoot", (int)InputSettings.shootKey);
		PlayerPrefs.SetInt("inputSettings_Aim", (int)InputSettings.aimKey);
		PlayerPrefs.SetInt("inputSettings_HUD", (int)InputSettings.hudKey);
		PlayerPrefs.SetInt("inputSettings_NVG", (int)InputSettings.nvgKey);
		PlayerPrefs.SetInt("inputSettings_Drop", (int)InputSettings.dropKey);
		PlayerPrefs.SetInt("inputSettings_Item", (int)InputSettings.itemKey);
		PlayerPrefs.SetInt("inputSettings_LookInvert", (!InputSettings.lookInvert ? 0 : 1));
		PlayerPrefs.SetInt("inputSettings_InventoryToggle", (!InputSettings.inventoryToggle ? 0 : 1));
		PlayerPrefs.SetInt("inputSettings_ProneToggle", (!InputSettings.proneToggle ? 0 : 1));
		PlayerPrefs.SetInt("inputSettings_CrouchToggle", (!InputSettings.crouchToggle ? 0 : 1));
		PlayerPrefs.SetInt("inputSettings_AimToggle", (!InputSettings.aimToggle ? 0 : 1));
		PlayerPrefs.SetFloat("inputSettings_MouseSensitivity", InputSettings.mouseSensitivity);
	}
}