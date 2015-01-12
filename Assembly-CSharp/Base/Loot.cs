using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Loot
{
	public readonly static float NORMAL_ITEM_CHANCE;

	public readonly static float BAMBI_ITEM_CHANCE;

	public readonly static float GOLD_ITEM_CHANCE;

	public readonly static float HARDCORE_ITEM_CHANCE;

	public readonly static float NORMAL_ANIMAL_CHANCE;

	public readonly static float BAMBI_ANIMAL_CHANCE;

	public readonly static float GOLD_ANIMAL_CHANCE;

	public readonly static float HARDCORE_ANIMAL_CHANCE;

	public readonly static float NORMAL_ZOMBIE_CHANCE;

	public readonly static float BAMBI_ZOMBIE_CHANCE;

	public readonly static float GOLD_ZOMBIE_CHANCE;

	public readonly static float HARDCORE_ZOMBIE_CHANCE;

	public readonly static float TEIR_3;

	public readonly static float TEIR_2;

	public readonly static float TEIR_1;

	public readonly static float TEIR_0;

	public readonly static int[] CIVLIAN_4;

	public readonly static int[] CIVLIAN_3;

	public readonly static int[] CIVLIAN_2;

	public readonly static int[] CIVLIAN_1;

	public readonly static int[] CIVLIAN_0;

	public readonly static int[] FARM_4;

	public readonly static int[] FARM_3;

	public readonly static int[] FARM_2;

	public readonly static int[] FARM_1;

	public readonly static int[] FARM_0;

	public readonly static int[] FOOD_4;

	public readonly static int[] FOOD_3;

	public readonly static int[] FOOD_2;

	public readonly static int[] FOOD_1;

	public readonly static int[] FOOD_0;

	public readonly static int[] FIRE_4;

	public readonly static int[] FIRE_3;

	public readonly static int[] FIRE_2;

	public readonly static int[] FIRE_1;

	public readonly static int[] FIRE_0;

	public readonly static int[] POLICE_4;

	public readonly static int[] POLICE_3;

	public readonly static int[] POLICE_2;

	public readonly static int[] POLICE_1;

	public readonly static int[] POLICE_0;

	public readonly static int[] MEDICAL_4;

	public readonly static int[] MEDICAL_3;

	public readonly static int[] MEDICAL_2;

	public readonly static int[] MEDICAL_1;

	public readonly static int[] MEDICAL_0;

	public readonly static int[] CONSTRUCTION_4;

	public readonly static int[] CONSTRUCTION_3;

	public readonly static int[] CONSTRUCTION_2;

	public readonly static int[] CONSTRUCTION_1;

	public readonly static int[] CONSTRUCTION_0;

	public readonly static int[] GARAGE_4;

	public readonly static int[] GARAGE_3;

	public readonly static int[] GARAGE_2;

	public readonly static int[] GARAGE_1;

	public readonly static int[] GARAGE_0;

	public readonly static int[] GEAR_4;

	public readonly static int[] GEAR_3;

	public readonly static int[] GEAR_2;

	public readonly static int[] GEAR_1;

	public readonly static int[] GEAR_0;

	public readonly static int[] MILITARY_4;

	public readonly static int[] MILITARY_3;

	public readonly static int[] MILITARY_2;

	public readonly static int[] MILITARY_1;

	public readonly static int[] MILITARY_0;

	public readonly static int[] RARE_4;

	public readonly static int[] RARE_3;

	public readonly static int[] RARE_2;

	public readonly static int[] RARE_1;

	public readonly static int[] RARE_0;

	public readonly static int[] CLOTHES_4;

	public readonly static int[] CLOTHES_3;

	public readonly static int[] CLOTHES_2;

	public readonly static int[] CLOTHES_1;

	public readonly static int[] CLOTHES_0;

	public readonly static int[] BOTANIST;

	public readonly static int[] ARENA_CENTER_4;

	public readonly static int[] ARENA_CENTER_3;

	public readonly static int[] ARENA_CENTER_2;

	public readonly static int[] ARENA_CENTER_1;

	public readonly static int[] ARENA_CENTER_0;

	public readonly static int[] MILITIA_4;

	public readonly static int[] MILITIA_3;

	public readonly static int[] MILITIA_2;

	public readonly static int[] MILITIA_1;

	public readonly static int[] MILITIA_0;

	public readonly static int[] BUTCHER;

	public readonly static int[] DEER;

	public readonly static int[] PIG;

	public readonly static int[] NATURE;
	
	private static Dictionary<string, int> LootMap;

	static Loot()
	{
		Loot.NORMAL_ITEM_CHANCE = 0.4f;
		Loot.BAMBI_ITEM_CHANCE = 0.3f;
		Loot.GOLD_ITEM_CHANCE = 0.2f;
		Loot.HARDCORE_ITEM_CHANCE = 0.5f;
		Loot.NORMAL_ANIMAL_CHANCE = 0.6f;
		Loot.BAMBI_ANIMAL_CHANCE = 0.55f;
		Loot.GOLD_ANIMAL_CHANCE = 0.5f;
		Loot.HARDCORE_ANIMAL_CHANCE = 0.7f;
		Loot.NORMAL_ZOMBIE_CHANCE = 0.4f;
		Loot.BAMBI_ZOMBIE_CHANCE = 0.45f;
		Loot.GOLD_ZOMBIE_CHANCE = 0.5f;
		Loot.HARDCORE_ZOMBIE_CHANCE = 0.3f;
		Loot.TEIR_3 = 0.9f;
		Loot.TEIR_2 = 0.75f;
		Loot.TEIR_1 = 0.55f;
		Loot.TEIR_0 = 0.3f;
		Loot.CIVLIAN_4 = new int[] { 16000, 8015, 8014, 7004, 3005 };
		Loot.CIVLIAN_3 = new int[] { 16008, 3004, 2002, 9, 7010, 18017, 7015, 7016 };
		Loot.CIVLIAN_2 = new int[] { 18013, 10003, 10002, 2001, 10008, 25002, 15007, 18020 };
		Loot.CIVLIAN_1 = new int[] { 8006, 2000, 7001, 14003, 14005, 14007, 14009, 15001, 15002, 25000 };
		Loot.CIVLIAN_0 = new int[] { 8011, 16008, 18009, 14011, 18010, 18011, 8008, 10002, 25000, 25001 };
		Loot.FARM_4 = new int[] { 21001, 4013, 5013, 7003, 13, 3000, 26000, 7014 };
		Loot.FARM_3 = new int[] { 7004, 16006, 16005, 10003, 7007, 8000, 8002 };
		Loot.FARM_2 = new int[] { 8016, 7002, 7001 };
		Loot.FARM_1 = new int[] { 18013, 10002 };
		Loot.FARM_0 = new int[] { 25001, 25000, 22000, 22001, 22002, 22003, 22004 };
		Loot.FOOD_4 = new int[] { 15003, 14024, 8012, 4020, 5020, 4014, 5014, 15008, 15009, 14 };
		Loot.FOOD_3 = new int[] { 15004, 14019, 14018, 14017, 14016, 14015, 15000, 14014, 14000, 8005 };
		Loot.FOOD_2 = new int[] { 15006, 15005, 8003, 15007 };
		Loot.FOOD_1 = new int[] { 14011, 14009, 14007, 14005, 14003 };
		Loot.FOOD_0 = new int[] { 15002, 15001, 14023, 14021, 14020, 14001 };
		Loot.FIRE_4 = new int[] { 4006, 5006, 4005, 5005, 4, 5 };
		Loot.FIRE_3 = new int[] { 8001, 18010 };
		Loot.FIRE_2 = new int[] { 8009 };
		Loot.FIRE_1 = new int[] { 8008 };
		Loot.FIRE_0 = new int[] { 23006, 23005, 23004, 23003, 23002, 23001, 23000 };
		Loot.POLICE_4 = new int[] { 7000, 3003, 2 };
		Loot.POLICE_3 = new int[] { 7005, 4002, 5002, 4001, 5001, 5, 10004 };
		Loot.POLICE_2 = new int[] { 7006, 13008 };
		Loot.POLICE_1 = new int[] { 8007 };
		Loot.POLICE_0 = new int[] { 18013 };
		Loot.MEDICAL_4 = new int[] { 13007, 13002, 13000, 4008, 5008 };
		Loot.MEDICAL_3 = new int[] { 13010, 13009 };
		Loot.MEDICAL_2 = new int[] { 13006, 13005, 13004, 13018 };
		Loot.MEDICAL_1 = new int[] { 13008, 18009 };
		Loot.MEDICAL_0 = new int[] { 13001, 13003, 13017 };
		Loot.CONSTRUCTION_4 = new int[] { 16024, 16022, 16007, 8010, 4007, 5007, 19 };
		Loot.CONSTRUCTION_3 = new int[] { 18011, 16002, 18010, 8013 };
		Loot.CONSTRUCTION_2 = new int[] { 18006, 18007, 8004, 8019, 8000, 8002, 3 };
		Loot.CONSTRUCTION_1 = new int[] { 18001, 18002 };
		Loot.CONSTRUCTION_0 = new int[] { 18008, 18000, 18004 };
		Loot.GARAGE_4 = new int[] { 21000, 8013, 17009 };
		Loot.GARAGE_3 = new int[] { 2002, 20001 };
		Loot.GARAGE_2 = new int[] { 20000 };
		Loot.GARAGE_1 = new int[] { 8010, 18010 };
		Loot.GARAGE_0 = new int[] { 18001, 18002 };
		Loot.GEAR_4 = new int[] { 16002 };
		Loot.GEAR_3 = new int[] { 20000, 16012, 16011, 2003 };
		Loot.GEAR_2 = new int[] { 8019, 8002, 8000, 26000 };
		Loot.GEAR_1 = new int[] { 24000, 18000, 18001, 18002 };
		Loot.GEAR_0 = new int[] { 23000, 25000, 23001, 23002, 23003, 23004, 23005, 23006, 28000 };
		Loot.MILITARY_4 = new int[] { 23007, 14022, 2004, 1, 18016, 9004, 30000, 7018 };
		Loot.MILITARY_3 = new int[] { 18012, 16004, 10005, 7008, 9000, 18020 };
		Loot.MILITARY_2 = new int[] { 4004, 5004, 4003, 5003, 15, 7, 8, 10000, 7001 };
		Loot.MILITARY_1 = new int[] { 16003, 10002 };
		Loot.MILITARY_0 = new int[] { 18014, 16010 };
		Loot.RARE_4 = new int[] { 16017, 16016, 16015, 16009, 10001, 17, 7011, 10010 };
		Loot.RARE_3 = new int[] { 23007, 23008, 11001, 9002, 10009, 11004, 7017 };
		Loot.RARE_2 = new int[] { 18012, 11003, 11002, 2005, 12000, 10013 };
		Loot.RARE_1 = new int[] { 9004, 12001 };
		Loot.RARE_0 = new int[] { 9000, 9003, 11000, 3001, 3002 };
		Loot.CLOTHES_4 = new int[] { 4011, 5011 };
		Loot.CLOTHES_3 = new int[] { 4012, 5012 };
		Loot.CLOTHES_2 = new int[] { 4015, 5015 };
		Loot.CLOTHES_1 = new int[] { 4019, 5019, 4010, 5010, 4009, 5009 };
		Loot.CLOTHES_0 = new int[] { 18009 };
		Loot.BOTANIST = new int[] { 22000, 22001, 22002, 22003, 22004, 27000 };
		Loot.ARENA_CENTER_4 = new int[] { 7004 };
		Loot.ARENA_CENTER_3 = new int[] { 25001 };
		Loot.ARENA_CENTER_2 = new int[] { 26000, 8000, 8002 };
		Loot.ARENA_CENTER_1 = new int[] { 8011 };
		Loot.ARENA_CENTER_0 = new int[] { 22000, 22001, 22002, 22003, 22004, 18006, 18009, 18010, 18001, 18002, 18008 };
		Loot.MILITIA_4 = new int[] { 10, 9003, 9004, 7013, 9012, 30001, 10007 };
		Loot.MILITIA_3 = new int[] { 16, 7009, 9000, 11005, 10012, 23008 };
		Loot.MILITIA_2 = new int[] { 10006, 7003, 16018, 7012 };
		Loot.MILITIA_1 = new int[] { 10003, 10011 };
		Loot.MILITIA_0 = new int[] { 25000, 18013 };
		Loot.BUTCHER = new int[] { 14013, 14033, 18005 };
		Loot.DEER = new int[] { 14013, 18005 };
		Loot.PIG = new int[] { 14033, 18005 };
		Loot.NATURE = new int[] { 14025, 14026, 14027, 14028, 14029, 14030 };
	}

	public Loot()
	{
	}

	public static int getCars()
	{
		int num = ServerSettings.map;
		if (num == 0)
		{
			return 1;
		}
		if (num == 1)
		{
			return 20;
		}
		return 0;
	}

	public static int getLoot(string id)
	{
		int num;
		float single = UnityEngine.Random.@value;
		string str = id;
		if (str != null)
		{
			if (Loot.LootMap == null)
			{
				Dictionary<string, int> strs = new Dictionary<string, int>(19)
				{
					{ "civilian", 0 },
					{ "farm", 1 },
					{ "food", 2 },
					{ "fire", 3 },
					{ "police", 4 },
					{ "medical", 5 },
					{ "construction", 6 },
					{ "garage", 7 },
					{ "gear", 8 },
					{ "military", 9 },
					{ "rare", 10 },
					{ "clothes", 11 },
					{ "botanist", 12 },
					{ "arenaCenter", 13 },
					{ "militia", 14 },
					{ "deer", 15 },
					{ "pig", 16 },
					{ "nature", 17 },
					{ "butcher", 18 }
				};
				Loot.LootMap = strs;
			}
			
			if (Loot.LootMap.TryGetValue(str, out num))
			{
				switch (num)
				{
					case 0:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.CIVLIAN_4[UnityEngine.Random.Range(0, (int)Loot.CIVLIAN_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.CIVLIAN_3[UnityEngine.Random.Range(0, (int)Loot.CIVLIAN_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.CIVLIAN_2[UnityEngine.Random.Range(0, (int)Loot.CIVLIAN_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.CIVLIAN_1[UnityEngine.Random.Range(0, (int)Loot.CIVLIAN_1.Length)];
						}
						return Loot.CIVLIAN_0[UnityEngine.Random.Range(0, (int)Loot.CIVLIAN_0.Length)];
					}
					case 1:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.FARM_4[UnityEngine.Random.Range(0, (int)Loot.FARM_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.FARM_3[UnityEngine.Random.Range(0, (int)Loot.FARM_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.FARM_2[UnityEngine.Random.Range(0, (int)Loot.FARM_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.FARM_1[UnityEngine.Random.Range(0, (int)Loot.FARM_1.Length)];
						}
						return Loot.FARM_0[UnityEngine.Random.Range(0, (int)Loot.FARM_0.Length)];
					}
					case 2:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.FOOD_4[UnityEngine.Random.Range(0, (int)Loot.FOOD_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.FOOD_3[UnityEngine.Random.Range(0, (int)Loot.FOOD_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.FOOD_2[UnityEngine.Random.Range(0, (int)Loot.FOOD_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.FOOD_1[UnityEngine.Random.Range(0, (int)Loot.FOOD_1.Length)];
						}
						return Loot.FOOD_0[UnityEngine.Random.Range(0, (int)Loot.FOOD_0.Length)];
					}
					case 3:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.FIRE_4[UnityEngine.Random.Range(0, (int)Loot.FIRE_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.FIRE_3[UnityEngine.Random.Range(0, (int)Loot.FIRE_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.FIRE_2[UnityEngine.Random.Range(0, (int)Loot.FIRE_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.FIRE_1[UnityEngine.Random.Range(0, (int)Loot.FIRE_1.Length)];
						}
						return Loot.FIRE_0[UnityEngine.Random.Range(0, (int)Loot.FIRE_0.Length)];
					}
					case 4:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.POLICE_4[UnityEngine.Random.Range(0, (int)Loot.POLICE_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.POLICE_3[UnityEngine.Random.Range(0, (int)Loot.POLICE_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.POLICE_2[UnityEngine.Random.Range(0, (int)Loot.POLICE_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.POLICE_1[UnityEngine.Random.Range(0, (int)Loot.POLICE_1.Length)];
						}
						return Loot.POLICE_0[UnityEngine.Random.Range(0, (int)Loot.POLICE_0.Length)];
					}
					case 5:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.MEDICAL_4[UnityEngine.Random.Range(0, (int)Loot.MEDICAL_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.MEDICAL_3[UnityEngine.Random.Range(0, (int)Loot.MEDICAL_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.MEDICAL_2[UnityEngine.Random.Range(0, (int)Loot.MEDICAL_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.MEDICAL_1[UnityEngine.Random.Range(0, (int)Loot.MEDICAL_1.Length)];
						}
						return Loot.MEDICAL_0[UnityEngine.Random.Range(0, (int)Loot.MEDICAL_0.Length)];
					}
					case 6:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.CONSTRUCTION_4[UnityEngine.Random.Range(0, (int)Loot.CONSTRUCTION_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.CONSTRUCTION_3[UnityEngine.Random.Range(0, (int)Loot.CONSTRUCTION_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.CONSTRUCTION_2[UnityEngine.Random.Range(0, (int)Loot.CONSTRUCTION_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.CONSTRUCTION_1[UnityEngine.Random.Range(0, (int)Loot.CONSTRUCTION_1.Length)];
						}
						return Loot.CONSTRUCTION_0[UnityEngine.Random.Range(0, (int)Loot.CONSTRUCTION_0.Length)];
					}
					case 7:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.GARAGE_4[UnityEngine.Random.Range(0, (int)Loot.GARAGE_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.GARAGE_3[UnityEngine.Random.Range(0, (int)Loot.GARAGE_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.GARAGE_2[UnityEngine.Random.Range(0, (int)Loot.GARAGE_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.GARAGE_1[UnityEngine.Random.Range(0, (int)Loot.GARAGE_1.Length)];
						}
						return Loot.GARAGE_0[UnityEngine.Random.Range(0, (int)Loot.GARAGE_0.Length)];
					}
					case 8:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.GEAR_4[UnityEngine.Random.Range(0, (int)Loot.GEAR_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.GEAR_3[UnityEngine.Random.Range(0, (int)Loot.GEAR_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.GEAR_2[UnityEngine.Random.Range(0, (int)Loot.GEAR_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.GEAR_1[UnityEngine.Random.Range(0, (int)Loot.GEAR_1.Length)];
						}
						return Loot.GEAR_0[UnityEngine.Random.Range(0, (int)Loot.GEAR_0.Length)];
					}
					case 9:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.MILITARY_4[UnityEngine.Random.Range(0, (int)Loot.MILITARY_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.MILITARY_3[UnityEngine.Random.Range(0, (int)Loot.MILITARY_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.MILITARY_2[UnityEngine.Random.Range(0, (int)Loot.MILITARY_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.MILITARY_1[UnityEngine.Random.Range(0, (int)Loot.MILITARY_1.Length)];
						}
						return Loot.MILITARY_0[UnityEngine.Random.Range(0, (int)Loot.MILITARY_0.Length)];
					}
					case 10:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.RARE_4[UnityEngine.Random.Range(0, (int)Loot.RARE_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.RARE_3[UnityEngine.Random.Range(0, (int)Loot.RARE_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.RARE_2[UnityEngine.Random.Range(0, (int)Loot.RARE_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.RARE_1[UnityEngine.Random.Range(0, (int)Loot.RARE_1.Length)];
						}
						return Loot.RARE_0[UnityEngine.Random.Range(0, (int)Loot.RARE_0.Length)];
					}
					case 11:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.CLOTHES_4[UnityEngine.Random.Range(0, (int)Loot.CLOTHES_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.CLOTHES_3[UnityEngine.Random.Range(0, (int)Loot.CLOTHES_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.CLOTHES_2[UnityEngine.Random.Range(0, (int)Loot.CLOTHES_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.CLOTHES_1[UnityEngine.Random.Range(0, (int)Loot.CLOTHES_1.Length)];
						}
						return Loot.CLOTHES_0[UnityEngine.Random.Range(0, (int)Loot.CLOTHES_0.Length)];
					}
					case 12:
					{
						return Loot.BOTANIST[UnityEngine.Random.Range(0, (int)Loot.BOTANIST.Length)];
					}
					case 13:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.ARENA_CENTER_4[UnityEngine.Random.Range(0, (int)Loot.ARENA_CENTER_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.ARENA_CENTER_3[UnityEngine.Random.Range(0, (int)Loot.ARENA_CENTER_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.ARENA_CENTER_2[UnityEngine.Random.Range(0, (int)Loot.ARENA_CENTER_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.ARENA_CENTER_1[UnityEngine.Random.Range(0, (int)Loot.ARENA_CENTER_1.Length)];
						}
						return Loot.ARENA_CENTER_0[UnityEngine.Random.Range(0, (int)Loot.ARENA_CENTER_0.Length)];
					}
					case 14:
					{
						if (single > Loot.TEIR_3)
						{
							return Loot.MILITIA_4[UnityEngine.Random.Range(0, (int)Loot.MILITIA_4.Length)];
						}
						if (single > Loot.TEIR_2)
						{
							return Loot.MILITIA_3[UnityEngine.Random.Range(0, (int)Loot.MILITIA_3.Length)];
						}
						if (single > Loot.TEIR_1)
						{
							return Loot.MILITIA_2[UnityEngine.Random.Range(0, (int)Loot.MILITIA_2.Length)];
						}
						if (single > Loot.TEIR_0)
						{
							return Loot.MILITIA_1[UnityEngine.Random.Range(0, (int)Loot.MILITIA_1.Length)];
						}
						return Loot.MILITIA_0[UnityEngine.Random.Range(0, (int)Loot.MILITIA_0.Length)];
					}
					case 15:
					{
						return Loot.DEER[UnityEngine.Random.Range(0, (int)Loot.DEER.Length)];
					}
					case 16:
					{
						return Loot.PIG[UnityEngine.Random.Range(0, (int)Loot.PIG.Length)];
					}
					case 17:
					{
						return Loot.NATURE[UnityEngine.Random.Range(0, (int)Loot.NATURE.Length)];
					}
					case 18:
					{
						return Loot.BUTCHER[UnityEngine.Random.Range(0, (int)Loot.BUTCHER.Length)];
					}
				}
			}
		}
		return -1;
	}

	public static float getRespawnRate()
	{
		int num = ServerSettings.map;
		if (num == 1)
		{
			return 5f;
		}
		if (num == 2)
		{
			return 60f;
		}
		return Single.MaxValue;
	}
}