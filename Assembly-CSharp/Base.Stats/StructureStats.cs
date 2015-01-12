using System;

public class StructureStats
{
	public static int getHealth(int id)
	{
		switch (id)
		{
			case 17000:
			{
				return 700;
			}
			case 17001:
			{
				return 500;
			}
			case 17002:
			{
				return 500;
			}
			case 17003:
			{
				return 500;
			}
			case 17004:
			{
				return 500;
			}
			case 17005:
			{
				return 500;
			}
			case 17006:
			{
				return 500;
			}
			case 17007:
			{
				return 500;
			}
			case 17008:
			{
				return 500;
			}
			case 17009:
			{
				return 100;
			}
			case 17010:
			{
				return 500;
			}
			case 17011:
			{
				return 500;
			}
			case 17012:
			{
				return 500;
			}
			case 17013:
			{
				return 800;
			}
			case 17014:
			{
				return 900;
			}
			case 17015:
			{
				return 1100;
			}
			case 17016:
			{
				return 900;
			}
			case 17017:
			{
				return 900;
			}
			case 17018:
			{
				return 900;
			}
			case 17019:
			{
				return 500;
			}
			case 17020:
			{
				return 500;
			}
			case 17021:
			{
				return 900;
			}
		}
		return 0;
	}

	public static int getRotation(int id)
	{
		return 0;
	}

	public static string getState(int id)
	{
		return string.Empty;
	}

	public static bool isFloor(int id)
	{
		switch (id)
		{
			case 17004:
			{
				return true;
			}
			case 17005:
			{
				return true;
			}
			case 17006:
			{
				return false;
			}
			case 17007:
			{
				return true;
			}
			case 17008:
			{
				return true;
			}
			default:
			{
				return false;
			}
		}
	}

	public static bool isFoundation(int id)
	{
		if (id == 17000)
		{
			return true;
		}
		if (id == 17006)
		{
			return true;
		}
		if (id == 17019)
		{
			return true;
		}
		return false;
	}

	public static bool isLadder(int id)
	{
		if (id == 17009)
		{
			return true;
		}
		return false;
	}

	public static bool isPillar(int id)
	{
		int num = id;
		if (num == 17003)
		{
			return true;
		}
		if (num == 17018)
		{
			return true;
		}
		return false;
	}

	public static bool isPost(int id)
	{
		switch (id)
		{
			case 17011:
			{
				return true;
			}
			case 17012:
			case 17013:
			{
				return false;
			}
			case 17014:
			{
				return true;
			}
			default:
			{
				return false;
			}
		}
	}

	public static bool isRampart(int id)
	{
		int num = id;
		if (num == 17012)
		{
			return true;
		}
		if (num == 17013)
		{
			return true;
		}
		return false;
	}

	public static bool isWall(int id)
	{
		int num = id;
		switch (num)
		{
			case 17010:
			{
				return true;
			}
			case 17015:
			{
				return true;
			}
			case 17016:
			{
				return true;
			}
			case 17017:
			{
				return true;
			}
			case 17020:
			{
				return true;
			}
			case 17021:
			{
				return true;
			}
			default:
			{
				if (num == 17001)
				{
					break;
				}
				else
				{
					if (num == 17002)
					{
						return true;
					}
					return false;
				}
			}
		}
		return true;
	}
}