using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace D_Quester
{
#pragma warning disable 0414

	/// <summary>
	/// The basic conversable script to be added to an NPC's GameObject (or any child GameObject). Allows the player to talk to an NPC with at least a correspondence, conversation, and dialog component attached to the same GameObject as this component.
	/// </summary>
	public class Conversable : MonoBehaviour, IInteractable
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

		private Correspondence correspondence;
		private ConversationManager cm;
		private GameObject player;

		void Start()
		{
			player = GameObject.Find(PlayerObjectName);
			correspondence = gameObject.GetComponent<Correspondence>();
			cm = GameObject.Find(ConversationManagerObjectName).GetComponent<ConversationManager>();
		}

		/// <summary>
		/// Starts the conversation with the NPC when the player interacts with them.
		/// </summary>
		public void InteractWith()
		{
			GetComponent<Interactable>().IsActive = false;
			cm.ProcessDialog(correspondence.Current.Beginning);
		}
	}
#pragma warning restore 0414
}
