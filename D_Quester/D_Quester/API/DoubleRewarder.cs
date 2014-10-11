

namespace D_Quester
{
	/// <summary>
	/// Handles specified rewards for all subscribed RewardableDoubles.
	/// </summary>
	class DoubleRewarder
	{
		/// <summary>
		/// Delegate for RewardableDouble reward handling. 
		/// </summary>
		/// <param name="rewardAmount">The value added to the RewardableDouble.</param>
		public delegate void RewardDoubleDel(double rewardAmount);
		/// <summary>
		/// Event that RewardableDoubles must subscribe to.
		/// </summary>
		public event RewardDoubleDel RewardDoubleEvent;
		/// <summary>
		/// Delegate for RewardableDoubles reward handling.
		/// </summary>
		public double RewardAmount { get; set; }

		/// <summary>
		/// Adds amount specified to RewardableDoubles when RewardDoubleEvent is triggered.
		/// </summary>
		/// <param name="rewardAmount">Amount added to subscribed RewardableDoubles.</param>
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

		/// <summary>
		/// Triggers the rewarder add reward to subscribed RewardableDoubles.
		/// </summary>
		public void Reward()
		{
			OnStateChange();
		}
	}
}
