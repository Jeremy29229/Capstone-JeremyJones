using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuest : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Add Quest", false, 0)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.AddComponent<Quest>(selectedGameObject);
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Quests/Add Quest", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
