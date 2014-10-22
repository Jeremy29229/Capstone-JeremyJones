using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace D_Quester
{
	/// <summary>
	///	Inherit from this script to make custom Rewardable Scripts
	/// </summary>
	/// <typeparam name="T">Underlying type being rewarded to.</typeparam>
	public class Rewardable<T> : MonoBehaviour
	{
		/// <summary>
		/// Indicates which method will be subscribing to each event.
		/// </summary>
		public string[] RewardableMethods;

		/// <summary>
		/// Game object that contains the rewarder.
		/// </summary>
		public string[] GameObjectsWithRewardNames;

		/// <summary>
		/// Name of the rewarder to disambiguate between GameObjects with multiple rewarders.
		/// </summary>
		public string[] RewarderName;

		/// <summary>
		/// Underlying type instance being rewarded to.
		/// </summary>
		public T Value;

		/// <summary>
		/// Name that represents Rewardable.
		/// </summary>
		public string Name;

		/// <summary>
		/// Adds the instance of amount to the underlying Value, if possible.
		/// </summary>
		/// <param name="amount">Instance of generic being added to Value.</param>
		/// <returns>Return true if addition was successful.</returns>
		public bool AddValue(T amount)
		{
			bool wasSuccessful = false;

			if (!HasPlusOperator())
			{
				Debug.LogException(new UnityException("Rewardable's value does not overload the + operator."));
			}
			else if (Value == null)
			{
				Debug.LogException(new UnityException("Rewardable's value cannot be null when calling AddValue()."));
			}
			else if (amount == null)
			{
				Debug.LogException(new UnityException("Null assignment is not allowed."));
			}
			else
			{
				var valueProperty = Expression.Constant(Value);
				var additionProperty = Expression.Constant(amount);
				var result = Expression.Add(valueProperty, additionProperty);
				Value = Expression.Lambda<Func<T>>(result).Compile()();
				wasSuccessful = true;
			}

			return wasSuccessful;
		}

		/// <summary>
		/// Subtracts the instance of amount from underlying Value, if possible.
		/// </summary>
		/// <param name="amount">Instance of generic being subtracted from Value.</param>
		/// <returns>Return true if subtraction was successful.</returns>
		public bool SubtractValue(T amount)
		{
			bool wasSuccessful = false;

			if (!HasMinusOperator())
			{
				Debug.LogException(new UnityException("Rewardable's value does not overload the - operator."));
			}
			else if (Value == null)
			{
				Debug.LogException(new UnityException("Rewardable's value cannot be null when calling SubtractValue()."));
			}
			else if (amount == null)
			{
				Debug.LogException(new UnityException("Null assignment is not allowed."));
			}
			else
			{
				var valueProperty = Expression.Constant(Value);
				var subtractionProperty = Expression.Constant(amount);
				var result = Expression.Subtract(valueProperty, subtractionProperty);
				Value = Expression.Lambda<Func<T>>(result).Compile()();
				wasSuccessful = true;
			}

			return wasSuccessful;
		}

		/// <summary>
		/// Sets the underlying Value to the instance of newValue, if possible.
		/// </summary>
		/// <param name="newValue">Instance to be assigned to Value.</param>
		/// <returns>Returns true if assignment was successful.</returns>
		public bool SetValue(T newValue)
		{
			bool wasSuccessful = false;

			if (newValue == null)
			{
				Debug.LogException(new UnityException("Null assignment is not allowed."));
			}
			else
			{
				Value = newValue;
				wasSuccessful = true;
			}

			return wasSuccessful;
		}

		public void AddRewarders(object callerInstance)
		{
			for (int i = 0; i < RewardableMethods.Length; i++)
			{
				var rewarderContainer = GameObject.Find(GameObjectsWithRewardNames[i]).GetComponents<Rewarder<T>>();
				Rewarder<T> rewarder = rewarderContainer.FirstOrDefault(x => x.Name == RewarderName[i]);
				AddRewarder(rewarder, callerInstance, RewardableMethods[i] );
			}
		}

		private void AddRewarder(Rewarder<T> rewarder, object callerInstance, string methodName)
		{
			EventInfo eventInfo = rewarder.GetType().GetEvent("RewardEvent");

			MethodInfo methodInfo = callerInstance.GetType().GetMethod(methodName);

			Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, callerInstance, methodInfo);

			eventInfo.AddEventHandler(rewarder, del);
		}

		public void RemoveRewarders(object callerInstance)
		{
			for (int i = 0; i < RewardableMethods.Length; i++)
			{
				var rewarderContainer = GameObject.Find(GameObjectsWithRewardNames[i]).GetComponents<Rewarder<T>>();
				Rewarder<T> rewarder = rewarderContainer.FirstOrDefault(x => x.Name == RewarderName[i]);
				RemoveRewarder(rewarder, callerInstance, RewardableMethods[i]);
			}
		}

		private void RemoveRewarder(Rewarder<T> rewarder, object callerInstance, string methodName)
		{
			EventInfo eventInfo = rewarder.GetType().GetEvent("RewardEvent");

			MethodInfo methodInfo = callerInstance.GetType().GetMethod(methodName);

			Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, callerInstance, methodInfo);

			eventInfo.RemoveEventHandler(rewarder, del);
		}

		private bool HasPlusOperator()
		{
			var exProp = Expression.Constant(default(T), typeof(T));

			try
			{
				Expression.Add(exProp, exProp);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private bool HasMinusOperator()
		{
			var exProp = Expression.Constant(default(T), typeof(T));

			try
			{
				Expression.Subtract(exProp, exProp);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
