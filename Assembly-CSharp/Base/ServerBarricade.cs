using System;
using UnityEngine;

public class ServerBarricade
{
	public int id;

	public int health;

	public string state;

	public Vector3 position;

	public Vector3 rotation;

	public ServerBarricade(int setID, int setHealth, string setState, Vector3 setPosition, Vector3 setRotation)
	{
		this.id = setID;
		this.health = setHealth;
		this.state = setState;
		this.position = setPosition;
		this.rotation = setRotation;
	}
}