using System;

namespace D_Quester
{
	class RewardableDouble
	{
		/// <summary>
		/// Name that represents what the class is being used for. Can be used when notifying the player that they were rewarded.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Current value of the underlying double.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Initializes class with starting value and name.
		/// </summary>
		/// <param name="value">Amount the underlying double will start at.</param>
		/// <param name="name">Name that represents the class. For example, gold or experience.</param>
		public RewardableDouble(double value = 0.0f, string name = "unknown")
		{
			Value = value;
			Name = name;
		}

		/// <summary>
		/// Called by the subscribed rewarders to handle value increases remotely.
		/// </summary>
		/// <param name="amount">Amount being added to underlying double's total.</param>
		public void IncreaseValue(double amount)
		{
			Value += amount;
			Console.WriteLine("You gained " + amount + " " + Name + " and now have a total of " + Value + ".");
		}

		/// <summary>
		/// Subscribes class to rewarder it can add to the underlying double remotely.
		/// </summary>
		/// <param name="dr">Double rewarder</param>
		public void AddRewarder(DoubleRewarder dr)
		{
			dr.RewardDoubleEvent += IncreaseValue;
		}

		/// <summary>
		/// Unsubscribes class from rewarder. Can be used as cleanup for one time rewarders.
		/// </summary>
		/// <param name="dr">Double rewarder</param>
		public void RemoveRewarder(DoubleRewarder dr)
		{
			dr.RewardDoubleEvent -= IncreaseValue;
		}
	}
}
