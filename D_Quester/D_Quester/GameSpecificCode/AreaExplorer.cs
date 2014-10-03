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

        public void DoSomething()
        {
            //Menu.PromptForMenuSelection("What do you want to do?");
        }
    }
}
