using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds nodes for a dynamic player choice. The node first marked as completed will have its resulting path selected.
	/// </summary>
	[System.Serializable]
	public class QuestPath : MonoBehaviour
	{
		/// <summary>
		/// Name of the QuestPath. Useful for Identifying the correct QuestPath in a GameObject.
		/// </summary>
		[Tooltip("Name of the QuestPath. Useful for Identifying the correct QuestPath in a GameObject.")]
		public string QuestPathName = "";

		/// <summary>
		/// All node options for this QuestPath.
		/// </summary>
		[Tooltip("All node options for this QuestPath.")]
		public QuestNode[] QuestNodes;

		/// <summary>
		/// Becomes the next path a quest will take when a node is completed.
		/// </summary>
		[Tooltip("Becomes the next path a quest will take when a node is completed.")]
		public QuestPath SelectedPath = null;

		/// <summary>
		/// Indicates if this path is finished or not.
		/// </summary>
		[Tooltip("Indicates if this path is finished or not.")]
		public bool isCompleted = false;

		private bool hasAnyNodeFinished = false;
		private QuestNode selectedNode = null;
		private bool isActivePath = true;

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

					isCompleted = true;
					isActivePath = false;
				}
			}
		}
	}
}
