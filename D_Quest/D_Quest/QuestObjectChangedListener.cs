using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestObjectChangedListener
    {
        //private QuestObject q;

        //public QuestObjectChangedListener(QuestObject q)
        //{
        //    q.changed += Changed;
        //}

        public void Attach(QuestObject q)
        {
            q.changed += Changed;
        }

        public void Detach(QuestObject q)
        {
            q.changed -= Changed;
        }

        public void Changed(QuestObject sender, QuestObjectChangedEventArgs e)
        {
            Console.WriteLine(sender.Name + "'s state changed to: " + e.Current + " from: " + e.Previous);
        }
    }
}
