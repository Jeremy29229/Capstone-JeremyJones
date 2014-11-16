using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Collection of methods for dynamically changing enumerations.
	/// Currently only supports D_Quester defined dynamic enumerations.
	/// </summary>
	static class Enums
	{
		private const string FIRST_LINE = "namespace D_Quester\r\n{";
		private const string LAST_LINE = "}";
		private const string FLAG_LINE = "\t[System.Flags]";
		private const string DOCUMENTATION_START_LINE = "\t/// <summary>";
		private const string DOCUMENTATION_END_LINE = "\t/// </summary>";

		private static readonly Dictionary<string, DEnumInfo> ENUMERATION_INFORMATION = new Dictionary<string, DEnumInfo>()
		{
			{"QuestNodeState", new DEnumInfo("QuestNodeState", typeof(QuestNodeState), "\t/// Base states for all quest nodes.", new string[]{"Uninitialized", "NotStarted", "InProgress", "Completed", "Failed"}, true)}
		};

		private static Dictionary<string, Type> enumerationAdditions = new Dictionary<string, Type>();
		private static Dictionary<string, Type> enumerationRemovals = new Dictionary<string, Type>();
		private static List<string> enumerationsWithFlagChanges = new List<string>();

		/// <summary>
		/// Adds an enumerator to an existing enumeration in D_Quester.
		/// </summary>
		/// <param name="enumeration">Name of the enumeration's type</param>
		/// <param name="newEnumerator">Name of the new enumerator to be added</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void AddEnum(string enumeration, string newEnumerator, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			AddEnum(Type.GetType("D_Quester." + enumeration), newEnumerator, enumUnityFilePath);
		}

		/// <summary>
		/// Adds an enumerator to an existing enumeration in D_Quester.
		/// </summary>
		/// <param name="enumeration">Enumeration's type</param>
		/// <param name="newEnumeration">Name of the new enumerator to be added</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void AddEnum(Type enumeration, string newEnumerator, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			if (newEnumerator == null || !Regex.IsMatch(newEnumerator, "^[a-zA-Z0-9_]+$"))
			{
				HandleError(new ArgumentException(newEnumerator + " is an invalid name. An enumerator must only contain alphanumeric characters and underscores.", "newEnumerator"));
				return;
			}

			List<string> currentEnums = Enum.GetNames(enumeration).ToList();

			foreach (var pair in enumerationAdditions.Where(x => x.Value == enumeration))
			{
				currentEnums.Add(pair.Key);
			}

			if (enumerationRemovals.Keys.Contains(newEnumerator))
			{
				enumerationRemovals.Remove(newEnumerator);
			}

			foreach (var key in enumerationRemovals.Keys)
			{
				if (enumerationRemovals[key] == enumeration)
				{
					currentEnums.Remove(key);
				}
			}

			if (currentEnums.Contains(newEnumerator))
			{
				HandleError(new ArgumentException(enumeration.Name + " already contains the enumeration " + newEnumerator + ".", "newEnumerator"));
				return;
			}

			string enumerationLine = "\tpublic enum " + enumeration.Name + " { ";

			if (enumeration.HasFlags())
			{
				int currentFlag = 1;
				foreach (string e in currentEnums)
				{
					enumerationLine += e + " = " + currentFlag + ", ";
					currentFlag *= 2;
				}
				enumerationLine += newEnumerator + " = " + currentFlag + ", ";
			}
			else
			{
				foreach (string e in currentEnums)
				{
					enumerationLine += e + ", ";
				}
				enumerationLine += newEnumerator + ", ";
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, enumeration.HasFlags(), enumUnityFilePath);

			enumerationAdditions.Add(newEnumerator, enumeration);
		}

		/// <summary>
		/// Removes an enumerator from an existing enumeration in D_Quester.
		/// </summary>
		/// <param name="enumeration">Name of the enumeration's type</param>
		/// <param name="enumerator">Name of the enumerator to be removed</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void RemoveEnum(string enumeration, string enumerator, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			RemoveEnum(Type.GetType("D_Quester." + enumeration), enumerator, enumUnityFilePath);
		}

		/// <summary>
		/// Removes an enumerator from an existing enumeration in D_Quester.
		/// </summary>
		/// <param name="enumeration">Enumeration's type</param>
		/// <param name="enumerator">Name of the enumerator to be removed</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void RemoveEnum(Type enumeration, string enumerator, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			if (ENUMERATION_INFORMATION[enumeration.Name].DefaultEnumerators.Contains(enumerator))
			{
				string message = "You cannot remove any default enumerators from an enumeration. The default enumerators for " + enumeration.Name + " are:\r\n";

				foreach (string defaultEnumerator in ENUMERATION_INFORMATION[enumeration.Name].DefaultEnumerators)
				{
					message += defaultEnumerator + "\r\n";
				}

				HandleError(new ArgumentException(message, "enumerator"));
				return;
			}

			List<string> currentEnums = Enum.GetNames(enumeration).ToList();

			foreach (var pair in enumerationAdditions.Where(x => x.Value == enumeration))
			{
				currentEnums.Add(pair.Key);
			}

			if (!currentEnums.Contains(enumerator))
			{
				HandleError(new ArgumentException(enumeration.Name + " does not contain the enumeration, " + enumerator + ", you are attempting to remove.", "enumerator"));
				return;
			}

			string enumerationLine = "\tpublic enum " + enumeration.Name + " { ";

			if (enumeration.HasFlags())
			{
				int currentFlag = 1;
				foreach (string e in currentEnums)
				{
					if (e != enumerator)
					{
						enumerationLine += e + " = " + currentFlag + ", ";
						currentFlag *= 2;
					}
				}
			}
			else
			{
				foreach (string e in currentEnums)
				{
					if (e != enumerator)
					{
						enumerationLine += e + ", ";
					}
				}
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, enumeration.HasFlags(), enumUnityFilePath);

			if (enumerationAdditions.Keys.Contains(enumerator))
			{
				enumerationAdditions.Remove(enumerator);
			}

			enumerationRemovals.Add(enumerator, enumeration);
		}

		/// <summary>
		/// Restores enumeration to D_Quester's default enumerators.
		/// </summary>
		/// <param name="enumeration">Name of enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void ResetToDefault(string enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			ResetToDefault(Type.GetType("D_Quester." + enumeration), enumUnityFilePath);
		}

		/// <summary>
		/// Restores enumeration to D_Quester's default enumerators.
		/// </summary>
		/// <param name="enumeration">Enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void ResetToDefault(Type enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			string enumerationLine = "\tpublic enum " + enumeration.Name + " { ";

			if (ENUMERATION_INFORMATION[enumeration.Name].HasFlags)
			{
				int currentFlag = 1;
				foreach (string e in ENUMERATION_INFORMATION[enumeration.Name].DefaultEnumerators)
				{
					enumerationLine += e + " = " + currentFlag + ", ";
					currentFlag *= 2;
				}
			}
			else
			{
				foreach (string e in ENUMERATION_INFORMATION[enumeration.Name].DefaultEnumerators)
				{
					enumerationLine += e + ", ";
				}
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, ENUMERATION_INFORMATION[enumeration.Name].HasFlags, enumUnityFilePath);

			if (!enumerationsWithFlagChanges.Contains(enumeration.Name))
			{
				enumerationsWithFlagChanges.Add(enumeration.Name);
			}

			foreach (var entry in enumerationAdditions.Where(x => x.Value == enumeration))
			{
				enumerationAdditions.Remove(entry.Key);
			}

			foreach (var entry in enumerationRemovals.Where(x => x.Value == enumeration))
			{
				enumerationRemovals.Remove(entry.Key);
			}

			foreach (string oldEnumerator in Enum.GetNames(enumeration))
			{
				if (!ENUMERATION_INFORMATION[enumeration.Name].DefaultEnumerators.Contains(oldEnumerator))
				{
					enumerationRemovals.Add(oldEnumerator, enumeration);
				}
			}

		}

		/// <summary>
		/// Adds flag attribute to existing enumeration and automatically updates the value of each enumeration to support a proper bit field.
		/// </summary>
		/// <param name="enumeration">Name of enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void AddFlags(string enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			AddFlags(Type.GetType("D_Quester." + enumeration));
		}

		/// <summary>
		/// Adds flag attribute to existing enumeration and automatically updates the value of each enumeration to support a proper bit field..
		/// </summary>
		/// <param name="enumeration">Enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void AddFlags(Type enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			if (enumerationsWithFlagChanges.Contains(enumeration.Name))
			{
				HandleError(new ArgumentException("You cannot modify the flag status of an enumeration more than once per script run-time.", "enumeration"));
				return;
			}
			if (enumeration.HasFlags())
			{
				HandleError(new ArgumentException(enumeration.ToString() + " already has the flags attribute enabled.", "enumeration"));
				return;
			}

			string[] currentEnums = Enum.GetNames(enumeration);
			string enumerationLine = "\tpublic enum " + enumeration.Name + " { ";
			int currentFlag = 1;

			foreach (string e in currentEnums)
			{
				enumerationLine += e + " = " + currentFlag + ", ";
				currentFlag *= 2;
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, true, enumUnityFilePath);
			enumerationsWithFlagChanges.Add(enumeration.Name);
		}

		/// <summary>
		/// Removes flag attribute from an existing enumeration. Changes the value of each enumerator to C#'s default.
		/// </summary>
		/// <param name="enumeration">Name of enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void RemoveFlags(string enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			RemoveFlags(Type.GetType("D_Quester." + enumeration), enumUnityFilePath);
		}

		/// <summary>
		/// Removes flag attribute from an existing enumeration. Changes the value of each enumerator to C#'s default.
		/// </summary>
		/// <param name="enumeration">Enumeration's type</param>
		/// <param name="enumUnityFilePath">File path of the enumeration being changed starting at the unity project's containing folder.</param>
		public static void RemoveFlags(Type enumeration, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			if (!EnumerationValidationCheck(enumeration))
			{
				return;
			}

			if (enumerationsWithFlagChanges.Contains(enumeration.Name))
			{
				HandleError(new ArgumentException("You cannot modify the flag status of an enumeration more than once per script run-time.", "enumeration"));
				return;
			}

			if (!enumeration.HasFlags())
			{
				HandleError(new ArgumentException(enumeration.ToString() + " does not have flags attribute enabled.", "enumeration"));
				return;
			}

			string[] currentEnums = Enum.GetNames(enumeration);
			string enumerationLine = "\tpublic enum " + enumeration.Name + " { ";

			foreach (string e in currentEnums)
			{
				enumerationLine += e + ", ";
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, false, enumUnityFilePath);
			enumerationsWithFlagChanges.Add(enumeration.Name);
		}

		/// <summary>
		/// Removes all run-time information stored about enumeration changes.
		/// </summary>
		public static void Clear()
		{
			enumerationAdditions.Clear();
			enumerationRemovals.Clear();
			enumerationsWithFlagChanges.Clear();
		}

		private static bool EnumerationValidationCheck(Type enumeration)
		{
			bool isValidated = false;

			if (enumeration.Namespace != "D_Quester")
			{
				HandleError(new ArgumentException(enumeration.ToString() + " is not in the namespace D_Quester. ", "enumeration"));
			}
			else if (!enumeration.IsEnum)
			{
				HandleError(new ArgumentException(enumeration.Name + " is not an enumeration.", "enumeration"));
			}
			else if (!ENUMERATION_INFORMATION.Keys.Contains(enumeration.Name))
			{
				string message = enumeration.Name + " is not a dynamic enumeration used by the D-Quester API."
					+ "\r\nThe following are supported dynamic enumerations:";

				foreach (string s in ENUMERATION_INFORMATION.Keys)
				{
					message += s + "\r\n";
				}

				HandleError(new ArgumentException(message, "enumeration"));
			}
			else
			{
				isValidated = true;
			}

			return isValidated;
		}

		private static bool EnumerationValidationCheck(string enumeration)
		{
			bool isValidated = true;

			if (!ENUMERATION_INFORMATION.Keys.Contains(enumeration))
			{
				string message = enumeration + " is not a dynamic enumeration used by the D-Quester API."
					+ "\r\nThe following are supported dynamic enumerations:\r\n";

				foreach (string s in ENUMERATION_INFORMATION.Keys)
				{
					message += s + "\r\n";
				}

				HandleError(new ArgumentException(message, "enumeration"));
				isValidated = false;
			}

			return isValidated;
		}

		private static void SaveEnumeration(string enumerationName, string enumerationLine, bool hasFlags = false, string enumUnityFilePath = "Assets/D_Quester/Enums/")
		{
			using (StreamWriter sw = new StreamWriter(enumUnityFilePath + enumerationName + ".cs"))
			{
				sw.NewLine = "\r\n";
				sw.WriteLine(FIRST_LINE);

				if (ENUMERATION_INFORMATION[enumerationName].Documentation != null && ENUMERATION_INFORMATION[enumerationName].Documentation != "")
				{
					sw.WriteLine(DOCUMENTATION_START_LINE);
					sw.WriteLine(ENUMERATION_INFORMATION[enumerationName].Documentation);
					sw.WriteLine(DOCUMENTATION_END_LINE);
				}

				if (hasFlags)
				{
					sw.WriteLine(FLAG_LINE);
				}

				sw.WriteLine(enumerationLine);
				sw.WriteLine(LAST_LINE);
			}
		}

		private static void HandleError(ArgumentException a)
		{
			Debug.LogException(a);
		}
	}
}
