using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Base object for D_Quester API. Represents a single task or event in a quest chain. Can be attached to any existing game objects to give them meaning in a quest or story.
	/// </summary>
	public class QuestNode : MonoBehaviour
	{
		public static QuestNodeState defaultState;

		public QuestNodeState StartingState = defaultState;

		/// <summary>
		/// 
		/// </summary>
		public QuestPath NextPathIfCompleted;
		
		/// <summary>
		/// 
		/// </summary>
		public QuestNode nextNodeIfNoPath;

		/// <summary>
		/// 
		/// </summary>
		public DateTime CompletionTime;


		/// <summary>
		/// 
		/// </summary>
		public string[] QuestObjectMethod;
		
		/// <summary>
		/// 
		/// </summary>
		public string[] GameObjectWithEventComponentName;
		
		/// <summary>
		/// 
		/// </summary>
		public string[] ComponentWithEvent;
		
		/// <summary>
		/// 
		/// </summary>
		public string[] eventName;

		private List<EventInfo> eventinfos;
		private List<object> classContainEvent;
		private List<Delegate> dels;

		void OnEnable()
		{
			if (QuestObjectMethod.Length != GameObjectWithEventComponentName.Length || GameObjectWithEventComponentName.Length != ComponentWithEvent.Length || ComponentWithEvent.Length != eventName.Length)
			{
				throw new UnityException("Invalid quest object configuration");
			}

			eventinfos = new List<EventInfo>();
			classContainEvent = new List<object>();
			dels = new List<Delegate>();

			for (int i = 0; i < QuestObjectMethod.Length; i++)
			{
				classContainEvent.Add(GameObject.Find(GameObjectWithEventComponentName[i]).GetComponent(ComponentWithEvent[i]));
				eventinfos.Add(classContainEvent[i].GetType().GetEvent(eventName[i]));
				dels.Add(Delegate.CreateDelegate(eventinfos[i].EventHandlerType, this, this.GetType().GetMethod(QuestObjectMethod[i])));
				eventinfos[i].AddEventHandler(classContainEvent[i], dels[i]);
			}

			CurrentState = StartingState;

		}

		void OnDisable()
		{
			for (int i = 0; i < QuestObjectMethod.Length; i++)
			{
				eventinfos[i].RemoveEventHandler(classContainEvent[i], dels[i]);
			}

			eventinfos.Clear();
			classContainEvent.Clear();
			dels.Clear();
		}

		/// <summary>
		/// Name of the quest object. Can be printed or displayed to indicate progress to the player.
		/// </summary>
		public string Name;

		/// <summary>
		/// Number of objectives this object has until it will be marked as finished.
		/// </summary>
		public int NumObjectives;

		/// <summary>
		/// Number of objectives currently completed.
		/// </summary>
		public int ObjectivesCompleted;

		/// <summary>
		/// Holds all the rewards this object will distribute when this node is completed.
		/// </summary>
		public QuestRewarder QuestRewarder;

		/// <summary>
		/// List of acceptable strings that will advance the number of completed objectives.
		/// </summary>
		public List<string> NamedObjectives;

		/// <summary>
		/// Last state of the object.
		/// </summary>
		public QuestNodeState PreviousState { get; private set; }

		private QuestNodeState _currentState;

		/// <summary>
		///	Current state of the object. Automatically updates the previous state. 
		/// </summary>
		public QuestNodeState CurrentState
		{
			get
			{
				return _currentState;
			}
			private set
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
		public bool IsHiddenFromPlayer = false;

		/// <summary>
		/// Changes the quest object to be "in-progress" if it is currently in the "not started" state.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void StartUp()
		{
			if (CurrentState == QuestNodeState.NotStarted)
			{
				print("You just started the quest node: " + Name);
				CurrentState = QuestNodeState.InProgress;
			}
		}

		/// <summary>
		/// Increments the number of objectives completed in the quest object if the quest object is in-progress.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void Progress()
		{
			if (CurrentState == QuestNodeState.InProgress)
			{
				ObjectivesCompleted++;
				print("You just progressed the quest node: " + Name);

				if (ObjectivesCompleted == NumObjectives)
				{
					print("You just finished the quest node: " + Name);

					if (QuestRewarder != null)
					{
						QuestRewarder.GiveRewards();
					}

					CompletionTime = DateTime.UtcNow;
					CurrentState = QuestNodeState.Completed;
				}
			}
		}

		/// <summary>
		/// Changes the quest object's completed objectives to have a count of 0 if the quest object is in-progress.
		/// Can be subscribed to ProgressionEvent which is contained in any class that implements IQuestProgresser.
		/// </summary>
		public void ResetProgress()
		{
			if (CurrentState == QuestNodeState.InProgress)
			{
				if (ObjectivesCompleted != 0)
				{
					ObjectivesCompleted = 0;
					print("Your progress in: " + Name + " has just been reset");
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
			if (CurrentState == QuestNodeState.InProgress)
			{
				if (ObjectivesCompleted != 0)
				{
					ObjectivesCompleted--;
					print("You have taken a step back in: " + Name);
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
	}
}
