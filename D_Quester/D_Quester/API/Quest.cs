using System;
using System.Linq;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class Quest
	{
		/// <summary>
		/// 
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Node<QuestObject> StartingNode { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Node<QuestObject> CurrentNode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Quest()
		{
			Title = "None";
		}

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
