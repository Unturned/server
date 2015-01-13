using System;
using UnityEngine;

public class Skills : MonoBehaviour
{
	private int realExperience;

	public Skill[] skills = new Skill[] { 
        new Skill("survivalist", "Survival", "Slower starvation and dehydration.", 4, 10), 
        new Skill("endurance", "Endurance", "Greater stamina and agility.", 5, 15), 
        new Skill("sneakybeaky", "Sneakybeaky", "Less agro and noise.", 6, 15), 
        new Skill("marksman", "Marksman", "Better accuracy and recoil.", 6, 15), 
        new Skill("warrior", "Warrior", "Do more and take less melee damage.", 4, 20), 
        new Skill("miner", "Outdoors", "Stronger mining and chopping.", 4, 15), 
        new Skill("crafting", "Craftsman", "Increases crafting smarts.", 2, 50), 
        new Skill("immunity", "Immunity", "Less disease and more vitality.", 4, 25)
    };

	private bool loaded;

	public int experience
	{
		get
		{
			return Sneaky.expose(this.realExperience);
		}
		set
		{
			this.realExperience = Sneaky.sneak(value);
		}
	}

    public Skills()
	{
	}

	[RPC]
	public void askUpgrade(int index)
	{
		if (this.skills[index].level < this.skills[index].maxLevel && this.experience >= this.skills[index].getCost())
		{
			NetworkManager.error(string.Concat("Upgraded ", this.skills[index].name), string.Concat("Textures/Skills/", this.skills[index].id), base.networkView.owner);
			Skills cost = this;
			cost.experience = cost.experience - this.skills[index].getCost();
			Skill skill = this.skills[index];
			skill.level = skill.level + 1;
			this.syncLevel(index);
			this.syncExperience();
		}
	}

	public float crafting()
	{
		return (float)this.skills[6].level / (float)this.skills[6].maxLevel;
	}

	public float endurance()
	{
		return (float)this.skills[1].level / (float)this.skills[1].maxLevel;
	}

	public float immunity()
	{
		return (float)this.skills[7].level / (float)this.skills[7].maxLevel;
	}

	public void learn(int amount)
	{
		// TODO: default XP amount
        amount = amount * 2;
		
        Skills skill = this;
		skill.experience = skill.experience + amount;
		NetworkManager.error(string.Concat("+", amount, " Experience"), "Textures/Icons/experience", base.networkView.owner);
		this.syncExperience();
	}

	public void load()
	{
		if (Network.isServer)
		{
			if (!ServerSettings.save)
			{
				this.loadAllKnowledge();
			}
			else
			{
				this.loadAllKnowledgeFromSerial(Savedata.loadSkills(base.GetComponent<Player>().owner.id));
			}
		}
		else if (!ServerSettings.save)
		{
			base.networkView.RPC("loadAllKnowledge", RPCMode.Server, new object[0]);
		}
		else
		{
			base.networkView.RPC("loadAllKnowledgeFromSerial", RPCMode.Server, new object[] { Savedata.loadSkills(PlayerSettings.id) });
		}
	}

	[RPC]
	public void loadAllKnowledge()
	{
		NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(base.networkView.owner);
		string empty = string.Empty;
		if (userFromPlayer != null)
		{
			empty = Savedata.loadSkills(userFromPlayer.id);
		}
		this.loadAllKnowledgeFromSerial(empty);
	}

	[RPC]
	public void loadAllKnowledgeFromSerial(string serial)
	{
		if (serial == string.Empty)
		{
			this.experience = 0;
			for (int i = 0; i < (int)this.skills.Length; i++)
			{
				this.skills[i].level = 0;
				this.syncLevel(i);
			}
		}
		else
		{
			string[] strArrays = Packer.unpack(serial, ';');
			this.experience = int.Parse(strArrays[0]);
			if (this.experience < 0)
			{
				this.experience = 0;
			}
			for (int j = 0; j < (int)this.skills.Length; j++)
			{
				if (j + 1 < (int)strArrays.Length)
				{
					this.skills[j].level = int.Parse(strArrays[j + 1]);
					if (this.skills[j].level < 0)
					{
						this.skills[j].level = 0;
					}
					else if (this.skills[j].level > this.skills[j].maxLevel)
					{
						this.skills[j].level = this.skills[j].maxLevel;
					}
				}
				this.syncLevel(j);
			}
		}
		this.syncExperience();
		this.loaded = true;
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("tellLoadedSkills", base.networkView.owner, new object[] { true });
		}
	}

	public float marksman()
	{
		return (float)this.skills[3].level / (float)this.skills[3].maxLevel;
	}

	public float miner()
	{
		return (float)this.skills[5].level / (float)this.skills[5].maxLevel;
	}

	public void saveAllKnowledge()
	{
		if (this.loaded)
		{
			string skillLine = string.Empty;
			if (base.GetComponent<Life>().dead)
			{
				skillLine = string.Concat(skillLine, this.experience / 2, ";");
				for (int i = 0; i < (int)this.skills.Length; i++)
				{
					skillLine = string.Concat(skillLine, this.skills[i].level / 2, ";");
				}
			}
			else
			{
				skillLine = string.Concat(skillLine, this.experience, ";");
				for (int j = 0; j < (int)this.skills.Length; j++)
				{
					skillLine = string.Concat(skillLine, this.skills[j].level, ";");
				}
			}
            Savedata.saveSkills(base.GetComponent<Player>().owner.id, skillLine);
		}
	}

	public void sendUpgrade(int index)
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("askUpgrade", RPCMode.Server, new object[] { index });
		}
		else
		{
			this.askUpgrade(index);
		}
	}

	public float sneakybeaky()
	{
		return (float)this.skills[2].level / (float)this.skills[2].maxLevel;
	}

	public void Start()
	{
		if (base.networkView.isMine)
		{
			this.load();
		}
	}

	public float survivalist()
	{
		return (float)this.skills[0].level / (float)this.skills[0].maxLevel;
	}

	public void syncExperience()
	{
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("tellExperience", base.networkView.owner, new object[] { this.experience });
		}
		else
		{
			this.tellExperience(this.experience);
		}
	}

	public void syncLevel(int index)
	{
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("tellLevel", base.networkView.owner, new object[] { index, this.skills[index].level });
		}
		else
		{
			this.tellLevel(index, this.skills[index].level);
		}
	}

	[RPC]
	public void tellExperience(int setExperience)
	{
		this.experience = setExperience;
	}

	[RPC]
	public void tellLevel(int index, int setLevel)
	{
		this.skills[index].level = setLevel;
		if (this.skills[index].level == this.skills[index].maxLevel)
		{
			int num = 0;
			while (num < (int)this.skills.Length)
			{
				if (this.skills[num].level == this.skills[num].maxLevel)
				{
					num++;
				}
				else
				{
					break;
				}
			}
		}
	}

	[RPC]
	public void tellLoadedSkills(bool setLoaded)
	{
		this.loaded = setLoaded;
	}

	public float warrior()
	{
		return (float)this.skills[4].level / (float)this.skills[4].maxLevel;
	}
}