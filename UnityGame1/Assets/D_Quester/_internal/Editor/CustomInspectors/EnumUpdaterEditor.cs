using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	[CustomEditor(typeof(EnumUpdater))]
	public class EnumUpdaterEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EnumUpdater enumUpdate = (EnumUpdater)target;

			if (GUILayout.Button("Run Enum Updater"))
			{
				enumUpdate.RunEnumUpdater();
			}
		}
	}
}
