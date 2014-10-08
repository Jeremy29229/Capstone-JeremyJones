using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class BoolRewarder
	{
		public delegate void RewardBoolDel(bool state);
		public event RewardBoolDel RewardBoolEvent;
		public bool RewardState { get; set; }

		public BoolRewarder(bool rewardState)
		{
			RewardState = rewardState;
		}

		protected virtual void OnStateChange()
		{
			if (RewardBoolEvent != null)
			{
				RewardBoolEvent(RewardState);
			}
		}

		public void Reward()
		{
			OnStateChange();
		}
	}
}
