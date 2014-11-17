using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	[CustomEditor(typeof(QuestPath))]
	public class QuestPathEditor : Editor
	{
		private QuestPath questPathInstance;
		private SerializedObject questPathInstanceObject;
		private bool showSelectedPath = true;

		void OnEnable()
		{
			if (target != null && target.GetType() == typeof(QuestPath))
			{
				questPathInstance = (QuestPath)target;
				questPathInstanceObject = new SerializedObject(target);
			}
		}

		public override void OnInspectorGUI()
		{
			if (questPathInstanceObject != null)
			{
				questPathInstanceObject.Update();
			}

			questPathInstance.QuestPathName = EditorGUILayout.TextField("Quest Path Name", questPathInstance.QuestPathName);
			EditorGUILayout.PropertyField(questPathInstanceObject.FindProperty("QuestNodes"), new GUIContent("Quest Nodes"), true);

			showSelectedPath = EditorGUILayout.Foldout(showSelectedPath, "Selected Path");
			EditorGUI.indentLevel++;
			if (showSelectedPath)
			{
				EditorGUILayout.PropertyField(questPathInstanceObject.FindProperty("SelectedPath"));
				if (questPathInstance.SelectedPath != null)
				{
					questPathInstance.SelectedPath.QuestPathName = EditorGUILayout.TextField("Quest Path Name", questPathInstance.SelectedPath.QuestPathName);
					questPathInstance.SelectedPath.isCompleted = EditorGUILayout.Toggle("Is Completed", questPathInstance.SelectedPath.isCompleted);
				}

			}
			EditorGUI.indentLevel--;

			questPathInstance.isCompleted = EditorGUILayout.Toggle("Is Completed", questPathInstance.isCompleted);

			questPathInstanceObject.ApplyModifiedProperties();
		}
	}
}
