using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace D_Quester
{
    class Program
    {
        static Game game;

        static void Main(string[] args)
        {
            CreateQuests();
            PlayGame();
        }

        public static void CreateQuests()
        {
            game = new Game() { World = new AreaExplorer() };

            Area farm1 = new Area("Joe's Farm");

            Area farm2 = new Area("Rick's Farm");

            NPC joe = new NPC() { Name = "Joe"};
            NPC rick = new NPC() { Name = "Rick" };


            //start.nearbyPlaces[Direction.right] = 
            //game.World.Current;

            //Node<QuestObject> 
            List<Dialog> JoesDialog = new List<Dialog>();
            Dialog current = new Dialog() { DialogLine = "Hey what can I do for ya?" };
            current.Responses = new List<DialogResponse> { new DialogResponse("Need help with anything dudebro?"), new DialogResponse("Die in a fire!") };
            JoesDialog.Add(current);

            current = new Dialog() { DialogLine = "Yeah actually. Can you tell Rick I need my tools?" };
            current.Responses = new List<DialogResponse> { new DialogResponse("Sure!"), new DialogResponse("No that's boring...") };
            JoesDialog.Add(current);

            current = new Dialog { DialogLine = "Wow what a jerk." };
            current.Responses = new List<DialogResponse> { new DialogResponse("<Leave Joe Be>") };
            JoesDialog.Add(current);

            current = new Dialog { DialogLine = "Thanks he should be left of my farm" };
            current.Responses = new List<DialogResponse> { new DialogResponse("Will do.") };
            JoesDialog.Add(current);

            JoesDialog[0].GetResponseByText("Need").Result = JoesDialog[1];
            JoesDialog[0].GetResponseByText("Die").Result = JoesDialog[2];
            JoesDialog[1].GetResponseByText("Sure").Result = JoesDialog[3];
            JoesDialog[1].GetResponseByText("No").Result = JoesDialog[2];

            joe.Convo = new Conversation() { Current = JoesDialog[0], Starter = JoesDialog[0] };


            List<Dialog> RicksDialog = new List<Dialog>();
            current = new Dialog() { DialogLine = "Sorry can this wait?" };
            current.Responses = new List<DialogResponse> { new DialogResponse("Fine I didn't want to talk with you anyway..."), new DialogResponse("Just a quick thing. Joe says he needs his toola.") };
            RicksDialog.Add(current);

            current = new Dialog() { DialogLine = "Oh yeah, I'll get them back to him at some point today." };
            current.Responses = new List<DialogResponse> { new DialogResponse("Cool thanks.") };
            RicksDialog.Add(current);

            RicksDialog[0].GetResponseByText("Just").Result = RicksDialog[1];

            rick.Convo = new Conversation() { Current = RicksDialog[0], Starter = RicksDialog[0] };

            farm1.People.Add(joe);
            farm2.People.Add(rick);

            farm1.AddNearbyPlace(Direction.right, farm2);

            game.World.Current = farm1;
        }

        public static void PlayGame()
        {
            game.Start();
        }
    }
}
