using UnityEngine;

namespace D_Quester
{
	public class Collectable : MonoBehaviour, IInteractable
	{
		public string PlayerObjectName = "Player";
		private Inventory inventory;

		void Start()
		{
			inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		}

		void IInteractable.InteractWith()
		{
			inventory.Add(gameObject.GetComponent<Interactable>().InteractableName);
			Destroy(gameObject);
		}
	}
}
