using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Allows the object to be picked up by the player and added to their inventory.
	/// </summary>
	public class Collectable : MonoBehaviour, IInteractable
	{
		/// <summary>
		/// Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.
		/// </summary>
		[Tooltip("Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.")]
		public string PlayerObjectName = "Player";

		private Inventory inventory;

		void Start()
		{
			inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		}

		/// <summary>
		/// Adds the collectable to the players inventory on interaction.
		/// </summary>
		void IInteractable.InteractWith()
		{
			inventory.Add(gameObject.GetComponent<Interactable>().InteractableName);
			Destroy(gameObject);
		}
	}
}
