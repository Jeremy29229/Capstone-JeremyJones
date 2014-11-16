namespace D_Quester
{
	/// <summary>
	/// Base class for object that can be added in the Inventory. Inherit from this class to allow custom components to be added to D_Quester's inventory component. 
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Name that will be displayed to the player for this item.
		/// </summary>
		public string ItemName = "";

		/// <summary>
		/// Quantity of the item.
		/// </summary>
		public int Quantity = 1;

		/// <summary>
		/// Is this item stackable in an inventory?
		/// </summary>
		public bool IsStackable = true;

		/// <summary>
		/// Determines how many of this item can be stacked in an inventory.
		/// </summary>
		public int NumberPerStack = int.MaxValue;

		/// <summary>
		/// Determines how big this item is if an inventory is configured to use carry weight.
		/// </summary>
		public int SizeInInventory = 1;

		/// <summary>
		/// Constructs an item instance.
		/// </summary>
		/// <param name="name">Name of the item.</param>
		/// <param name="quantity">Quantity of item.</param>
		/// <param name="numPerStack">Number of this item that can be stacked</param>
		public Item(string name = "Unknown", int quantity = 1, int numPerStack = int.MaxValue)
		{
			ItemName = name;
			Quantity = quantity;
		}

		/// <summary>
		/// Sets the quantity of this item, ignoring the current quantity.
		/// </summary>
		/// <param name="amount">The new quantity this item instance will have.</param>
		void SetQuantity(int amount)
		{
			this.Quantity = amount;
		}

		/// <summary>
		/// Adds or subtracts the amount passed from the items quantity.
		/// </summary>
		/// <param name="amount">Amount the item's quantity will be changed.</param>
		void ModifyQuanity(int amount)
		{
			this.Quantity += amount;
		}

		/// <summary>
		/// Formats the items name and quantity.
		/// </summary>
		/// <returns>String representing this item.</returns>
		public override string ToString()
		{
			return ItemName + ": " + Quantity;
		}
	}
}
