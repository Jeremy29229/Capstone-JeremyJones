using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateCollectable : ScriptableObject
	{
		[MenuItem("D_Quester/Conversations/Create Collectable", false, 8)]
		public static void ShowWindow()
		{
			var collectable = new GameObject();
			Undo.RegisterCreatedObjectUndo(collectable, "Created Collectable");

			collectable.name = "Collectable";
			var interactable = collectable.AddComponent<Interactable>();
			interactable.Action = "pick up";
			interactable.InteractableName = "CollectableName";
			collectable.AddComponent<Collectable>();

			Selection.activeTransform = collectable.transform;
		}
	}
}
