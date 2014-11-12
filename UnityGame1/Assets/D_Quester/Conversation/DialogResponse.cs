using UnityEngine;
using System;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class DialogResponse : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public string DialogResponseName = "";

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public Dialog Resulting;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public Conversation NewCurrent;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public string Text = "";

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public bool IsOneTimeOption = false;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public int NumTimesSelected = 0;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public ItemRequirement[] Items;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public QuestRequirement requirement;
	}
}
