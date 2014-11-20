using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddCorrespondenceWithOthers : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Add Full Correspondence", false, 6)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				var correspondence = Undo.AddComponent<Correspondence>(selectedGameObject);
				var convo = Undo.AddComponent<Conversation>(selectedGameObject);
				var dialog = Undo.AddComponent<Dialog>(selectedGameObject);
				var dialogResponse = Undo.AddComponent<DialogResponse>(selectedGameObject);
				correspondence.Current = convo;
				convo.Beginning = dialog;
				dialog.Responses[0] = dialogResponse;
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Conversations/Add Full Correspondence", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
