using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDInventory
{
	public static SleekContainer container;

	public static bool state;

	public static Point2 selection;

	public static Point2 dragging;

	public static SleekButton[,] items;

	public static SleekContainer itemContainer;

	public static SleekSlider itemSlider;

	public static SleekBox box;

	public static SleekBox weight;

	public static SleekBox panelBox;

	public static SleekBox itemBox;

	public static SleekImage itemIcon;

	public static SleekBox infoBox;

	public static SleekLabel labelName;

	public static SleekLabel labelWeight;

	public static SleekLabel labelAmount;

	public static SleekLabel labelDescription;

	public static SleekButton useButton;

	public static SleekButton dropButton;

	public static SleekButton characterButton;

	public static SleekImage iconCharacter;

	public static SleekButton craftingButton;

	public static SleekImage iconCrafting;

	public static SleekButton skillsButton;

	public static SleekImage iconSkills;

	public static SleekImage drag;

	public static SleekLabel dragAmount;

	public static int offset;

	static HUDInventory()
	{
		HUDInventory.selection = Point2.NONE;
		HUDInventory.dragging = Point2.NONE;
	}

	public HUDInventory()
	{
		HUDInventory.container = new SleekContainer()
		{
			position = new Coord2(0, 0, 0f, 1f),
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDGame.container.addFrame(HUDInventory.container);
		HUDInventory.container.visible = false;
		HUDInventory.box = new SleekBox();
		HUDInventory.container.addFrame(HUDInventory.box);
		HUDInventory.itemSlider = new SleekSlider()
		{
			position = new Coord2(-30, 10, 1f, 0f),
			size = new Coord2(20, -20, 0f, 1f),
			orientation = Orient2.VERTICAL
		};
		HUDInventory.itemSlider.onUsed += new SleekDelegate(HUDInventory.usedSlider);
		HUDInventory.box.addFrame(HUDInventory.itemSlider);
		HUDInventory.weight = new SleekBox()
		{
			position = new Coord2(0, -50, 0f, 0f),
			size = new Coord2(0, 40, 1f, 0f)
		};
		HUDInventory.box.addFrame(HUDInventory.weight);
		HUDInventory.panelBox = new SleekBox()
		{
			position = new Coord2(2500, -180, 0f, 0f),
			size = new Coord2(0, 120, 1f, 0f)
		};
		HUDInventory.box.addFrame(HUDInventory.panelBox);
		HUDInventory.itemBox = new SleekBox()
		{
			position = new Coord2(10, 10, 0f, 0f),
			size = new Coord2(100, 100, 0f, 0f)
		};
		HUDInventory.panelBox.addFrame(HUDInventory.itemBox);
		HUDInventory.itemIcon = new SleekImage()
		{
			position = new Coord2(-32, -32, 0.5f, 0.5f),
			size = new Coord2(64, 64, 0f, 0f)
		};
		HUDInventory.itemBox.addFrame(HUDInventory.itemIcon);
		HUDInventory.infoBox = new SleekBox()
		{
			position = new Coord2(120, 10, 0f, 0f),
			size = new Coord2(-240, 100, 1f, 0f)
		};
		HUDInventory.panelBox.addFrame(HUDInventory.infoBox);
		HUDInventory.labelName = new SleekLabel()
		{
			position = new Coord2(10, 0, 0f, 0f),
			size = new Coord2(-20, 40, 1f, 0f),
			alignment = TextAnchor.MiddleLeft
		};
		HUDInventory.infoBox.addFrame(HUDInventory.labelName);
		HUDInventory.labelWeight = new SleekLabel()
		{
			position = new Coord2(10, 0, 0f, 0f),
			size = new Coord2(-20, 40, 1f, 0f),
			alignment = TextAnchor.MiddleRight
		};
		HUDInventory.infoBox.addFrame(HUDInventory.labelWeight);
		HUDInventory.labelAmount = new SleekLabel()
		{
			position = new Coord2(10, -30, 0f, 1f),
			size = new Coord2(-20, 20, 1f, 0f),
			alignment = TextAnchor.MiddleRight
		};
		HUDInventory.itemBox.addFrame(HUDInventory.labelAmount);
		HUDInventory.labelDescription = new SleekLabel()
		{
			position = new Coord2(10, 40, 0f, 0f),
			size = new Coord2(-20, -50, 1f, 1f),
			alignment = TextAnchor.UpperLeft
		};
		HUDInventory.infoBox.addFrame(HUDInventory.labelDescription);
		HUDInventory.useButton = new SleekButton()
		{
			position = new Coord2(-110, 10, 1f, 0f),
			size = new Coord2(100, 45, 0f, 0f)
		};
		HUDInventory.useButton.onUsed += new SleekDelegate(HUDInventory.usedUse);
		HUDInventory.panelBox.addFrame(HUDInventory.useButton);
		HUDInventory.dropButton = new SleekButton()
		{
			position = new Coord2(-110, 65, 1f, 0f),
			size = new Coord2(100, 45, 0f, 0f)
		};
		HUDInventory.dropButton.onUsed += new SleekDelegate(HUDInventory.usedDrop);
		HUDInventory.panelBox.addFrame(HUDInventory.dropButton);
		HUDInventory.characterButton = new SleekButton()
		{
			position = new Coord2(0, 10, 0f, 1f),
			size = new Coord2(0, 40, 0.3f, 0f),
			text = Texts.LABEL_CHARACTERTAB
		};
		HUDInventory.characterButton.onUsed += new SleekDelegate(HUDInventory.usedCharacter);
		HUDInventory.box.addFrame(HUDInventory.characterButton);
		HUDInventory.iconCharacter = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDInventory.iconCharacter.setImage("Textures/Icons/character");
		HUDInventory.characterButton.addFrame(HUDInventory.iconCharacter);
		HUDInventory.craftingButton = new SleekButton()
		{
			position = new Coord2(0, 10, 0.35f, 1f),
			size = new Coord2(0, 40, 0.3f, 0f),
			text = Texts.LABEL_CRAFTINGTAB
		};
		HUDInventory.craftingButton.onUsed += new SleekDelegate(HUDInventory.usedCrafting);
		HUDInventory.box.addFrame(HUDInventory.craftingButton);
		HUDInventory.iconCrafting = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDInventory.iconCrafting.setImage("Textures/Icons/craft");
		HUDInventory.craftingButton.addFrame(HUDInventory.iconCrafting);
		HUDInventory.skillsButton = new SleekButton()
		{
			position = new Coord2(0, 10, 0.7f, 1f),
			size = new Coord2(0, 40, 0.3f, 0f),
			text = Texts.LABEL_SKILLSTAB
		};
		HUDInventory.skillsButton.onUsed += new SleekDelegate(HUDInventory.usedSkills);
		HUDInventory.box.addFrame(HUDInventory.skillsButton);
		HUDInventory.iconSkills = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDInventory.iconSkills.setImage("Textures/Icons/skills");
		HUDInventory.skillsButton.addFrame(HUDInventory.iconSkills);
		HUDInventory.updateSelection();
		HUDInventory.itemContainer = new SleekContainer()
		{
			size = new Coord2(0, 0, 1f, 1f)
		};
		HUDInventory.box.addFrame(HUDInventory.itemContainer);
		HUDCharacter hUDCharacter = new HUDCharacter();
		HUDCrafting hUDCrafting = new HUDCrafting();
		HUDSkills hUDSkill = new HUDSkills();
		HUDInteract hUDInteract = new HUDInteract();
		HUDInventory.drag = new SleekImage()
		{
			size = new Coord2(32, 32, 0f, 0f),
			visible = true
		};
		HUDGame.window.addFrame(HUDInventory.drag);
		HUDInventory.dragAmount = new SleekLabel()
		{
			size = new Coord2(32, 32, 0f, 0f),
			position = new Coord2(0, 0, 1f, 0f),
			text = "x100"
		};
		HUDInventory.drag.addFrame(HUDInventory.dragAmount);
		HUDInventory.state = false;
		HUDInventory.selection = Point2.NONE;
		HUDInventory.dragging = Point2.NONE;
		HUDInventory.offset = 0;
	}

	public static void close()
	{
		HUDInventory.state = false;
		HUDInventory.container.position = Coord2.ZERO;
		HUDInventory.container.lerp(new Coord2(0, 0, 0f, 1f), HUDInventory.container.size, 4f, true);
		if (HUDCharacter.state)
		{
			HUDCharacter.close();
		}
		if (HUDCrafting.state)
		{
			HUDCrafting.close();
		}
		if (HUDSkills.state)
		{
			HUDSkills.close();
		}
		if (HUDInteract.state)
		{
			HUDInteract.close();
		}
		HUDInventory.selection = Point2.NONE;
		HUDInventory.dragging = Point2.NONE;
		HUDInventory.drag.setImage(string.Empty);
		HUDInventory.updateSelection();
		HUDInventory.updateItems();
	}

	public static void open()
	{
		HUDInventory.state = true;
		HUDInventory.container.position = new Coord2(0, 0, 0f, 1f);
		HUDInventory.container.lerp(Coord2.ZERO, HUDInventory.container.size, 4f);
		HUDInventory.container.visible = true;
		HUDCharacter.open();
	}

	public static void openInteract()
	{
		HUDInventory.state = true;
		HUDInventory.container.position = new Coord2(0, 0, 0f, 1f);
		HUDInventory.container.lerp(Coord2.ZERO, HUDInventory.container.size, 4f);
		HUDInventory.container.visible = true;
		HUDInteract.open();
	}

	public static void resize(int width, int height)
	{
		HUDInventory.itemContainer.clearFrames();
		HUDInventory.items = new SleekButton[width, 3];
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				SleekButton sleekButton = new SleekButton()
				{
					position = new Coord2(10 + i * 110, 10 + j * 110, 0f, 0f),
					size = new Coord2(100, 100, 0f, 0f)
				};
				HUDInventory.itemContainer.addFrame(sleekButton);
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
				SleekLabel sleekLabel1 = new SleekLabel()
				{
					position = new Coord2(10, 10, 0f, 0f),
					size = new Coord2(-20, 20, 1f, 0f),
					alignment = TextAnchor.MiddleLeft
				};
				int num = i + 1;
				sleekLabel1.text = string.Concat("[", num.ToString(), "]");
				sleekLabel1.visible = j == 0;
				sleekButton.addFrame(sleekLabel1);
				HUDInventory.items[i, j] = sleekButton;
				sleekButton.onUsed += new SleekDelegate(HUDInventory.usedItem);
			}
		}
		HUDInventory.box.position = new Coord2(-width * 55 - 25, -120, 0.4f, 0.5f);
		HUDInventory.box.size = new Coord2(width * 110 + 40, 340, 0f, 0f);
	}

	public static void updateItems()
	{
		if (Player.inventory != null && Player.inventory.items != null)
		{
			if (Player.inventory.height >= 3)
			{
				HUDInventory.offset = (int)Mathf.Ceil((float)(Player.inventory.height - 3) * HUDInventory.itemSlider.state);
				HUDInventory.itemSlider.scale = 3f / (float)Player.inventory.height;
			}
			else
			{
				HUDInventory.offset = 0;
				HUDInventory.itemSlider.scale = 1f;
			}
			for (int i = 0; i < Player.inventory.width; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (j + HUDInventory.offset < Player.inventory.height)
					{
						HUDInventory.items[i, j].visible = true;
						HUDInventory.items[i, j].children[2].visible = j + HUDInventory.offset == 0;
						if (Player.inventory.items[i, j + HUDInventory.offset].id == -1)
						{
							HUDInventory.items[i, j].children[0].color = Color.white;
							((SleekImage)HUDInventory.items[i, j].children[0]).setImage(string.Empty);
							((SleekLabel)HUDInventory.items[i, j].children[1]).text = string.Empty;
							HUDInventory.items[i, j].tooltip = string.Empty;
						}
						else if (i == HUDInventory.dragging.x && j + HUDInventory.offset == HUDInventory.dragging.y)
						{
							HUDInventory.items[i, j].children[0].color = new Color(1f, 1f, 1f, 0.25f);
							((SleekLabel)HUDInventory.items[i, j].children[1]).text = string.Empty;
							HUDInventory.items[i, j].tooltip = string.Empty;
						}
						else if (!HUDCrafting.isReferred(i, j + HUDInventory.offset))
						{
							HUDInventory.items[i, j].children[0].color = Color.white;
							((SleekImage)HUDInventory.items[i, j].children[0]).setImage(string.Concat("Textures/Items/", Player.inventory.items[i, j + HUDInventory.offset].id));
							if (ItemType.getType(Player.inventory.items[i, j + HUDInventory.offset].id) != 10)
							{
								((SleekLabel)HUDInventory.items[i, j].children[1]).text = string.Concat("x", Player.inventory.items[i, j + HUDInventory.offset].amount);
							}
							else
							{
								((SleekLabel)HUDInventory.items[i, j].children[1]).text = string.Concat("x", Player.inventory.items[i, j + HUDInventory.offset].amount - 1);
							}
							HUDInventory.items[i, j].tooltip = ItemName.getName(Player.inventory.items[i, j + HUDInventory.offset].id);
						}
						else
						{
							if (HUDCrafting.crafts[(int)HUDCrafting.crafts.Length - 2].x != i || HUDCrafting.crafts[(int)HUDCrafting.crafts.Length - 2].y != j + HUDInventory.offset)
							{
								HUDInventory.items[i, j].children[0].color = Color.white;
								((SleekImage)HUDInventory.items[i, j].children[0]).setImage("Textures/Icons/supply");
								HUDInventory.items[i, j].tooltip = "Supply";
							}
							else
							{
								HUDInventory.items[i, j].children[0].color = Color.white;
								((SleekImage)HUDInventory.items[i, j].children[0]).setImage("Textures/Icons/tool");
								HUDInventory.items[i, j].tooltip = "Tool";
							}
							((SleekLabel)HUDInventory.items[i, j].children[1]).text = string.Empty;
						}
					}
					else
					{
						HUDInventory.items[i, j].visible = false;
					}
					if (i == HUDInventory.selection.x && j + HUDInventory.offset == HUDInventory.selection.y)
					{
						HUDInventory.updateSelection();
					}
				}
			}
		}
	}

	public static void updateSelection()
	{
		if (Player.inventory != null && Player.inventory.items != null)
		{
			if (HUDInventory.selection.x == -1 || Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id == -1 || HUDCrafting.isReferred(HUDInventory.selection.x, HUDInventory.selection.y))
			{
				HUDInventory.selection = Point2.NONE;
				if (HUDInventory.panelBox.position.offset_x == 0)
				{
					HUDInventory.panelBox.position = new Coord2(0, -180, 0f, 0f);
					HUDInventory.panelBox.lerp(new Coord2(-2000, -180, 0f, 0f), HUDInventory.panelBox.size, 4f);
				}
				HUDInventory.labelName.text = string.Empty;
				HUDInventory.labelWeight.text = string.Empty;
				HUDInventory.labelAmount.text = string.Empty;
				HUDInventory.labelDescription.text = string.Empty;
				HUDInventory.useButton.text = string.Empty;
				HUDInventory.dropButton.text = string.Empty;
				HUDInventory.itemIcon.setImage(string.Empty);
			}
			else
			{
				if (HUDInventory.panelBox.position.offset_x != 0)
				{
					HUDInventory.panelBox.position = new Coord2(2000, -180, 0f, 0f);
					HUDInventory.panelBox.lerp(new Coord2(0, -180, 0f, 0f), HUDInventory.panelBox.size, 4f);
				}
				HUDInventory.labelName.text = ItemName.getName(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id);
				HUDInventory.labelWeight.text = ItemWeight.getText(ItemWeight.getWeight(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id));
				if (ItemType.getType(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id) != 10)
				{
					HUDInventory.labelAmount.text = string.Concat("x", Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].amount);
				}
				else
				{
					HUDInventory.labelAmount.text = string.Concat("x", Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].amount - 1);
				}
				HUDInventory.labelDescription.text = ItemDescription.getDescription(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id, Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].state);
				if (!ItemEquipable.getEquipable(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id))
				{
					HUDInventory.useButton.text = string.Empty;
				}
				else if (Equipment.equipped.x != HUDInventory.selection.x || Equipment.equipped.y != HUDInventory.selection.y)
				{
					HUDInventory.useButton.text = Texts.LABEL_EQUIP;
				}
				else
				{
					HUDInventory.useButton.text = Texts.LABEL_DEQUIP;
				}
				HUDInventory.dropButton.text = Texts.LABEL_DROP;
				HUDInventory.itemIcon.setImage(string.Concat("Textures/Items/", Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id));
			}
		}
	}

	public static void updateWeight()
	{
		if (Player.clothes.backpack != -1)
		{
			HUDInventory.weight.text = string.Concat(new string[] { ItemName.getName(Player.clothes.backpack), " ", ItemWeight.getText(Player.inventory.weight), "/", ItemWeight.getText(Player.inventory.capacity) });
		}
		else
		{
			HUDInventory.weight.text = string.Concat(ItemWeight.getText(Player.inventory.weight), "/", ItemWeight.getText(Player.inventory.capacity));
		}
	}

	public static void usedCharacter(SleekFrame frame)
	{
		if (!HUDCharacter.state)
		{
			if (HUDCrafting.state)
			{
				HUDCrafting.close();
			}
			if (HUDSkills.state)
			{
				HUDSkills.close();
			}
			if (HUDInteract.state)
			{
				HUDInteract.close();
			}
			HUDCharacter.open();
		}
		else
		{
			HUDCharacter.close();
		}
	}

	public static void usedCrafting(SleekFrame frame)
	{
		if (!HUDCrafting.state)
		{
			if (HUDCharacter.state)
			{
				HUDCharacter.close();
			}
			if (HUDSkills.state)
			{
				HUDSkills.close();
			}
			if (HUDInteract.state)
			{
				HUDInteract.close();
			}
			HUDCrafting.open();
		}
		else
		{
			HUDCrafting.close();
		}
	}

	public static void usedDrop(SleekFrame frame)
	{
		if (HUDInventory.selection.x != -1 && Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].amount > 0)
		{
			if (Equipment.equipped.x == HUDInventory.selection.x && Equipment.equipped.y == HUDInventory.selection.y)
			{
				Equipment.dequip();
			}
			SpawnItems.dropItem(HUDInventory.selection.x, HUDInventory.selection.y, Input.GetKey(InputSettings.otherKey));
			HUDInventory.updateSelection();
		}
	}

	public static void usedItem(SleekFrame frame)
	{
		int offsetX = (frame.position.offset_x - 10) / 110;
		int offsetY = (frame.position.offset_y - 10) / 110;
		offsetY = offsetY + HUDInventory.offset;
		if (HUDCrafting.isReferred(offsetX, offsetY))
		{
			for (int i = 0; i < (int)HUDCrafting.crafts.Length; i++)
			{
				if (HUDCrafting.crafts[i].x == offsetX && HUDCrafting.crafts[i].y == offsetY)
				{
					HUDCrafting.crafts[i] = Point2.NONE;
				}
			}
			HUDInventory.updateItems();
			HUDCrafting.updateItems();
		}
		else if (HUDInventory.dragging.x != -1)
		{
			int num = HUDInventory.dragging.x;
			int num1 = HUDInventory.dragging.y;
			HUDInventory.dragging = Point2.NONE;
			HUDInventory.drag.setImage(string.Empty);
			if (Equipment.equipped.x == num && Equipment.equipped.y == num1)
			{
				Equipment.equipped = new Point2(offsetX, offsetY);
			}
			else if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.equipped = new Point2(num, num1);
			}
			Player.inventory.sendDrag(num, num1, offsetX, offsetY, !Input.GetKey(InputSettings.otherKey));
		}
		else if (!Input.GetKey(InputSettings.otherKey) || Player.inventory.items[offsetX, offsetY].amount <= 0)
		{
			if (Event.current.button == 1 && Player.inventory.items[offsetX, offsetY].id != -1)
			{
				HUDInventory.selection = Point2.NONE;
				HUDInventory.dragging = new Point2(offsetX, offsetY);
				HUDInventory.drag.setImage(string.Concat("Textures/Items/", Player.inventory.items[offsetX, offsetY].id));
				HUDInventory.updateItems();
			}
			else if (offsetX != HUDInventory.selection.x || offsetY != HUDInventory.selection.y)
			{
				HUDInventory.selection = new Point2(offsetX, offsetY);
			}
			else
			{
				HUDInventory.selection = Point2.NONE;
			}
		}
		else if (HUDCrafting.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			if (ItemEquipable.getEquipable(Player.inventory.items[offsetX, offsetY].id))
			{
				if (HUDCrafting.crafts[2].x == -1)
				{
					HUDCrafting.crafts[2] = new Point2(offsetX, offsetY);
				}
			}
			else if (HUDCrafting.crafts[0].x == -1)
			{
				HUDCrafting.crafts[0] = new Point2(offsetX, offsetY);
			}
			else if (HUDCrafting.crafts[1].x == -1)
			{
				HUDCrafting.crafts[1] = new Point2(offsetX, offsetY);
			}
			HUDInventory.updateItems();
			HUDCrafting.updateItems();
		}
		else if (ItemType.getType(Player.inventory.items[offsetX, offsetY].id) == 0 && HUDCharacter.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			int num2 = Player.inventory.items[offsetX, offsetY].id;
			Player.inventory.sendUseItem(offsetX, offsetY);
			Player.clothes.changeHat(num2);
		}
		else if (ItemType.getType(Player.inventory.items[offsetX, offsetY].id) == 2 && HUDCharacter.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			int num3 = Player.inventory.items[offsetX, offsetY].id;
			Player.inventory.sendUseItem(offsetX, offsetY);
			Player.clothes.changeBackpack(num3);
		}
		else if (ItemType.getType(Player.inventory.items[offsetX, offsetY].id) == 3 && HUDCharacter.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			int num4 = Player.inventory.items[offsetX, offsetY].id;
			Player.inventory.sendUseItem(offsetX, offsetY);
			Player.clothes.changeVest(num4);
		}
		else if (ItemType.getType(Player.inventory.items[offsetX, offsetY].id) == 4 && HUDCharacter.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			int num5 = Player.inventory.items[offsetX, offsetY].id;
			Player.inventory.sendUseItem(offsetX, offsetY);
			Player.clothes.changeShirt(num5);
		}
		else if (ItemType.getType(Player.inventory.items[offsetX, offsetY].id) == 5 && HUDCharacter.state)
		{
			if (Equipment.equipped.x == offsetX && Equipment.equipped.y == offsetY)
			{
				Equipment.dequip();
			}
			int num6 = Player.inventory.items[offsetX, offsetY].id;
			Player.inventory.sendUseItem(offsetX, offsetY);
			Player.clothes.changePants(num6);
		}
		else if (ItemEquipable.getEquipable(Player.inventory.items[offsetX, offsetY].id))
		{
			Equipment.equip(offsetX, offsetY);
			HUDInventory.close();
		}
		HUDInventory.updateSelection();
	}

	public static void usedSkills(SleekFrame frame)
	{
		if (!HUDSkills.state)
		{
			if (HUDCharacter.state)
			{
				HUDCharacter.close();
			}
			if (HUDCrafting.state)
			{
				HUDCrafting.close();
			}
			if (HUDInteract.state)
			{
				HUDInteract.close();
			}
			HUDSkills.open();
		}
		else
		{
			HUDSkills.close();
		}
	}

	public static void usedSlider(SleekFrame frame)
	{
		HUDInventory.updateItems();
	}

	public static void usedUse(SleekFrame frame)
	{
		if (HUDInventory.selection.x != -1 && ItemEquipable.getEquipable(Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].id) && Player.inventory.items[HUDInventory.selection.x, HUDInventory.selection.y].amount > 0)
		{
			Equipment.equip(HUDInventory.selection.x, HUDInventory.selection.y);
			HUDInventory.close();
			HUDInventory.updateSelection();
		}
	}
}