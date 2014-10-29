using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestNode : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create QuestNode", false, 0)]
		public static void ShowWindow()
		{
			var questNode = new GameObject();
			Undo.RegisterCreatedObjectUndo(questNode, "Created QuestNode");

			questNode.name = "QuestNode";
			questNode.AddComponent<QuestNode>();
			Selection.activeTransform = questNode.transform;
		}
	}
}
