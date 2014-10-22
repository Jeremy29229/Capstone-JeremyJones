using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds the flow of QuestPaths for a chain of events.
	/// </summary>
	public class Quest : MonoBehaviour
	{
		/// <summary>
		/// Title of the quest.
		/// </summary>
		public string Title;

		/// <summary>
		/// Start path or collection of quest nodes. The first node to complete becomes the chosen path.
		/// </summary>
		public QuestPath StartingPath;

		/// <summary>
		/// Current path the quest is on.
		/// </summary>
		public QuestPath CurrentPath;

		private void Advance(QuestPath newPath)
		{
			if (newPath != null)
			{
				CurrentPath = newPath;
			}
			else
			{
				print("You just finished the Quest: " + Title + "!");
				CurrentPath = null;
			}
		}

		void Update()
		{
			if (CurrentPath != null && CurrentPath.isCompleted)
			{
				Advance(CurrentPath.SelectedPath);
			}
		}
	}
}
