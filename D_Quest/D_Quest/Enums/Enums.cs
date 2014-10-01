using System;
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
        private const string FIRST_LINE = "namespace D_Quester\n{";
        private const string LAST_LINE = "}";
        public static readonly string[] DYNAMIC_ENUMERATION_NAMES = { "QuestObjectState" };

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
                throw new ArgumentException(newEnumerator + " is an invalid name. An enumerator must only contain alphanumeric characters and underscores.", "newEnumerator");
            }

            string[] currentEnums = enumeration.GetEnumNames();

            if (currentEnums.Contains(newEnumerator))
            {
                throw new ArgumentException(enumeration.Name + " already contains the enumeration " + newEnumerator + ".", "newEnumerator");
            }

            string enumerationLine = "enum " + enumeration.Name + " { ";

            foreach (string e in currentEnums)
            {
                enumerationLine += e + ", ";
            }
            enumerationLine += newEnumerator + ", ";

            enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
            enumerationLine += " }";

            SaveEnumeration(enumeration.Name, enumerationLine);
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
                throw new ArgumentException(enumeration.Name + " does not contain the enumeration, " + enumerator + ", you are attempting to remove.", "enumerator");
            }

            string enumerationLine = "enum " + enumeration.Name + " { ";

            foreach (string e in currentEnums)
            {
                if (e != enumerator)
                {
                    enumerationLine += e + ", ";
                }
            }
    
            enumerationLine = enumerationLine.Substring(0, enumerationLine.Length - 2);
            enumerationLine += " }";

            SaveEnumeration(enumeration.Name, enumerationLine);
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

            switch (enumeration.Name)
            {
                case "QuestObjectState":
                    enumerationLine = "    enum QuestObjectState { Uninitialized, NotStarted, InProgress, Completed, Failed };";
                    break;
            }

            SaveEnumeration(enumeration.Name, enumerationLine);
        }

        private static void EnumerationValidationCheck(Type enumeration)
        {
            if (enumeration.Namespace != "D_Quester")
            {
                throw new ArgumentException(enumeration.ToString() + " is not in the namespace D_Quester. ", "enumeration");
            }

            if (!enumeration.IsEnum)
            {
                throw new ArgumentException(enumeration.Name + " is not an enumeration.", "enumeration");
            }

            if (!DYNAMIC_ENUMERATION_NAMES.Contains(enumeration.Name))
            {
                string message = enumeration.Name + " is not a dynamic enumeration used by the D-Quester API."
                    + "\nThe following are supported dynamic enumerations:";

                foreach (string s in DYNAMIC_ENUMERATION_NAMES)
                {
                    message += s + "\n";
                }

                throw new ArgumentException(message, "enumeration");
            }
        }

        private static void EnumerationValidationCheck(string enumeration)
        {
            if (!DYNAMIC_ENUMERATION_NAMES.Contains(enumeration))
            {
                string message = enumeration + " is not a dynamic enumeration used by the D-Quester API."
                    + "\nThe following are supported dynamic enumerations:\n";

                foreach (string s in DYNAMIC_ENUMERATION_NAMES)
                {
                    message += s + "\n";
                }

                throw new ArgumentException(message, "enumeration");
            }
        }

        private static void SaveEnumeration(string enumerationName, string enumerationLine)
        {
            using (StreamWriter sw = new StreamWriter("../../Enums/" + enumerationName + ".cs"))
            {
                sw.NewLine = "\n";
                sw.WriteLine(FIRST_LINE);
                sw.WriteLine(enumerationLine);
                sw.WriteLine(LAST_LINE);
            }
        }
    }
}
