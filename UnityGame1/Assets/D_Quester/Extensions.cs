using System;
using System.Collections.Generic;
using System.Linq;

namespace D_Quester
{
	/// <summary>
	/// Extension methods. Don't call this class directly.
	/// </summary>
	public static class Extensions
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

		/// <summary>
		/// Checks if the enumeration contains the FlagsAttribute. Throws an InvalidOperationException if called on an non-enumeration type.
		/// </summary>
		/// <param name="enumeration">Enumeration type instance this is being called on.</param>
		/// <returns>True if the enumeration implements the FlagsAttribute.</returns>
		public static bool HasFlags(this Type enumeration)
		{
			if (!enumeration.IsEnum)
			{
				throw new InvalidOperationException("This method can only be called on enumeration types.");
			}

			return enumeration.IsDefined(typeof(FlagsAttribute), false);
		}
	}
}
