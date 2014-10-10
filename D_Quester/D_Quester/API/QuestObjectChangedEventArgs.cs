using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
