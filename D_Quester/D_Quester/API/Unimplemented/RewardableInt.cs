using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class RewardableInt
    {
        public int Value { get; set; }

        public RewardableInt(int value = 0)
        {
            Value = value;
        }

        public void IncreaseValue(int amount)
        {
            Value += amount;
        }

        public void AddRewarder(TestQuestRewarder t)
        {
            t.goldReward += IncreaseValue;
        }

        public void RemoveRewarder(TestQuestRewarder t)
        {
            t.goldReward -= IncreaseValue;
        }
    }
}
