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
using System.Xml.Serialization;
using System.Xml;

namespace Unturned.Entity
{
	[Serializable]
	public class InventoryItem
	{
		[XmlAttribute("ItemID")]
		public int ItemID { get; set; }

		[XmlAttribute("Amount")]
		public int Amount { get; set; }

		[XmlAttribute("State")]
		public string State { get; set; }

		[XmlAttribute("X")]
		public int X { get; set; }

		[XmlAttribute("Y")]
		public int Y { get; set; }

		public InventoryItem (int itemID, int amount, string state, int x, int y)
		{
			this.ItemID = itemID;
			this.Amount = amount;
			this.State = state;
			this.X = x;
			this.Y = y;
		}
	
		[Obsolete("Use just for serialization only!")]
		public InventoryItem ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[InventoryItem: ItemID={0}, Amount={1}, State={2}]", ItemID, Amount, State);
		}
	}
}

