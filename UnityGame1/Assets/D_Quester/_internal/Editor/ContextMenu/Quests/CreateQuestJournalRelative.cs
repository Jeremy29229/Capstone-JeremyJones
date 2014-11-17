using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestJournalRelative : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestJournal Relative", false, 10)]
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

			var questJournal = new GameObject();
			Undo.RegisterCreatedObjectUndo(questJournal, "Created QuestJournal");

			if (Selection.activeTransform != null)
			{
				questJournal.transform.parent = Selection.activeTransform;

				questJournal.transform.localPosition = Vector3.zero;
				questJournal.transform.localRotation = Quaternion.identity;
				questJournal.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			questJournal.name = "QuestJournal";
			questJournal.AddComponent<QuestJournal>();
			Selection.activeTransform = questJournal.transform;
		}
	}
}
