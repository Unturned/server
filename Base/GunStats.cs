using System;
using UnityEngine;

public class GunStats
{
	public GunStats()
	{
	}

	public static float getADS(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return 0.5f;
			}
			case 7001:
			{
				return 0.5f;
			}
			case 7002:
			{
				return 0.9f;
			}
			case 7003:
			{
				return 0.5f;
			}
			case 7004:
			{
				return 0.01f;
			}
			case 7005:
			{
				return 0.9f;
			}
			case 7006:
			{
				return 1f;
			}
			case 7007:
			{
				return 0.01f;
			}
			case 7008:
			{
				return 0.5f;
			}
			case 7009:
			{
				return 0.5f;
			}
			case 7010:
			{
				return 0.5f;
			}
			case 7011:
			{
				return 0.01f;
			}
			case 7012:
			{
				return 0.3f;
			}
			case 7013:
			{
				return 0.01f;
			}
			case 7014:
			{
				return 0.01f;
			}
			case 7015:
			{
				return 0.5f;
			}
			case 7016:
			{
				return 0.9f;
			}
			case 7017:
			{
				return 0.3f;
			}
			case 7018:
			{
				return 0.5f;
			}
			default:
			{
				return 1f;
			}
		}
	}

	public static int getDamage(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return 81;
			}
			case 7001:
			{
				return 34;
			}
			case 7002:
			{
				return 20;
			}
			case 7003:
			{
				return 90;
			}
			case 7004:
			{
				return 70;
			}
			case 7005:
			{
				return 18;
			}
			case 7006:
			{
				return 35;
			}
			case 7007:
			{
				return 99;
			}
			case 7008:
			{
				return 81;
			}
			case 7009:
			{
				return 81;
			}
			case 7010:
			{
				return 65;
			}
			case 7011:
			{
				return 115;
			}
			case 7012:
			{
				return 31;
			}
			case 7013:
			{
				return 99;
			}
			case 7014:
			{
				return 99;
			}
			case 7015:
			{
				return 85;
			}
			case 7016:
			{
				return 16;
			}
			case 7017:
			{
				return 36;
			}
			case 7018:
			{
				return 67;
			}
		}
		return 0;
	}

	public static float getEffectiveness(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return 400f;
			}
			case 7001:
			{
				return 100f;
			}
			case 7002:
			{
				return 20f;
			}
			case 7003:
			{
				return 300f;
			}
			case 7004:
			{
				return 150f;
			}
			case 7005:
			{
				return 20f;
			}
			case 7006:
			{
				return 100f;
			}
			case 7007:
			{
				return 150f;
			}
			case 7008:
			{
				return 400f;
			}
			case 7009:
			{
				return 400f;
			}
			case 7010:
			{
				return 50f;
			}
			case 7011:
			{
				return 400f;
			}
			case 7012:
			{
				return 200f;
			}
			case 7013:
			{
				return 350f;
			}
			case 7014:
			{
				return 200f;
			}
			case 7015:
			{
				return 325f;
			}
			case 7016:
			{
				return 15f;
			}
			case 7017:
			{
				return 250f;
			}
			case 7018:
			{
				return 60f;
			}
		}
		return 0f;
	}

	public static int getFiretype(int id)
	{
		switch (id)
		{
			case 7003:
			{
				return 1;
			}
			case 7004:
			{
				return 2;
			}
			case 7005:
			{
				return 1;
			}
			case 7006:
			case 7007:
			case 7008:
			case 7009:
			case 7010:
			case 7012:
			case 7013:
			{
				return 0;
			}
			case 7011:
			{
				return 1;
			}
			case 7014:
			{
				return 2;
			}
			case 7015:
			{
				return 1;
			}
			case 7016:
			{
				return 1;
			}
			default:
			{
				return 0;
			}
		}
	}

	public static int getPellets(int id)
	{
		switch (id)
		{
			case 25000:
			{
				return 12;
			}
			case 25001:
			{
				return 1;
			}
			case 25002:
			{
				return 1;
			}
			default:
			{
				return 1;
			}
		}
	}

	public static float getRange(int id)
	{
		int num = id;
		switch (num)
		{
			case 9009:
			{
				return 4.5f;
			}
			case 9010:
			{
				return 10f;
			}
			case 9012:
			{
				return 12.8571424f;
			}
			default:
			{
				switch (num)
				{
					case 9002:
					{
						return 7.5f;
					}
					case 9003:
					{
						return 90f;
					}
					case 9004:
					{
						return 15f;
					}
					default:
					{
						return 90f;
					}
				}
				break;
			}
		}
	}

	public static float getRecoil_X(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return UnityEngine.Random.Range(-0.5f, 0.5f);
			}
			case 7001:
			{
				return UnityEngine.Random.Range(-0.25f, 0.25f);
			}
			case 7002:
			{
				return UnityEngine.Random.Range(-1f, 1f);
			}
			case 7003:
			{
				return UnityEngine.Random.Range(-0.5f, 0.5f);
			}
			case 7004:
			{
				return UnityEngine.Random.Range(-0.1f, 0.1f);
			}
			case 7005:
			{
				return UnityEngine.Random.Range(-1f, 1f);
			}
			case 7006:
			{
				return UnityEngine.Random.Range(-0.25f, 0.25f);
			}
			case 7007:
			{
				return UnityEngine.Random.Range(-0.1f, 0.1f);
			}
			case 7008:
			{
				return UnityEngine.Random.Range(-0.4f, 0.4f);
			}
			case 7009:
			{
				return UnityEngine.Random.Range(-0.25f, -0.1f);
			}
			case 7010:
			{
				return UnityEngine.Random.Range(-0.4f, -0.25f);
			}
			case 7011:
			{
				return UnityEngine.Random.Range(0.5f, 0.7f);
			}
			case 7012:
			{
				return UnityEngine.Random.Range(-0.3f, -0.5f);
			}
			case 7013:
			{
				return UnityEngine.Random.Range(5.4f, 5.6f);
			}
			case 7014:
			{
				return UnityEngine.Random.Range(-0.07f, 0.07f);
			}
			case 7015:
			{
				return UnityEngine.Random.Range(-0.7f, -0.5f);
			}
			case 7016:
			{
				return UnityEngine.Random.Range(3.7f, 3.9f);
			}
			case 7017:
			{
				return UnityEngine.Random.Range(-0.8f, -1f);
			}
			case 7018:
			{
				return UnityEngine.Random.Range(0.5f, 0.7f);
			}
		}
		return 0f;
	}

	public static float getRecoil_Y(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return UnityEngine.Random.Range(15f, 18f);
			}
			case 7001:
			{
				return UnityEngine.Random.Range(6f, 9f);
			}
			case 7002:
			{
				return UnityEngine.Random.Range(30f, 33f);
			}
			case 7003:
			{
				return UnityEngine.Random.Range(40f, 43f);
			}
			case 7004:
			{
				return UnityEngine.Random.Range(5f, 8f);
			}
			case 7005:
			{
				return UnityEngine.Random.Range(32f, 35f);
			}
			case 7006:
			{
				return UnityEngine.Random.Range(7f, 10f);
			}
			case 7007:
			{
				return UnityEngine.Random.Range(5f, 8f);
			}
			case 7008:
			{
				return UnityEngine.Random.Range(16f, 19f);
			}
			case 7009:
			{
				return UnityEngine.Random.Range(17f, 20f);
			}
			case 7010:
			{
				return UnityEngine.Random.Range(25f, 28f);
			}
			case 7011:
			{
				return UnityEngine.Random.Range(60f, 63f);
			}
			case 7012:
			{
				return UnityEngine.Random.Range(6f, 9f);
			}
			case 7013:
			{
				return UnityEngine.Random.Range(33f, 36f);
			}
			case 7014:
			{
				return UnityEngine.Random.Range(6f, 9f);
			}
			case 7015:
			{
				return UnityEngine.Random.Range(35f, 38f);
			}
			case 7016:
			{
				return UnityEngine.Random.Range(20f, 23f);
			}
			case 7017:
			{
				return UnityEngine.Random.Range(5f, 8f);
			}
			case 7018:
			{
				return UnityEngine.Random.Range(30f, 33f);
			}
		}
		return 0f;
	}

	public static float getROF(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return 0.24f;
			}
			case 7001:
			{
				return 0.2f;
			}
			case 7002:
			{
				return 0.6f;
			}
			case 7003:
			{
				return 0.6f;
			}
			case 7004:
			{
				return 0.2f;
			}
			case 7005:
			{
				return 0.6f;
			}
			case 7006:
			{
				return 0.2f;
			}
			case 7007:
			{
				return 0.2f;
			}
			case 7008:
			{
				return 0.15f;
			}
			case 7009:
			{
				return 0.15f;
			}
			case 7010:
			{
				return 0.2f;
			}
			case 7011:
			{
				return 0.2f;
			}
			case 7012:
			{
				return 0.1f;
			}
			case 7013:
			{
				return 0.3f;
			}
			case 7014:
			{
				return 0.2f;
			}
			case 7015:
			{
				return 0.6f;
			}
			case 7016:
			{
				return 0.6f;
			}
			case 7017:
			{
				return 0.08571429f;
			}
			case 7018:
			{
				return 0.2f;
			}
		}
		return 0f;
	}

	public static float getSpray(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return 10f;
			}
			case 7001:
			{
				return 5f;
			}
			case 7002:
			{
				return 30f;
			}
			case 7003:
			{
				return 20f;
			}
			case 7004:
			{
				return 10f;
			}
			case 7005:
			{
				return 30f;
			}
			case 7006:
			{
				return 5f;
			}
			case 7007:
			{
				return 30f;
			}
			case 7008:
			{
				return 10f;
			}
			case 7009:
			{
				return 15f;
			}
			case 7010:
			{
				return 15f;
			}
			case 7011:
			{
				return 30f;
			}
			case 7012:
			{
				return 10f;
			}
			case 7013:
			{
				return 25f;
			}
			case 7014:
			{
				return 10f;
			}
			case 7015:
			{
				return 15f;
			}
			case 7016:
			{
				return 30f;
			}
			case 7017:
			{
				return 8f;
			}
			case 7018:
			{
				return 15f;
			}
		}
		return 0f;
	}

	public static float getSpread(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return UnityEngine.Random.Range(-0.05f, 0.05f);
			}
			case 7001:
			{
				return UnityEngine.Random.Range(-0.075f, 0.075f);
			}
			case 7002:
			{
				return UnityEngine.Random.Range(-0.65f, 0.65f);
			}
			case 7003:
			{
				return UnityEngine.Random.Range(-0.04f, 0.04f);
			}
			case 7004:
			{
				return UnityEngine.Random.Range(-0.7f, 0.7f);
			}
			case 7005:
			{
				return UnityEngine.Random.Range(-0.5f, 0.5f);
			}
			case 7006:
			{
				return UnityEngine.Random.Range(-0.075f, 0.075f);
			}
			case 7007:
			{
				return UnityEngine.Random.Range(-0.7f, 0.7f);
			}
			case 7008:
			{
				return UnityEngine.Random.Range(-0.04f, 0.04f);
			}
			case 7009:
			{
				return UnityEngine.Random.Range(-0.06f, 0.06f);
			}
			case 7010:
			{
				return UnityEngine.Random.Range(-0.05f, 0.05f);
			}
			case 7011:
			{
				return UnityEngine.Random.Range(-0.7f, 0.7f);
			}
			case 7012:
			{
				return UnityEngine.Random.Range(-0.15f, 0.15f);
			}
			case 7013:
			{
				return UnityEngine.Random.Range(-0.6f, 0.6f);
			}
			case 7014:
			{
				return UnityEngine.Random.Range(-0.7f, 0.7f);
			}
			case 7015:
			{
				return UnityEngine.Random.Range(-0.05f, 0.05f);
			}
			case 7016:
			{
				return UnityEngine.Random.Range(-0.55f, 0.55f);
			}
			case 7017:
			{
				return UnityEngine.Random.Range(-0.1f, 0.1f);
			}
			case 7018:
			{
				return UnityEngine.Random.Range(-0.06f, 0.06f);
			}
		}
		return 0f;
	}

	public static float getZoom(int id)
	{
		switch (id)
		{
			case 9000:
			{
				return 10f;
			}
			case 9001:
			case 9005:
			case 9006:
			case 9007:
			case 9008:
			case 9011:
			{
				return 0f;
			}
			case 9002:
			{
				return 20f;
			}
			case 9003:
			{
				return 10f;
			}
			case 9004:
			{
				return 20f;
			}
			case 9009:
			{
				return 20f;
			}
			case 9010:
			{
				return 10f;
			}
			case 9012:
			{
				return 10f;
			}
			default:
			{
				return 0f;
			}
		}
	}

	public static bool hasAuto(int id)
	{
		switch (id)
		{
			case 7008:
			{
				return true;
			}
			case 7009:
			{
				return true;
			}
			case 7010:
			case 7011:
			case 7014:
			case 7015:
			case 7016:
			{
				return false;
			}
			case 7012:
			{
				return true;
			}
			case 7013:
			{
				return true;
			}
			case 7017:
			{
				return true;
			}
			default:
			{
				return false;
			}
		}
	}

	public static bool hasHammer(int id)
	{
		int num = id;
		switch (num)
		{
			case 7002:
			{
				return false;
			}
			case 7004:
			{
				return false;
			}
			case 7005:
			{
				return false;
			}
			case 7007:
			{
				return false;
			}
			default:
			{
				switch (num)
				{
					case 7014:
					{
						return false;
					}
					case 7015:
					{
						return true;
					}
					case 7016:
					{
						return false;
					}
					default:
					{
						return true;
					}
				}
				break;
			}
		}
	}

	public static bool hasSafety(int id)
	{
		int num = id;
		switch (num)
		{
			case 7000:
			{
				return true;
			}
			case 7001:
			{
				return true;
			}
			case 7005:
			{
				return true;
			}
			case 7006:
			{
				return true;
			}
			case 7008:
			{
				return true;
			}
			default:
			{
				if (num == 7017)
				{
					break;
				}
				else
				{
					if (num == 7018)
					{
						return true;
					}
					return false;
				}
			}
		}
		return true;
	}

	public static bool hasSingle(int id)
	{
		switch (id)
		{
			case 7000:
			{
				return true;
			}
			case 7001:
			{
				return true;
			}
			case 7002:
			{
				return true;
			}
			case 7003:
			{
				return true;
			}
			case 7004:
			{
				return true;
			}
			case 7005:
			{
				return true;
			}
			case 7006:
			{
				return true;
			}
			case 7007:
			{
				return true;
			}
			case 7008:
			{
				return true;
			}
			case 7009:
			{
				return true;
			}
			case 7010:
			{
				return true;
			}
			case 7011:
			{
				return true;
			}
			case 7012:
			case 7013:
			{
				return false;
			}
			case 7014:
			{
				return true;
			}
			case 7015:
			{
				return true;
			}
			case 7016:
			{
				return true;
			}
			case 7017:
			{
				return true;
			}
			case 7018:
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