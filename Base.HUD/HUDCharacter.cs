using System;
using UnityEngine;

public class HUDCharacter
{
	public static bool state;

	public static SleekBox box;

	public static SleekImage view;

	public static SleekSlider sliderRotation;

	public static SleekImage repIcon;

	public static SleekLabel nameLabel;

	private static GameObject inspect;

	private static RenderTexture texture;

	private static RaycastHit hit;

	public static float rotation;

	public HUDCharacter()
	{
		HUDCharacter.box = new SleekBox()
		{
			position = new Coord2(10, -170, 1f, -5f),
			size = new Coord2(230, 340, 0f, 0f)
		};
		HUDInventory.box.addFrame(HUDCharacter.box);
		HUDCharacter.box.visible = false;
		HUDCharacter.inspect = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Game/inspect"));
		HUDCharacter.inspect.name = "inspect";
		HUDCharacter.inspect.transform.position = new Vector3(0f, -24f, 0f);
		HUDCharacter.inspect.transform.rotation = Quaternion.identity;
		HUDCharacter.texture = HUDCharacter.inspect.transform.FindChild("camera").camera.targetTexture;
		HUDCharacter.view = new SleekImage()
		{
			position = new Coord2(-256, -256, 0.5f, 0.5f),
			size = new Coord2(512, 512)
		};
		HUDCharacter.box.addFrame(HUDCharacter.view);
		HUDCharacter.view.setImage(HUDCharacter.texture);
		HUDCharacter.nameLabel = new SleekLabel()
		{
			position = new Coord2(40, 0, 0f, 0f),
			size = new Coord2(-40, 40, 1f, 0f)
		};
		if (PlayerSettings.status == 21)
		{
			HUDCharacter.nameLabel.paint = Colors.GOLD;
		}
		if (!(PlayerSettings.friendHash != string.Empty) || !(PlayerSettings.nickname != string.Empty))
		{
			HUDCharacter.nameLabel.text = PlayerSettings.user;
		}
		else
		{
			HUDCharacter.nameLabel.text = string.Concat(PlayerSettings.user, " [", PlayerSettings.nickname, "]");
		}
		HUDCharacter.box.addFrame(HUDCharacter.nameLabel);
		HUDCharacter.repIcon = new SleekImage()
		{
			position = new Coord2(4, 4, 0f, 0f),
			size = new Coord2(32, 32, 0f, 0f)
		};
		HUDCharacter.repIcon.setImage(Reputation.getIcon(NetworkUserList.getUserFromPlayer(Network.player).reputation));
		HUDCharacter.box.addFrame(HUDCharacter.repIcon);
		HUDCharacter.sliderRotation = new SleekSlider()
		{
			position = new Coord2(10, -30, 0f, 1f),
			size = new Coord2(-20, 20, 1f, 0f),
			orientation = Orient2.HORIZONTAL
		};
		HUDCharacter.sliderRotation.onUsed += new SleekDelegate(HUDCharacter.usedRotation);
		HUDCharacter.sliderRotation.state = PlayerPrefs.GetFloat("igRot");
		HUDCharacter.sliderRotation.scale = 0.2f;
		HUDCharacter.box.addFrame(HUDCharacter.sliderRotation);
		HUDCharacter.updateItems();
		HUDCharacter.state = false;
	}

	public static bool click(Vector3 world, int x, int y)
	{
		if (Mathf.Abs(world.x - (float)x) < 25f && Mathf.Abs(world.y - (float)y) < 25f)
		{
			return true;
		}
		return false;
	}

	public static void close()
	{
		HUDCharacter.state = false;
		HUDCharacter.box.position = new Coord2(10, -170, 1f, 0.5f);
		HUDCharacter.box.lerp(new Coord2(10, -170, 1f, -5f), HUDCharacter.box.size, 4f, true);
	}

	public static void open()
	{
		HUDCharacter.state = true;
		HUDCharacter.box.position = new Coord2(10, -170, 1f, -5f);
		HUDCharacter.box.lerp(new Coord2(10, -170, 1f, 0.5f), HUDCharacter.box.size, 4f);
		HUDCharacter.box.visible = true;
		HUDCharacter.repIcon.setImage(Reputation.getIcon(NetworkUserList.getUserFromPlayer(Network.player).reputation));
	}

	public static void updateItems()
	{
		if (Player.clothes != null)
		{
			Character component = HUDCharacter.inspect.transform.FindChild("character").GetComponent<Character>();
			component.face = PlayerSettings.face;
			component.hair = PlayerSettings.hair;
			component.skinColor = PlayerSettings.skinColor;
			component.hairColor = PlayerSettings.hairColor;
			component.arm = PlayerSettings.arm;
			component.shirt = Player.clothes.shirt;
			component.pants = Player.clothes.pants;
			component.hat = Player.clothes.hat;
			component.backpack = Player.clothes.backpack;
			component.vest = Player.clothes.vest;
			component.item = Player.clothes.item;
			if (Equipment.id == -1 || !(Player.inventory != null) || Equipment.equipped.x >= Player.inventory.width || Equipment.equipped.y >= Player.inventory.height)
			{
				component.state = string.Empty;
			}
			else
			{
				component.state = Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state;
			}
			if (component.item == -1)
			{
				component.animation.Play("standIdleBasic");
			}
			else if (component.item == 7004 || component.item == 7014)
			{
				component.animation.Play("standFriendlyBow");
			}
			else if (ItemType.getType(component.item) == 7)
			{
				component.animation.Play("standFriendlyGun");
			}
			else if (component.item == 8001 || component.item == 8008)
			{
				component.animation.Play("standIdleFlashlight");
			}
			else
			{
				component.animation.Play("standIdleItem");
			}
			component.wear();
		}
	}

	public static void usedCharacter()
	{
		Vector2 vector2 = Event.current.mousePosition;
		int positionX = (int)vector2.x - HUDCharacter.view.getPosition_x();
		Vector2 vector21 = Event.current.mousePosition;
		int positionY = 512 - ((int)vector21.y - HUDCharacter.view.getPosition_y());
		if (positionX >= 0 && positionY >= 0 && positionX < 512 && positionY < 512)
		{
			Camera camera = HUDCharacter.inspect.transform.FindChild("camera").camera;
			Physics.Raycast(camera.ScreenPointToRay(new Vector3((float)positionX, (float)positionY, 0f)), out HUDCharacter.hit, 8f);
			if (HUDCharacter.hit.collider != null)
			{
				int limb = OwnerFinder.getLimb(HUDCharacter.hit.collider.gameObject);
				if (limb == 0 || limb == 1)
				{
					if (HUDInventory.dragging.x == -1 || ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) != 5)
					{
						Player.clothes.changePants(-1);
					}
					else
					{
						if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
						{
							Equipment.dequip();
						}
						int num = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
						Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
						Player.clothes.changePants(num);
						HUDInventory.dragging = Point2.NONE;
						HUDInventory.drag.setImage(string.Empty);
					}
				}
				else if (limb == 2 || limb == 3)
				{
					if (HUDInventory.dragging.x == -1 || ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) != 4)
					{
						Player.clothes.changeShirt(-1);
					}
					else
					{
						if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
						{
							Equipment.dequip();
						}
						int num1 = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
						Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
						Player.clothes.changeShirt(num1);
						HUDInventory.dragging = Point2.NONE;
						HUDInventory.drag.setImage(string.Empty);
					}
				}
				else if (limb == 4)
				{
					if (HUDInventory.dragging.x == -1 || ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) != 0)
					{
						Player.clothes.changeHat(-1);
					}
					else
					{
						if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
						{
							Equipment.dequip();
						}
						int num2 = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
						Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
						Player.clothes.changeHat(num2);
						HUDInventory.dragging = Point2.NONE;
						HUDInventory.drag.setImage(string.Empty);
					}
				}
				else if (HUDInventory.dragging.x == -1)
				{
					Vector3 vector3 = HUDCharacter.hit.point + HUDCharacter.hit.collider.transform.up;
					float single = vector3.magnitude;
					Vector3 vector31 = HUDCharacter.hit.point - HUDCharacter.hit.collider.transform.up;
					float single1 = vector31.magnitude;
					if (single < single1 && Player.clothes.vest != -1)
					{
						Player.clothes.changeVest(-1);
					}
					else if (single1 >= single || Player.clothes.backpack == -1)
					{
						Player.clothes.changeShirt(-1);
					}
					else
					{
						Player.clothes.changeBackpack(-1);
					}
				}
				else if (ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) == 3)
				{
					if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
					{
						Equipment.dequip();
					}
					int num3 = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
					Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
					Player.clothes.changeVest(num3);
					HUDInventory.dragging = Point2.NONE;
					HUDInventory.drag.setImage(string.Empty);
				}
				else if (ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) == 2)
				{
					if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
					{
						Equipment.dequip();
					}
					int num4 = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
					Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
					Player.clothes.changeBackpack(num4);
					HUDInventory.dragging = Point2.NONE;
					HUDInventory.drag.setImage(string.Empty);
				}
				else if (ItemType.getType(Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id) == 4)
				{
					if (HUDInventory.dragging.x == Equipment.equipped.x && HUDInventory.dragging.y == Equipment.equipped.y)
					{
						Equipment.dequip();
					}
					int num5 = Player.inventory.items[HUDInventory.dragging.x, HUDInventory.dragging.y].id;
					Player.inventory.sendUseItem(HUDInventory.dragging.x, HUDInventory.dragging.y);
					Player.clothes.changeShirt(num5);
					HUDInventory.dragging = Point2.NONE;
					HUDInventory.drag.setImage(string.Empty);
				}
			}
		}
		HUDInventory.updateItems();
	}

	public static void usedRotation(SleekFrame frame)
	{
		PlayerPrefs.SetFloat("igRot", ((SleekSlider)frame).state);
		HUDCharacter.rotation = ((SleekSlider)frame).state * 360f;
		HUDCharacter.inspect.transform.FindChild("character").rotation = Quaternion.Euler(0f, 20f - HUDCharacter.rotation, 0f);
	}
}