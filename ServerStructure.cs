using System;
using UnityEngine;

public class ServerStructure
{
	public int id;

	public int health;

	public string state;

	public Vector3 position;

	public int rotation;

	public ServerStructure(int setID, int setHealth, string setState, Vector3 setPosition, int setRotation)
	{
		this.id = setID;
		this.health = setHealth;
		this.state = setState;
		this.position = setPosition;
		this.rotation = setRotation;
	}
}