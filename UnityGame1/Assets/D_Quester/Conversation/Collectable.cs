using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class Collectable : MonoBehaviour, IInteractable
	{
		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public string PlayerObjectName = "Player";
		
		private Inventory inventory;

		void Start()
		{
			inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		}

		/// <summary>
		/// 
		/// </summary>
		void IInteractable.InteractWith()
		{
			inventory.Add(gameObject.GetComponent<Interactable>().InteractableName);
			Destroy(gameObject);
		}
	}
}
