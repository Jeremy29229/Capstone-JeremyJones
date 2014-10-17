using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using D_Quester;
using System.Reflection;

public class Inventory : MonoBehaviour
{
	public List<InventoryItem> items;
	public KeyCode InventoryPrintKey;
	public KeyCode InventoryDisplayKey;

	public delegate void PickupDelegate(string name);
	public event PickupDelegate PickupEvent;

	private bool inventoryVisible;

	public virtual void OnPickUpEvent(string name)
	{
		if (PickupEvent != null)
		{
			PickupEvent(name);
		}
	}

	void Start()
	{
		items = new List<InventoryItem>();
		inventoryVisible = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(InventoryPrintKey))
		{
			Debug.Log(ToString());
		}
		if (Input.GetKeyDown(InventoryDisplayKey))
		{
			DisplayInventory();
		}
	}

	public void DisplayInventory()
	{
		inventoryVisible = !inventoryVisible;
		GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = inventoryVisible;
	}

	public void AddItem(string name, int amount = 1)
	{
		OnPickUpEvent(name);
		var potentialItem = items.FirstOrDefault(x => x.Name == name);
		if (potentialItem == null)
		{
			items.Add(new InventoryItem(name, amount));
		}
		else
		{
			potentialItem.Amount += amount;
		}
	}

	public void AddItem(InventoryItem i)
	{
		OnPickUpEvent(i.Name);

		var potentialItem = items.FirstOrDefault(x => x.Name == i.Name);
		if (potentialItem == null)
		{
			items.Add(i);
		}
		else
		{
			potentialItem.Amount += i.Amount;
		}
	}

	public void RemoveItem(InventoryItem I)
	{

	}

	public void RemoveItem(string name, int amount)
	{

	}

	public override string ToString()
	{
		string stuff = "";
		foreach (var i in items)
		{
			stuff += i.ToString() + ", ";
		}

		if (stuff == "")
		{
			stuff = "Inventory is empty";
		}
		else
		{
			stuff = stuff.Substring(0, stuff.Length - 2);
		}

		return stuff;
	}
}
