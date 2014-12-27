using System;

public class ItemStackable
{
	public ItemStackable()
	{
	}

	public static bool getStackable(int id)
	{
		switch (ItemType.getType(id))
		{
			case 13:
			{
				return true;
			}
			case 14:
			{
				return true;
			}
			case 15:
			{
				return true;
			}
			case 16:
			{
				return true;
			}
			case 17:
			{
				return true;
			}
			case 18:
			{
				return true;
			}
			case 19:
			{
				return true;
			}
			case 20:
			case 21:
			case 24:
			case 26:
			{
				return false;
			}
			case 22:
			{
				return true;
			}
			case 23:
			{
				return true;
			}
			case 25:
			{
				return true;
			}
			case 27:
			{
				return true;
			}
			default:
			{
				return false;
			}
		}
	}
}