using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddDialogResponse : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Add DialogResponse %G", false, 5)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added DialogResponse");
				selectedGameObject.AddComponent<DialogResponse>();
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Conversations/Add DialogResponse %G", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
