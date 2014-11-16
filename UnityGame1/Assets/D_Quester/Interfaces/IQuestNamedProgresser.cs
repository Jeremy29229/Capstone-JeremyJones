
namespace D_Quester
{
	/// <summary>
	/// Generic delegate for quest node progression.
	/// </summary>
	public delegate void NamedProgressionHandler(string objectiveName);

	/// <summary>
	/// Must be implemented by any class that wishes to modify a quest node with events with a single string parameter that return void.
	/// </summary>
	public interface IQuestNamedProgresser
	{
		/// <summary>
		/// Event the QuestNode needs to be subscribe to in order to be modified.
		/// </summary>
		event NamedProgressionHandler NamedProgressionEvent;

		/// <summary>
		/// Calls NamedProgressionEvent with null handling.
		/// </summary>
		/// <param name="objectiveName">Name of the objective being completed.</param>
		void OnNamedProgressionEvent(string objectiveName);
	}
}
