using System;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class RewardableBool
	{
		/// <summary>
		/// Message printed to the console when the underlying bool is changed to true.
		/// </summary>
		public string TrueMessage { get; set; }
		/// <summary>
		/// Message printed to the console when the underlying bool is changed to false.
		/// </summary>
		public string FalseMessage { get; set; }
		/// <summary>
		/// Current state of the underlying bool.
		/// </summary>
		public bool State { get; set; }

		/// <summary>
		/// Initializes class with default or passed in strings and bool value.
		/// </summary>
		/// <param name="startingState">Starting state of underlying bool.</param>
		/// <param name="trueMessage">Message printed to the console when the underlying bool is changed to true. Can be changed later.</param>
		/// <param name="falseMessage">Message printed to the console when the underlying bool is changed to false. Can be changed later.</param>
		public RewardableBool(bool startingState = false, string trueMessage = "You unlocked something.", string falseMessage = "You lost the ability to do something.")
		{
			State = startingState;
			TrueMessage = trueMessage;
			FalseMessage = falseMessage;
		}

		/// <summary>
		/// Called by the subscribed rewarders to modify underlying bool remotely.
		/// </summary>
		/// <param name="newState">The state the bool will be changed to.</param>
		public void ChangeState(bool newState)
		{
			State = newState;
			string message = (State) ? TrueMessage : FalseMessage;
			Console.WriteLine(message);
		}

		/// <summary>
		/// Subscribes class to rewarder it can modify the underlying bool remotely.
		/// </summary>
		/// <param name="br"></param>
		public void AddRewarder(BoolRewarder br)
		{
			br.RewardBoolEvent += ChangeState;
		}

		/// <summary>
		/// Unsubscribes class from rewarder. Can be used as cleanup for one time rewarders.
		/// </summary>
		/// <param name="br">Bool rewarder</param>
		public void RemoveRewarder(BoolRewarder br)
		{
			br.RewardBoolEvent -= ChangeState;
		}
	}
}
