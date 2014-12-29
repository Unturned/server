using System;
using UnityEngine;

public class AmmoStats : MonoBehaviour
{
	public AmmoStats()
	{
	}

	public static bool getCaliberCompatible(int ammo_0, int ammo_1)
	{
		if (ammo_0 == ammo_1)
		{
			return true;
		}
		if (ammo_0 == 10000 && ammo_1 == 10001)
		{
			return true;
		}
		if (ammo_0 == 10001 && ammo_1 == 10000)
		{
			return true;
		}
		if (ammo_0 == 10006 && ammo_1 == 10007)
		{
			return true;
		}
		if (ammo_0 == 10007 && ammo_1 == 10006)
		{
			return true;
		}
		return false;
	}

	public static int getCapacity(int gun, int ammo)
	{
		int num = gun;
		switch (num)
		{
			case 7002:
			{
				return 2;
			}
			case 7004:
			{
				return 1;
			}
			case 7005:
			{
				return 5;
			}
			case 7007:
			{
				return 1;
			}
			default:
			{
				switch (num)
				{
					case 7014:
					{
						return 1;
					}
					case 7015:
					{
						return ItemAmount.getAmount(ammo) - 1;
					}
					case 7016:
					{
						return 4;
					}
					default:
					{
						return ItemAmount.getAmount(ammo) - 1;
					}
				}
				break;
			}
		}
	}

	public static bool getGunCompatible(int gun, int ammo)
	{
		switch (gun)
		{
			case 7000:
			{
				return (ammo == 10000 || ammo == 10001 ? true : ammo == 10005);
			}
			case 7001:
			{
				return ammo == 10002;
			}
			case 7002:
			{
				return (ammo == 25000 ? true : ammo == 25002);
			}
			case 7003:
			{
				return ammo == 10003;
			}
			case 7004:
			{
				return ammo == 25001;
			}
			case 7005:
			{
				return (ammo == 25000 ? true : ammo == 25002);
			}
			case 7006:
			{
				return ammo == 10004;
			}
			case 7007:
			{
				return ammo == 25001;
			}
			case 7008:
			{
				return (ammo == 10000 || ammo == 10001 ? true : ammo == 10005);
			}
			case 7009:
			{
				return (ammo == 10006 ? true : ammo == 10007);
			}
			case 7010:
			{
				return ammo == 10008;
			}
			case 7011:
			{
				return (ammo == 10009 ? true : ammo == 10010);
			}
			case 7012:
			{
				return ammo == 10011;
			}
			case 7013:
			{
				return ammo == 10012;
			}
			case 7014:
			{
				return ammo == 25001;
			}
			case 7015:
			{
				return ammo == 10003;
			}
			case 7016:
			{
				return (ammo == 25000 ? true : ammo == 25002);
			}
			case 7017:
			{
				return ammo == 10013;
			}
			case 7018:
			{
				return ammo == 10002;
			}
		}
		return false;
	}

	public static bool getTracer(int ammo)
	{
		int num = ammo;
		if (num == 10005)
		{
			return true;
		}
		if (num == 10010)
		{
			return true;
		}
		return false;
	}
}