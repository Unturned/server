using System;

public class ArmorStats
{
	public ArmorStats()
	{
	}

	public static float getArmor(int id)
	{
		int num = id;
		switch (num)
		{
			case 3:
			{
				return 0.95f;
			}
			case 4:
			{
				return 0.95f;
			}
			case 6:
			{
				return 0.95f;
			}
			case 7:
			{
				return 0.8f;
			}
			case 8:
			{
				return 0.8f;
			}
			default:
			{
				switch (num)
				{
					case 3000:
					{
						return 0.85f;
					}
					case 3001:
					{
						return 0.8f;
					}
					case 3002:
					{
						return 0.8f;
					}
					case 3003:
					{
						return 0.9f;
					}
					default:
					{
						switch (num)
						{
							case 17:
							{
								return 0.8f;
							}
							case 19:
							{
								return 0.95f;
							}
							default:
							{
								if (num != 4003)
								{
									if (num == 4004)
									{
										return 0.95f;
									}
									if (num == 5003)
									{
										return 0.95f;
									}
									if (num == 5004)
									{
										return 0.95f;
									}
									if (num == 4016)
									{
										return 0.975f;
									}
									if (num == 5016)
									{
										return 0.975f;
									}
									return 1f;
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
		return 0.95f;
	}

	public static bool getCover(int id)
	{
		int num = id;
		if (num == 0)
		{
			return false;
		}
		if (num == 1)
		{
			return false;
		}
		if (num == 10)
		{
			return false;
		}
		return true;
	}

	public static bool getMask(int id)
	{
		switch (id)
		{
			case 0:
			{
				return false;
			}
			case 1:
			{
				return false;
			}
			case 2:
			{
				return false;
			}
			case 3:
			{
				return false;
			}
			case 4:
			{
				return false;
			}
			case 5:
			{
				return false;
			}
			case 6:
			{
				return false;
			}
			case 7:
			{
				return false;
			}
			case 8:
			{
				return false;
			}
			case 9:
			{
				return false;
			}
			case 10:
			case 11:
			case 12:
			case 18:
			{
				return true;
			}
			case 13:
			{
				return false;
			}
			case 14:
			{
				return false;
			}
			case 15:
			{
				return false;
			}
			case 16:
			{
				return false;
			}
			case 17:
			{
				return false;
			}
			case 19:
			{
				return false;
			}
			default:
			{
				return true;
			}
		}
	}
}