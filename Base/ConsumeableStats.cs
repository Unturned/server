using System;

public class ConsumeableStats
{
	public ConsumeableStats()
	{
	}

	public static int getDrug(int id)
	{
		switch (id)
		{
			case 14026:
			{
				return 60;
			}
			case 14027:
			case 14029:
			{
				return 0;
			}
			case 14028:
			{
				return 60;
			}
			case 14030:
			{
				return 60;
			}
			default:
			{
				return 0;
			}
		}
	}

	public static int getFood(int id)
	{
		switch (id)
		{
			case 14000:
			{
				return 50;
			}
			case 14001:
			{
				return 15;
			}
			case 14002:
			{
				return 20;
			}
			case 14003:
			{
				return 10;
			}
			case 14004:
			{
				return 20;
			}
			case 14005:
			{
				return 10;
			}
			case 14006:
			{
				return 25;
			}
			case 14007:
			{
				return 10;
			}
			case 14008:
			{
				return 20;
			}
			case 14009:
			{
				return 10;
			}
			case 14010:
			{
				return 30;
			}
			case 14011:
			{
				return 12;
			}
			case 14012:
			{
				return 35;
			}
			case 14013:
			{
				return 10;
			}
			case 14014:
			{
				return 30;
			}
			case 14015:
			{
				return 30;
			}
			case 14016:
			{
				return 30;
			}
			case 14017:
			{
				return 30;
			}
			case 14018:
			{
				return 30;
			}
			case 14019:
			{
				return 30;
			}
			case 14020:
			{
				return 15;
			}
			case 14021:
			{
				return 15;
			}
			case 14022:
			{
				return 80;
			}
			case 14023:
			{
				return 10;
			}
			case 14024:
			{
				return 40;
			}
			case 14025:
			{
				return 10;
			}
			case 14026:
			{
				return 10;
			}
			case 14027:
			{
				return 10;
			}
			case 14028:
			{
				return 10;
			}
			case 14029:
			{
				return 10;
			}
			case 14030:
			{
				return 10;
			}
			case 14031:
			{
				return 10;
			}
			case 14032:
			{
				return 30;
			}
			case 14033:
			{
				return 8;
			}
		}
		return 0;
	}

	public static int getSickness(int id)
	{
		int num = id;
		switch (num)
		{
			case 14003:
			{
				return 10;
			}
			case 14005:
			{
				return 10;
			}
			case 14007:
			{
				return 15;
			}
			case 14009:
			{
				return 10;
			}
			case 14011:
			{
				return 15;
			}
			case 14013:
			{
				return 30;
			}
			default:
			{
				switch (num)
				{
					case 14030:
					{
						return 20;
					}
					case 14033:
					{
						return 10;
					}
					default:
					{
						switch (num)
						{
							case 15005:
							{
								return 30;
							}
							case 15006:
							{
								return 30;
							}
							case 15007:
							{
								return 20;
							}
							default:
							{
								if (num != 14025)
								{
									return 0;
								}
								break;
							}
						}
						break;
					}
				}
				break;
			}
		}
		return 30;
	}

	public static int getStamina(int id)
	{
		int num = id;
		switch (num)
		{
			case 14027:
			{
				return 25;
			}
			case 14028:
			{
				return 50;
			}
			case 14032:
			{
				return 20;
			}
			default:
			{
				switch (num)
				{
					case 15004:
					{
						return 40;
					}
					case 15006:
					{
						return 10;
					}
					default:
					{
						if (num != 14001)
						{
							if (num == 14012)
							{
								return 25;
							}
							if (num == 14023)
							{
								return 10;
							}
							return 0;
						}
						break;
					}
				}
				break;
			}
		}
		return 50;
	}

	public static int getWater(int id)
	{
		int num = id;
		switch (num)
		{
			case 14014:
			{
				return 20;
			}
			case 14022:
			{
				return 80;
			}
			case 14025:
			{
				return 5;
			}
			case 14026:
			{
				return 5;
			}
			case 14027:
			{
				return 5;
			}
			case 14028:
			{
				return 5;
			}
			case 14029:
			{
				return 5;
			}
			case 14030:
			{
				return 5;
			}
			case 14031:
			{
				return 20;
			}
			default:
			{
				switch (num)
				{
					case 15000:
					{
						return 50;
					}
					case 15001:
					{
						return 15;
					}
					case 15002:
					{
						return 10;
					}
					case 15003:
					{
						return 60;
					}
					case 15004:
					{
						return 40;
					}
					case 15005:
					{
						return 35;
					}
					case 15006:
					{
						return 35;
					}
					case 15007:
					{
						return 35;
					}
					case 15008:
					{
						return 50;
					}
					case 15009:
					{
						return 50;
					}
					default:
					{
						if (num != 26000)
						{
							return 0;
						}
						break;
					}
				}
				break;
			}
		}
		return 25;
	}
}