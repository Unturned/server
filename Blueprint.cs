using System;

public class Blueprint
{
	public int id_0;

	public int amount_0;

	public int id_1;

	public int amount_1;

	public int idTool;

	public int idReward;

	public int amountReward;

	public bool fire;

	public float knowledge;

	public Blueprint(int setID_0, int setAmount_0, int setID_1, int setAmount_1, int setIDTool, int setIDReward, int setAmountReward)
	{
		this.id_0 = setID_0;
		this.amount_0 = setAmount_0;
		this.id_1 = setID_1;
		this.amount_1 = setAmount_1;
		this.idTool = setIDTool;
		this.idReward = setIDReward;
		this.amountReward = setAmountReward;
		this.fire = false;
		this.knowledge = 0f;
	}

	public Blueprint(int setID_0, int setAmount_0, int setID_1, int setAmount_1, int setIDTool, int setIDReward, int setAmountReward, bool setFire)
	{
		this.id_0 = setID_0;
		this.amount_0 = setAmount_0;
		this.id_1 = setID_1;
		this.amount_1 = setAmount_1;
		this.idTool = setIDTool;
		this.idReward = setIDReward;
		this.amountReward = setAmountReward;
		this.fire = setFire;
		this.knowledge = 0f;
	}

	public Blueprint(int setID_0, int setAmount_0, int setID_1, int setAmount_1, int setIDTool, int setIDReward, int setAmountReward, float setKnowledge)
	{
		this.id_0 = setID_0;
		this.amount_0 = setAmount_0;
		this.id_1 = setID_1;
		this.amount_1 = setAmount_1;
		this.idTool = setIDTool;
		this.idReward = setIDReward;
		this.amountReward = setAmountReward;
		this.fire = false;
		this.knowledge = setKnowledge;
	}
}