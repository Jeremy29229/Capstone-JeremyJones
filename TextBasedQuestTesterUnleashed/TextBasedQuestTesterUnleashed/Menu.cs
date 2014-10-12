using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextBasedQuestTesterUnleashed
{
	static class Menu
	{
		public static int PromptForMenuSelection(string message, IEnumerable<string> options)
		{
			IEnumerable<string> sOptions = (IEnumerable<string>)options;

			bool validInput = false;
			int selection = -1;

			while (!validInput)
			{
				Console.WriteLine("\n" + message);
				for (int index = 0; index < sOptions.Count(); index++)
				{
					Console.WriteLine(index + 1 + ". " + sOptions.ElementAt(index));
				}

				String input = Console.ReadLine();
				if (int.TryParse(input, out selection))
				{
					validInput = (selection >= 0 && selection <= sOptions.Count());

					if (!validInput)
					{
						printOutOfRangeError();
					}
				}
				else if (FindChoiceByString(input, options, out selection))
				{
					validInput = (selection >= 0 && selection <= sOptions.Count());

					if (!validInput)
					{
						printOutOfRangeError();
					}
				}
				else
				{
					printInvalidError();
				}
			}

			return selection;
		}

		private static void printInvalidError()
		{
			Console.WriteLine("Your input is invalid.\n");
		}

		private static void printOutOfRangeError()
		{
			Console.WriteLine("Your input is not within the acceptable range.\n");
		}

		private static bool FindChoiceByString(string input, IEnumerable<string> options, out int selection)
		{
			bool isSuccessful = false;
			selection = -1;

			for (int i = 0; i < options.Count(); i++)
			{
				if (options.ElementAt(i).ToLowerInvariant().Contains(input.ToLowerInvariant()))
				{
					isSuccessful = true;
					selection = i + 1;
					break;
				}
			}

			return isSuccessful;
		}
	}
}
