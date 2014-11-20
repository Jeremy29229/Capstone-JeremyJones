using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddConversationComponentWithOthers : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Add Basic Conversation", false, 0)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				var convo = Undo.AddComponent<Conversation>(selectedGameObject);
				var dialog = Undo.AddComponent<Dialog>(selectedGameObject);
				var dialogResponse = Undo.AddComponent<DialogResponse>(selectedGameObject);
				convo.Beginning = dialog;
				dialog.Responses[0] = dialogResponse;
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Conversations/Add Basic Conversation", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
