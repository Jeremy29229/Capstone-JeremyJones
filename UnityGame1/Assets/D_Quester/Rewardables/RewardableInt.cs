using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

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
