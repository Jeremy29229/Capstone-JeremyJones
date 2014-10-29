using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuestRewarder : ScriptableObject
	{
		[MenuItem("D_Quester/Components/Add QuestRewarder", false, 1)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added QuestRewarder");
				selectedGameObject.AddComponent<QuestRewarder>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Components/Add QuestRewarder", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
