using System;

public class MeleeStats
{
	public MeleeStats()
	{
	}

	public static int getDamage(int id)
	{
		int num = id;
		switch (num)
		{
			case 8000:
			{
				return 60;
			}
			case 8001:
			{
				return 10;
			}
			case 8002:
			{
				return 60;
			}
			case 8003:
			{
				return 40;
			}
			case 8004:
			{
				return 30;
			}
			case 8005:
			{
				return 35;
			}
			case 8006:
			{
				return 45;
			}
			case 8007:
			{
				return 30;
			}
			case 8008:
			{
				return 10;
			}
			case 8009:
			{
				return 70;
			}
			case 8010:
			{
				return 30;
			}
			case 8011:
			{
				return 35;
			}
			case 8012:
			{
				return 50;
			}
			case 8013:
			{
				return 100;
			}
			case 8014:
			{
				return 50;
			}
			case 8015:
			{
				return 60;
			}
			case 8016:
			{
				return 50;
			}
			case 8017:
			{
				return 15;
			}
			case 8018:
			{
				return 25;
			}
			case 8019:
			{
				return 35;
			}
			default:
			{
				if (num == 21000)
				{
					break;
				}
				else
				{
					if (num == 21001)
					{
						return 8;
					}
					return 0;
				}
			}
		}
		return 4;
	}

	public static float getRange(int id)
	{
		int num = id;
		switch (num)
		{
			case 8000:
			{
				return 2f;
			}
			case 8001:
			{
				return 1.5f;
			}
			case 8002:
			{
				return 2f;
			}
			case 8003:
			{
				return 1.5f;
			}
			case 8004:
			{
				return 1.75f;
			}
			case 8005:
			{
				return 1.5f;
			}
			case 8006:
			{
				return 2f;
			}
			case 8007:
			{
				return 1.75f;
			}
			case 8008:
			{
				return 1.5f;
			}
			case 8009:
			{
				return 2f;
			}
			case 8010:
			{
				return 1.75f;
			}
			case 8011:
			{
				return 1.5f;
			}
			case 8012:
			{
				return 1.5f;
			}
			case 8013:
			{
				return 2f;
			}
			case 8014:
			{
				return 2f;
			}
			case 8015:
			{
				return 2f;
			}
			case 8016:
			{
				return 1.75f;
			}
			case 8017:
			{
				return 1.75f;
			}
			case 8018:
			{
				return 1.75f;
			}
			case 8019:
			{
				return 1.5f;
			}
			default:
			{
				if (num == 21000)
				{
					break;
				}
				else
				{
					if (num == 21001)
					{
						return 2f;
					}
					return 0f;
				}
			}
		}
		return 1.5f;
	}
}