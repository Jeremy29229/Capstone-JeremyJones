using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuestNode : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Add QuestNode %J", false, 2)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added QuestNode");
				selectedGameObject.AddComponent<QuestNode>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Quests/Add QuestNode %J", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
