using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateEnumUpdater : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create EnumUpdater", false, 6)]
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
