using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	class QuestRewarder
	{
		public List<IntRewarder> ints { get; set; }
		public List<DoubleRewarder> doubles { get; set; }
		public List<BoolRewarder> bools { get; set; }

		private object lastAddition;

		private void setup()
		{
			ints = new List<IntRewarder>();
			doubles = new List<DoubleRewarder>();
			bools = new List<BoolRewarder>();
		}

		public QuestRewarder AddReward(bool state)
		{
			setup();
			BoolRewarder br = new BoolRewarder(state);
			lastAddition = br;
			bools.Add(br);
			return this;
		}

		public QuestRewarder AddReward(int amount)
		{
			setup();
			IntRewarder ir = new IntRewarder(amount);
			lastAddition = ir;
			ints.Add(ir);
			return this;
		}

		public QuestRewarder AddReward(double amount)
		{
			setup();
			DoubleRewarder dr = new DoubleRewarder(amount);
			lastAddition = dr;
			doubles.Add(dr);
			return this;
		}

		/// <summary>
		/// Shortcut to add listener to new reward. Must be changed to an AddReward() call.
		/// </summary>
		/// <param name="target"></param>
		public void To(RewardableBool target)
		{
			if (lastAddition.GetType() != typeof(BoolRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((BoolRewarder)lastAddition);
		}

		/// <summary>
		/// Shortcut to add listener to new reward. Must be changed to an AddReward() call.
		/// </summary>
		/// <param name="target"></param>
		public void To(RewardableDouble target)
		{
			if (lastAddition.GetType() != typeof(DoubleRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((DoubleRewarder)lastAddition);
		}

		/// <summary>
		/// Shortcut to add listener to new reward. Must be changed to an AddReward() call.
		/// </summary>
		/// <param name="target"></param>
		public void To(RewardableInt target)
		{
			if (lastAddition.GetType() != typeof(IntRewarder))
			{
				throw new InvalidOperationException("Method call not properly chained.");
			}

			target.AddRewarder((IntRewarder)lastAddition);
		}

		/// <summary>
		/// 
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
