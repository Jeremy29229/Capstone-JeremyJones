using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    /// <summary>
    /// Holds all quests in a list. Represents the current state of all quests and internal objects. For example, a Player class could contain an instance of QuestJournal to handle all quests created.
    /// </summary>
    class QuestJournal
    {
        public List<Quest> Quests { get; set; }

        public QuestJournal()
        {
            Quests = new List<Quest>();
        }
    }
}
