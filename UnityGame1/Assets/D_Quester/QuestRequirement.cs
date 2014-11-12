using System;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Represents the condition a quest node must be in and can be checked to show or hide aspects of a game depending on the implementation.
	/// </summary>
	public class QuestRequirement : MonoBehaviour
	{
		/// <summary>
		/// Quest node to be watched for state.
		/// </summary>
		[Tooltip("Quest node to be watched for state.")]
		public QuestNode Requirement;

		/// <summary>
		/// State or states the quest object must be in for this Quest Requirement to be met.
		/// By default, QuestObjectState is a bit field backed enumeration allowing for the used of the | operator to signify multiple states.
		/// </summary>
		[Tooltip("State or states the quest object must be in for this Quest Requirement to be met.")]
		public QuestNodeState RequiredStates;

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
