using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class RewardableBool
	{
		public string TrueMessage { get; set; }
		public string FalseMessage { get; set; }
		public bool State { get; set; }

		public RewardableBool(bool startingState = false, string trueMessage = "You unlocked something.", string falseMessage = "You lost the ability to do something.")
		{
			State = startingState;
			TrueMessage = trueMessage;
			FalseMessage = falseMessage;
		}

		public void ChangeState(bool newState)
		{
			State = newState;
			string message = (State) ? TrueMessage : FalseMessage;
			Console.WriteLine(message);
		}

		public void AddRewarder(BoolRewarder t)
		{
			t.RewardBoolEvent += ChangeState;
		}

		public void RemoveRewarder(BoolRewarder t)
		{
			t.RewardBoolEvent -= ChangeState;
		}
	}
}
