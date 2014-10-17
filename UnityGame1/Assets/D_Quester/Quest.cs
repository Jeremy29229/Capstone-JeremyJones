using System;
using System.Linq;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class Quest : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		public string Title;
		/// <summary>
		/// 
		/// </summary>
		public Node<QuestObject> StartingNode;
		/// <summary>
		/// 
		/// </summary>
		public Node<QuestObject> CurrentNode;

		/// <summary>
		/// 
		/// </summary>
		public void Advance()
		{
			if (CurrentNode.children.FirstOrDefault() != null)
			{
				CurrentNode = CurrentNode.children.First();
				CurrentNode.info.StartUp();
			}
			else
			{
				Console.WriteLine("You just finished the Quest: " + Title + "!");
			}
		}
	}
}
