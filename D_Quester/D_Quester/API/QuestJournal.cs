using System.Collections.Generic;

namespace D_Quester
{
	/// <summary>
	/// Holds all quests in a list. Represents the current state of all quests and internal objects. For example, a Player class could contain an instance of QuestJournal to handle all quests for the player.
	/// </summary>
	public class QuestJournal
	{
		/// <summary>
		/// List of quests.
		/// </summary>
		public List<Quest> Quests { get; set; }

		/// <summary>
		/// Initializes quest list.
		/// </summary>
		public QuestJournal()
		{
			Quests = new List<Quest>();
		}
	}
}
