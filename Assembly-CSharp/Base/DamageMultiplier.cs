using System;

public class DamageMultiplier
{
	public DamageMultiplier()
	{
	}

	public static float getMultiplierPlayer(int limb)
	{
		if (ServerSettings.mode != 2)
		{
			switch (limb)
			{
				case 0:
				{
					return 0.1f;
				}
				case 1:
				{
					return 0.3f;
				}
				case 2:
				{
					return 0.1f;
				}
				case 3:
				{
					return 0.3f;
				}
				case 4:
				{
					return 1.05f;
				}
				case 5:
				{
					return 0.6f;
				}
			}
		}
		else
		{
			switch (limb)
			{
				case 0:
				{
					return 0.25f;
				}
				case 1:
				{
					return 0.4f;
				}
				case 2:
				{
					return 0.25f;
				}
				case 3:
				{
					return 0.4f;
				}
				case 4:
				{
					return 1.2f;
				}
				case 5:
				{
					return 0.95f;
				}
			}
		}
		return 1f;
	}

	public static float getMultiplierZombie(int limb)
	{
		if (ServerSettings.mode != 2)
		{
			switch (limb)
			{
				case 0:
				{
					return 0.2f;
				}
				case 1:
				{
					return 0.4f;
				}
				case 2:
				{
					return 0.2f;
				}
				case 3:
				{
					return 0.4f;
				}
				case 4:
				{
					return 1.55f;
				}
				case 5:
				{
					return 0.6f;
				}
			}
		}
		else
		{
			switch (limb)
			{
				case 0:
				{
					return 0.1f;
				}
				case 1:
				{
					return 0.2f;
				}
				case 2:
				{
					return 0.1f;
				}
				case 3:
				{
					return 0.2f;
				}
				case 4:
				{
					return 1f;
				}
				case 5:
				{
					return 0.5f;
				}
			}
		}
		return 1f;
	}
}