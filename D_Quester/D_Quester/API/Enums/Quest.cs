using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{

    /// <summary>
    /// 
    /// </summary>
    class Quest
    {
        public Node<QuestObject> StartingNode { get; set; }
        public Node<QuestObject> CurrentNode { get; set; }
    }
}
