
namespace D_Quester
{
	/// <summary>
	/// Generic delegate for quest node progression.
	/// </summary>
	public delegate void ProgressionHandler();

	/// <summary>
	/// Must be implemented by any class that wishes to modify a quest node with events with no parameters that return void.
	/// </summary>
	public interface IQuestProgresser
	{
		/// <summary>
		/// Event the quest node needs to be subscribe to in order to be modified.
		/// </summary>
		event ProgressionHandler ProgressionEvent;
		
		/// <summary>
		/// Calls ProgressionEvent with null handling.
		/// </summary>
		void OnProgressionEvent();
	}
}
