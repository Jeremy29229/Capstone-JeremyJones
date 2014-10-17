using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using D_Quester;
using UnityEditor;

/// <summary>
/// Allows D_Quester Dynamic Enumerations to be updated.
/// </summary>
[ExecuteInEditMode]
public class EnumUpdater : MonoBehaviour
{
	/// <summary>
	/// Controls when this script is run. Click Run Script in an instance of this script in the editor to apply your changes to D_Quester's dynamic enumerations.
	/// </summary>
	public bool RunScript = false;

	/// <summary>
	/// Proof of live update.
	/// </summary>
	public QuestNodeState qns;

	/// <summary>
	/// Runs the YourEnumChanges function whenever the RunScript bool is true. Can be easily run once by checking RunScript in an instance of this script in the editor.
	/// </summary>
	void Update()
	{
		if (RunScript)
		{
			print("Running EnumUpdater script.");
			YourEnumChanges();
			Enums.Clear();
			print("EnumUpdater script completed.");
			RunScript = false;
			AssetDatabase.Refresh();
		}
	}

	/// <summary>
	/// Add any changes you want to make to D_Quester dynamic enumerations. It is generally a good idea to remove or comment out this code after you run the script once.
	/// </summary>
	void YourEnumChanges()
	{
		//Enums.AddEnum("QuestNodeState", "test1");
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
	}
}
