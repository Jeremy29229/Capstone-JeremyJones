using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateBasicNPC : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Create Basic NPC", false, 7)]
		public static void ShowWindow()
		{
			var npc = new GameObject();
			Undo.RegisterCreatedObjectUndo(npc, "Created Basic NPC");

			npc.name = "NPC";
			
			var questInfo = new GameObject();
			questInfo.name = "QuestInfo";
			questInfo.transform.parent = npc.transform;

			var conversationInfo = new GameObject();
			conversationInfo.transform.parent = npc.transform;
			conversationInfo.AddComponent<Interactable>();
			conversationInfo.AddComponent<Conversable>();

			var correspondence = conversationInfo.AddComponent<Correspondence>();
			var convo = conversationInfo.AddComponent<Conversation>();
			var dialog = conversationInfo.AddComponent<Dialog>();
			var dialogResponse = conversationInfo.AddComponent<DialogResponse>();
			correspondence.Current = convo;
			convo.Beginning = dialog;
			dialog.Responses[0] = dialogResponse;

			Selection.activeTransform = npc.transform;
		}
	}
}
