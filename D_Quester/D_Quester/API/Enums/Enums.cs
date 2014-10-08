using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D_Quester
{
	static class Enums
	{
		private const string FIRST_LINE = "namespace D_Quester\r\n{";
		private const string LAST_LINE = "}";
		private const string FLAG_LINE = "\t[System.Flags]";
		public static readonly string[] DYNAMIC_ENUMERATION_NAMES = { "QuestObjectState" };
		//private static bool isThrowingExceptions = true;
		private static List<Type> enumsOperatedOn = new List<Type>();

		public static void AddEnum(string enumeration, string newEnumeration)
		{
			EnumerationValidationCheck(enumeration);
			AddEnum(Type.GetType("D_Quester." + enumeration), newEnumeration);
		}

		public static void AddEnum(Type enumeration, string newEnumerator)
		{
			EnumerationValidationCheck(enumeration);

			if (!Regex.IsMatch(newEnumerator, "^[a-zA-Z0-9_]+$"))
			{
				HandleError(new ArgumentException(newEnumerator + " is an invalid name. An enumerator must only contain alphanumeric characters and underscores.", "newEnumerator"));
				return;
			}

			string[] currentEnums = enumeration.GetEnumNames();

			if (currentEnums.Contains(newEnumerator))
			{
				HandleError(new ArgumentException(enumeration.Name + " already contains the enumeration " + newEnumerator + ".", "newEnumerator"));
				return;
			}

			string enumerationLine = "\tenum " + enumeration.Name + " { ";


			if (HasFlags(enumeration))
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

			SaveEnumeration(enumeration.Name, enumerationLine, HasFlags(enumeration));
		}

		public static void RemoveEnum(string enumeration, string enumerator)
		{
			EnumerationValidationCheck(enumeration);
			RemoveEnum(Type.GetType("D_Quester." + enumeration), enumerator);
		}

		public static void RemoveEnum(Type enumeration, string enumerator)
		{
			EnumerationValidationCheck(enumeration);

			string[] currentEnums = enumeration.GetEnumNames();

			if (!currentEnums.Contains(enumerator))
			{
				HandleError(new ArgumentException(enumeration.Name + " does not contain the enumeration, " + enumerator + ", you are attempting to remove.", "enumerator"));
				return;
			}

			string enumerationLine = "\tenum " + enumeration.Name + " { ";

			if (HasFlags(enumeration))
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

			SaveEnumeration(enumeration.Name, enumerationLine, HasFlags(enumeration));
		}

		public static void ResetToDefault(string enumeration)
		{
			EnumerationValidationCheck(enumeration);
			ResetToDefault(Type.GetType("D_Quester." + enumeration));
		}

		public static void ResetToDefault(Type enumeration)
		{
			EnumerationValidationCheck(enumeration);
			string enumerationLine = "";
			bool hasFlags = false;

			switch (enumeration.Name)
			{
				case "QuestObjectState":
					enumerationLine = "\tenum QuestObjectState { Uninitialized, NotStarted, InProgress, Completed, Failed };";
					hasFlags = false;
					break;
			}

			SaveEnumeration(enumeration.Name, enumerationLine, hasFlags);
		}

		public static void AddFlags(string enumeration)
		{
			EnumerationValidationCheck(enumeration);
			AddFlags(Type.GetType("D_Quester." + enumeration));
		}

		public static void AddFlags(Type enumeration)
		{
			EnumerationValidationCheck(enumeration);
			if (HasFlags(enumeration))
			{
				HandleError(new ArgumentException(enumeration.ToString() + " already has the flags attribute enabled.", "enumeration"));
				return;
			}

			string[] currentEnums = enumeration.GetEnumNames();
			string enumerationLine = "\tenum " + enumeration.Name + " { ";
			int currentFlag = 1;

			foreach (string e in currentEnums)
			{
				enumerationLine += e + " = " + currentFlag + ", ";
				currentFlag *= 2;
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, true);
		}

		public static void RemoveFlags(string enumeration)
		{
			EnumerationValidationCheck(enumeration);
			RemoveFlags(Type.GetType("D_Quester." + enumeration));
		}

		public static void RemoveFlags(Type enumeration)
		{
			EnumerationValidationCheck(enumeration);
			if (!HasFlags(enumeration))
			{
				HandleError(new ArgumentException(enumeration.ToString() + " does not have flags attribute enabled.", "enumeration"));
				return;
			}

			string[] currentEnums = enumeration.GetEnumNames();
			string enumerationLine = "\tenum " + enumeration.Name + " { ";

			foreach (string e in currentEnums)
			{
				enumerationLine += e + ", ";
			}

			enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
			enumerationLine += " }";

			SaveEnumeration(enumeration.Name, enumerationLine, false);
		}

		private static void EnumerationValidationCheck(Type enumeration)
		{
			if (enumeration.Namespace != "D_Quester")
			{
				HandleError(new ArgumentException(enumeration.ToString() + " is not in the namespace D_Quester. ", "enumeration"));
				return;
			}

			if (!enumeration.IsEnum)
			{
				HandleError( new ArgumentException(enumeration.Name + " is not an enumeration.", "enumeration"));
				return;
			}

			if (!DYNAMIC_ENUMERATION_NAMES.Contains(enumeration.Name))
			{
				string message = enumeration.Name + " is not a dynamic enumeration used by the D-Quester API."
					+ "\r\nThe following are supported dynamic enumerations:";

				foreach (string s in DYNAMIC_ENUMERATION_NAMES)
				{
					message += s + "\r\n";
				}

				HandleError(new ArgumentException(message, "enumeration"));
				return;
			}

			if (enumsOperatedOn.Contains(enumeration))
			{
				HandleError(new ArgumentException("Only one change is allowed to each enumeration per compilation.", "enumeration"));
				return;
			}
			else
			{
				enumsOperatedOn.Add(enumeration);
			}
		}

		private static void EnumerationValidationCheck(string enumeration)
		{
			if (!DYNAMIC_ENUMERATION_NAMES.Contains(enumeration))
			{
				string message = enumeration + " is not a dynamic enumeration used by the D-Quester API."
					+ "\r\nThe following are supported dynamic enumerations:\r\n";

				foreach (string s in DYNAMIC_ENUMERATION_NAMES)
				{
					message += s + "\r\n";
				}

				HandleError(new ArgumentException(message, "enumeration"));
				return;
			}
		}

		private static void SaveEnumeration(string enumerationName, string enumerationLine, bool hasFlags = false)
		{
			using (StreamWriter sw = new StreamWriter("../../API/Enums/" + enumerationName + ".cs"))
			{
				sw.NewLine = "\r\n";
				sw.WriteLine(FIRST_LINE);
				if (hasFlags)
				{
					sw.WriteLine(FLAG_LINE);
				}
				sw.WriteLine(enumerationLine);
				sw.WriteLine(LAST_LINE);
			}
		}

		private static bool HasFlags(Type enumeration)
		{
			return enumeration.IsDefined(typeof(FlagsAttribute), false);
		}

		//public static void DisableExceptionThrowing(bool disabledExceptions = true)
		//{
		//	isThrowingExceptions = !disabledExceptions;
		//}

		private static void HandleError(ArgumentException a)
		{
			//if (isThrowingExceptions)
			//{
			//	throw a;
			//}
			//else
			//{
			//	Console.WriteLine(a.ToString());
			//}
			throw a;
		}
	}
}
