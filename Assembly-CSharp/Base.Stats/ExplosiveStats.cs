using System;

public class ExplosiveStats
{
	public ExplosiveStats()
	{
	}

	public static int getDamage(int id)
	{
		int num = id;
		switch (num)
		{
			case 16015:
			{
				return 300;
			}
			case 16016:
			{
				return 80;
			}
			case 16017:
			{
				return 150;
			}
			default:
			{
				if (num == 23007)
				{
					break;
				}
				else
				{
					return 0;
				}
			}
		}
		return 80;
	}

	public static int getRange(int id)
	{
		int num = id;
		switch (num)
		{
			case 16015:
			{
				return 12;
			}
			case 16016:
			{
				return 6;
			}
			case 16017:
			{
				return 4;
			}
			default:
			{
				if (num == 23007)
				{
					break;
				}
				else
				{
					return 0;
				}
			}
		}
		return 8;
	}
}