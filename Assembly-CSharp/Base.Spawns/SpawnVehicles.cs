using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicles : MonoBehaviour
{
	public static GameObject model;

	public static List<GameObject> models;

	public SpawnVehicles()
	{
	}

    public static Vehicle createNew(string id, Vector3 position, Quaternion rotation)
    {
        GameObject vehicleObject = (GameObject)Network.Instantiate(Resources.Load(string.Concat("Prefabs/Vehicles/", id)), position, rotation, 0);
        Vehicle vehicle = vehicleObject.GetComponent<Vehicle>();
        vehicleObject.name = id;

        vehicle.health = vehicle.maxHealth;
        vehicle.fuel = vehicle.maxFuel;

        if (vehicleObject.GetComponent<Painter>() != null)
        {
            vehicleObject.GetComponent<Painter>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        }

        SpawnVehicles.models.Add(vehicleObject);
        return vehicle;
    }

    public static void create(string id, Vector3 position, Quaternion rotation)
	{
        GameObject vehicleObject = (GameObject)Network.Instantiate(Resources.Load(string.Concat("Prefabs/Vehicles/", id)), position, rotation, 0);
        Vehicle vehicle = vehicleObject.GetComponent<Vehicle>();
		vehicleObject.name = id;
		if (ServerSettings.map != 0)
		{
			float single = UnityEngine.Random.@value;
			if ((double)single > 0.95)
			{
				vehicle.health = 10;
			}
			else if ((double)single > 0.8)
			{
				vehicle.health = 25;
			}
			else if ((double)single <= 0.5)
			{
				vehicle.health = vehicle.maxHealth;
			}
			else
			{
				vehicle.health = 50;
			}
			single = UnityEngine.Random.@value;
			if ((double)single > 0.95)
			{
				vehicle.fuel = vehicle.maxFuel;
			}
			if ((double)single <= 0.7)
			{
				vehicle.fuel = UnityEngine.Random.Range((int)((float)vehicle.maxFuel * 0.05f), (int)((float)vehicle.maxFuel * 0.2f));
			}
			else
			{
				vehicle.fuel = 0;
			}
		}
		else
		{
			vehicle.health = vehicle.maxHealth;
			vehicle.fuel = vehicle.maxFuel;
		}
		if (vehicleObject.GetComponent<Painter>() != null)
		{
			vehicleObject.GetComponent<Painter>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}

        // Adding to list...
        SpawnVehicles.models.Add(vehicleObject);
	}

	public static void create(string id, int health, int fuel, Vector3 position, Quaternion rotation, Color color)
	{
		GameObject gameObject = (GameObject)Network.Instantiate(Resources.Load(string.Concat("Prefabs/Vehicles/", id)), position, rotation, 0);
		Vehicle component = gameObject.GetComponent<Vehicle>();
		gameObject.name = id;
		component.health = health;
		component.fuel = fuel;
		if (gameObject.GetComponent<Painter>() != null)
		{
			gameObject.GetComponent<Painter>().color = color;
		}
		SpawnVehicles.models.Add(gameObject);
	}

	public void onReady()
	{
        SpawnVehicles.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("vehicles").gameObject;

        Debug.Log("Temporary disabled car spawn");
        return;

		if (Network.isServer)
		{
			SpawnVehicles.models = new List<GameObject>();
			if (Loot.getCars() > 0)
			{
				string str = Savedata.loadVehicles();
				string[] empty = new string[Loot.getCars()];
				for (int i = 0; i < (int)empty.Length; i++)
				{
					empty[i] = string.Empty;
				}
				if (str != string.Empty)
				{
					string[] strArrays = Packer.unpack(str, ';');
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						if (j < (int)empty.Length)
						{
							empty[j] = strArrays[j];
						}
					}
				}
				int num = 0;
				List<Transform> transforms = new List<Transform>();
				for (int k = 0; k < SpawnVehicles.model.transform.FindChild("spawns").childCount; k++)
				{
					transforms.Add(SpawnVehicles.model.transform.FindChild("spawns").GetChild(k));
				}
				for (int l = 0; l < (int)empty.Length; l++)
				{
					if (empty[l] == string.Empty)
					{
						int num1 = UnityEngine.Random.Range(0, transforms.Count);
						Transform item = transforms[num1];
						transforms.RemoveAt(num1);
						float single = UnityEngine.Random.@value;
						if (item.name == "civilianCar")
						{
							if ((double)single > 0.66)
							{
								SpawnVehicles.create(string.Concat("truck_", UnityEngine.Random.Range(0, 1)), item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							}
							else if ((double)single <= 0.33)
							{
								SpawnVehicles.create(string.Concat("van_", UnityEngine.Random.Range(0, 1)), item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							}
							else
							{
								SpawnVehicles.create(string.Concat("car_", UnityEngine.Random.Range(0, 2)), item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							}
							num++;
						}
						else if (item.name == "policeCar")
						{
							SpawnVehicles.create("policeCar_0", item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							num++;
						}
						else if (item.name == "fireCar")
						{
							SpawnVehicles.create("fireTruck_0", item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							num++;
						}
						else if (item.name == "militaryCar")
						{
							if ((double)single <= 0.9)
							{
								SpawnVehicles.create(string.Concat("humvee_", UnityEngine.Random.Range(0, 2)), item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							}
							else
							{
								SpawnVehicles.create(string.Concat("apc_", UnityEngine.Random.Range(0, 2)), item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							}
							num++;
						}
						else if (item.name == "medicalCar")
						{
							SpawnVehicles.create("medic_0", item.transform.position + new Vector3(0f, 0.1f, 0f), item.rotation * Quaternion.Euler(-90f, 0f, 0f));
							num++;
						}
					}
					else
					{
						string[] strArrays1 = Packer.unpack(empty[l], ':');
						Vector3 vector3 = new Vector3(float.Parse(strArrays1[3]), float.Parse(strArrays1[4]) + 0.1f, float.Parse(strArrays1[5]));
						Quaternion quaternion = Quaternion.Euler(float.Parse(strArrays1[6]), float.Parse(strArrays1[7]), float.Parse(strArrays1[8]));
						Color color = new Color(float.Parse(strArrays1[9]), float.Parse(strArrays1[10]), float.Parse(strArrays1[11]));
						SpawnVehicles.create(strArrays1[0], int.Parse(strArrays1[1]), int.Parse(strArrays1[2]), vector3, quaternion, color);
					}
				}
				SpawnVehicles.save();
			}
		}
	}

	public static void save()
	{
		string saveData = string.Empty;
		for (int i = 0; i < SpawnVehicles.models.Count; i++)
		{
			if (SpawnVehicles.models[i] != null)
			{
				Vehicle component = SpawnVehicles.models[i].GetComponent<Vehicle>();
				if (component.transform.position.y >= Ocean.level - 1f && !component.exploded)
				{
					saveData = string.Concat(saveData, component.name, ":");
					saveData = string.Concat(saveData, component.health, ":");
					saveData = string.Concat(saveData, component.fuel, ":");
					Vector3 vector3 = component.transform.position;
					saveData = string.Concat(saveData, Mathf.Floor(vector3.x * 100f) / 100f, ":");
					Vector3 vector31 = component.transform.position;
					saveData = string.Concat(saveData, Mathf.Floor(vector31.y * 100f) / 100f, ":");
					Vector3 vector32 = component.transform.position;
					saveData = string.Concat(saveData, Mathf.Floor(vector32.z * 100f) / 100f, ":");
					Vector3 vector33 = component.transform.rotation.eulerAngles;
					saveData = string.Concat(saveData, (int)vector33.x, ":");
					Vector3 vector34 = component.transform.rotation.eulerAngles;
					saveData = string.Concat(saveData, (int)vector34.y, ":");
					Vector3 vector35 = component.transform.rotation.eulerAngles;
					saveData = string.Concat(saveData, (int)vector35.z, ":");
					if (component.GetComponent<Painter>() == null)
					{
						saveData = string.Concat(saveData, "0:");
						saveData = string.Concat(saveData, "0:");
						saveData = string.Concat(saveData, "0:;");
					}
					else
					{
						saveData = string.Concat(saveData, Mathf.Floor(component.GetComponent<Painter>().color.r * 100f) / 100f, ":");
						saveData = string.Concat(saveData, Mathf.Floor(component.GetComponent<Painter>().color.g * 100f) / 100f, ":");
						saveData = string.Concat(saveData, Mathf.Floor(component.GetComponent<Painter>().color.b * 100f) / 100f, ":;");
					}
				}
			}
		}
		Savedata.saveVehicles(saveData);
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}
}