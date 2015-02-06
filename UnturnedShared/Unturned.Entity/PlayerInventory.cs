using System.Collections.Generic;

using System;
using System.Xml.Serialization;

namespace Unturned.Entity {

	[Serializable]
	[XmlRootAttribute("Inventory")]
	public class PlayerInventory
	{
		[XmlAttribute("Height")]
		public int Height { get; set; }

		[XmlAttribute("Width")]
		public int Width { get; set; }

		[XmlAttribute("Capacity")]
		public int Capacity { get; set; }

		[XmlElement("Item")]
		public List<InventoryItem> Items { get; set; }

		public PlayerInventory (int height, int width, int capacity)
		{
			this.Height = height;
			this.Width = width;
			this.Capacity = capacity;
		}

		[Obsolete("Use just for serialization!")]
		public PlayerInventory ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Inventory: Height={0}, Width={1}, Capacity={2}]", Height, Width, Capacity);
		}
	}
}

