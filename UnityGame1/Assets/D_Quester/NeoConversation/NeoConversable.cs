using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace D_Quester
{
#pragma warning disable 0414

	/// <summary>
	/// The basic conversable script to be added to an NPC's GameObject (or any child GameObject). Allows the player to talk to an NPC with at least a correspondence, conversation, and dialog component attached to the same GameObject as this component.
	/// </summary>
	public class NeoConversable : MonoBehaviour, IInteractable
	{
		/// <summary>
		/// Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.
		/// </summary>
		[Tooltip("Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.")]
		public string PlayerObjectName = "Player";

		/// <summary>
		/// Name of the GameObject that contains the ConversationManager Component.
		/// </summary>
		[Tooltip("Name of the GameObject that contains the ConversationManager Component.")]
		public string ConversationManagerObjectName = "ConvoGUI";

		private NeoCorrespondence correspondence;
		private NeoConversationManager cm;
		private GameObject player;

		void Start()
		{
			player = GameObject.Find(PlayerObjectName);
			correspondence = gameObject.GetComponent<NeoCorrespondence>();
			cm = GameObject.Find(ConversationManagerObjectName).GetComponent<NeoConversationManager>();
		}

		/// <summary>
		/// Starts the conversation with the NPC when the player interacts with them.
		/// </summary>
		public void InteractWith()
		{
			GetComponent<Interactable>().IsActive = false;
			NeoConversation startingConvo = correspondence.transform.FindChild(correspondence.CurrentConversationName).gameObject.GetComponent<NeoConversation>();
			if (startingConvo == null)
			{
				Debug.LogException(new UnityException("Unable to find NeoConversation with the name: " + correspondence.CurrentConversationName + "."));
			}
			else
			{
				NeoDialog startingDialog = startingConvo.GetComponentInChildren<NeoDialog>();
				if (startingDialog == null)
				{
					Debug.LogException(new UnityException("Unable to find starting NeoDialog of NeoConversation: " + correspondence.CurrentConversationName + "."));
				}
				else
				{
					cm.ProcessDialog(startingDialog);
				}
			}
		}
	}
#pragma warning restore 0414
}
