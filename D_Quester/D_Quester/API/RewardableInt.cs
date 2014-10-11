using System;


namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	class RewardableInt
	{
		/// <summary>
		/// Name that represents what the class is being used for. Can be used when notifying the player that they were rewarded.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Current value of the underlying int.
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// Initializes class with starting value and name.
		/// </summary>
		/// <param name="value">Amount the underlying int will start at.</param>
		/// <param name="name">Name that represents the class. For example, gold or experience.</param>
		public RewardableInt(int value = 0, string name = "unknown")
		{
			Value = value;
			Name = name;
		}

		/// <summary>
		/// Called by the subscribed rewarders to handle value increases remotely.
		/// </summary>
		/// <param name="amount">Amount being added to underlying int's total.</param>
		public void IncreaseValue(int amount)
		{
			Value += amount;
			Console.WriteLine("You gained " + amount + " " + Name + " and now have a total of " + Value + ".");
		}

		/// <summary>
		/// Subscribes class to rewarder it can add to the underlying int remotely.
		/// </summary>
		/// <param name="ir">Int rewarder</param>
		public void AddRewarder(IntRewarder ir)
		{
			ir.RewardIntEvent += IncreaseValue;
		}

		/// <summary>
		/// Unsubscribes class from rewarder. Can be used as cleanup for one time rewarders.
		/// </summary>
		/// <param name="ir">Int rewarder</param>
		public void RemoveRewarder(IntRewarder ir)
		{
			ir.RewardIntEvent -= IncreaseValue;
		}
	}
}
