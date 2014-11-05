using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Base class for object that can be added in the Inventory. Inherit from this class to allow custom scripts to be added to D_Questers inventory. 
	/// </summary>
	public class Item
	{
		/// <summary>
		/// 
		/// </summary>
		public string ItemName = "";

		/// <summary>
		/// 
		/// </summary>
		public int Quantity = 1;

		/// <summary>
		/// 
		/// </summary>
		public bool IsStackable = true;

		/// <summary>
		/// 
		/// </summary>
		public int NumberPerStack = -1;

		/// <summary>
		/// 
		/// </summary>
		public int SizeInInventory = 1;

		public Item(string name = "Unknown", int quantity = 1, int numPerStack = -1)
		{
			ItemName = name;
			Quantity = quantity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="amount"></param>
		void SetQuantity(int amount)
		{
			this.Quantity = amount;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="amount"></param>
		void ModifyQuanity(int amount)
		{
			this.Quantity += amount;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ItemName + ": " + Quantity;
		}
	}
}
