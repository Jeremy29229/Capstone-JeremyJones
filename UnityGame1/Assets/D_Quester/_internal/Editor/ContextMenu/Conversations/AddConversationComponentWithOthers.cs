﻿using UnityEngine;
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
				Undo.RegisterCreatedObjectUndo(selectedGameObject, "Added Basic Conversation");
				var convo = selectedGameObject.AddComponent<Conversation>();
				var dialog = selectedGameObject.AddComponent<Dialog>();
				var dialogResponse = selectedGameObject.AddComponent<DialogResponse>();
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
