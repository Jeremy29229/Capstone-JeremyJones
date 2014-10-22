using UnityEngine;

namespace D_Quester
{
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
