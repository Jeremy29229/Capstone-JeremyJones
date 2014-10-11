using System;

namespace Game.D_Quester
{
	class Game
	{
		public AreaExplorer World { get; set; }
		public Player Player { get; set; }

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
			bool isRestarting = true;

			while (isRestarting)
			{
				bool isPlaying = true;

				while (isPlaying)
				{

					isPlaying = World.DoSomething();
				}

				switch (Menu.PromptForMenuSelection("\nWant to play again?", new string[] { "Sure", "No thanks" }))
				{
					case 2:
						isRestarting = false;
						break;
				}
			}
		}

		public void Stop()
		{
			Console.WriteLine("\nThanks for playing!");
		}
	}
}
