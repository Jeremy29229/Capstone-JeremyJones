using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_Quester
{
	/// <summary>
	/// Handles specified rewards for all subscribed Rewardables.
	/// </summary>
	public class Rewarder<T> : MonoBehaviour
	{
		/// <summary>
		/// Delegate for remotely modifying Rewardable class.
		/// </summary>
		/// <param name="rewardInstance">The value added to the RewardableInt.</param>
		public delegate void RewardDel(T rewardInstance);

		/// <summary>
		/// Name that represents this rewarder.
		/// </summary>
		public string Name;

		/// <summary>
		/// Event that Rewardables must subscribe to.
		/// </summary>
		public event RewardDel RewardEvent;

		/// <summary>
		/// Instance that will be given to subscribed rewardables.
		/// </summary>
		public T RewardInstance;

		protected virtual void OnStateChange()
		{
			if (RewardEvent != null)
			{
				RewardEvent(RewardInstance);
			}
		}

		/// <summary>
		/// Triggers class to add RewardAmount to subscribed RewardableInts.
		/// </summary>
		public void Reward()
		{
			OnStateChange();
		}
	}
}
