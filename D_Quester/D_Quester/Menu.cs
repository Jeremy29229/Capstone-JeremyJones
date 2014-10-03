using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
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
    }
}
