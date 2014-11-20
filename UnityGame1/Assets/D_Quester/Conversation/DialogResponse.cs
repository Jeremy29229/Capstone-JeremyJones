using UnityEngine;
using System;

namespace D_Quester
{
	/// <summary>
	/// A component representing a dialog response the player may choose in response to an NPC dialog.
	/// </summary>
	public class DialogResponse : MonoBehaviour
	{
		/// <summary>
		/// Name of this DialogResponse.
		/// </summary>
		[Tooltip("Name of this DialogResponse.")]
		public string DialogResponseName = "";

		/// <summary>
		/// Resulting NPC dialog if this dialog responses is selected by the player.
		/// </summary>
		[Tooltip("Resulting NPC dialog if this dialog responses is selected by the player.")]
		public Dialog Resulting;

		/// <summary>
		/// If not null, will change the current starting conversation to this if this dialog response is selected.
		/// </summary>
		[Tooltip("If not null, will change the current starting conversation to this if this dialog response is selected.")]
		public Conversation NewCurrent;

		/// <summary>
		/// Text displayed to the player.
		/// </summary>
		[Tooltip("Text displayed to the player.")]
		public string Text = "";

		/// <summary>
		/// Should this dialog response only be selectable once?
		/// </summary>
		[Tooltip("Should this dialog response only be selectable once?")]
		public bool IsOneTimeOption = false;

		/// <summary>
		/// The number of times this DialogResponse has been selected by the player.
		/// </summary>
		[HideInInspector]
		public int NumTimesSelected = 0;

		/// <summary>
		/// An array of all the item requirements for this DialogResponse to appear for the player.
		/// </summary>
		[Tooltip("An array of all the item requirements for this DialogResponse to appear for the player.")]
		public ItemRequirement[] ItemRequirements;

		/// <summary>
		/// Required state a certain QuestNode for this dialog option to be selectable by the player.
		/// </summary>
		[Tooltip("Required state a certain QuestNode needs to be in for this dialog option to be selectable by the player.")]
		public QuestRequirement Quest_Requirement;
	}
}
