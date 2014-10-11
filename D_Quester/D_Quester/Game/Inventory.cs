using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.D_Quester
{
	class Inventory
	{
		private List<Item> items { get; set; }
		public delegate void ItemGotDel(string itemName);
		public event ItemGotDel ItemGotEvent;

		public Inventory()
		{
			items = new List<Item>();
		}

		public List<Item> getItems()
		{
			return items.ToList();
		}

		public void addItem(Item i)
		{
			Item selectedItem = items.FirstOrDefault(x => x.Name == i.Name);
			if(selectedItem == null)
			{
				items.Add(i);
			}
			else
			{
				selectedItem.Amount += i.Amount;
			}

			for (int it = 0; it < i.Amount; it++)
			{
				OnItemGet(i.Name);
			}
		}

		public void removeItem(Item i, int amount)
		{
			Item selectedItem = items.FirstOrDefault(x => x.Name == i.Name);
			if (selectedItem == null)
			{
				throw new ArgumentException("Item does not exist in inventory");
			}

			if (selectedItem.Amount < amount)
			{
				throw new ArgumentException("Attempting to remove more item amount than inventory has.");
			}


			selectedItem.Amount -= amount;

			if (selectedItem.Amount == 0)
			{
				items.Remove(selectedItem);
			}
		}

		public void removeAll(Item i)
		{
			Item selectedItem = items.FirstOrDefault(x => x.Name == i.Name);
			if (selectedItem == null)
			{
				throw new ArgumentException("Item does not exist in inventory");
			}

			items.Remove(selectedItem);
		}

		protected virtual void OnItemGet(string name)
		{
			if (ItemGotEvent != null)
			{
				ItemGotEvent(name);
			}
		}
	}
}
