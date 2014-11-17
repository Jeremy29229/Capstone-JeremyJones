using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddDialogAndResponse : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Add Dialog With Response", false, 4)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added Dialog With Response");
				var dialog = selectedGameObject.AddComponent<Dialog>();
				var dialogResponse = selectedGameObject.AddComponent<DialogResponse>();
				dialog.Responses[0] = dialogResponse;
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Conversations/Add Dialog With Response", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
