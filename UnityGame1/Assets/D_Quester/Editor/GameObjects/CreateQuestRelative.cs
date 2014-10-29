using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestRelative : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create Quest Relative", false, 7)]
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

			var quest = new GameObject();
			Undo.RegisterCreatedObjectUndo(quest, "Created Quest");

			if (Selection.activeTransform != null)
			{
				quest.transform.parent = Selection.activeTransform;

				quest.transform.localPosition = Vector3.zero;
				quest.transform.localRotation = Quaternion.identity;
				quest.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			quest.name = "Quest";
			quest.AddComponent<Quest>();
			Selection.activeTransform = quest.transform;
		}
	}
}
