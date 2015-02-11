using System;

public class HairStyles
{
	public readonly static int[] HAIRS;

	static HairStyles()
	{
		HairStyles.HAIRS = new int[] { 
			1000, 
			1001, 
			1011, 
			1002, 
			1013, 
			1003, 
			1015, 
			1016, 
			1006, 
			1008, 
			1009, 
			1010, 
			1012, 
			1007, 
			1005, 
			1004, 
			1014 
		};
	}

	public HairStyles()
	{
	}

	public static bool getBeard(int id)
	{
		switch (id)
		{
			case 1006:
			{
				return true;
			}
			case 1007:
			case 1011:
			{
				return false;
			}
			case 1008:
			{
				return true;
			}
			case 1009:
			{
				return true;
			}
			case 1010:
			{
				return true;
			}
			case 1012:
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