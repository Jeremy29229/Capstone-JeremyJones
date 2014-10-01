using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    /// <summary>
    /// Adds quest object functionality to existing object
    /// </summary>
    interface IInteractable
    {
        List<Node<QuestObject>> Nodes { get; set; }
    }
}
