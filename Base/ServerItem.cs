using System;
using UnityEngine;

public class ServerItem
{
	private int realID;

	private int realAmount;

	private string realState;

	public Vector3 position;

	public int amount
	{
		get
		{
			return Sneaky.expose(this.realAmount);
		}
		set
		{
			this.realAmount = Sneaky.sneak(value);
		}
	}

	public int id
	{
		get
		{
			return Sneaky.expose(this.realID);
		}
		set
		{
			this.realID = Sneaky.sneak(value);
		}
	}

	public string state
	{
		get
		{
			return Sneaky.expose(this.realState);
		}
		set
		{
			this.realState = Sneaky.sneak(value);
		}
	}

	public ServerItem(int setID, int setAmount, string setState, Vector3 setPosition)
	{
		this.id = setID;
		this.amount = setAmount;
		this.state = setState;
		this.position = setPosition;
	}
}