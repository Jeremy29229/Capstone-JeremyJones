using UnityEngine;
using UnityEditor;
using System;

namespace D_Quester
{
	[CustomEditor(typeof(Quest))]
	public class QuestEditor : Editor
	{
		private Quest questInstance;
		private SerializedObject questInstanceObject;
		private bool showStartingPath = true;
		private bool showCurrentPath = true;

		void OnEnable()
		{
			if (target != null && target.GetType() == typeof(Quest))
			{
				questInstance = (Quest)target;
				questInstanceObject = new SerializedObject(target);
			}
		}

		public override void OnInspectorGUI()
		{
			if (questInstanceObject != null)
			{
				questInstanceObject.Update();
			}

			if (questInstance != null)
			{

				questInstance.Title = EditorGUILayout.TextField("Title", questInstance.Title);

				showStartingPath = EditorGUILayout.Foldout(showStartingPath, "Starting Path");
				EditorGUI.indentLevel++;
				if (showStartingPath)
				{
					EditorGUILayout.PropertyField(questInstanceObject.FindProperty("StartingPath"));
					if (questInstance.StartingPath != null)
					{
						questInstance.StartingPath.QuestPathName = EditorGUILayout.TextField("Quest Path Name", questInstance.StartingPath.QuestPathName);
						questInstance.StartingPath.isCompleted = EditorGUILayout.Toggle("Is Completed", questInstance.StartingPath.isCompleted);
					}

				}
				EditorGUI.indentLevel--;

				showCurrentPath = EditorGUILayout.Foldout(showCurrentPath, "Current Path");
				EditorGUI.indentLevel++;
				if (showCurrentPath)
				{
					EditorGUILayout.PropertyField(questInstanceObject.FindProperty("CurrentPath"), new GUIContent(""));
					if (questInstance.CurrentPath != null)
					{
						questInstance.CurrentPath.QuestPathName = EditorGUILayout.TextField("Quest Path Name", questInstance.CurrentPath.QuestPathName);
						questInstance.CurrentPath.isCompleted = EditorGUILayout.Toggle("Is Completed", questInstance.CurrentPath.isCompleted);
					}

				}
				EditorGUI.indentLevel--;

				questInstanceObject.ApplyModifiedProperties();
			}
		}
	}
}
