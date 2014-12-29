using System;

public struct ClientItem
{
	private int realID;
	private int realAmount;
	private string realState;

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

	public ClientItem(ServerItem item)
	{
		this.realID = Sneaky.sneak(item.id);
		this.realAmount = Sneaky.sneak(item.amount);
		this.realState = Sneaky.sneak(item.state);
	}

	public ClientItem(int setID, int setAmount, string setState)
	{
		this.realID = Sneaky.sneak(setID);
		this.realAmount = Sneaky.sneak(setAmount);
		this.realState = Sneaky.sneak(setState);
	}
}