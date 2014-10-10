using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester
{
	/// <summary>
	/// Generic delegate for quest object progression.
	/// </summary>
	public delegate void ProgressionHandler();

	/// <summary>
	/// Must be implemented by any class that wishes to modify a quest object with events.
	/// </summary>
	interface IQuestProgresser
	{
		/// <summary>
		/// Event the quest object needs to be subscribe to in order to be modified.
		/// </summary>
		event ProgressionHandler ProgressionEvent;
		/// <summary>
		/// Calls ProgressionEvent with null handling.
		/// </summary>
		void OnProgressionEvent();
	}
}
