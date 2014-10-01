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
    class QuestNode
    {
        /// <summary>
        /// 
        /// </summary>
        public List<QuestNode> parents{ get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<QuestNode> children{ get; private set; }
     
        /// <summary>
        /// 
        /// </summary>
        public QuestNode()
        {
            parents = new List<QuestNode>();
            children = new List<QuestNode>();
        }

        /// <summary>
        /// Adds child to this' children list and add this to child's parents list.
        /// </summary>
        /// <param name="child">Object being added to children's list</param>
        public void AddChild(QuestNode child)
        {
            children.Add(child);
            child.AddParent(this);
        }

        /// <summary>
        /// Adds parent to this' parents list and add this to parent's children list.
        /// </summary>
        /// <param name="parent">Object being added to parent's list</param>
        public void AddParent(QuestNode parent)
        {
            parents.Add(parent);
            parent.AddChild(this);
        }
    }
}
