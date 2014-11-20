using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateHierNPC : ScriptableObject
	{
		[MenuItem("D_Quester/HierConversations/Create Basic NPC", false, 0)]
		public static void ShowWindow()
		{
			var npc = new GameObject();
			Undo.RegisterCreatedObjectUndo(npc, "Created Basic NPC");

			npc.name = "NPC";

			var questInfo = new GameObject();
			questInfo.name = "HierConversationInfo";
			questInfo.transform.parent = npc.transform;

			var conversationInfo = new GameObject();
			conversationInfo.transform.parent = npc.transform;
			conversationInfo.AddComponent<Interactable>();
			conversationInfo.AddComponent<HierConversable>();
			conversationInfo.AddComponent<HierCorrespondence>();

			Selection.activeGameObject = npc;
			SceneView.lastActiveSceneView.MoveToView(npc.transform);
		}
	}
}
