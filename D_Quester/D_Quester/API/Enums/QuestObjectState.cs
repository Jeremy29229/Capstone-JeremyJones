namespace D_Quester
{
	[System.Flags]
	enum QuestObjectState { Uninitialized = 1, NotStarted = 2, InProgress = 4, Completed = 8, Failed = 16, Meow = 32 }
}
