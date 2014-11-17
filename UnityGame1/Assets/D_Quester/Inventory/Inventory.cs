using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace D_Quester
{
	/// <summary>
	/// A component that can hold D_Quester.Item(s) or any class that inherits from item.
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
		/// The inventory's capacity configuration.
		/// </summary>
		[Tooltip("The inventory's capacity configuration.")]
		public CapacityUnit capacityType;

		/// <summary>
		/// List of items currently in the inventory.
		/// </summary>
		[Tooltip("List of items currently in the inventory.")]
		public List<Item> items = new List<Item>();

		/// <summary>
		/// Indicates what key can be pressed to print the contents of the inventory to the debug log.
		/// </summary>
		[Tooltip("Indicates what key can be pressed to print the contents of the inventory to the debug log.")]
		public KeyCode InventoryPrintKey;

		/// <summary>
		/// Delegate for picking up items.
		/// </summary>
		/// <param name="itemName">Name of the picked up item.</param>
		[HideInInspector]
		public delegate void PickupDelegate(string itemName);

		/// <summary>
		/// Event for picking up items.
		/// </summary>
		[HideInInspector]
		public event PickupDelegate PickupEvent;

		/// <summary>
		/// Called whenever an item is picked up.
		/// </summary>
		/// <param name="name">Name of the picked up item.</param>
		[HideInInspector]
		public virtual void OnPickUpEvent(string name)
		{
			if (PickupEvent != null)
			{
				PickupEvent(name);
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(InventoryPrintKey))
			{
				print(ToString());
			}
		}

		/// <summary>
		/// Formats the inventory's item's name and quantity.
		/// </summary>
		/// <returns>String representing this inventory.</returns>
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
		/// <param name="item">Item instance to be added.</param>
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
						if (remainItemAmount < 0)
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
			else if (capacityType == CapacityUnit.CarryWeight)
			{
				if (item.Quantity * item.SizeInInventory <= Capacity - currentCapacity)
				{
					Item existingItem = items.FirstOrDefault(x => x.ItemName == item.ItemName);
					if (existingItem == null)
					{
						items.Add(item);

					}
					else
					{
						existingItem.Quantity += item.Quantity;
					}
					currentCapacity += item.Quantity * item.SizeInInventory;
					wasSuccessful = true;
				}
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

		/// <summary>
		/// Attempts to add the item to the inventory with capacity and item size logic.
		/// </summary>
		/// <param name="itemName">Name of the item.</param>
		/// <param name="quantity">Quantity of the item.</param>
		/// <returns>Returns true if the addition was successful.</returns>
		public bool Add(string itemName, int quantity = 1)
		{
			return Add(new Item(itemName, quantity));
		}

		/// <summary>
		/// Attempts to remove item from the inventory if it exists.
		/// </summary>
		/// <param name="item">Item instance to be removed.</param>
		/// <returns>Returns true if the item existed in the inventory.</returns>
		public bool Remove(Item item)
		{
			bool wasSuccessful = false;

			int amountRemaining = item.Quantity;

			Item[] allItems = items.Where(x => x.ItemName == item.ItemName).ToArray();
			int amountAvailable = 0;

			foreach (Item i in allItems)
			{
				amountAvailable += i.Quantity;
			}

			if (amountAvailable <= amountRemaining)
			{
				foreach (Item i in allItems)
				{
					if (i.Quantity <= amountRemaining)
					{
						items.Remove(i);
						amountRemaining -= i.Quantity;
						if (capacityType == CapacityUnit.NumItemStacks)
						{
							currentCapacity -= i.Quantity;
						}
						else if (capacityType == CapacityUnit.CarryWeight)
						{
							currentCapacity -= i.Quantity * i.SizeInInventory;
						}
					}
					else
					{
						if (capacityType == CapacityUnit.NumItemStacks)
						{
							currentCapacity -= amountRemaining;
						}
						else if (capacityType == CapacityUnit.CarryWeight)
						{
							currentCapacity -= amountRemaining * i.SizeInInventory;
						}
						i.Quantity -= amountRemaining;
						amountRemaining = 0;
					}
				}
				wasSuccessful = true;
			}

			return wasSuccessful;
		}

		/// <summary>
		/// Attempts to remove item from the inventory if it exists.
		/// </summary>
		/// <param name="itemName">Name of the item being removed.</param>
		/// <param name="quantity">Quantity of the item being removed.</param>
		/// <returns>Returns true if the item existed in the inventory.</returns>
		public bool Remove(string itemName, int quantity = 1)
		{
			return Remove(new Item(itemName, quantity));
		}

		/// <summary>
		/// Attempts to add all items in the collection passed in. If any items fail to fit into the inventory, none will be added.
		/// </summary>
		/// <returns>Returns true if the operation succeeds.</returns>
		public bool AddRange(IEnumerable<Item> items)
		{
			bool wasSuccessful = true;

			foreach (var i in items)
			{
				if (!IsItemAddable(i))
				{
					wasSuccessful = false;
					break;
				}
			}

			if (wasSuccessful)
			{
				foreach (var i in items)
				{
					Add(i);
				}
			}

			return wasSuccessful;
		}

		/// <summary>
		/// Attempts to remove all items in the collection passed in. If any items fail to be found in the inventory, none will be removed.
		/// </summary>
		/// <returns>Returns true if the operation succeeds.</returns>
		public bool RemoveRange(IEnumerable<Item> items)
		{
			bool wasSuccessful = true;

			foreach (var i in items)
			{
				if (!IsItemRemovable(i))
				{
					wasSuccessful = false;
					break;
				}
			}

			if (wasSuccessful)
			{
				foreach (var i in items)
				{
					Remove(i);
				}
			}

			return wasSuccessful;
		}

		/// <summary>
		/// Adds all items that will fit into the inventory.
		/// </summary>
		/// <returns>All items that did not fit into the inventory.</returns>
		public IEnumerable<Item> AddWhatFits(IEnumerable<Item> items)
		{
			List<Item> leftoverItems = new List<Item>();

			foreach (var i in items)
			{
				if (!Add(i))
				{
					leftoverItems.Add(i);
				}
			}

			return leftoverItems;
		}

		private bool IsItemAddable(Item item)
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
							remainItemAmount = 0;
							wasSuccessful = true;
						}
						else
						{
							remainItemAmount -= item.NumberPerStack;
						}
					}
				}
				else
				{
					var existingItems = items.Where(x => x.ItemName == item.ItemName);

					foreach (var inItem in existingItems)
					{
						if (remainItemAmount < 0)
						{
							break;
						}

						if (inItem.NumberPerStack < 1)
						{
							remainItemAmount = 0;
							wasSuccessful = true;
						}
						else if (inItem.Quantity < inItem.NumberPerStack)
						{
							if (remainItemAmount > inItem.NumberPerStack - inItem.Quantity)
							{
								remainItemAmount -= inItem.NumberPerStack - inItem.Quantity;
							}
							else
							{
								remainItemAmount = 0;
								wasSuccessful = true;
							}
						}
					}

					while (currentCapacity < Capacity && remainItemAmount > 0)
					{
						if (item.NumberPerStack < 0 || item.NumberPerStack > item.Quantity)
						{
							remainItemAmount = 0;
							wasSuccessful = true;
						}
						else
						{
							remainItemAmount -= item.NumberPerStack;
						}
					}
				}
			}
			else if (capacityType == CapacityUnit.CarryWeight)
			{
				if (item.Quantity * item.SizeInInventory <= Capacity - currentCapacity)
				{
					wasSuccessful = true;
				}
			}

			return wasSuccessful;
		}
		private bool IsItemRemovable(Item item)
		{
			bool wasSuccessful = false;

			int amountRemaining = item.Quantity;

			Item[] allItems = items.Where(x => x.ItemName == item.ItemName).ToArray();
			int amountAvailable = 0;

			foreach (Item i in allItems)
			{
				amountAvailable += i.Quantity;
			}

			if (amountAvailable <= amountRemaining)
			{
				wasSuccessful = true;
			}

			return wasSuccessful;
		}
	}
}
