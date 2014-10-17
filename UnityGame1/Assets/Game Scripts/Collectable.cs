using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "Player";
	private Inventory inventory;
	public Sprite inventorySprite;

	void Start()
	{
		inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
	}

	void IInteractable.InteractWith()
	{
		inventory.AddItem(gameObject.GetComponent<Interactable>().InteractableName);
		Destroy(gameObject);
	}
}
