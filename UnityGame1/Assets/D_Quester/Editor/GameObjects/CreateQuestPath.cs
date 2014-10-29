﻿using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestPath : ScriptableObject
	{
		[MenuItem("D_Quester/GameObjects/Create QuestPath", false, 1)]
		public static void ShowWindow()
		{
			var questPath = new GameObject();
			Undo.RegisterCreatedObjectUndo(questPath, "Created QuestPath");

			questPath.name = "QuestPath";
			questPath.AddComponent<QuestPath>();
			Selection.activeTransform = questPath.transform;
		}
	}
}