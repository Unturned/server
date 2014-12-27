using System;

public class Reputation
{
	public readonly static int SPAWN_DELAY;

	static Reputation()
	{
		Reputation.SPAWN_DELAY = 30;
	}

	public Reputation()
	{
	}

	public static string getIcon(int reputation)
	{
		if (reputation == 0)
		{
			return "Textures/Icons/neutral";
		}
		if (reputation > 70)
		{
			return "Textures/Icons/hero_5";
		}
		if (reputation > 40)
		{
			return "Textures/Icons/hero_4";
		}
		if (reputation > 20)
		{
			return "Textures/Icons/hero_3";
		}
		if (reputation > 10)
		{
			return "Textures/Icons/hero_2";
		}
		if (reputation > 5)
		{
			return "Textures/Icons/hero_1";
		}
		if (reputation > 0)
		{
			return "Textures/Icons/hero_0";
		}
		if (reputation < -70)
		{
			return "Textures/Icons/bandit_5";
		}
		if (reputation < -40)
		{
			return "Textures/Icons/bandit_4";
		}
		if (reputation < -20)
		{
			return "Textures/Icons/bandit_3";
		}
		if (reputation < -10)
		{
			return "Textures/Icons/bandit_2";
		}
		if (reputation < -5)
		{
			return "Textures/Icons/bandit_1";
		}
		if (reputation < 0)
		{
			return "Textures/Icons/bandit_0";
		}
		return string.Empty;
	}
}