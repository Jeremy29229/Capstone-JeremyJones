using System;
using System.Collections.Generic;


namespace D_Quester
{
	/// <summary>
	/// Base object for D_Quester API. Represents a single task or event in a quest chain. Can be attached to any existing objects to give them meaning in a quest or story.
	/// </summary>
	class QuestObject
	{
		/// <summary>
		/// Name of the quest object. Can be printed or displayed to indicate progress to the player.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Quest that this quest node belongs to.
		/// </summary>
		public Quest Owner { get; private set; }
		/// <summary>
		/// Number of objectives this object has until it will be marked as finished.
		/// </summary>
		public int NumObjectives { get; set; }
		/// <summary>
		/// Number of objectives currently completed.
		/// </summary>
		public int ObjectivesCompleted { get; set; }
		/// <summary>
		/// Holds all the rewards this object will distribute when this node is completed.
		/// </summary>
		public QuestRewarder QuestRewarder { get; set; }
		/// <summary>
		/// List of acceptable strings that will advance the number of completed objectives.
		/// </summary>
		public List<string> NamedObjectives { get; set; }
		/// <summary>
		/// Last state of the object.
		/// </summary>
		public QuestObjectState PreviousState { get; set; }

		private QuestObjectState _currentState;
		/// <summary>
		///	Current state of the object. Automatically updates the previous state. 
		/// </summary>
		public QuestObjectState CurrentState
		{
			get
			{
				return _currentState;
			}
			set
			{
				if (value != CurrentState)
				{
					PreviousState = CurrentState;
					_currentState = value;
				}
			}
		}

		/// <summary>
		/// Indicates if object should be hidden from the player. This could be used by a GUI to decide what to populate a task list with.
		/// </summary>
		public bool IsHiddenFromPlayer { get; set; }

		/// <summary>
		/// Initializes quest object with the information most quest objects need at minimum. All parameters have default values.
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="name">Name of the quest object. May be displayed to the player if object is not hidden.</param>
		/// <param name="isHidden">Sets if the objects existence should be hidden from the player.</param>
		/// <param name="starting">Sets the initial state the object should be in.</param>
		public QuestObject(Quest owner = null, string name = "", bool isHidden = false, QuestObjectState starting = QuestObjectState.Uninitialized)
		{
			Owner = owner;
			Name = name;
			NamedObjectives = new List<string>();
			PreviousState = QuestObjectState.Uninitialized;
			CurrentState = QuestObjectState.Uninitialized;
			CurrentState = starting;
			IsHiddenFromPlayer = isHidden;
			QuestRewarder = new QuestRewarder();
		}

		/// <summary>
		/// Changes the quest object to be "in-progress" if it is currently in the "not started" state.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void StartUp()
		{
			Console.WriteLine("You just started the quest node: " + Name);
			if (CurrentState == QuestObjectState.NotStarted)
			{
				CurrentState = QuestObjectState.InProgress;
			}
		}

		/// <summary>
		/// Increments the number of objectives completed in the quest object if the quest object is in-progress.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void Progress()
		{
			if (CurrentState == QuestObjectState.InProgress)
			{
				ObjectivesCompleted++;
				Console.WriteLine("You just progressed the quest node: " + Name);

				if (ObjectivesCompleted == NumObjectives)
				{
					Console.WriteLine("You just finished the quest node: " + Name);
					QuestRewarder.GiveRewards();
					CurrentState = QuestObjectState.Completed;
					Owner.Advance();
				}
			}
		}

		/// <summary>
		/// Changes the quest object's completed objectives to have a count of 0 if the quest object is in-progress.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void ResetProgress()
		{
			if (CurrentState == QuestObjectState.InProgress)
			{
				if (ObjectivesCompleted != 0)
				{
					ObjectivesCompleted = 0;
					Console.WriteLine("Your progress in: " + Name + " has just been reset");
				}
			}
		}

		/// <summary>
		/// Decrements the number of objectives completed in the quest object if the quest object is in-progress.
		/// Will not do anything if the number of objectives is 0.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void Regress()
		{
			if (CurrentState == QuestObjectState.InProgress)
			{
				if (ObjectivesCompleted != 0)
				{
					ObjectivesCompleted--;
					Console.WriteLine("You have taken a step back in: " + Name);
				}
			}
		}

		/// <summary>
		/// Calls the StartUp() function of the quest object has the string passed in on its named parameter list.
		/// This method is useful for quests nodes that want to advance once a, b, and c happen but QuestProgress emits a, b, c, and d.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		/// <param name="objectName">Name of the objective that is occurring.</param>
		public void NamedStartUp(string objectName)
		{
			if (NamedObjectives.Contains(objectName))
			{
				StartUp();
			}
		}

		/// <summary>
		/// Calls the Progress() function of the quest object has the string passed in on its named parameter list.
		/// This method is useful for quests nodes that want to advance once a, b, and c happen but QuestProgress emits a, b, c, and d.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		/// <param name="objectName">Name of the objective that is occurring.</param>
		public void NamedProgress(string objectName)
		{
			if (NamedObjectives.Contains(objectName))
			{
				Progress();
			}
		}

		/// <summary>
		/// Calls the ResetProgress() function of the quest object has the string passed in on its named parameter list.
		/// This method is useful for quests nodes that want to advance once a, b, and c happen but QuestProgress emits a, b, c, and d.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		/// <param name="objectName">Name of the objective that is occurring.</param>
		public void NamedResetProgress(string objectName)
		{
			if (NamedObjectives.Contains(objectName))
			{
				ResetProgress();
			}
		}

		/// <summary>
		/// Calls the Regress() function of the quest object has the string passed in on its named parameter list.
		/// This method is useful for quests nodes that want to advance once a, b, and c happen but QuestProgress emits a, b, c, and d.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		/// <param name="objectName">Name of the objective that is occurring.</param>
		public void NamedRegress(string objectName)
		{
			if (NamedObjectives.Contains(objectName))
			{
				Regress();
			}
		}

		/// <summary>
		/// Makes a QuestRequirement with this quest object with the acceptable that you pass in. 
		/// </summary>
		/// <param name="requiredStates">States the QuestObject must be in for the new quest requirement to be met.</param>
		/// <returns>new QuestRequirement setup with the criteria passed in and this quest object being monitored.</returns>
		public QuestRequirement MakeQuestRequirement(QuestObjectState requiredStates)
		{
			return new QuestRequirement(this, requiredStates);
		}
	}
}
