using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddDialog : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Add Dialog %H", false, 3)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.AddComponent<Dialog>(selectedGameObject);
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Conversations/Add Dialog %H", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
