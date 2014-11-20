using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestNodeRelative : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestNode Relative", false, 12)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null)
			{
				PrefabType type = PrefabUtility.GetPrefabType(Selection.activeGameObject);
				if (type == PrefabType.PrefabInstance)
				{
					if (!EditorUtility.DisplayDialog("Losing prefab",
												   "This action will lose the prefab connection. Are you sure you wish to continue?",
												   "Continue", "Cancel"))
					{
						return;
					}
				}
			}

			var questNode = new GameObject();
			Undo.RegisterCreatedObjectUndo(questNode, "Created QuestNode");

			if (Selection.activeTransform != null)
			{
				questNode.transform.parent = Selection.activeTransform;

				questNode.transform.localPosition = Vector3.zero;
				questNode.transform.localRotation = Quaternion.identity;
				questNode.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			questNode.name = "QuestNode";
			questNode.AddComponent<QuestNode>();
			Selection.activeGameObject = questNode;
			SceneView.lastActiveSceneView.MoveToView(questNode.transform);
		}
	}
}
