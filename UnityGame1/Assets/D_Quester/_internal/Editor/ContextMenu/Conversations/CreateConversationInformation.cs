using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateConversationInformation : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Create ConversationInfo", false, 9)]
		public static void ShowWindow()
		{
			var conversationInfo = new GameObject();
			Undo.RegisterCreatedObjectUndo(conversationInfo, "Created ConversationInfo");

			conversationInfo.name = "ConversationInfo";

			conversationInfo.AddComponent<Interactable>();
			conversationInfo.AddComponent<Conversable>();

			var correspondence = conversationInfo.AddComponent<Correspondence>();
			var convo = conversationInfo.AddComponent<Conversation>();
			var dialog = conversationInfo.AddComponent<Dialog>();
			var dialogResponse = conversationInfo.AddComponent<DialogResponse>();
			correspondence.Current = convo;
			convo.Beginning = dialog;
			dialog.Responses[0] = dialogResponse;

			Selection.activeTransform = conversationInfo.transform;
		}
	}
}