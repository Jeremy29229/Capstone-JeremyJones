using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace D_Quester.API
{
	/// <summary>
	/// Extension methods. Don't call this class directly.
	/// </summary>
	static class APIExtension
	{
		/// <summary>
		/// Find a quest in the list by title.
		/// </summary>
		/// <param name="questList">Instance of list.</param>
		/// <param name="title">Title of desired quest, can be a partial match.</param>
		/// <returns>The quest that contains the title passed in (can be partial) or null if no quest contain the title passed in.</returns>
		public static Quest GetByTitle(this List<Quest> questList, string title)
		{
			return questList.FirstOrDefault(x => x.Title.ToLowerInvariant().Contains(title.ToLowerInvariant()));
		}
	}
}
