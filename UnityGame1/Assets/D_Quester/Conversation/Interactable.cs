using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace D_Quester
{
	/// <summary>
	/// Allows an object to be interacted with. GameObject must also have another script component that implements IIteractable.
	/// </summary>
	public class Interactable : MonoBehaviour
	{
		/// <summary>
		/// Indicates how close the player must be to this object for the popup appear, allowing for interaction, unless another interactable is closer.
		/// </summary>
		[Tooltip("Indicates how close the player must be to this object for the popup appear, allowing for interaction, unless another interactable is closer.")]
		public float InteractionRadius = 2.0f;

		/// <summary>
		/// Indicates what key the player must press to interact with this interactable.
		/// </summary>
		[Tooltip("Indicates what key the player must press to interact with this interactable.")]
		public KeyCode InteractionKey = KeyCode.E;

		/// <summary>
		/// Should this interactable be actively displayed?
		/// </summary>
		[Tooltip("Should this interactable be actively displayed?")]
		public bool IsActive = true;
		
		/// <summary>
		/// Displays how the player is interacting with this object. Examples include: "pick up", "steal", or "open". Should be all lowercase as this is in the middle of the interactable's display text.
		/// </summary>
		[Tooltip("Displays how the player is interacting with this object. Examples include: \"pick up\", \"steal\", or \"open\". Should be all lowercase as this is in the middle of the interactable's display text.")]
		public string Action = "";

		/// <summary>
		/// Name displayed to the player when the popup appears for this object. Examples include: "door", "coin", or "Fred".
		/// </summary>
		[Tooltip("Name displayed to the player when the popup appears for this object. Examples include: \"door\", \"coin\", or \"Fred\".")]
		public string InteractableName = "";

		/// <summary>
		/// Anything else you want displayed in the interaction popup. Will be appended to the end of the display text.
		/// </summary>
		[Tooltip("Anything else you want displayed in the interaction popup. Will be appended to the end of the display text.")]
		public string AdditionalInformation = "";

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

		/// <summary>
		/// Name of the GameObject that contains the InteractionManager script.
		/// </summary>
		[Tooltip("Name of the GameObject that contains the InteractionManager script.")]
		public string InteractionManagerObjectName = "InteractionManager";

		protected IInteractable behavior;
		protected GameObject Player;
		protected Canvas UI;

		public virtual void Start()
		{
			UI = (Canvas)GameObject.Find(InteractionGUIObjectName).GetComponent(typeof(Canvas));
			UI.enabled = false;

			Player = GameObject.Find(PlayerObjectName);
			if (Player == null)
			{
				throw new UnassignedReferenceException("Unable to find player's GameObject. Make sure the PlayerObjectName matches the Player's GameObject name.");
			}

			behavior = (IInteractable)gameObject.GetComponent("IInteractable");
			if (behavior == null)
			{
				throw new UnassignedReferenceException("A script that implements IInteractable must be a component in the same GameObject as this script.");
			}

			GameObject.Find(InteractionManagerObjectName).GetComponent<InteractionManager>().Interactables.Add(gameObject);
		}

		/// <summary>
		/// Called by the InteractionManager component to check if the player has attempted to interact with this object.
		/// </summary>
		public void InteractionUpdate()
		{
			if (Vector3.Distance(Player.transform.position, gameObject.transform.position) <= InteractionRadius)
			{
				UI.enabled = true;
				if (Input.GetKeyDown(InteractionKey))
				{
					behavior.InteractWith();
				}
			}
			else
			{
				UI.enabled = false;
			}
		}

		/// <summary>
		/// Called by the InteractionManager component to update the interaction popup when this object is the closest in-range.
		/// </summary>
		public virtual void updateGUIText()
		{
			UI.GetComponentInChildren<Text>().text = "Press " + InteractionKey.ToString() + " to " + Action + " " + InteractableName;
			if (!string.IsNullOrEmpty(AdditionalInformation))
			{
				UI.GetComponentInChildren<Text>().text += " " + AdditionalInformation;
			}

			UI.enabled = false;
		}
	}
}