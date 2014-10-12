

namespace D_Quester
{
	/// <summary>
	/// Handles specified rewards for all subscribed RewardableBools.
	/// </summary>
	public class BoolRewarder
	{
		/// <summary>
		/// Delegate for RewardableBool reward handling.
		/// </summary>
		/// <param name="state">State RewardableBools will be changed to on event</param>
		public delegate void RewardBoolDel(bool state);
		/// <summary>
		/// Event that RewardableBools must subscribe to.
		/// </summary>
		public event RewardBoolDel RewardBoolEvent;
		/// <summary>
		/// Delegate for RewardableBools reward handling.
		/// </summary>
		public bool RewardState { get; set; }
		/// <summary>
		/// Sets the state given to the RewardableBools when RewardBoolEvent is triggered.
		/// </summary>
		/// <param name="rewardState">State subscribed RewardableBools will be changed to.</param>
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
		/// Triggers the rewarder to change all subscribed RewardableBools.
		/// </summary>
		public void Reward()
		{
			OnStateChange();
		}
	}
}
