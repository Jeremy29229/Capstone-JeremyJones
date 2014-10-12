using System;
using System.Collections.Generic;
using D_Quester;

namespace TextBasedQuestTesterUnleashed
{
	class Area
	{
		public Dictionary<Direction, Area> NearbyPlaces { get; set; }
		public List<NPC> People { get; set; }
		public List<Monster> Monsters { get; set; }
		public List<Item> Collectables { get; set; }
		public string Name { get; set; }
		private Inventory _PlayerInventory { get; set; }
		public RewardableBool Access { get; set; }
		Random r;

		public Area(string name = "unknown", Inventory playerInventory = null, bool access = true)
		{
			Name = name;
			NearbyPlaces = new Dictionary<Direction, Area>();
			People = new List<NPC>();
			Monsters = new List<Monster>();
			Collectables = new List<Item>();
			_PlayerInventory = playerInventory;
			Access = new RewardableBool(access, "You now have access to " + Name, "You have list access to " + Name);

#if FRAMEWORK_V3_5
			foreach (Direction d in Enum.GetValues(typeof(Direction)))
			{
				NearbyPlaces.Add(d, this);
			}
#elif FRAMEWORK_V4_5
			foreach (Direction d in typeof(Direction).GetEnumValues())
			{
				NearbyPlaces.Add(d, this);
			}
#endif

			r = new Random();
		}

		/// <summary>
		/// Updates this area's nearby places to the new area passed and gives the newArea this locations in it's nearby places in the opposite directions
		/// </summary>
		/// <param name="newLocationDirection">Direction the new area will be in reference to the current area</param>
		/// <param name="newArea">Location to added to this area's nearby places</param>
		public void AddNearbyPlace(Direction newLocationDirection, Area newArea)
		{
			NearbyPlaces[newLocationDirection] = newArea;
			newArea.NearbyPlaces[newLocationDirection.Opposite()] = this;
		}

		public bool InteractWith()
		{
			string message = "\nYou are currently at " + Name + ".\nWhat would you like to do?";
			string[] options = { "Talk with someone nearby.", "Look for treasure.", "Search for things to slay.", "Find an inn and take a nap. (Quit to Menu)", "Leave Area"};
			bool isPlaying = true;

			switch (Menu.PromptForMenuSelection(message, options))
			{
				case 1:
					isPlaying = TalkWith();
					break;
				case 2:
					isPlaying = FindTreasure();
					break;
				case 3:
					isPlaying = HuntMonsters();
					break;
				case 4:
					isPlaying = false;
					break;
				case 5:
					break;
			}

			return isPlaying;
		}

		public bool TalkWith()
		{
			bool isPlaying = true;

			if (People.Count < 1)
			{
				Console.WriteLine("After walking around for a while you come to the conclusion that there isn't anyone around.");
			}
			else
			{
				string message = "You come across someone. Do you want to talk with them?";
				List<string> options = new List<string>();

				foreach (NPC n in People)
				{
					options.Add("Approach " + n.Name);
				}

				options.Add("Continue walking around like a creeper.");

				int choice = Menu.PromptForMenuSelection(message, options);

				if (choice != options.Count)
				{
					People[choice - 1].TalkTo();
				}
				else
				{
					isPlaying = InteractWith();
				}
			}

			return isPlaying;
		}

		public bool FindTreasure()
		{
			bool isPlaying = true;

			if (Collectables.Count < 1)
			{
				Console.WriteLine("You look everywhere but can't find anything of use or interest.");
			}
			else
			{
				string message = "You find some things that might be useful. Do you want to take anything?";
				List<string> options = new List<string>();

				foreach(Item i in Collectables)
				{
					options.Add("Take " + i.Name);
				}

				options.Add("Why would I want to take any of this?");

				int choice = Menu.PromptForMenuSelection(message, options);

				if (choice != options.Count)
				{
					_PlayerInventory.addItem(Collectables[choice - 1]);
					Collectables.RemoveAt(choice - 1);

				}
				isPlaying = InteractWith();
			}

			return isPlaying;
		}

		public bool HuntMonsters()
		{
			bool isPlaying = true;

			if (Monsters.Count < 1)
			{
				Console.WriteLine("You doesn't see any monsters in the immediate area.");
			}
			else
			{
				bool monsterFound = false;
				bool areMonstersRemaing = true;
				int current = 0;
				Monster selected = null;

				while (!monsterFound && areMonstersRemaing)
				{
					if (Monsters[current].EncounterRate > r.NextDouble())
					{
						selected = Monsters[current];
						monsterFound = true;
					}
					else if(++current >= Monsters.Count)
					{
						areMonstersRemaing = false;
					}
				}

				if (selected == null)
				{
					Console.WriteLine("You don't find any monsters but you have a feeling there are some lurking.");
				}
				else
				{
					string message = "You find a " + selected.Name + "!";

					int choice = Menu.PromptForMenuSelection(message, new string[]{"Kill it!", "Run away!"});

					if (choice == 1)
					{
						Console.WriteLine("You kill it dead. It was an epic off-screen battle.");
						selected.Kill(_PlayerInventory);
					}
				}
				isPlaying = InteractWith();
			}

			return isPlaying;
		}
	}
}
