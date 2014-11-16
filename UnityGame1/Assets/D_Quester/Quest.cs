using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds the flow of QuestPaths for a chain of events.
	/// </summary>
	[System.Serializable]
	public class Quest : MonoBehaviour
	{
		/// <summary>
		/// Title of the quest.
		/// </summary>
		[Tooltip("Title of the quest.")]
		public string Title;

		/// <summary>
		/// Starting path or collection of quest nodes. The first node to be completed becomes the chosen path.
		/// </summary>
		[Tooltip("Starting path or collection of quest nodes. The first node to be completed becomes the chosen path.")]
		public QuestPath StartingPath;

		/// <summary>
		/// Current path the quest is on.
		/// </summary>
		[Tooltip("Current path the quest is on.")]
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
