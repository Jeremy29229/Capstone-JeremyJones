using System;


namespace D_Quester
{
	/// <summary>
	/// Represents the condition a quest object must be in and can be checked to show or hide aspects of a game depending on the implementation.
	/// </summary>
	class QuestRequirement
	{
		/// <summary>
		/// Quest object to be watched for state.
		/// </summary>
		public QuestObject Requirement { get; set; }
		/// <summary>
		/// State or states the quest object must be in for this Quest Requirement to be met.
		/// By default, QuestObjectState is a bit field backed enumeration allowing for the used of the | operator to signify multiple states.
		/// </summary>
		public QuestObjectState RequiredStates { get; set; }

		/// <summary>
		/// Initializes the quest requirement.
		/// </summary>
		/// <param name="requirement">Quest object to be watched as a requirement.</param>
		/// <param name="requiredStates">Accepted states the quest object can be in to meet the requirement.</param>
		public QuestRequirement(QuestObject requirement, QuestObjectState requiredStates)
		{
			RequiredStates = requiredStates;
			Requirement = requirement;
		}

		/// <summary>
		/// Indicates if the quest requirement is being met.
		/// </summary>
		/// <returns>Returns bool indicating if requirement has been met.</returns>
		public bool IsMet()
		{
			if (Requirement == null)
			{
				throw new InvalidOperationException("Contains null QuestObject. Make sure QuestObject passed in still exists and is not null. The contained QuestObject can be changed at any time after construction if needed.");
			}

			return (RequiredStates == 0 || (RequiredStates & Requirement.CurrentState) != 0);
		}
	}
}
