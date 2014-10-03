using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestRequirment
    {
        public QuestObject Requirement { get; set; }
        public List<QuestObjectState> RequiredStates { get; set; }

        public QuestRequirment()
        {
            Requirement = null;
        }

        public bool IsMet()
        {
            if (Requirement == null || RequiredStates == null)
            {
                throw new InvalidOperationException("Contains null object. Make sure QuestRequirement is correct populated");
            }

            return (RequiredStates.Contains(Requirement.CurrentState));
        }
    }
}
