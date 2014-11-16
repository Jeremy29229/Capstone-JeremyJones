using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace D_Quester
{
	/// <summary>
	/// Handles the displaying of the interactable's popups and updating the GUI's text appropriately.
	/// </summary>
	public class InteractionManager : MonoBehaviour
	{
		/// <summary>
		/// List of the all existing interactables that the player can see if they are close enough to them. Do not directly add to this list. Use the Interactable script.
		/// </summary>
		[Tooltip("List of the all existing interactables that the player can see if they are close enough to them. Do not directly add to this list. Use the Interactable script.")]
		public List<GameObject> Interactables;

		/// <summary>
		/// Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.
		/// </summary>
		[Tooltip("Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.")]
		public string PlayerObjectName = "Player";

		/// <summary>
		/// Name of the GameObject is prefabed to the InteractionGUI.
		/// </summary>
		[Tooltip("Name of the GameObject is prefabed to the InteractionGUI.")]
		public string InteractionGUIObjectName = "InteractionGUI";

		private GameObject player;
		private Canvas UI;

		void Start()
		{
			UI = (Canvas)GameObject.Find(InteractionGUIObjectName).GetComponent(typeof(Canvas));
			player = GameObject.Find(PlayerObjectName);
		}

		void Update()
		{
			GameObject closest = null;
			float minDistance = float.MaxValue;

			foreach (var v in Interactables)
			{
				if (v == null)
				{
					Interactables.Remove(v);
					break;
				}
				else
				{
					float currentDistance = Vector3.Distance(player.transform.position, v.transform.position);
					if (currentDistance < minDistance && v.GetComponent<Interactable>().IsActive)
					{
						minDistance = currentDistance;
						closest = v;
					}
				}
			}

			if (closest == null)
			{
				UI.enabled = false;
			}
			else
			{
				closest.GetComponent<Interactable>().updateGUIText();
				closest.GetComponent<Interactable>().InteractionUpdate();
			}
		}
	}
}
