using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestJournal : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestJournal", false, 9)]
		public static void ShowWindow()
		{
			var questJournal = new GameObject();
			Undo.RegisterCreatedObjectUndo(questJournal, "Created QuestJournal");

			questJournal.name = "QuestJournal";
			questJournal.AddComponent<QuestJournal>();
			Selection.activeGameObject = questJournal;
			SceneView.lastActiveSceneView.MoveToView(questJournal.transform);
		}
	}
}
