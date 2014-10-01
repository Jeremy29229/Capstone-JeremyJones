using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class TestQuestRewarder
    {
        public string Name { get; set; }
        public int RewardAmount { get; set; }
        public delegate void RewardIntEvent(int rewardAmount);
        public event RewardIntEvent goldReward;

        public TestQuestRewarder(int rewardAmount, string name)
        {
            RewardAmount = rewardAmount;
            Name = name;
        }

        protected virtual void OnStateChange()
        {
            if (goldReward != null)
            {
                Console.WriteLine(Name + " is handing out rewards.");
                goldReward(RewardAmount);
            }
        }

        public void Reward()
        {
            OnStateChange();
        }
    }
}
