using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_Quester
{
	class RewardableDouble
	{
		public string Name { get; set; }
		public double Value { get; set; }

		public RewardableDouble(double value = 0.0f, string name = "unknown")
		{
			Value = value;
			Name = name;
		}

		public void IncreaseValue(double amount)
		{
			Value += amount;
			Console.WriteLine("You gained " + amount + " " + Name + " and now have a total of " + Value + ".");
		}

		public void AddRewarder(DoubleRewarder t)
		{
			t.RewardDoubleEvent += IncreaseValue;
		}

		public void RemoveRewarder(DoubleRewarder t)
		{
			t.RewardDoubleEvent -= IncreaseValue;
		}
	}
}
