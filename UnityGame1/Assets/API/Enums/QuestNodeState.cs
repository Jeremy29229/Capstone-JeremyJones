namespace D_Quester
{
	/// <summary>
	/// Base states for all quest objects.
	/// </summary>
	[System.Flags]
	public enum QuestNodeState { Uninitialized = 1, NotStarted = 2, InProgress = 4, Completed = 8, Failed = 16 }
}
