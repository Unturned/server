using System;

public class BagSize
{
	public BagSize()
	{
	}

	public static int getCapacity(int id)
	{
		switch (id)
		{
			case 2000:
			{
				return 7500;
			}
			case 2001:
			{
				return 10000;
			}
			case 2002:
			{
				return 12500;
			}
			case 2003:
			{
				return 15000;
			}
			case 2004:
			{
				return 17500;
			}
			case 2005:
			{
				return 20000;
			}
			case 2006:
			{
				return 10000;
			}
		}
		return 5000;
	}

	public static int getHeight(int id)
	{
		switch (id)
		{
			case 2000:
			{
				return 2;
			}
			case 2001:
			{
				return 3;
			}
			case 2002:
			{
				return 4;
			}
			case 2003:
			{
				return 5;
			}
			case 2004:
			{
				return 5;
			}
			case 2005:
			{
				return 4;
			}
			case 2006:
			{
				return 2;
			}
		}
		return 1;
	}

	public static int getWidth(int id)
	{
		switch (id)
		{
			case 2000:
			{
				return 4;
			}
			case 2001:
			{
				return 4;
			}
			case 2002:
			{
				return 4;
			}
			case 2003:
			{
				return 4;
			}
			case 2004:
			{
				return 5;
			}
			case 2005:
			{
				return 5;
			}
			case 2006:
			{
				return 5;
			}
		}
		return 4;
	}
}