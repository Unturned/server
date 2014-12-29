using System;
using UnityEngine;

public class Savedata
{
	public readonly static int INVENTORY_VERSION;
	public readonly static int CLOTHES_VERSION;
	public readonly static int LIFE_VERSION;
	public readonly static int POSITION_VERSION;
	public readonly static int VEHICLES_VERSION;
	public readonly static int BARRICADES_VERSION;
	public readonly static int STRUCTURES_VERSION;
	public readonly static int REPUTATION_VERSION;
	public readonly static int SKILLS_VERSION;

	static Savedata()
	{
		Savedata.INVENTORY_VERSION = 8;
		Savedata.CLOTHES_VERSION = 7;
		Savedata.LIFE_VERSION = 6;
		Savedata.POSITION_VERSION = 5;
		Savedata.VEHICLES_VERSION = 5;
		Savedata.BARRICADES_VERSION = 3;
		Savedata.STRUCTURES_VERSION = 2;
		Savedata.REPUTATION_VERSION = 1;
	}

	public Savedata() {
	}

	public static string loadBarricades(int x, int y)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat(new object[] { "barricades_", x, "_", y, "_", ServerSettings.map }));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.BARRICADES_VERSION.ToString()) || !(strArrays[2] == Maps.MAP_VERSION[ServerSettings.map].ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[3]);
	}

	public static string loadClothes(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("clothes_", id));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.CLOTHES_VERSION.ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[2]);
	}

	public static string loadInventory(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("inventory_", id));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.INVENTORY_VERSION.ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[2]);
	}

	public static string loadLife(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("life_", id));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.LIFE_VERSION.ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[2]);
	}

	public static string loadPosition(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat(new object[] { "position_", id, "_", ServerSettings.map }));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.POSITION_VERSION.ToString()) || !(strArrays[2] == Maps.MAP_VERSION[ServerSettings.map].ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[3]);
	}

	public static string loadReputation(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("reputation_", id));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.REPUTATION_VERSION.ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[2]);
	}

	public static string loadSkills(string id)
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("skills_", id));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.SKILLS_VERSION.ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[2]);
	}

	public static string loadStructures()
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("structures_", ServerSettings.map));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.STRUCTURES_VERSION.ToString()) || !(strArrays[2] == Maps.MAP_VERSION[ServerSettings.map].ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[3]);
	}

	public static string loadVehicles()
	{
		if (ServerSettings.map == 0)
		{
			return string.Empty;
		}
		string str = PlayerPrefs.GetString(string.Concat("vehicles_", ServerSettings.map));
		if (str == string.Empty)
		{
			return string.Empty;
		}
		string[] strArrays = Packer.unpack(str, '\u005F');
		if (!(strArrays[0] == MasterSettings.MASTERSETTINGS_VERSION.ToString()) || !(strArrays[1] == Savedata.VEHICLES_VERSION.ToString()) || !(strArrays[2] == Maps.MAP_VERSION[ServerSettings.map].ToString()))
		{
			return string.Empty;
		}
		return Sneaky.expose(strArrays[3]);
	}

	public static void saveBarricades(int x, int y, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat(new object[] { "barricades_", x, "_", y, "_", ServerSettings.map }), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.BARRICADES_VERSION, "_", Maps.MAP_VERSION[ServerSettings.map], "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveClothes(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat("clothes_", id), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.CLOTHES_VERSION, "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveInventory(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat("inventory_", id), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.INVENTORY_VERSION, "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveLife(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat("life_", id), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.LIFE_VERSION, "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void savePosition(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat(new object[] { "position_", id, "_", ServerSettings.map }), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.POSITION_VERSION, "_", Maps.MAP_VERSION[ServerSettings.map], "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveReputation(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat("reputation_", id), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.REPUTATION_VERSION, "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveSkills(string id, string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(string.Concat("skills_", id), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.SKILLS_VERSION, "_", Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveStructures(string serial)
	{
		if (ServerSettings.map != 0)
		{
			PlayerPrefs.SetString(
			string.Concat("structures_", ServerSettings.map), 
			string.Concat(new object[] { 
				MasterSettings.MASTERSETTINGS_VERSION, 
				"_", 
				Savedata.STRUCTURES_VERSION, 
				"_", 
				Maps.MAP_VERSION[ServerSettings.map], 
				"_", 
				Sneaky.sneak(serial), "_" }));
		}
	}

	public static void saveVehicles(string serial)
	{
		if (ServerSettings.map != 0) {
			PlayerPrefs.SetString(string.Concat("vehicles_", ServerSettings.map), string.Concat(new object[] { MasterSettings.MASTERSETTINGS_VERSION, "_", Savedata.VEHICLES_VERSION, "_", Maps.MAP_VERSION[ServerSettings.map], "_", Sneaky.sneak(serial), "_" }));
		}
	}
}