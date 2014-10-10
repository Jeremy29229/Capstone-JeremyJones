using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
    class QuestObjectChangedListener
    {
        public List<QuestObject> Clients { get; private set; }

        public QuestObjectChangedListener()
        {
            Clients = new List<QuestObject>();
        }

        public void Attach(QuestObject q)
        {
            q.changed += Changed;
            Clients.Add(q);
        }

        public void AttachAll(IEnumerable<QuestObject> clients)
        {
            foreach (QuestObject q in clients)
            {
                q.changed += Changed;
                Clients.Add(q);
            }
        }

        public void Detach(QuestObject q)
        {
            q.changed -= Changed;
            Clients.Remove(q);
        }

        public void DetachAll()
        {
            foreach (QuestObject q in Clients)
            {
                q.changed -= Changed;
            }
            Clients.Clear();
        }

        public void Changed(QuestObject sender, QuestObjectChangedEventArgs e)
        {
            Console.WriteLine(sender.Name + "'s state changed to: " + e.Current + " from: " + e.Previous);
        }
    }
}
