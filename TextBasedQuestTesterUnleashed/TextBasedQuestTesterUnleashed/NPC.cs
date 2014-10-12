
namespace TextBasedQuestTesterUnleashed
{
	class NPC
	{
		public string Name { get; set; }
		public Conversation Convo { get; set; }

		public NPC()
		{
			Convo = new Conversation();
		}

		public void TalkTo()
		{
			Convo.Current = Convo.Starter;

			while (Convo.Current != null)
			{
				int selectionResponse = Menu.PromptForMenuSelection(Name + ": \"" + Convo.Current.DialogLine + "\"", Convo.Current.Responses.AsStrings());
				Convo.Advance(Convo.Current.Responses[selectionResponse - 1]);
			}
		}
	}
}
