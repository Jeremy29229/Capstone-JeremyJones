using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class BoolRewarder
	{
		/// <summary>
		/// Delegate for reward handling.
		/// </summary>
		/// <param name="state">State bool will be changed to on event</param>
		public delegate void RewardBoolDel(bool state);
		/// <summary>
		/// Event that rewarders must subscribe to.
		/// </summary>
		public event RewardBoolDel RewardBoolEvent;
		/// <summary>
		/// 
		/// </summary>
		public bool RewardState { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rewardState"></param>
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

		/// <summary>
		/// 
		/// </summary>
		public void Reward()
		{
			OnStateChange();
		}
	}
}
