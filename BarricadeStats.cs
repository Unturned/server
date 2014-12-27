using System;

public class BarricadeStats
{
	public BarricadeStats()
	{
	}

	public static bool getBarrier(int id)
	{
		int num = id;
		if (num == 16020)
		{
			return true;
		}
		if (num == 16021)
		{
			return true;
		}
		return false;
	}

	public static int getCapacity(int id)
	{
		int num = id;
		switch (num)
		{
			case 16023:
			{
				return 2;
			}
			case 16025:
			{
				return 3;
			}
			default:
			{
				if (num == 16019)
				{
					break;
				}
				else
				{
					return 2;
				}
			}
		}
		return 2;
	}

	public static int getDamage(int id)
	{
		int num = id;
		switch (num)
		{
			case 16003:
			{
				return 20;
			}
			case 16004:
			{
				return 80;
			}
			case 16005:
			{
				return 40;
			}
			case 16006:
			{
				return 60;
			}
			case 16009:
			{
				return 50;
			}
			default:
			{
				if (num == 16020)
				{
					break;
				}
				else
				{
					if (num == 16021)
					{
						return 150;
					}
					return 0;
				}
			}
		}
		return 150;
	}

	public static string getEffect(int id)
	{
		int num = id;
		switch (num)
		{
			case 16001:
			{
				return "Effects/debrisMetal";
			}
			case 16003:
			{
				return "Effects/debrisMetal";
			}
			case 16004:
			{
				return "Effects/debrisMetal";
			}
			case 16006:
			{
				return "Effects/debrisMetal";
			}
			case 16009:
			{
				return "Effects/debrisMetal";
			}
			case 16010:
			{
				return "Effects/debrisCloth";
			}
			case 16011:
			{
				return "Effects/debrisCloth";
			}
			case 16012:
			{
				return "Effects/debrisCloth";
			}
			case 16014:
			{
				return "Effects/debrisMetal";
			}
			case 16015:
			{
				return "Effects/debrisMetal";
			}
			case 16016:
			{
				return "Effects/debrisMetal";
			}
			case 16017:
			{
				return "Effects/debrisMetal";
			}
			case 16018:
			{
				return "Effects/debrisMetal";
			}
			case 16020:
			{
				return "Effects/debrisMetal";
			}
			case 16021:
			{
				return "Effects/debrisMetal";
			}
			case 16023:
			{
				return "Effects/debrisMetal";
			}
			case 16028:
			{
				return "Effects/debrisMetal";
			}
			default:
			{
				switch (num)
				{
					case 22000:
					{
						return "Effects/debrisPlant";
					}
					case 22001:
					{
						return "Effects/debrisPlant";
					}
					case 22002:
					{
						return "Effects/debrisPlant";
					}
					case 22003:
					{
						return "Effects/debrisPlant";
					}
					case 22004:
					{
						return "Effects/debrisPlant";
					}
				}
				break;
			}
		}
		return "Effects/debrisWood";
	}

	public static bool getElectric(int id)
	{
		int num = id;
		switch (num)
		{
			case 16007:
			{
				return true;
			}
			case 16009:
			{
				return true;
			}
			default:
			{
				if (num == 16002)
				{
					break;
				}
				else
				{
					if (num == 16021)
					{
						return true;
					}
					return false;
				}
			}
		}
		return true;
	}

	public static bool getFocused(int id)
	{
		int num = id;
		if (num == 16014)
		{
			return true;
		}
		if (num == 16024)
		{
			return true;
		}
		return false;
	}

	public static int getHealth(int id)
	{
		int num = id;
		switch (num)
		{
			case 16000:
			{
				return 200;
			}
			case 16001:
			{
				return 600;
			}
			case 16002:
			{
				return 200;
			}
			case 16003:
			{
				return 70;
			}
			case 16004:
			{
				return 150;
			}
			case 16005:
			{
				return 200;
			}
			case 16006:
			{
				return 1;
			}
			case 16007:
			{
				return 250;
			}
			case 16008:
			{
				return 20;
			}
			case 16009:
			{
				return 450;
			}
			case 16010:
			{
				return 250;
			}
			case 16011:
			{
				return 50;
			}
			case 16012:
			{
				return 250;
			}
			case 16013:
			{
				return 100;
			}
			case 16014:
			{
				return 600;
			}
			case 16015:
			{
				return 1;
			}
			case 16016:
			{
				return 1;
			}
			case 16017:
			{
				return 1;
			}
			case 16018:
			{
				return 500;
			}
			case 16019:
			{
				return 300;
			}
			case 16020:
			{
				return 300;
			}
			case 16021:
			{
				return 600;
			}
			case 16022:
			{
				return 300;
			}
			case 16023:
			{
				return 600;
			}
			case 16024:
			{
				return 300;
			}
			case 16025:
			{
				return 500;
			}
			case 16026:
			{
				return 100;
			}
			case 16027:
			{
				return 400;
			}
			case 16028:
			{
				return 400;
			}
			default:
			{
				switch (num)
				{
					case 22000:
					{
						return 1;
					}
					case 22001:
					{
						return 1;
					}
					case 22002:
					{
						return 1;
					}
					case 22003:
					{
						return 1;
					}
					case 22004:
					{
						return 1;
					}
				}
				break;
			}
		}
		return 0;
	}

	public static bool getLinked(int id)
	{
		int num = id;
		switch (num)
		{
			case 16024:
			{
				return true;
			}
			case 16026:
			{
				return true;
			}
			default:
			{
				if (num == 16014)
				{
					break;
				}
				else
				{
					if (num == 16015)
					{
						return true;
					}
					if (num == 16004)
					{
						return true;
					}
					if (num == 16008)
					{
						return true;
					}
					return false;
				}
			}
		}
		return true;
	}

	public static bool getMaulable(int id)
	{
		switch (id)
		{
			case 16003:
			{
				return false;
			}
			case 16004:
			{
				return false;
			}
			case 16005:
			{
				return false;
			}
			case 16006:
			{
				return false;
			}
			case 16007:
			case 16010:
			case 16011:
			case 16012:
			case 16013:
			case 16014:
			case 16015:
			case 16018:
			case 16019:
			{
				return true;
			}
			case 16008:
			{
				return false;
			}
			case 16009:
			{
				return false;
			}
			case 16016:
			{
				return false;
			}
			case 16017:
			{
				return false;
			}
			case 16020:
			{
				return false;
			}
			case 16021:
			{
				return false;
			}
			default:
			{
				return true;
			}
		}
	}

	public static bool getOriented(int id)
	{
		int num = id;
		if (num == 16008)
		{
			return true;
		}
		if (num == 16015)
		{
			return true;
		}
		if (num == 16026)
		{
			return true;
		}
		return false;
	}

	public static int getRotation(int id)
	{
		int num = id;
		switch (num)
		{
			case 16022:
			{
				return 180;
			}
			case 16024:
			{
				return 180;
			}
			default:
			{
				if (num == 16001)
				{
					break;
				}
				else
				{
					if (num == 16014)
					{
						return 180;
					}
					return 0;
				}
			}
		}
		return 180;
	}

	public static string getSound(int id)
	{
		int num = id;
		switch (num)
		{
			case 16001:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16003:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16004:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16006:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16009:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16014:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16015:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16016:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16017:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16018:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16020:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16021:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16023:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			case 16028:
			{
				return "Sounds/Barricades/debrisMetal";
			}
			default:
			{
				switch (num)
				{
					case 22000:
					{
						return "Sounds/Barricades/debrisPlant";
					}
					case 22001:
					{
						return "Sounds/Barricades/debrisPlant";
					}
					case 22002:
					{
						return "Sounds/Barricades/debrisPlant";
					}
					case 22003:
					{
						return "Sounds/Barricades/debrisPlant";
					}
					case 22004:
					{
						return "Sounds/Barricades/debrisPlant";
					}
				}
				break;
			}
		}
		return "Sounds/Barricades/debrisWood";
	}

	public static string getState(int id)
	{
		int num = id;
		switch (num)
		{
			case 16001:
			{
				return string.Concat(PlayerSettings.id, "_", PlayerSettings.friendHash, "_f_");
			}
			case 16002:
			{
				return "f";
			}
			case 16007:
			{
				return "f";
			}
			case 16008:
			{
				return Sneaky.sneak(string.Empty);
			}
			case 16013:
			{
				return "f";
			}
			case 16014:
			{
				return string.Concat(PlayerSettings.id, "_", PlayerSettings.friendHash, "_f_");
			}
			case 16019:
			{
				return string.Empty;
			}
			case 16022:
			{
				return "f";
			}
			case 16023:
			{
				return string.Concat(PlayerSettings.id, "_", PlayerSettings.friendHash, "__");
			}
			case 16024:
			{
				return "f";
			}
			case 16025:
			{
				return string.Concat(PlayerSettings.id, "_", PlayerSettings.friendHash, "__");
			}
			case 16026:
			{
				return "f";
			}
			case 16027:
			{
				return "f";
			}
			case 16028:
			{
				return string.Concat(PlayerSettings.id, "_", PlayerSettings.friendHash, "_f_");
			}
			default:
			{
				switch (num)
				{
					case 22000:
					{
						return Epoch.getSeconds().ToString();
					}
					case 22001:
					{
						return Epoch.getSeconds().ToString();
					}
					case 22002:
					{
						return Epoch.getSeconds().ToString();
					}
					case 22003:
					{
						return Epoch.getSeconds().ToString();
					}
					case 22004:
					{
						return Epoch.getSeconds().ToString();
					}
				}
				break;
			}
		}
		return string.Empty;
	}
}