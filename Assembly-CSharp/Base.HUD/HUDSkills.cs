using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDSkills
{
	public static bool state;

	public static SleekBox box;

	public static SleekSlider skillSlider;

	public static SleekBox title;

	public static SleekImage iconExperience;

	public static SleekButton[] skills;

	public static int offset;

	public HUDSkills()
	{
		HUDSkills.box = new SleekBox()
		{
			position = new Coord2(10, 0, 1f, -5f),
			size = new Coord2(320, 40, 0f, 0f)
		};
		HUDInventory.box.addFrame(HUDSkills.box);
		HUDSkills.box.visible = false;
		HUDSkills.skillSlider = new SleekSlider()
		{
			position = new Coord2(-30, 60, 1f, 0f),
			size = new Coord2(20, -70, 0f, 1f),
			orientation = Orient2.VERTICAL
		};
		HUDSkills.skillSlider.onUsed += new SleekDelegate(HUDSkills.usedSlider);
		HUDSkills.box.addFrame(HUDSkills.skillSlider);
		HUDSkills.title = new SleekBox()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(-20, 40, 1f, 0f)
		};
		HUDSkills.box.addFrame(HUDSkills.title);
		HUDSkills.iconExperience = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDSkills.iconExperience.setImage("Textures/Icons/experience");
		HUDSkills.title.addFrame(HUDSkills.iconExperience);
		HUDSkills.state = false;
		HUDSkills.offset = 0;
	}

	public static void close()
	{
		HUDSkills.state = false;
		HUDSkills.box.position = new Coord2(10, -(int)HUDSkills.skills.Length * 25 - 30, 1f, 0.5f);
		HUDSkills.box.lerp(new Coord2(10, -(int)HUDSkills.skills.Length * 25 - 30, 1f, -5f), HUDSkills.box.size, 4f, true);
	}

	public static void open()
	{
		HUDSkills.state = true;
		HUDSkills.box.position = new Coord2(10, -(int)HUDSkills.skills.Length * 25 - 5, 1f, -5f);
		HUDSkills.box.lerp(new Coord2(10, -(int)HUDSkills.skills.Length * 25 - 30, 1f, 0.5f), HUDSkills.box.size, 4f);
		HUDSkills.box.visible = true;
	}

	public static void updateExperience()
	{
		HUDSkills.title.text = string.Concat("Experience: ", Player.skills.experience);
	}

	public static void updateLevel(int index)
	{
		if (index >= HUDSkills.offset && index < (int)HUDSkills.skills.Length + HUDSkills.offset)
		{
			for (int i = 0; i < Player.skills.skills[index].maxLevel; i++)
			{
				if (i >= Player.skills.skills[index].level)
				{
					((SleekImage)HUDSkills.skills[index - HUDSkills.offset].children[HUDSkills.skills[index - HUDSkills.offset].children.Count - 2 - i]).setImage("Textures/Icons/noSkill");
				}
				else
				{
					((SleekImage)HUDSkills.skills[index - HUDSkills.offset].children[HUDSkills.skills[index - HUDSkills.offset].children.Count - 2 - i]).setImage("Textures/Icons/yesSkill");
				}
			}
			if (Player.skills.skills[index].level != Player.skills.skills[index].maxLevel)
			{
				((SleekLabel)HUDSkills.skills[index - HUDSkills.offset].children[HUDSkills.skills[index - HUDSkills.offset].children.Count - 1]).text = string.Concat("Cost: ", Player.skills.skills[index].getCost());
			}
			else
			{
				((SleekLabel)HUDSkills.skills[index - HUDSkills.offset].children[HUDSkills.skills[index - HUDSkills.offset].children.Count - 1]).text = "Max Level";
			}
		}
	}

	public static void updateSkills()
	{
		HUDSkills.offset = (int)Mathf.Ceil((float)((int)Player.skills.skills.Length - 6) * HUDSkills.skillSlider.state);
		HUDSkills.skillSlider.scale = 6f / (float)((int)Player.skills.skills.Length);
		if (HUDSkills.skills != null)
		{
			for (int i = 0; i < (int)HUDSkills.skills.Length; i++)
			{
				HUDSkills.skills[i].@remove();
			}
		}
		HUDSkills.skills = new SleekButton[6];
		for (int j = 0; j < (int)HUDSkills.skills.Length; j++)
		{
			SleekButton sleekButton = new SleekButton()
			{
				position = new Coord2(10, 60 + j * 50, 0f, 0f),
				size = new Coord2(-50, 40, 1f, 0f),
				tooltip = Player.skills.skills[j + HUDSkills.offset].description
			};
			HUDSkills.box.addFrame(sleekButton);
			SleekImage sleekImage = new SleekImage()
			{
				position = new Coord2(4, 4, 0f, 0f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			sleekImage.setImage(string.Concat("Textures/Skills/", Player.skills.skills[j + HUDSkills.offset].id));
			sleekButton.addFrame(sleekImage);
			SleekLabel sleekLabel = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, 20, 1f, 0f),
				text = Player.skills.skills[j + HUDSkills.offset].name,
				alignment = TextAnchor.MiddleLeft
			};
			sleekButton.addFrame(sleekLabel);
			for (int k = 0; k < Player.skills.skills[j + HUDSkills.offset].maxLevel; k++)
			{
				SleekImage sleekImage1 = new SleekImage()
				{
					position = new Coord2(-12 - k * 12, 4, 1f, 0f),
					size = new Coord2(8, 32, 0f, 0f)
				};
				if (Player.skills.skills[j + HUDSkills.offset].maxLevel - k - 1 >= Player.skills.skills[j + HUDSkills.offset].level)
				{
					sleekImage1.setImage("Textures/Icons/noSkill");
				}
				else
				{
					sleekImage1.setImage("Textures/Icons/yesSkill");
				}
				sleekButton.addFrame(sleekImage1);
			}
			SleekLabel sleekLabel1 = new SleekLabel()
			{
				position = new Coord2(50, 10, 0f, 0f),
				size = new Coord2(-60, 20, 1f, 0f),
				alignment = TextAnchor.MiddleRight
			};
			if (Player.skills.skills[j + HUDSkills.offset].level != Player.skills.skills[j + HUDSkills.offset].maxLevel)
			{
				sleekLabel1.text = string.Concat("Cost: ", Player.skills.skills[j + HUDSkills.offset].getCost());
			}
			else
			{
				sleekLabel1.text = "Max Level";
			}
			sleekButton.addFrame(sleekLabel1);
			HUDSkills.skills[j] = sleekButton;
			sleekButton.onUsed += new SleekDelegate(HUDSkills.usedSkill);
		}
		HUDSkills.box.size = new Coord2(320, 60 + (int)HUDSkills.skills.Length * 50, 0f, 0f);
		HUDSkills.box.position = new Coord2(10, -(int)HUDSkills.skills.Length * 25 - 30, 1f, 0.5f);
	}

	public static void usedSkill(SleekFrame frame)
	{
		int offsetY = (frame.position.offset_y - 60) / 50 + HUDSkills.offset;
		if (Player.skills.skills[offsetY].level >= Player.skills.skills[offsetY].maxLevel)
		{
			HUDGame.openError(Texts.ERROR_LEVEL, "Textures/Icons/error");
		}
		else if (Player.skills.experience < Player.skills.skills[offsetY].getCost())
		{
			HUDGame.openError(Texts.ERROR_COST, "Textures/Icons/error");
		}
		else
		{
			Player.skills.sendUpgrade(offsetY);
		}
	}

	public static void usedSlider(SleekFrame frame)
	{
		HUDSkills.updateSkills();
	}
}