using System;
using UnityEngine;

public class Skill
{
	public string id;

	public string name;

	public string description;

	public int maxLevel;

	public int cost;

	private int realLevel;

	public int level
	{
		get
		{
			return Sneaky.expose(this.realLevel);
		}
		set
		{
			this.realLevel = Sneaky.sneak(value);
		}
	}

	public Skill(string setID, string setName, string setDescription, int setMaxLevel, int setCost)
	{
		this.id = setID;
		this.name = setName;
		this.description = setDescription;
		this.maxLevel = setMaxLevel;
		this.cost = setCost;
		this.level = 0;
	}

	public int getCost()
	{
		return this.cost + (int)Mathf.Pow((float)(this.cost * this.level), 2f) / this.cost;
	}
}