using System;
using System.Linq;


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
				Console.WriteLine("You just finished the Quest: " + Title + "!");
			}
		}
	}
}
