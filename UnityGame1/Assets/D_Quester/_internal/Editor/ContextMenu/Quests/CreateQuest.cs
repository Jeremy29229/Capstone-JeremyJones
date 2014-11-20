using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuest : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create Quest", false, 8)]
		public static void ShowWindow()
		{
			var quest = new GameObject();
			Undo.RegisterCreatedObjectUndo(quest, "Created Quest");

			quest.name = "Quest";
			quest.AddComponent<Quest>();
			Selection.activeGameObject = quest;
			SceneView.lastActiveSceneView.MoveToView(quest.transform);
		}
	}
}
