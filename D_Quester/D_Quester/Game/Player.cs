using D_Quester;

namespace Game.D_Quester
{
	class Player
	{
		public string Name { get; set; }
		public Inventory Inventory { get; set; }
		public QuestJournal QuestJournal { get; set; }
		public RewardableInt Gold { get; set; }
		public RewardableDouble Experience { get; set; }
		public RewardableBool DarkCastleAccess { get; set; }

		public Player(string name = "???")
		{
			Name = name;
			Inventory = new Inventory();
			QuestJournal = new QuestJournal();
			Gold = new RewardableInt();
			Experience = new RewardableDouble();
			DarkCastleAccess = new RewardableBool();
		}
	}
}
