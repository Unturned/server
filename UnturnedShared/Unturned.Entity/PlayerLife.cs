//------------------------------------------------------------------------------
// <auto-generated>
//     Ezt a kódot eszköz generálta.
//     Futásidejű verzió:4.0.30319.0
//
//     Ennek a fájlnak a módosítása helytelen viselkedést eredményezhet, és elvész, ha
//     a kódot újragenerálják.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Unturned.Entity
{
	[Serializable]
	public class PlayerLife
	{
		[XmlAttribute("Health")]
		public int Health { get; set; }

		[XmlAttribute("Food")]
		public int Food { get; set; }

		[XmlAttribute("Water")]
		public int Water { get; set; }

		[XmlAttribute("Sickness")]
		public int Sickness { get; set; }

		[XmlAttribute("Bleeding")]
		public bool Bleeding { get; set; }

		[XmlAttribute("BrokenBones")]
		public bool BrokenBones { get; set; }

		[Obsolete("Using just for serialization")]
		public PlayerLife ()
		{
		}

		public PlayerLife (int health, int food, int water, int sickness, bool bleeding, bool brokenBones)
		{
			this.Health = health;
			this.Food = food;
			this.Water = water;
			this.Sickness = sickness;
			this.Bleeding = bleeding;
			this.BrokenBones = brokenBones;
		}
		

		public override string ToString ()
		{
			return string.Format ("[PlayerLife: Health={0}, Food={1}, Water={2}, Sickness={3}, Bleeding={4}, BrokenBones={5}]", Health, Food, Water, Sickness, Bleeding, BrokenBones);
		}
		
	}
}

