using UnityEngine;
using System;

namespace D_Quester
{
	/// <summary>
	/// A component representing a dialog response the player may choose in response to an NPC dialog.
	/// </summary>
	public class HierDialogResponse : MonoBehaviour
	{
		private HierCorrespondence _correspondence;

		[HideInInspector]
		public HierCorrespondence Correspondence
		{
			get
			{
				if (_correspondence == null)
				{
					if (GetComponentInParent<HierDialog>() != null)
					{
						_correspondence = GetComponentInParent<HierDialog>().Correspondence;
					}
					else
					{
						Debug.LogException(new UnityException("HierDialogResponse's parent object does not contain a HierDialog."));
					}
				}
				return _correspondence;
			}
			private set
			{
				_correspondence = value;
			}
		}

		/// <summary>
		/// Name of this DialogResponse.
		/// </summary>
		[Tooltip("Name of this DialogResponse.")]
		public string DialogResponseName = "";

		/// <summary>
		/// If not null or empty, the name of the conversation that the correspondence will use the next the this conversation is interacted with,
		/// </summary>
		[Tooltip("If not null or empty, the name of the conversation that the correspondence will use the next the this conversation is interacted with,")]
		public string NewConversationName = "";

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
