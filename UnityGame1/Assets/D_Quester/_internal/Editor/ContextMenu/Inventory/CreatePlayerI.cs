using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreatePlayerI : ScriptableObject
	{
		[MenuItem("D_Quester/Inventory/Create Player", false, 2)]
		public static void ShowWindow()
		{
			var player = new GameObject();
			Undo.RegisterCreatedObjectUndo(player, "Created Player");

			player.name = "Player";
			player.AddComponent<Inventory>();
			Selection.activeTransform = player.transform;
		}
	}
}
