using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Game
    {
        public AreaExplorer World { get; set; }

        public void Start()
        {
            Console.WriteLine("Welcome to Text-based Quest Tester Unleashed");
            string message = "Would you like to start?";
            switch (Menu.PromptForMenuSelection(message, new string[]{"Yeah!", "Quit"}))
            {
                case 1:
                    Loop();
                    break;
                    
                case 2:
                    Stop();
                    break;
            }
        }

        public void Loop()
        {
            bool isPlaying = true;

            while(isPlaying)
            {

            }
        }

        public void Stop()
        {
            Console.WriteLine("Thanks for playing!");
        }
    }
}
