using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class IntRewarder
	{
		public delegate void RewardIntDel(int rewardAmount);
		public event RewardIntDel RewardIntEvent;
		public int RewardAmount { get; set; }

		public IntRewarder(int rewardAmount)
		{
			RewardAmount = rewardAmount;
		}

		protected virtual void OnStateChange()
		{
			if (RewardIntEvent != null)
			{
				RewardIntEvent(RewardAmount);
			}
		}

		public void Reward()
		{
			OnStateChange();
		}
	}
}
