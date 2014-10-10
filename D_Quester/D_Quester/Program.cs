using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Reflection.Emit;

namespace D_Quester
{
	class Program
	{
		static Game game;

		static void Main(string[] args)
		{
			CreateQuests();
			PlayGame();
		}

		public static void CreateQuests()
		{
			game = new Game() { World = new AreaExplorer(), Player = new Player() { Gold = new RewardableInt(0, "Gold"), Experience = new RewardableDouble(0.0f, "XP")} };
			game.Player.QuestJournal = new QuestJournal();

			Quest farmersAid = new Quest() { Title = "Farmer's Aid" };
			game.Player.QuestJournal.Quests.Add(farmersAid);

			Quest oddSack = new Quest(){ Title = "Odd Sack" };
			game.Player.QuestJournal.Quests.Add(oddSack);

			Quest wolfQuest2014 = new Quest() { Title = "Wolf Quest 2014" };
			game.Player.QuestJournal.Quests.Add(wolfQuest2014);

			Area farm1 = new Area("Joe's Farm", game.Player.Inventory);
			

			Area farm2 = new Area("Rick's Farm", game.Player.Inventory);
			farm2.Collectables.Add(new Item { Name = "Odd Sack", Amount = 1 });
			//game.Player.Inventory.ItemGotEvent

			oddSack.StartingNode = new Node<QuestObject>(new QuestObject(oddSack, "Odd Sack 1"));
			oddSack.CurrentNode = oddSack.StartingNode;
			oddSack.CurrentNode.info.CurrentState = QuestObjectState.InProgress;
			oddSack.CurrentNode.info.NumObjectives = 1;
			oddSack.CurrentNode.info.QuestRewarder.AddReward(10.5).To(game.Player.Experience);
			oddSack.CurrentNode.info.NamedObjectives.Add("Odd Sack");
			game.Player.Inventory.ItemGotEvent += oddSack.CurrentNode.info.NamedProgress;

			oddSack.CurrentNode.AddChild(new Node<QuestObject>(new QuestObject(oddSack, "Odd Sack 2")));
			oddSack.CurrentNode = oddSack.CurrentNode.children.Last();
			oddSack.CurrentNode.info.CurrentState = QuestObjectState.NotStarted;
			oddSack.CurrentNode.info.NumObjectives = 1;
			oddSack.CurrentNode.info.QuestRewarder.AddReward(20).To(game.Player.Gold);

			wolfQuest2014.StartingNode = new Node<QuestObject>(new QuestObject(wolfQuest2014, "Wolf Quest 2014 1"));
			wolfQuest2014.CurrentNode = wolfQuest2014.StartingNode;
			wolfQuest2014.CurrentNode.info.CurrentState = QuestObjectState.InProgress;
			wolfQuest2014.CurrentNode.info.NumObjectives = 1;
			wolfQuest2014.CurrentNode.info.QuestRewarder.AddReward(25.1).To(game.Player.Experience);
			wolfQuest2014.CurrentNode.info.NamedObjectives.Add("Wolf Head");
			game.Player.Inventory.ItemGotEvent += wolfQuest2014.CurrentNode.info.NamedProgress;

			wolfQuest2014.CurrentNode.AddChild(new Node<QuestObject>(new QuestObject(wolfQuest2014, "Wolf Quest 2014 2")));
			wolfQuest2014.CurrentNode = wolfQuest2014.CurrentNode.children.Last();
			wolfQuest2014.CurrentNode.info.CurrentState = QuestObjectState.NotStarted;
			wolfQuest2014.CurrentNode.info.NumObjectives = 1;
			

			Area forest1 = new Area("Backwoods", game.Player.Inventory);
			forest1.Monsters.Add(new Monster("Wolf"){ Drop = new Item(){ Name = "Wolf Head", Amount = 1}});
			

			Area darkCastle = new Area("Dark Castle", game.Player.Inventory, false);
			wolfQuest2014.CurrentNode.info.QuestRewarder.AddReward(true).To(darkCastle.Access);


			NPC joe = new NPC() { Name = "Joe" };
			NPC rick = new NPC() { Name = "Rick" };
			NPC oddOldMan = new NPC() { Name = "Odd Old Man" };
			NPC wolfHeadLady = new NPC() { Name = "Wolf Head Lady" };

			List<Dialog> JoesDialog = new List<Dialog>();
			Dialog current = new Dialog() { DialogLine = "Hey what can I do for ya?" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Need help with anything dudebro?"), new DialogResponse("Die in a fire!") };
			JoesDialog.Add(current);

			current = new Dialog() { DialogLine = "Yeah actually. Can you tell Rick I need my tools?" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Sure!"), new DialogResponse("No that's boring...") };
			JoesDialog.Add(current);

			current = new Dialog { DialogLine = "Wow what a jerk." };
			current.Responses = new List<DialogResponse> { new DialogResponse("<Leave Joe Be>") };
			JoesDialog.Add(current);

			current = new Dialog { DialogLine = "Thanks he should be right of my farm" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Will do.") };
			JoesDialog.Add(current);

			farmersAid.StartingNode = new Node<QuestObject>(new QuestObject(farmersAid, "Farmer's Aid 1"));
			farmersAid.CurrentNode = farmersAid.StartingNode;
			farmersAid.StartingNode.info.CurrentState = QuestObjectState.NotStarted;
			farmersAid.StartingNode.info.NumObjectives = 1;
			farmersAid.StartingNode.info.QuestRewarder.AddReward(50).To(game.Player.Gold);
			//farmersAid.StartingNode.info.QuestRewarder.AddReward(5).To(game.Player.penisPurse);

			JoesDialog[0].GetResponseByText("Need").Result = JoesDialog[1];
			JoesDialog[0].GetResponseByText("Need").Requirment = farmersAid.StartingNode.info.MakeQuestRequirement(QuestObjectState.NotStarted);
			JoesDialog[0].GetResponseByText("Die").Result = JoesDialog[2];
			JoesDialog[1].GetResponseByText("Sure").Result = JoesDialog[3];
			JoesDialog[1].GetResponseByText("Sure").ProgressionEvent += farmersAid.StartingNode.info.StartUp;
			JoesDialog[1].GetResponseByText("No").Result = JoesDialog[2];

			joe.Convo = new Conversation() { Current = JoesDialog[0], Starter = JoesDialog[0] }; ;


			List<Dialog> RicksDialog = new List<Dialog>();
			current = new Dialog() { DialogLine = "Sorry can this wait?" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Fine I didn't want to talk with you anyway..."), new DialogResponse("Just a quick thing. Joe says he needs his tools.") };
			RicksDialog.Add(current);

			current = new Dialog() { DialogLine = "Oh yeah, I'll get them back to him at some point today." };
			current.Responses = new List<DialogResponse> { new DialogResponse("Cool thanks.") };
			RicksDialog.Add(current);

			RicksDialog[0].GetResponseByText("Just").ProgressionEvent += farmersAid.StartingNode.info.Progress;
			RicksDialog[0].GetResponseByText("Just").Result = RicksDialog[1];
			RicksDialog[0].GetResponseByText("Just").Requirment = new QuestRequirment(farmersAid.CurrentNode.info, QuestObjectState.InProgress);

			rick.Convo = new Conversation() { Current = RicksDialog[0], Starter = RicksDialog[0] };

			List<Dialog> OddsDialog = new List<Dialog>();
			current = new Dialog() { DialogLine = "..." };
			current.Responses = new List<DialogResponse> { new DialogResponse("..."), new DialogResponse("Are you alright?"), new DialogResponse("<Leave, this is getting weird>") };
			OddsDialog.Add(current);

			current = new Dialog() { DialogLine = "You have the sack!!! Give it to me or die!" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Nope. Nope. Nope. <Run Away>"), new DialogResponse("Just take it. I'm not looking for any trouble.") };
			OddsDialog.Add(current);

			//RicksDialog[0].GetResponseByText("Just").ProgressionEvent += farmersAid.StartingNode.info.Progress;
			OddsDialog[0].GetResponseByText("...").Result = OddsDialog[0];
			OddsDialog[0].GetResponseByText("Are").Result = OddsDialog[1];
			OddsDialog[0].GetResponseByText("Are").Requirment = new QuestRequirment(oddSack.CurrentNode.info, QuestObjectState.InProgress);
			OddsDialog[1].GetResponseByText("Just").ProgressionEvent += oddSack.CurrentNode.info.Progress;

			oddOldMan.Convo = new Conversation() { Current = OddsDialog[0], Starter = OddsDialog[0] };

			List<Dialog> WL = new List<Dialog>();
			current = new Dialog() { DialogLine = "Hey you there! Yes you! Got any wolf heads?" };
			current.Responses = new List<DialogResponse>{ new DialogResponse("What? No, why would I just carry around wolf heads with me?"), new DialogResponse("Of course! Who do you think I am?")};
			WL.Add(current);

			current = new Dialog() { DialogLine = "If you'd seen what I saw you'd carry them around every day..." };
			current.Responses = new List<DialogResponse> { new DialogResponse("I'm gonna stop you right there. I've reached my daily crazy allowance.") };
			WL.Add(current);

			current = new Dialog() { DialogLine = "You're a saint! Can I take them off your hands?" };
			current.Responses = new List<DialogResponse> { new DialogResponse("Yeah, it's not like I need them for anything"), new DialogResponse("No, I'm concerned about what you might do with them.") };
			WL.Add(current);

			current = new Dialog() { DialogLine = "Curse you! You're probably a vile scorpio." };
			current.Responses = new List<DialogResponse> { new DialogResponse("You have a great day too..") };
			WL.Add(current);

			WL[0].GetResponseByText("What").Result = WL[1];
			WL[0].GetResponseByText("Of course").Result = WL[2];
			WL[2].GetResponseByText("Yeah").ProgressionEvent += wolfQuest2014.CurrentNode.info.Progress;
			WL[0].GetResponseByText("Of course").Requirment = new QuestRequirment(wolfQuest2014.CurrentNode.info, QuestObjectState.InProgress);
			WL[2].GetResponseByText("No").Result = WL[3];

			wolfHeadLady.Convo = new Conversation() { Current = WL[0], Starter = WL[0] };

			farm1.People.Add(joe);
			farm1.People.Add(wolfHeadLady);
			farm2.People.Add(rick);
			farm2.People.Add(oddOldMan);

			farm1.AddNearbyPlace(Direction.right, farm2);
			farm1.AddNearbyPlace(Direction.left, forest1);
			forest1.AddNearbyPlace(Direction.left, darkCastle);

			oddSack.CurrentNode = oddSack.StartingNode;
			farmersAid.CurrentNode = farmersAid.StartingNode;
			wolfQuest2014.CurrentNode = wolfQuest2014.StartingNode;

			game.World.Current = farm1;
		}

		public static void PlayGame()
		{
			game.Start();
		}
	}
}
