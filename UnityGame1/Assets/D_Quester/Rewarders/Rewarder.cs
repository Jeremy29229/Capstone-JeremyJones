using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Inherit from this to implement a specific class for the generic. 
	/// Handles specified rewards for all subscribed Rewardables.
	/// </summary>
	public class Rewarder<T> : Rewarder
	{
		/// <summary>
		/// Delegate for remotely modifying Rewardable class.
		/// </summary>
		/// <param name="rewardInstance">The value added to the RewardableInt.</param>
		[HideInInspector]
		public delegate bool RewardDel(T rewardInstance);

		/// <summary>
		/// Name that represents this rewarder.
		/// </summary>
		[Tooltip("Name that represents this rewarder.")]
		public string Name;

		/// <summary>
		/// Event that Rewardables must subscribe to.
		/// </summary>
		[HideInInspector]
		public event RewardDel RewardEvent;

		/// <summary>
		/// Instance that will be given to subscribed Rewardables.
		/// </summary>
		[Tooltip("Instance that will be given to subscribed Rewardables.")]
		public T RewardInstance;

		protected virtual void OnStateChange()
		{
			if (RewardEvent != null)
			{
				RewardEvent(RewardInstance);
			}
		}

		/// <summary>
		/// Triggers class to reward all rewardables with approached rewards.
		/// </summary>
		public override void Reward()
		{
			OnStateChange();
		}
	}
}
