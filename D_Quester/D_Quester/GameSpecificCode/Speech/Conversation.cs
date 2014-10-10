using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	class Conversation
	{
		public Dialog Starter { get; set; }
		Dialog _current;
		public Dialog Current
		{ 
			get
			{
				return _current;
			}
			set
			{
				_current = value;
			}
		}

		public void Advance(DialogResponse selected)
		{
			if(!Current.Responses.Contains(selected))
			{
				throw new ArgumentException("DialogResponse is not a valid response to the current Dialog");
			}
			selected.Pop();
			Current = selected.Result;
		}
	}
}
