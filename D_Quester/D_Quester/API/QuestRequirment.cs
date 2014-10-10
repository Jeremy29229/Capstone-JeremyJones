using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	class QuestRequirment
	{
		public QuestObject Requirement { get; set; }
		public QuestObjectState RequiredStates { get; set; }

		public QuestRequirment(QuestObject requirement, QuestObjectState requiredStates)
		{
			RequiredStates = requiredStates;
			Requirement = requirement;
		}

		public bool IsMet()
		{
			if (Requirement == null)
			{
				throw new InvalidOperationException("Contains null object. Make sure QuestRequirement is correctly populated.");
			}

			return (RequiredStates == 0 || (RequiredStates & Requirement.CurrentState) != 0);
		}
	}
}
