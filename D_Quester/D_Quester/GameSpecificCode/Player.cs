using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Player
    {
        //Information not relavent to Q_Quester
        public string Name { get; set; }
        public int Position { get; set; }

        QuestJournal qj;

        //public RewardableInt gold;

        public Player(string name = "Jim", int position = 0)
        {
            Name = name;
            Position = position;
            //gold = new RewardableInt();
        }
    }
}
