using System.Collections.Generic;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds all quests in a list. Represents the current state of all quests and internal objects. For example, a Player class could contain an instance of QuestJournal to handle all quests for the player.
	/// </summary>
	public class QuestJournal : MonoBehaviour
	{
		/// <summary>
		/// List of quests.
		/// </summary>
		[Tooltip("List of quests.")]
		public List<Quest> Quests;
	}
}
