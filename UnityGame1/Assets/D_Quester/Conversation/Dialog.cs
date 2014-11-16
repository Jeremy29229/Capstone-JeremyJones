using UnityEngine;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// An NPC's single dialog.
	/// </summary>
	public class Dialog : MonoBehaviour
	{
		/// <summary>
		/// "Text of the NPC's dialog."
		/// </summary>
		[Tooltip("Text of the NPC's dialog.")]
		public string NPCDialog = "";

		/// <summary>
		/// Dialog responses the player can choose in the form of DialogResponse components. Current there can only be up to 4 responses per dialog.
		/// </summary>
		[Tooltip("Dialog responses the player can choose in the form of DialogResponse components. Current there can only be up to 4 responses per dialog.")]
		public DialogResponse[] Responses = new DialogResponse[4];

		private Interactable npc;

		void Start()
		{
			npc = (GetComponent<Interactable>()) ? GetComponent<Interactable>() : null;
		}

		/// <summary>
		/// NPC name of the owner of this dialog. This should never be called if there is no Interactable component in the GameObject this component is inside.
		/// </summary>
		/// <returns>NPC name of the owner of this dialog. This should never be called if there is no Interactable component in the GameObject this component is inside.</returns>
		public string GetNPCName()
		{
			return npc.InteractableName;
		}
	}
}
