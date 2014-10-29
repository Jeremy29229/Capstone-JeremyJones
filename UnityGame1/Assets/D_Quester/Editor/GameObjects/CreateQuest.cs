using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuest : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create Quest", false, 2)]
		public static void ShowWindow()
		{
			var quest = new GameObject();
			Undo.RegisterCreatedObjectUndo(quest, "Created Quest");

			quest.name = "Quest";
			quest.AddComponent<Quest>();
			Selection.activeTransform = quest.transform;
		}
	}
}
