using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds nodes for a dynamic player choice. The node first marked as completed will become the selected path or node.
	/// </summary>
	[System.Serializable]
	public class QuestPath : MonoBehaviour
	{
		/// <summary>
		/// Name of the QuestPath. Useful for Identifying the correct QuestPath in a GameObject.
		/// </summary>
		public string QuestPathName;

		/// <summary>
		/// All node options for this QuestPath
		/// </summary>
		public QuestNode[] QuestNodes;

		/// <summary>
		/// Becomes the next path a quest will take when a node is completed.
		/// </summary>
		public QuestPath SelectedPath = null;
		
		/// <summary>
		/// Indicates if this path is finished or not.
		/// </summary>
		public bool isCompleted = false;

		private bool hasAnyNodeFinished = false;
		private QuestNode selectedNode = null;
		private bool isActivePath = true;

		/// <summary>
		/// Obsolete?
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static QuestPath MakeSingleNodePath(QuestNode node)
		{
			QuestPath p = new QuestPath();
			p.QuestNodes = new QuestNode[1];
			p.QuestNodes[0] = node;
			return p;
		}

		void Update()
		{
			if (isActivePath)
			{
				foreach (var node in QuestNodes)
				{
					if (node.CurrentState == QuestNodeState.Completed)
					{
						hasAnyNodeFinished = true;
						if (selectedNode == null || selectedNode.CompletionTime > node.CompletionTime)
						{
							selectedNode = node;
						}
					}
				}

				if (hasAnyNodeFinished)
				{
					if (selectedNode.NextPathIfCompleted != null)
					{
						SelectedPath = selectedNode.NextPathIfCompleted;
					}
					else if (selectedNode.nextNodeIfNoPath != null)
					{
						SelectedPath = MakeSingleNodePath(selectedNode.nextNodeIfNoPath);
					}
					
					isCompleted = true;
					isActivePath = false;
				}
			}
		}
	}
}
