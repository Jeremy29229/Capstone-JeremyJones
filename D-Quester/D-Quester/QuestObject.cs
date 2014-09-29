using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestObject
    {
        public string Name { get; set; }
        public object Owner { get; private set; }
        public QuestObjectStates State { get; set; }
        public bool IsHiddenFromPlayer { get; set; }
        public QuestNode Node { get; set; }

        public QuestObject(Object owner = null, string name = "", bool isHidden = false)
        {
            Owner = owner;
            Name = name;
            State = QuestObjectStates.Uninitialized;
            Node = new QuestNode();
            IsHiddenFromPlayer = isHidden;
        }
    }
}
