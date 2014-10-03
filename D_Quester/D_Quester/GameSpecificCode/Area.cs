using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Area
    {
        public Dictionary<Direction, Area> NearbyPlaces { get; set; }
        public List<NPC> People { get; set; }
        public string Name { get; set; }

        public Area(string name = "unknown")
        {
            Name = name;
            NearbyPlaces = new Dictionary<Direction, Area>();
            People = new List<NPC>();

            foreach (Direction d in typeof(Direction).GetEnumValues())
            {
                NearbyPlaces.Add(d, this); 
            }
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
            string message = "\nYou are currentlu at " + Name + ".\nWhat would you like to do?";
            string[] options = {"Talk with someone nearby.", "Find an inn and take a nap."};
            bool isPlaying = true;

            switch(Menu.PromptForMenuSelection(message, options))
            {
                case 1:
                    TalkWith();
                    break;
                case 2:
                    isPlaying = false;
                    break;
            }

            return isPlaying;
        }

        public void TalkWith()
        {
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
            }
        }
    }
}
