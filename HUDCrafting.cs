using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDCrafting
{
	public static bool state;

	public static SleekBox box;

	public static SleekButton[] items;

	public static SleekButton buttonCraft;

	public static SleekImage iconCraft;

	public static SleekButton buttonRevert;

	public static SleekImage iconRevert;

	public static SleekButton noneButton;

	public static SleekImage noneIcon;

	public static SleekButton fireButton;

	public static SleekImage fireIcon;

	public static Point2[] crafts;

	private static bool available;

	private static bool fire;

	private static int mode;

	public HUDCrafting()
	{
		HUDCrafting.box = new SleekBox()
		{
			position = new Coord2(10, -190, 1f, -5f),
			size = new Coord2(230, 380, 0f, 0f)
		};
		HUDInventory.box.addFrame(HUDCrafting.box);
		HUDCrafting.box.visible = false;
		HUDCrafting.crafts = new Point2[4];
		for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
		{
			HUDCrafting.crafts[i] = Point2.NONE;
		}
		HUDCrafting.items = new SleekButton[(int)HUDCrafting.crafts.Length];
		for (int j = 0; j < (int)HUDCrafting.crafts.Length; j++)
		{
			SleekButton sleekButton = new SleekButton();
			if (j == 0)
			{
				sleekButton.position = new Coord2(10, 10, 0f, 0f);
			}
			else if (j == 1)
			{
				sleekButton.position = new Coord2(120, 10, 0f, 0f);
			}
			else if (j != 2)
			{
				sleekButton.position = new Coord2(120, 120, 0f, 0f);
			}
			else
			{
				sleekButton.position = new Coord2(10, 120, 0f, 0f);
			}
			sleekButton.size = new Coord2(100, 100, 0f, 0f);
			HUDCrafting.box.addFrame(sleekButton);
			SleekImage sleekImage = new SleekImage()
			{
				position = new Coord2(-16, -16, 0.5f, 0.5f),
				size = new Coord2(32, 32, 0f, 0f)
			};
			sleekButton.addFrame(sleekImage);
			SleekLabel sleekLabel = new SleekLabel()
			{
				position = new Coord2(10, -30, 0f, 1f),
				size = new Coord2(-20, 20, 1f, 0f),
				alignment = TextAnchor.MiddleRight
			};
			sleekButton.addFrame(sleekLabel);
			HUDCrafting.items[j] = sleekButton;
			sleekButton.onUsed += new SleekDelegate(HUDCrafting.usedItem);
		}
		HUDCrafting.buttonCraft = new SleekButton()
		{
			position = new Coord2(10, 280, 0f, 0f),
			size = new Coord2(210, 40, 0f, 0f)
		};
		HUDCrafting.buttonCraft.onUsed += new SleekDelegate(HUDCrafting.usedCraft);
		HUDCrafting.box.addFrame(HUDCrafting.buttonCraft);
		HUDCrafting.iconCraft = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDCrafting.iconCraft.setImage("Textures/Icons/go");
		HUDCrafting.buttonCraft.addFrame(HUDCrafting.iconCraft);
		HUDCrafting.buttonRevert = new SleekButton()
		{
			position = new Coord2(10, 330, 0f, 0f),
			size = new Coord2(210, 40, 0f, 0f),
			text = Texts.LABEL_REVERT
		};
		HUDCrafting.buttonRevert.onUsed += new SleekDelegate(HUDCrafting.usedRevert);
		HUDCrafting.box.addFrame(HUDCrafting.buttonRevert);
		HUDCrafting.iconRevert = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDCrafting.iconRevert.setImage("Textures/Icons/back");
		HUDCrafting.buttonRevert.addFrame(HUDCrafting.iconRevert);
		HUDCrafting.noneButton = new SleekButton()
		{
			position = new Coord2(10, 230, 0f, 0f),
			size = new Coord2(40, 40, 0f, 0f)
		};
		HUDCrafting.noneButton.onUsed += new SleekDelegate(HUDCrafting.usedNone);
		HUDCrafting.box.addFrame(HUDCrafting.noneButton);
		HUDCrafting.noneIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDCrafting.noneIcon.setImage("Textures/Icons/craft");
		HUDCrafting.noneButton.addFrame(HUDCrafting.noneIcon);
		HUDCrafting.fireButton = new SleekButton()
		{
			position = new Coord2(60, 230, 0f, 0f),
			size = new Coord2(40, 40, 0f, 0f),
			visible = false
		};
		HUDCrafting.fireButton.onUsed += new SleekDelegate(HUDCrafting.usedFire);
		HUDCrafting.box.addFrame(HUDCrafting.fireButton);
		HUDCrafting.fireIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDCrafting.fireIcon.setImage("Textures/Icons/fire");
		HUDCrafting.fireButton.addFrame(HUDCrafting.fireIcon);
		HUDCrafting.updateItems();
		HUDCrafting.state = false;
		HUDCrafting.available = false;
	}

	public static void close()
	{
		HUDCrafting.state = false;
		HUDCrafting.box.position = new Coord2(10, -190, 1f, 0.5f);
		HUDCrafting.box.lerp(new Coord2(10, -190, 1f, -5f), HUDCrafting.box.size, 4f, true);
		for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
		{
			HUDCrafting.crafts[i] = Point2.NONE;
		}
		HUDCrafting.updateItems();
		HUDInventory.updateItems();
	}

	public static bool isReferred(int x, int y)
	{
		for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
		{
			if (HUDCrafting.crafts[i].x == x && HUDCrafting.crafts[i].y == y)
			{
				return true;
			}
		}
		return false;
	}

	public static void open()
	{
		HUDCrafting.state = true;
		HUDCrafting.box.position = new Coord2(10, -190, 1f, -5f);
		HUDCrafting.box.lerp(new Coord2(10, -190, 1f, 0.5f), HUDCrafting.box.size, 4f);
		HUDCrafting.box.visible = true;
		HUDCrafting.fire = Cooking.fire(Player.model.transform.position);
		HUDCrafting.fireButton.visible = HUDCrafting.fire;
		if (HUDCrafting.mode == 1 && !HUDCrafting.fire)
		{
			HUDCrafting.mode = 0;
		}
		HUDCrafting.updateItems();
	}

	public static void updateItems()
	{
		for (int i = 0; i < (int)HUDCrafting.crafts.Length - 1; i++)
		{
			if (HUDCrafting.crafts[i].x != -1 && Player.inventory.items[HUDCrafting.crafts[i].x, HUDCrafting.crafts[i].y].id == -1)
			{
				HUDCrafting.crafts[i] = Point2.NONE;
			}
		}
		for (int j = 0; j < (int)HUDCrafting.crafts.Length - 1; j++)
		{
			if (HUDCrafting.crafts[j].x != -1)
			{
				((SleekImage)HUDCrafting.items[j].children[0]).setImage(string.Concat("Textures/Items/", Player.inventory.items[HUDCrafting.crafts[j].x, HUDCrafting.crafts[j].y].id));
				if (ItemType.getType(Player.inventory.items[HUDCrafting.crafts[j].x, HUDCrafting.crafts[j].y].id) != 10)
				{
					((SleekLabel)HUDCrafting.items[j].children[1]).text = string.Concat("x", Player.inventory.items[HUDCrafting.crafts[j].x, HUDCrafting.crafts[j].y].amount);
				}
				else
				{
					((SleekLabel)HUDCrafting.items[j].children[1]).text = string.Concat("x", Player.inventory.items[HUDCrafting.crafts[j].x, HUDCrafting.crafts[j].y].amount - 1);
				}
				HUDCrafting.items[j].tooltip = ItemName.getName(Player.inventory.items[HUDCrafting.crafts[j].x, HUDCrafting.crafts[j].y].id);
			}
			else if (j != 2)
			{
				((SleekImage)HUDCrafting.items[j].children[0]).setImage("Textures/Icons/supply");
				((SleekLabel)HUDCrafting.items[j].children[1]).text = string.Empty;
				HUDCrafting.items[j].tooltip = "Supply";
			}
			else if (HUDCrafting.mode != 1)
			{
				((SleekImage)HUDCrafting.items[j].children[0]).setImage("Textures/Icons/tool");
				((SleekLabel)HUDCrafting.items[j].children[1]).text = string.Empty;
				HUDCrafting.items[j].tooltip = "Tool";
			}
			else
			{
				((SleekImage)HUDCrafting.items[j].children[0]).setImage("Textures/Icons/fire");
				((SleekLabel)HUDCrafting.items[j].children[1]).text = string.Empty;
				HUDCrafting.items[j].tooltip = "Fire";
			}
		}
		HUDCrafting.buttonCraft.text = Texts.LABEL_CRAFT;
		HUDCrafting.available = false;
		if (HUDCrafting.crafts[0].x == -1 || HUDCrafting.crafts[1].x == -1 || ItemType.getType(Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].id) != 10 || ItemType.getType(Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id) != 10 || !AmmoStats.getCaliberCompatible(Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].id, Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id) || Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].amount <= 1 || Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].amount <= 1 || HUDCrafting.mode != 0)
		{
			int num = 0;
			while (num < (int)Blueprints.prints.Length)
			{
				Blueprint blueprint = Blueprints.prints[num];
				if ((!blueprint.fire || HUDCrafting.mode != 1) && (blueprint.fire || HUDCrafting.mode != 0) || !(Player.skills != null) || blueprint.knowledge != 0f && Player.skills.crafting() < blueprint.knowledge || ((blueprint.id_0 != -1 || HUDCrafting.crafts[0].x != -1) && (HUDCrafting.crafts[0].x == -1 || blueprint.id_0 != Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].id || Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].amount < blueprint.amount_0) || (blueprint.id_1 != -1 || HUDCrafting.crafts[1].x != -1) && (HUDCrafting.crafts[1].x == -1 || blueprint.id_1 != Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id || Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].amount < blueprint.amount_1)) && ((blueprint.id_0 != -1 || HUDCrafting.crafts[1].x != -1) && (HUDCrafting.crafts[1].x == -1 || blueprint.id_0 != Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id || Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].amount < blueprint.amount_0) || (blueprint.id_1 != -1 || HUDCrafting.crafts[0].x != -1) && (HUDCrafting.crafts[0].x == -1 || blueprint.id_1 != Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].id || Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].amount < blueprint.amount_1)) || (blueprint.idTool != -1 || HUDCrafting.crafts[2].x != -1) && (HUDCrafting.crafts[2].x == -1 || blueprint.idTool != Player.inventory.items[HUDCrafting.crafts[2].x, HUDCrafting.crafts[2].y].id))
				{
					num++;
				}
				else
				{
					((SleekImage)HUDCrafting.items[3].children[0]).setImage(string.Concat("Textures/Items/", blueprint.idReward));
					if (ItemStackable.getStackable(blueprint.idReward))
					{
						((SleekLabel)HUDCrafting.items[3].children[1]).text = string.Concat("x", blueprint.amountReward);
					}
					else if (ItemType.getType(blueprint.idReward) != 10)
					{
						((SleekLabel)HUDCrafting.items[3].children[1]).text = string.Concat("x", ItemAmount.getAmount(blueprint.idReward));
					}
					else
					{
						((SleekLabel)HUDCrafting.items[3].children[1]).text = string.Concat("x", ItemAmount.getAmount(blueprint.idReward) - 1);
					}
					HUDCrafting.items[3].tooltip = ItemName.getName(blueprint.idReward);
					HUDCrafting.available = true;
					break;
				}
			}
		}
		else
		{
			((SleekImage)HUDCrafting.items[3].children[0]).setImage(string.Concat("Textures/Items/", Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id));
			((SleekLabel)HUDCrafting.items[3].children[1]).text = string.Concat("x", Player.inventory.items[HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y].amount + Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].amount - 2);
			HUDCrafting.items[3].tooltip = ItemName.getName(Player.inventory.items[HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y].id);
			HUDCrafting.available = true;
		}
		if (!HUDCrafting.available)
		{
			((SleekImage)HUDCrafting.items[3].children[0]).setImage("Textures/Icons/result");
			((SleekLabel)HUDCrafting.items[3].children[1]).text = string.Empty;
			HUDCrafting.items[3].tooltip = "Result";
		}
	}

	public static void usedCraft(SleekFrame frame)
	{
		if (!HUDCrafting.available)
		{
			HUDGame.openError(Texts.ERROR_BLUEPRINT, "Textures/Icons/error");
		}
		else
		{
			Player.inventory.sendCraft(HUDCrafting.crafts[0].x, HUDCrafting.crafts[0].y, HUDCrafting.crafts[1].x, HUDCrafting.crafts[1].y, HUDCrafting.crafts[2].x, HUDCrafting.crafts[2].y, HUDCrafting.mode, Input.GetKey(InputSettings.otherKey));
		}
	}

	public static void usedFire(SleekFrame frame)
	{
		HUDCrafting.mode = 1;
		HUDCrafting.crafts[2] = Point2.NONE;
		HUDCrafting.updateItems();
	}

	public static void usedItem(SleekFrame frame)
	{
		int num = -1;
		if (frame.position.offset_x != 10)
		{
			num = 1;
		}
		else
		{
			num = (frame.position.offset_y != 10 ? 2 : 0);
		}
		if (HUDInventory.dragging.x == -1)
		{
			HUDCrafting.crafts[num] = Point2.NONE;
		}
		else if (num != 2 || HUDCrafting.mode == 0)
		{
			if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
			{
				Equipment.dequip();
			}
			for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
			{
				if (HUDCrafting.crafts[i].x == HUDInventory.dragging.x && HUDCrafting.crafts[i].y == HUDInventory.dragging.y)
				{
					HUDCrafting.crafts[i] = Point2.NONE;
				}
			}
			HUDCrafting.crafts[num] = HUDInventory.dragging;
			HUDInventory.dragging = Point2.NONE;
			HUDInventory.drag.setImage(string.Empty);
		}
		HUDCrafting.updateItems();
		HUDInventory.updateItems();
	}

	public static void usedNone(SleekFrame frame)
	{
		HUDCrafting.mode = 0;
		HUDCrafting.updateItems();
	}

	public static void usedRevert(SleekFrame frame)
	{
		for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
		{
			HUDCrafting.crafts[i] = Point2.NONE;
		}
		HUDInventory.updateItems();
		HUDCrafting.updateItems();
	}
}