using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateInventory : ScriptableObject
	{
		[MenuItem("D_Quester/Inventory/Create Inventory", false, 1)]
		public static void ShowWindow()
		{
			var inventory = new GameObject();
			Undo.RegisterCreatedObjectUndo(inventory, "Created Inventory");

			inventory.name = "Inventory";
			inventory.AddComponent<Inventory>();
			Selection.activeGameObject = inventory;
			SceneView.lastActiveSceneView.MoveToView(inventory.transform);
		}
	}
}
