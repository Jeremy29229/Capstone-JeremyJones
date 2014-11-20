using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestPathRelative : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestPath Relative", false, 14)]
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

			var questPath = new GameObject();
			Undo.RegisterCreatedObjectUndo(questPath, "Created QuestPath");

			if (Selection.activeTransform != null)
			{
				questPath.transform.parent = Selection.activeTransform;

				questPath.transform.localPosition = Vector3.zero;
				questPath.transform.localRotation = Quaternion.identity;
				questPath.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			questPath.name = "QuestPath";
			questPath.AddComponent<QuestPath>();
			Selection.activeGameObject = questPath;
			SceneView.lastActiveSceneView.MoveToView(questPath.transform);
		}
	}
}
