using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class QuestNodeStateEditorWindow : EditorWindow
	{
		public static QuestNodeState defaultState = QuestNodeState.NotStarted;

		[MenuItem("D_Quester/Options/Quest Node State Options")]
		static void init()
		{
			QuestNodeStateEditorWindow window = (QuestNodeStateEditorWindow)EditorWindow.GetWindow(typeof(QuestNodeStateEditorWindow));
			window.name = "Node Options";
			window.title = "Node Options";
		}

		void OnGUI()
		{
			Debug.Log("OnGUI");
			defaultState = (QuestNodeState)EditorGUILayout.EnumPopup("Default Node State", defaultState);
			QuestNode.defaultState = defaultState;
		}
	}
}
