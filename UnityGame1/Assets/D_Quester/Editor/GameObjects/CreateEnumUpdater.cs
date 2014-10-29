using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateEnumUpdater : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create EnumUpdater", false, 4)]
		public static void ShowWindow()
		{
			var enumUpdater = new GameObject();
			Undo.RegisterCreatedObjectUndo(enumUpdater, "Created EnumUpdater");
			enumUpdater.name = "EnumUpdater";
			enumUpdater.AddComponent<EnumUpdater>();
			Selection.activeTransform = enumUpdater.transform;
		}
	}
}
