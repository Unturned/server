using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDInteract
{
	public static bool state;

	public static SleekBox box;

	public static SleekDoc noteDoc;

	public static SleekButton noteWrite;

	public static SleekButton noteCancel;

	public static SleekImage writeIcon;

	public static SleekImage cancelIcon;

	public static SleekButton[,] crateButtons;

	public static ClientItem[,] crateItems;

	public static SleekButton crateCancel;

	public HUDInteract()
	{
		HUDInteract.box = new SleekBox()
		{
			position = new Coord2(10, -115, 1f, -5f)
		};
		HUDInventory.box.addFrame(HUDInteract.box);
		HUDInteract.box.visible = false;
		HUDInteract.noteDoc = new SleekDoc()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(-20, -70, 1f, 1f),
			visible = false,
			alignment = TextAnchor.UpperLeft
		};
		HUDInteract.box.addFrame(HUDInteract.noteDoc);
		HUDInteract.noteCancel = new SleekButton()
		{
			position = new Coord2(0, 10, 0f, 1f),
			size = new Coord2(-5, 40, 0.5f, 0f),
			text = "Cancel"
		};
		HUDInteract.noteCancel.onUsed += new SleekDelegate(HUDInteract.usedWrite);
		HUDInteract.noteDoc.addFrame(HUDInteract.noteCancel);
		HUDInteract.cancelIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDInteract.cancelIcon.setImage("Textures/Icons/back");
		HUDInteract.noteCancel.addFrame(HUDInteract.cancelIcon);
		HUDInteract.noteWrite = new SleekButton()
		{
			position = new Coord2(5, 10, 0.5f, 1f),
			size = new Coord2(-5, 40, 0.5f, 0f),
			text = "Write"
		};
		HUDInteract.noteWrite.onUsed += new SleekDelegate(HUDInteract.usedWrite);
		HUDInteract.noteDoc.addFrame(HUDInteract.noteWrite);
		HUDInteract.writeIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDInteract.writeIcon.setImage("Textures/Icons/write");
		HUDInteract.noteWrite.addFrame(HUDInteract.writeIcon);
		HUDInteract.crateCancel = new SleekButton()
		{
			position = new Coord2(10, -50, 0f, 1f),
			size = new Coord2(-20, 40, 1f, 0f),
			text = "Cancel"
		};
		HUDInteract.crateCancel.onUsed += new SleekDelegate(HUDInteract.usedCancel);
		HUDInteract.box.addFrame(HUDInteract.crateCancel);
		HUDInteract.crateCancel.addFrame(HUDInteract.cancelIcon);
		HUDInteract.crateButtons = new SleekButton[2, 3];
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				SleekButton sleekButton = new SleekButton()
				{
					position = new Coord2(10 + i * 110, 10 + j * 110, 0f, 0f),
					size = new Coord2(100, 100, 0f, 0f),
					visible = false
				};
				HUDInteract.box.addFrame(sleekButton);
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
				HUDInteract.crateButtons[i, j] = sleekButton;
				sleekButton.onUsed += new SleekDelegate(HUDInteract.usedCrate);
			}
		}
		HUDInteract.state = false;
	}

	public static void close()
	{
		HUDInteract.state = false;
		HUDInteract.box.position = new Coord2(10, -HUDInteract.box.size.offset_y / 2, 1f, 0.5f);
		HUDInteract.box.lerp(new Coord2(10, -HUDInteract.box.size.offset_y / 2, 1f, -5f), HUDInteract.box.size, 4f, true);
	}

	public static void crate(int id, ClientItem[,] items)
	{
		HUDInteract.noteDoc.visible = false;
		HUDInteract.crateItems = items;
		HUDInteract.crateCancel.visible = true;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (j < BarricadeStats.getCapacity(id))
				{
					HUDInteract.crateButtons[i, j].visible = true;
					if (items[i, j].id != -1)
					{
						((SleekImage)HUDInteract.crateButtons[i, j].children[0]).setImage(string.Concat("Textures/Items/", items[i, j].id));
						if (ItemType.getType(items[i, j].id) != 10)
						{
							((SleekLabel)HUDInteract.crateButtons[i, j].children[1]).text = string.Concat("x", items[i, j].amount);
						}
						else
						{
							((SleekLabel)HUDInteract.crateButtons[i, j].children[1]).text = string.Concat("x", items[i, j].amount - 1);
						}
						HUDInteract.crateButtons[i, j].tooltip = ItemName.getName(items[i, j].id);
					}
					else
					{
						((SleekImage)HUDInteract.crateButtons[i, j].children[0]).setImage(string.Empty);
						((SleekLabel)HUDInteract.crateButtons[i, j].children[1]).text = string.Empty;
						HUDInteract.crateButtons[i, j].tooltip = string.Empty;
					}
				}
				else
				{
					HUDInteract.crateButtons[i, j].visible = false;
				}
			}
		}
		HUDInteract.box.size = new Coord2(230, BarricadeStats.getCapacity(id) * 110 + 60, 0f, 0f);
	}

	public static void note(string state)
	{
		HUDInteract.noteDoc.text = state;
		HUDInteract.noteDoc.visible = true;
		HUDInteract.crateCancel.visible = false;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				HUDInteract.crateButtons[i, j].visible = false;
			}
		}
		HUDInteract.box.size = new Coord2(300, 300, 0f, 0f);
	}

	public static void open()
	{
		HUDInteract.state = true;
		HUDInteract.box.position = new Coord2(10, -HUDInteract.box.size.offset_y / 2, 1f, -5f);
		HUDInteract.box.lerp(new Coord2(10, -HUDInteract.box.size.offset_y / 2, 1f, 0.5f), HUDInteract.box.size, 4f);
		HUDInteract.box.visible = true;
	}

	public static void usedCancel(SleekFrame frame)
	{
		Interact.edit = null;
		HUDGame.interacting = false;
		HUDInventory.close();
	}

	public static void usedCrate(SleekFrame frame)
	{
		int offsetX = (frame.position.offset_x - 10) / 110;
		int offsetY = (frame.position.offset_y - 10) / 110;
		if (HUDInventory.dragging.x != -1)
		{
			if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
			{
				Equipment.dequip();
			}
			if (HUDInteract.crateItems[offsetX, offsetY].id == -1 || ItemStackable.getStackable(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) && Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id == HUDInteract.crateItems[offsetX, offsetY].id)
			{
				int num = HUDInventory.dragging.x;
				int num1 = HUDInventory.dragging.y;
				if (Input.GetKey(InputSettings.otherKey) || !ItemStackable.getStackable(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) || Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].amount < 2)
				{
					HUDInventory.dragging = Point2.NONE;
					HUDInventory.drag.setImage(string.Empty);
				}
				if (Interact.edit.transform.parent.name != "16019")
				{
					InteractionInterface.sendLockedCrateAdd(Interact.edit.transform.parent.position, num, num1, offsetX, offsetY, Input.GetKey(InputSettings.otherKey));
				}
				else
				{
					InteractionInterface.sendCrateAdd(Interact.edit.transform.parent.position, num, num1, offsetX, offsetY, Input.GetKey(InputSettings.otherKey));
				}
			}
		}
		else if (HUDInteract.crateItems[offsetX, offsetY].id != -1)
		{
			if (Interact.edit.transform.parent.name != "16019")
			{
				InteractionInterface.sendLockedCrateRemove(Interact.edit.transform.parent.position, offsetX, offsetY, Input.GetKey(InputSettings.otherKey));
			}
			else
			{
				InteractionInterface.sendCrateRemove(Interact.edit.transform.parent.position, offsetX, offsetY, Input.GetKey(InputSettings.otherKey));
			}
		}
	}

	public static void usedWrite(SleekFrame frame)
	{
		Interact.edit.GetComponent<Note>().write(HUDInteract.noteDoc.text);
		Interact.edit = null;
		HUDGame.interacting = false;
		HUDInventory.close();
	}
}