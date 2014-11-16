using System;
using System.Collections.Generic;
using System.Linq;

namespace D_Quester
{
	/// <summary>
	/// Internal class for D_Quester. Do not use.
	/// </summary>
	class DEnumInfo
	{
		public string Name;
		public Type Enumeration;
		public string Documentation;
		public string[] DefaultEnumerators;
		public bool HasFlags;

		public DEnumInfo(string name, Type enumeration, string documentation, IEnumerable<string> defaultEnumerators, bool hasFlags)
		{
			Name = name;
			Enumeration = enumeration;
			Documentation = documentation;
			DefaultEnumerators = defaultEnumerators.ToArray();
			HasFlags = hasFlags;
		}
	}
}
