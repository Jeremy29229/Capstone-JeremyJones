using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateInteractionManager : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Create InteractionManager", false, 10)]
		public static void ShowWindow()
		{
			var interactionManager = new GameObject();
			Undo.RegisterCreatedObjectUndo(interactionManager, "Created InteractionManager");

			interactionManager.name = "InteractionManager";
			interactionManager.AddComponent<InteractionManager>();

			Selection.activeGameObject = interactionManager;
			SceneView.lastActiveSceneView.MoveToView(interactionManager.transform);
		}
	}
}
