using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateHierConversationInfo : ScriptableObject
	{
		[MenuItem("D_Quester/HierConversations/Create HierConversationInfo Relative", false, 1)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null)
			{
				PrefabType type = PrefabUtility.GetPrefabType(Selection.activeGameObject);
				if (type == PrefabType.PrefabInstance)
				{
					if (!EditorUtility.DisplayDialog("Losing prefab",
												   "This action will lose the prefab connection. Are you sure you wish to continue?",
												   "Continue", "Cancel"))
					{
						return;
					}
				}
			}

			var neoConversationInfo = new GameObject();
			Undo.RegisterCreatedObjectUndo(neoConversationInfo, "Created HierConversationinfo");

			if (Selection.activeTransform != null)
			{
				neoConversationInfo.transform.parent = Selection.activeTransform;

				neoConversationInfo.transform.localPosition = Vector3.zero;
				neoConversationInfo.transform.localRotation = Quaternion.identity;
				neoConversationInfo.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			neoConversationInfo.name = "HierConversationInfo";
			neoConversationInfo.AddComponent<Interactable>();
			neoConversationInfo.AddComponent<HierConversable>();
			neoConversationInfo.AddComponent<HierCorrespondence>();
			Selection.activeGameObject = neoConversationInfo;
			SceneView.lastActiveSceneView.MoveToView(neoConversationInfo.transform);
		}
	}
}
