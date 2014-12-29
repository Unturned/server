using System;
using UnityEngine;

public class Item : Interactable
{
	public Item()
	{
	}

	public override string hint()
	{
		return ItemName.getName(int.Parse(base.name));
	}

	public override string icon()
	{
		return string.Concat("Textures/Items/", int.Parse(base.name));
	}

	public override void trigger()
	{
		int num = Player.inventory.hasSpace(new ServerItem(int.Parse(base.name), 1, string.Empty, Vector3.zero));
		if (num == 0)
		{
			if (Equipment.model == null)
			{
				Viewmodel.play("take");
			}
			SpawnItems.takeItem(base.gameObject);
		}
		else if (num == 1)
		{
			HUDGame.openError(Texts.ERROR_NO_SPACE, "Textures/Icons/errror");
		}
		else if (num == 2)
		{
			HUDGame.openError(Texts.ERROR_NO_WEIGHT, "Textures/Icons/errror");
		}
	}
}