using System;

public class HarvestStats
{
	public HarvestStats()
	{
	}

	public static int getCrop(int id)
	{
		switch (id)
		{
			case 22000:
			{
				return 14002;
			}
			case 22001:
			{
				return 14004;
			}
			case 22002:
			{
				return 14006;
			}
			case 22003:
			{
				return 14008;
			}
			case 22004:
			{
				return 14010;
			}
		}
		return -1;
	}

	public static int getGrowth(int id)
	{
		switch (id)
		{
			case 22000:
			{
				return 800;
			}
			case 22001:
			{
				return 700;
			}
			case 22002:
			{
				return 900;
			}
			case 22003:
			{
				return 700;
			}
			case 22004:
			{
				return 800;
			}
		}
		return 0;
	}
}