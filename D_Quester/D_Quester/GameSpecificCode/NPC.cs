using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
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
				//Convo.Current = Convo.Current.Responses[selectionResponse - 1].Result;
				Convo.Advance(Convo.Current.Responses[selectionResponse - 1]);
			}
		}


	}
}
