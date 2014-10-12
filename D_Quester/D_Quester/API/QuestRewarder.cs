using System;
using System.Collections.Generic;

namespace D_Quester
{
	/// <summary>
	/// Represents anything that needs to remotely reward one or more rewardable classes.
	/// </summary>
	public class QuestRewarder
	{
		/// <summary>
		/// List of all int rewards this class has to give out.
		/// </summary>
		public List<IntRewarder> ints { get; set; }
		/// <summary>
		/// List of all double rewards this class has to give out.
		/// </summary>
		public List<DoubleRewarder> doubles { get; set; }
		/// <summary>
		/// List of all bool rewards this class has to give out.
		/// </summary>
		public List<BoolRewarder> bools { get; set; }
		private object lastAddition;

		/// <summary>
		/// Initializes all internal rewarder lists.
		/// </summary>
		public QuestRewarder()
		{
			setup();
		}

		private void setup()
		{
			ints = new List<IntRewarder>();
			doubles = new List<DoubleRewarder>();
			bools = new List<BoolRewarder>();
		}

		/// <summary>
		/// Adds a bool rewarder to the list of rewarders this class offers with the bool state passed in.
		/// </summary>
		/// <param name="state">State the bool will be changed to when reward triggers.</param>
		/// <returns>Returns class instance to allow the chaining of the To() function.</returns>
		public QuestRewarder AddReward(bool state)
		{
			BoolRewarder br = new BoolRewarder(state);
			lastAddition = br;
			bools.Add(br);
			return this;
		}

		/// <summary>
		/// Adds a int rewarder to the list of rewarders this class offers adding the amount passed in when triggered later.
		/// </summary>
		/// <param name="amount">Amount added to ints when the reward triggers.</param>
		/// <returns>Returns class instance to allow the chaining of the To() function.</returns>
		public QuestRewarder AddReward(int amount)
		{
			IntRewarder ir = new IntRewarder(amount);
			lastAddition = ir;
			ints.Add(ir);
			return this;
		}
		
		/// <summary>
		/// Adds a double rewarder to the list of rewarders this class offers adding the amount passed in when triggered later.
		/// </summary>
		/// <param name="amount">Amount added to doubles when the reward triggers.</param>
		/// <returns>Returns class instance to allow the chaining of the To() function.</returns>
		public QuestRewarder AddReward(double amount)
		{
			DoubleRewarder dr = new DoubleRewarder(amount);
			lastAddition = dr;
			doubles.Add(dr);
			return this;
		}

		/// <summary>
		/// Shortcut to add listener to new rewarder. Must be chained to an AddReward() call.
		/// </summary>
		/// <param name="target">Object to be subscribed to new rewarder.</param>
		public void To(RewardableBool target)
		{
			if (lastAddition.GetType() != typeof(BoolRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((BoolRewarder)lastAddition);
		}

		/// <summary>
		/// Shortcut to add listener to new rewarder. Must be chained to an AddReward() call.
		/// </summary>
		/// <param name="target">Object to be subscribed to new rewarder.</param>
		public void To(RewardableDouble target)
		{
			if (lastAddition.GetType() != typeof(DoubleRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((DoubleRewarder)lastAddition);
		}

		/// <summary>
		/// Shortcut to add listener to new rewarder. Must be chained to an AddReward() call.
		/// </summary>
		/// <param name="target">Object to be subscribed to new rewarder.</param>
		public void To(RewardableInt target)
		{
			if (lastAddition.GetType() != typeof(IntRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((IntRewarder)lastAddition);
		}

		/// <summary>
		/// When called, triggers all individual rewarders events to reward every object listening with their specified rewards.
		/// </summary>
		public void GiveRewards()
		{
			foreach (var item in ints)
			{
				item.Reward();
			}

			foreach (var item in doubles)
			{
				item.Reward();
			}

			foreach (var item in bools)
			{
				item.Reward();
			}
		}
	}
}
