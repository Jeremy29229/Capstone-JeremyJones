using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreatePlayerQ : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create Player", false, 7)]
		public static void ShowWindow()
		{
			var player = new GameObject();
			Undo.RegisterCreatedObjectUndo(player, "Created Player");

			player.name = "Player";
			player.AddComponent<Inventory>();
			Selection.activeGameObject = player;
			SceneView.lastActiveSceneView.MoveToView(player.transform);
		}
	}
}
