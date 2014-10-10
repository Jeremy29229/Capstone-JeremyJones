using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	/// <summary>
	/// Handles specified rewards for all subscribed RewardableInts.
	/// </summary>
	class IntRewarder
	{
		/// <summary>
		/// Delegate for RewardableInt reward handling. 
		/// </summary>
		/// <param name="rewardAmount">The value added to the RewardableInt.</param>
		public delegate void RewardIntDel(int rewardAmount);
		/// <summary>
		/// Event that RewardableInts must subscribe to.
		/// </summary>
		public event RewardIntDel RewardIntEvent;
		/// <summary>
		/// Delegate for RewardableInts reward handling.
		/// </summary>
		public int RewardAmount { get; set; }

		/// <summary>
		/// Adds amount specified to RewardableInts when RewardIntEvent is triggered.
		/// </summary>
		/// <param name="rewardAmount">Amount added to subscribed RewardableInts.</param>
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

		/// <summary>
		/// Triggers the rewarder add reward to subscribed RewardableInts.
		/// </summary>
		public void Reward()
		{
			OnStateChange();
		}
	}
}
