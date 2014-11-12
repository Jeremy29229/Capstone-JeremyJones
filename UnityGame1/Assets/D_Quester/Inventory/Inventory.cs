using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class Inventory : MonoBehaviour
	{
		/// <summary>
		/// This represents the maximum number of items the inventory can hold in the form of quantity or carry weight depending on the inventories configuration.
		/// </summary>
		[Tooltip("This represents the maximum number of items the inventory can hold in the form of quantity or carry weight depending on the inventories configuration.")]
		public int Capacity = 100;

		/// <summary>
		/// The weight or amount of items that are currently in the inventory.
		/// </summary>
		[Tooltip("The weight or amount of items that are currently in the inventory.")]
		public int currentCapacity = 0;

		/// <summary>
		/// 
		/// </summary>
		public CapacityUnit capacityType;

		/// <summary>
		/// 
		/// </summary>
		public List<Item> items = new List<Item>();

		/// <summary>
		/// 
		/// </summary>
		public KeyCode InventoryPrintKey;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="itemName"></param>
		public delegate void PickupDelegate(string itemName);

		/// <summary>
		/// 
		/// </summary>
		public event PickupDelegate PickupEvent;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public virtual void OnPickUpEvent(string name)
		{
			if (PickupEvent != null)
			{
				PickupEvent(name);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		void Update()
		{
			if (Input.GetKeyDown(InventoryPrintKey))
			{
				print(ToString());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string inventoryString = "";
			foreach (var i in items)
			{
				inventoryString += i.ToString() + ", ";
			}

			if (inventoryString == "")
			{
				inventoryString = "Inventory is empty";
			}
			else
			{
				inventoryString = inventoryString.Substring(0, inventoryString.Length - 2);
			}

			return inventoryString;
		}

		/// <summary>
		/// Attempts to add the item to the inventory with capacity and item size logic.
		/// </summary>
		/// <returns>Returns true if the addition was successful.</returns>
		public bool Add(Item item)
		{
			bool wasSuccessful = false;

			if (capacityType == CapacityUnit.NumItemStacks)
			{
				int remainItemAmount = item.Quantity;

				if (items.FirstOrDefault(x => x.ItemName == item.ItemName) == null)
				{
					while (currentCapacity < Capacity && remainItemAmount > 0)
					{
						if (item.NumberPerStack < 0 || item.NumberPerStack > item.Quantity)
						{
							Item newItem = new Item(item.ItemName, item.Quantity) { IsStackable = item.IsStackable, NumberPerStack = item.NumberPerStack, SizeInInventory = item.SizeInInventory };
							items.Add(newItem);
							remainItemAmount = 0;
							wasSuccessful = true;
							currentCapacity++;
						}
						else
						{
							Item newItem = new Item(item.ItemName, item.NumberPerStack) { IsStackable = item.IsStackable, NumberPerStack = item.NumberPerStack, SizeInInventory = item.SizeInInventory };
							items.Add(newItem);
							remainItemAmount -= item.NumberPerStack;
							currentCapacity++;
						}
					}
				}
				else
				{
					var existingItems = items.Where(x => x.ItemName == item.ItemName);

					foreach (var inItem in existingItems)
					{
						if(remainItemAmount < 0)
						{
							break;
						}

						if (inItem.NumberPerStack < 1)
						{
							inItem.Quantity += remainItemAmount;
							remainItemAmount = 0;
							wasSuccessful = true;
						}
						else if (inItem.Quantity < inItem.NumberPerStack)
						{
							if (remainItemAmount > inItem.NumberPerStack - inItem.Quantity)
							{
								remainItemAmount -= inItem.NumberPerStack - inItem.Quantity;
								inItem.Quantity = inItem.NumberPerStack;
							}
							else
							{
								inItem.Quantity += remainItemAmount;
								remainItemAmount = 0;
								wasSuccessful = true;
							}
						}
					}

					while (currentCapacity < Capacity && remainItemAmount > 0)
					{
						if (item.NumberPerStack < 0 || item.NumberPerStack > item.Quantity)
						{
							Item newItem = new Item(item.ItemName, item.Quantity) { IsStackable = item.IsStackable, NumberPerStack = item.NumberPerStack, SizeInInventory = item.SizeInInventory };
							items.Add(newItem);
							remainItemAmount = 0;
							wasSuccessful = true;
							currentCapacity++;
						}
						else
						{
							Item newItem = new Item(item.ItemName, item.NumberPerStack) { IsStackable = item.IsStackable, NumberPerStack = item.NumberPerStack, SizeInInventory = item.SizeInInventory };
							items.Add(newItem);
							remainItemAmount -= item.NumberPerStack;
							currentCapacity++;
						}
					}
				}
			}
			else if(capacityType == CapacityUnit.CarryWeight)
			{
				throw new InvalidOperationException("Carry weight not yet supported.");
			}

			if (wasSuccessful)
			{
				for (int i = 0; i < item.Quantity; i++)
				{
					OnPickUpEvent(item.ItemName);
				}
			}

			return wasSuccessful;
		}

		public bool Add(string itemName, int quantity = 1)
		{
			return Add(new Item(itemName, quantity));
		}

		/// <summary>
		/// Attempts to remove item from the inventory if it exists.
		/// </summary>
		/// <returns>Returns true if the item existed in the inventory</returns>
		public bool Remove(Item item)
		{
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Remove(string itemName, int quantity = 1)
		{
			return Remove(new Item(itemName, quantity));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool AddRange()
		{
			throw new NotImplementedException("Method not supported in current D_Quester version.");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool RemoveRange()
		{
			throw new NotImplementedException("Method not supported in current D_Quester version.");
		}

		/// <summary>
		/// /
		/// </summary>
		/// <returns></returns>
		public Item[] AddWhatFits()
		{
			throw new NotImplementedException("Method not supported in current D_Quester version.");
		}
	}
}
