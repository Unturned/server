using System;
using UnityEngine;

public class ItemState
{
	public ItemState()
	{
	}

	public static int getAmmo(int max)
	{
		if ((double)UnityEngine.Random.@value > 0.8)
		{
			return max;
		}
		return UnityEngine.Random.Range(1, max + 1);
	}

	public static bool getMarket(int id)
	{
		int num = id;
		if (num == 0)
		{
			return true;
		}
		if (num == 4000)
		{
			return true;
		}
		if (num == 5000)
		{
			return true;
		}
		return false;
	}

	public static string getState(int id)
	{
		int num = id;
		switch (num)
		{
			case 7000:
			{
				return string.Concat(ItemState.getAmmo(30), "_10000_-1_-1_9001_0_y_");
			}
			case 7001:
			{
				return string.Concat(ItemState.getAmmo(7), "_10002_-1_-1_-1_0_y_");
			}
			case 7002:
			{
				return string.Concat(ItemState.getAmmo(2), "_25000_-1_-1_-1_1_y_");
			}
			case 7003:
			{
				return string.Concat(ItemState.getAmmo(5), "_10003_-1_-1_9005_1_y_");
			}
			case 7004:
			{
				return string.Concat(ItemState.getAmmo(1), "_25001_-1_-1_-1_1_y_");
			}
			case 7005:
			{
				return string.Concat(ItemState.getAmmo(5), "_25000_-1_-1_9006_0_y_");
			}
			case 7006:
			{
				return string.Concat(ItemState.getAmmo(13), "_10004_-1_-1_-1_0_y_");
			}
			case 7007:
			{
				return string.Concat(ItemState.getAmmo(1), "_25001_-1_-1_9004_1_y_");
			}
			case 7008:
			{
				return string.Concat(ItemState.getAmmo(30), "_10000_-1_-1_9007_0_y_");
			}
			case 7009:
			{
				return string.Concat(ItemState.getAmmo(30), "_10006_-1_-1_9008_1_y_");
			}
			case 7010:
			{
				return string.Concat(ItemState.getAmmo(6), "_10008_-1_-1_-1_1_y_");
			}
			case 7011:
			{
				return string.Concat(ItemState.getAmmo(8), "_10009_-1_-1_9009_1_y_");
			}
			case 7012:
			{
				return string.Concat(ItemState.getAmmo(40), "_10011_-1_-1_9011_2_y_");
			}
			case 7013:
			{
				return string.Concat(ItemState.getAmmo(10), "_10012_-1_-1_9012_2_y_");
			}
			case 7014:
			{
				return string.Concat(ItemState.getAmmo(1), "_25001_-1_-1_9011_1_y_");
			}
			case 7015:
			{
				return string.Concat(ItemState.getAmmo(5), "_10003_-1_-1_9013_1_y_");
			}
			case 7016:
			{
				return string.Concat(ItemState.getAmmo(4), "_25000_-1_-1_-1_1_y_");
			}
			case 7017:
			{
				return string.Concat(ItemState.getAmmo(50), "_10013_-1_-1_9011_0_y_");
			}
			case 7018:
			{
				return string.Concat(ItemState.getAmmo(7), "_10002_-1_-1_-1_0_y_");
			}
			default:
			{
				if (num == 8001)
				{
					break;
				}
				else
				{
					if (num == 8008)
					{
						return "b";
					}
					if (num == 20000)
					{
						return "f";
					}
					if (num == 26000)
					{
						return "f";
					}
					return string.Empty;
				}
			}
		}
		return "b";
	}
}