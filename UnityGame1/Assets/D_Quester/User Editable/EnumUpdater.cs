using UnityEditor;
using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Allows D_Quester Dynamic Enumerations to be updated.
	/// </summary>
	[ExecuteInEditMode]
	public class EnumUpdater : MonoBehaviour
	{
		/// <summary>
		/// Runs the YourEnumChanges function whenever "Run Enum Updater" button is clicked.
		/// </summary>
		public void RunEnumUpdater()
		{
			print("Running EnumUpdater script.");
			YourEnumChanges();
			Enums.Clear();
			AssetDatabase.Refresh();
			print("EnumUpdater script completed.");
		}

		/// <summary>
		/// Add any changes you want to make to D_Quester dynamic enumerations. It is generally a good idea to remove or comment out this code after you run the script once.
		/// </summary>
		void YourEnumChanges()
		{
			Enums.AddEnum("QuestNodeState", "test1");

			//Enums.AddEnum("QuestNodeState", "test2");
			//Enums.AddEnum("QuestNodeState", "test3");

			//Enums.ResetToDefault("QuestNodeState");

			//Enums.AddEnum("QuestNodeState", "test1");
			//Enums.AddEnum("QuestNodeState", "test2");
			//Enums.AddEnum("QuestNodeState", "test3");
			//Enums.AddFlags("QuestNodeState");
			//Enums.RemoveFlags("QuestNodeState");

			//Enums.AddEnum("QuestNodeState", "test1");
			//Enums.AddEnum("QuestNodeState", "test2");
			//Enums.AddEnum("QuestNodeState", "test3");
			//Enums.RemoveEnum("QuestNodeState", "test2");

			//Enums.RemoveEnum("QuestNodeState", "NotStarted");
			//Enums.RemoveEnum("QuestNodeState", "lolnope");
			//Enums.RemoveEnum(typeof(int), "lolnope");
			//Enums.AddEnum(typeof(int), "lolnope");
			//Enums.AddEnum("QuestNodeState", "_");
			//Enums.AddEnum("QuestNodeState", null);
		}
	}
}
