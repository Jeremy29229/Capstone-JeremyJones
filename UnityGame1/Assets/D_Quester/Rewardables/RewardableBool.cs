using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Rewardable implementation for bool.
	/// </summary>
	public class RewardableBool : Rewardable<bool>
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
