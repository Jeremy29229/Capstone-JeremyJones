using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class DoubleRewarder
	{
		public delegate void RewardDoubleDel(double rewardAmount);
		public event RewardDoubleDel RewardDoubleEvent;
		public double RewardAmount { get; set; }

		public DoubleRewarder(double rewardAmount)
		{
			RewardAmount = rewardAmount;
		}

		protected virtual void OnStateChange()
		{
			if (RewardDoubleEvent != null)
			{
				RewardDoubleEvent(RewardAmount);
			}
		}

		public void Reward()
		{
			OnStateChange();
		}
	}
}
