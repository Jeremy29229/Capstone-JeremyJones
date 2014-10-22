using System;
using System.Collections.Generic;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Represents anything that needs to remotely reward one or more rewardable classes.
	/// </summary>
	public class QuestRewarder : MonoBehaviour
	{
		/// <summary>
		/// List of all rewards this class has to give out.
		/// </summary>
		public Rewarder[] rewarders;

		/// <summary>
		/// When called, triggers all individual rewarders events to reward every object listening with their specified rewards.
		/// </summary>
		public void GiveRewards()
		{
			foreach (var rewarder in rewarders)
			{
				rewarder.Reward();
			}
		}

		public bool GiveReward = false;

		void Update()
		{
			if (GiveReward)
			{
				GiveRewards();
				GiveReward = false;
			}
		}
	}
}
