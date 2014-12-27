using System;

public class ItemAmount
{
	public ItemAmount()
	{
	}

	public static int getAmount(int id)
	{
		switch (id)
		{
			case 10000:
			{
				return 31;
			}
			case 10001:
			{
				return 101;
			}
			case 10002:
			{
				return 8;
			}
			case 10003:
			{
				return 6;
			}
			case 10004:
			{
				return 14;
			}
			case 10005:
			{
				return 21;
			}
			case 10006:
			{
				return 31;
			}
			case 10007:
			{
				return 76;
			}
			case 10008:
			{
				return 7;
			}
			case 10009:
			{
				return 9;
			}
			case 10010:
			{
				return 7;
			}
			case 10011:
			{
				return 41;
			}
			case 10012:
			{
				return 11;
			}
			case 10013:
			{
				return 51;
			}
		}
		return 1;
	}
}