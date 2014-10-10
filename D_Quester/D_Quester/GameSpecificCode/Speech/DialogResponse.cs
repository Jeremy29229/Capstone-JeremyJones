using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	class DialogResponse : IQuestProgresser
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

		public event ProgressionHandler ProgressionEvent;

		public void OnProgressionEvent()
		{
			if (ProgressionEvent != null)
			{
				ProgressionEvent();
			}
		}

		public void Pop()
		{
			if (ProgressionEvent != null)
			{
				ProgressionEvent();
			}
		}
	}
}
