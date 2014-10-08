using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class AreaExplorer
	{
		public Area Current { get; set; }

		public void GoTo(Direction d)
		{
			if (Current.NearbyPlaces[d] == Current)
			{
				Console.WriteLine("\nDoesn't look there's anything in that direction. You decide to stay here.");
			}
			else
			{
				if (Current.NearbyPlaces[d].Access.State)
				{
					Current = Current.NearbyPlaces[d];
				}
				else
				{
					Console.WriteLine("\nDoesn't look there's anything in that direction. You decide to stay here.");
				}
			}
		}

		public bool DoSomething()
		{
			bool isPlaying = true;

			string message = "\nYou are currently near " + Current.Name + ". What would you like to do?";
			string[] options = { "Go to " + Current.Name, "Explore somewhere else." };

			switch(Menu.PromptForMenuSelection(message, options))
			{
				case 1:
					isPlaying = Current.InteractWith();
					break;
				case 2:
					Travel();
					break;
			}

			return isPlaying;
		}

		public void Travel()
		{
			string message = "\nWhat direction do you want to go in?";
			string[] directions = Enum.GetNames(typeof(Direction));

			int choice = Menu.PromptForMenuSelection(message, directions);

			GoTo((Direction)(Enum.Parse(typeof(Direction), directions[choice - 1])));
		}
	}
}
