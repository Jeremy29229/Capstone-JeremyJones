using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class DialogResponse : IQuestStarter
	{
		public string Text { get; set; }
		public Dialog Result { get; set; }
		public QuestRequirment Requirment { get; set; }

		public DialogResponse(string text)
		{
			Text = text;
			Result = null;
		}

		public override string ToString()
		{
			return Text;
		}

		public event StartHandler StarterEvent;

		void IQuestStarter.OnStartEvent()
		{
			if (StarterEvent != null)
			{
				StarterEvent();
			}
		}

		public void Pop()
		{
			if (StarterEvent != null)
			{
				StarterEvent();
			}
		}
	}
}
