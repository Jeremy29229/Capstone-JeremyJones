using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{

	/// <summary>
	/// 
	/// </summary>
	class Quest
	{
		public string Title { get; set; }
		public Node<QuestObject> StartingNode { get; set; }
		public Node<QuestObject> CurrentNode { get; set; }

		public Quest()
		{
			Title = "None";
		}

		public void Advance()
		{
			if (CurrentNode.children.FirstOrDefault() != null)
			{
				CurrentNode = CurrentNode.children.First();
				CurrentNode.info.StartUp();
			}
			else
			{
				Console.WriteLine("You just finished the Quest " + Title + "!");
			}
		}
	}
}
