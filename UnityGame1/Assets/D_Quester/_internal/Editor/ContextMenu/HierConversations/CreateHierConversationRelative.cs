﻿using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateHierConversationRelative : ScriptableObject
	{
		static int currentNum = 0;

		[MenuItem("D_Quester/HierConversations/Create HierConversation Relative", false, 2)]
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

			var neoConversation = new GameObject();
			Undo.RegisterCreatedObjectUndo(neoConversation, "Created HierConversation");

			if (Selection.activeTransform != null)
			{
				neoConversation.transform.parent = Selection.activeTransform;

				neoConversation.transform.localPosition = Vector3.zero;
				neoConversation.transform.localRotation = Quaternion.identity;
				neoConversation.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			neoConversation.name = "HierConversation" + currentNum++;
			neoConversation.AddComponent<HierConversation>();
			Selection.activeGameObject = neoConversation;
			SceneView.lastActiveSceneView.MoveToView(neoConversation.transform);
		}
	}
}
