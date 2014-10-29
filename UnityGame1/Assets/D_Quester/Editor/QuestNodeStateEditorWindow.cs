using UnityEngine;
using UnityEditor;

namespace D_Quester
{
	public class QuestNodeStateEditorWindow : EditorWindow
	{
		public static QuestNodeState defaultState = 0;
		
		[MenuItem("D_Quester/Quest Node State Options")]
		static void init()
		{
			QuestNodeStateEditorWindow window = (QuestNodeStateEditorWindow)EditorWindow.GetWindow(typeof(QuestNodeStateEditorWindow));
			window.name = "Node Options";
		}

		void OnGUI()
		{
			defaultState = (QuestNodeState)EditorGUILayout.EnumPopup("Default Node State", defaultState);
			QuestNode.defaultState = defaultState;
		}
	}
}
