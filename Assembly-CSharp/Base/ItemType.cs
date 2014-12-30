using System;

public class ItemType
{
	public ItemType()
	{
	}

	public static int getType(int id)
	{
		return id / 1000;
	}
}