using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuestJournal : ScriptableObject
	{
		[MenuItem("D_Quester/Components/Add QuestJournal", false, 5)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added QuestJournal");
				selectedGameObject.AddComponent<QuestJournal>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Components/Add QuestJournal", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
