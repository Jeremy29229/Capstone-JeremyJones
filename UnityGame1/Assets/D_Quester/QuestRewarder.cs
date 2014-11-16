using System;
using System.Collections.Generic;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Represents anything that needs to remotely reward one or more rewardable components.
	/// </summary>
	public class QuestRewarder : MonoBehaviour
	{
		/// <summary>
		/// List of all rewards this component has to give out.
		/// </summary>
		[Tooltip("List of all rewards this component has to give out.")]
		public Rewarder[] rewarders;

		/// <summary>
		/// Allows QuestNode to deliver rewards when it is completed.
		/// </summary>
		[HideInInspector]
		public bool GiveReward = false;

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
