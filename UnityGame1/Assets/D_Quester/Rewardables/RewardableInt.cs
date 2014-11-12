using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Rewardable implementation for int.
	/// </summary>
	public class RewardableInt : Rewardable<int>
	{
		void OnEnable()
		{
			AddRewarders(this);

		}

		void OnDisable()
		{
			RemoveRewarders(this);
		}
	}
}
