namespace D_Quester
{
	/// <summary>
	/// Handles specified rewards for all subscribed RewardableInts.
	/// </summary>
	public class IntRewarder : Rewarder<int>
	{
		public bool GiveReward = false;

		void update()
		{
			if (GiveReward)
			{
				Reward();
				GiveReward = false;
			}
		}
	}
}