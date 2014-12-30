using System;

public class Maps
{
	public readonly static int[] MAPS;

	public readonly static int[] MAP_VERSION;

	static Maps()
	{
		Maps.MAPS = new int[] { 1, 2 };
		Maps.MAP_VERSION = new int[] { 0, 0, 1, 0 };
	}

	public Maps()
	{
	}

	public static string getDescription(int index)
	{
		switch (index)
		{
			case 1:
			{
				return "An island off the East coast of Canada. Known for its massive beaches, great golf and potato farming.";
			}
			case 2:
			{
				return "Wild wilderness with no hope of contact with civilization. Survival at its roots.";
			}
			case 3:
			{
				return "Arizona.";
			}
		}
		return string.Empty;
	}

	public static string getFile(int index)
	{
		switch (index)
		{
			case 0:
			{
				return "tutorial";
			}
			case 1:
			{
				return "pei";
			}
			case 2:
			{
				return "arena";
			}
			case 3:
			{
				return "arizona";
			}
		}
		return string.Empty;
	}

	public static string getName(int index)
	{
		switch (index)
		{
			case 1:
			{
				return "PEI";
			}
			case 2:
			{
				return "Arena";
			}
			case 3:
			{
				return "Arizona Testing";
			}
		}
		return string.Empty;
	}
}