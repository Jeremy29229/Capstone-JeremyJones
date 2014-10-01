using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestObjectChangedEventArgs : EventArgs
    {
        public QuestObjectChangedEventArgs(QuestObjectState previous = QuestObjectState.Uninitialized, QuestObjectState current = QuestObjectState.Uninitialized)
        {
            Previous = previous;
            Current = current;
        }

        public QuestObjectState Previous { get; set; }
        public QuestObjectState Current { get; set; }
    }
}
