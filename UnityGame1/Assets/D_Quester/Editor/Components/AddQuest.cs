using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddQuest : ScriptableObject
	{
		[MenuItem("D_Quester/Components/Add Quest", false, 4)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added Quest");
				selectedGameObject.AddComponent<Quest>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Components/Add Quest", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
