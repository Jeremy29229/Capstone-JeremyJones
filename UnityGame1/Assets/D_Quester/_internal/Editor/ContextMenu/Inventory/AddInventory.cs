using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class AddInventory : ScriptableObject
	{
		[MenuItem("D_Quester/Inventory/Add Inventory", false, 0)]
		public static void ShowWindow()
		{
			if (Selection.activeTransform != null && Selection.activeTransform.gameObject != null)
			{
				var selectedGameObject = Selection.activeTransform.gameObject;
				Undo.AddComponent<Inventory>(selectedGameObject);
			}
			else
			{
				Debug.LogException(new UnityException("Q_Quester menu's add component called on null transform or game object. This should not happen."));
			}
		}

		[MenuItem("D_Quester/Inventory/Add Inventory", true)]
		public static bool ValidateMenuItem()
		{
			return Selection.activeTransform != null;
		}
	}
}
