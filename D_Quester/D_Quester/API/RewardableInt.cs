using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class RewardableInt
	{
		public string Name { get; set; }
		public int Value { get; set; }

		public RewardableInt(int value = 0, string name = "unknown")
		{
			Value = value;
			Name = name;
		}

		public void IncreaseValue(int amount)
		{
			Value += amount;
			Console.WriteLine("You gained " + amount + " " + Name + " and now have a total of " + Value + ".");
		}

		public void AddRewarder(IntRewarder t)
		{
			t.RewardIntEvent += IncreaseValue;
		}

		public void RemoveRewarder(IntRewarder t)
		{
			t.RewardIntEvent -= IncreaseValue;
		}
	}
}
