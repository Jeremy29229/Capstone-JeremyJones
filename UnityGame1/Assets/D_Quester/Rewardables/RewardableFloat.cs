using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Rewardable implementation for float.
	/// </summary>
	public class RewardableFloat : Rewardable<float>
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
