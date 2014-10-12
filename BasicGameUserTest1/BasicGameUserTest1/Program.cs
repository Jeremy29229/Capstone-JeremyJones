using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_Quester;

namespace BasicGameUserTest1
{
	class Program
	{
		static void Main(string[] args)
		{
			SetupGame();
			PlayGame();

			#region Post-Feedback Information
			//SetupGameSolution();
			//PlayGameSolution();
			#endregion
		}

		static NPC npc;
		static Player player;

		//Edit this
		public static void SetupGame()
		{

		}

		//Don't edit this
		public static void PlayGame()
		{
			npc.TalkToPlayer();
		}

		#region Post-Feedback Information
		static Solution.NPC joe;
		public static void SetupGameSolution()
		{
			Solution.Player player = new Solution.Player() { Gold = new RewardableInt(0, "Gold"), Name = "Dudebro" };

			joe = new Solution.NPC { Name = "Joe", questInfo = new QuestObject("Talk To Joe", QuestObjectState.InProgress) };
			joe.questInfo.QuestRewarder.AddReward(5).To(player.Gold);
			joe.questInfo.NumObjectives = 1;
		}

		public static void PlayGameSolution()
		{
			joe.TalkToPlayer();
		}
		#endregion
	}
}
