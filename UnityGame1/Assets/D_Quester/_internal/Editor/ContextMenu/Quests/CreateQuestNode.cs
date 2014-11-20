using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestNode : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestNode", false, 11)]
		public static void ShowWindow()
		{
			var questNode = new GameObject();
			Undo.RegisterCreatedObjectUndo(questNode, "Created QuestNode");

			questNode.name = "QuestNode";
			questNode.AddComponent<QuestNode>();
			Selection.activeGameObject = questNode;
			SceneView.lastActiveSceneView.MoveToView(questNode.transform);
		}
	}
}
