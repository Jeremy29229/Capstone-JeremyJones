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
        static void Main(string[] args)
        {
            Player p = new Player();
            Console.WriteLine(p.gold.Value);

            TestQuestRewarder quest1 = new TestQuestRewarder(5, "Quest1");
            TestQuestRewarder quest2 = new TestQuestRewarder(11, "Quest2");

            p.gold.AddRewarder(quest1);
            p.gold.AddRewarder(quest2);

            Console.WriteLine(p.gold.Value);

            quest1.Reward();

            Console.WriteLine(p.gold.Value);

        }
    }
}
