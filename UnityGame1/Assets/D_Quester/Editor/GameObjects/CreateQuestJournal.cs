using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestJournal : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create QuestJournal", false, 3)]
		public static void ShowWindow()
		{
			var questJournal = new GameObject();
			Undo.RegisterCreatedObjectUndo(questJournal, "Created QuestJournal");

			questJournal.name = "QuestJournal";
			questJournal.AddComponent<QuestJournal>();
			Selection.activeTransform = questJournal.transform;
		}
	}
}
