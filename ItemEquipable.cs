using System;

public class ItemEquipable
{
	public ItemEquipable()
	{
	}

	public static bool getEquipable(int id)
	{
		int type = ItemType.getType(id);
		switch (type)
		{
			case 9:
			{
				return false;
			}
			case 10:
			{
				return false;
			}
			case 11:
			{
				return false;
			}
			case 12:
			{
				return false;
			}
			case 18:
			{
				return false;
			}
			case 19:
			{
				return false;
			}
			default:
			{
				if (type == 25)
				{
					break;
				}
				else
				{
					return true;
				}
			}
		}
		return false;
	}
}