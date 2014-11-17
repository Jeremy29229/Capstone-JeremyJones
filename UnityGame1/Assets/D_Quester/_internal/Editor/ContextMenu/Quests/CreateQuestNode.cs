using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class CreateQuestNode : ScriptableObject
	{
		[MenuItem("D_Quester/Quests/Create QuestNode", false, 11)]
		public static void ShowWindow()
		{
			var questNode = new GameObject();
			Undo.RegisterCreatedObjectUndo(questNode, "Created QuestNode");

			questNode.name = "QuestNode";
			questNode.AddComponent<QuestNode>();
			Selection.activeTransform = questNode.transform;
		}

		void OnGUI()
		{
			Debug.Log("OnGUI");
		}

		void Update()
		{
			Debug.Log("Update");
		}

		void OnEnable()
		{
			Debug.Log("OnEnable");
		}

		void OnDisable()
		{
			Debug.Log("OnDisable");
		}

		void OnDestroy()
		{
			Debug.Log("OnDestroy");
		}

		void OnFocus()
		{
			Debug.Log("OnFocus");
		}

		void OnHierarchyChange()
		{
			Debug.Log("OnHierarchyChange");
		}

		void OnInspectorUpdate()
		{
			Debug.Log("OnInspectorUpdate");
		}

		void OnLostFocus()
		{
			Debug.Log("OnLostFocus");
		}

		void OnProjectChange()
		{
			Debug.Log("OnProjectChange");
		}

		void OnSelectionChange()
		{
			Debug.Log("OnSelectionChange");
		}
	}
}
