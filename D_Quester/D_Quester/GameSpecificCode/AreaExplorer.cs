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
                Console.WriteLine("Doesn't look there's anything in that direction. You decide to stay here.");
            }
            else
            {
                Current = Current.NearbyPlaces[d];
            }
        }

        public bool DoSomething()
        {
            bool isPlaying = true;

            //object result = Current.InteractWith();

            //if (result.GetType() == typeof(bool))
            //{
            //    isPlaying = (bool)result;
            //}
            //else if (result.GetType() == typeof(Direction))
            //{
            //    GoTo((Direction)result);
            //}

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
