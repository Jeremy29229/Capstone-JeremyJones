using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuestPath : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Add QuestPath", false, 3)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added QuestPath");
				selectedGameObject.AddComponent<QuestPath>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Quests/Add QuestPath", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
