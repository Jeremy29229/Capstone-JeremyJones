using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_Quester;

namespace BasicGameUserTest1.Solution
{
	class NPC
	{
		public string Name { get; set; }
		public QuestObject questInfo { get; set; }

		public void TalkToPlayer()
		{
			questInfo.Progress();
		}
	}
}
