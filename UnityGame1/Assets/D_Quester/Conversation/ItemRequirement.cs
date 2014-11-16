using UnityEngine;

/// <summary>
/// Describes an item needed in the player's inventory for a dialog response to appear.
/// </summary>
public class ItemRequirement : MonoBehaviour
{
	/// <summary>
	/// Name of the required item.
	/// </summary>
	[Tooltip("Name of the required item.")]
	public string ItemName = "";

	/// <summary>
	/// Amount of the item required to be in the player's inventory.
	/// </summary>
	[Tooltip("Amount of the item required to be in the player's inventory.")]
	public int Amount = 1;

	/// <summary>
	/// Should the items be taken if the dialog response is selected?
	/// </summary>
	[Tooltip("Should the items be taken if the dialog response is selected?")]
	public bool AreItemsTaken = true;
}
