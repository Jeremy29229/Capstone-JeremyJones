using D_Quester;

namespace TextBasedQuestTesterUnleashed
{
	class DialogResponse : IQuestProgresser
	{
		public string Text { get; set; }
		public Dialog Result { get; set; }
		public QuestRequirement Requirement { get; set; }

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
