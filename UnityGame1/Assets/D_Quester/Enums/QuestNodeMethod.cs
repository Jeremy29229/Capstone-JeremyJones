using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_Quester
{
	/// <summary>
	/// Enumeration for each method in a QuestNode that can be subscribed to events.
	/// </summary>
	public enum QuestNodeMethod { StartUp, Progress, ResetProgress, Regress, NamedStartup, NamedProgress, NamedResetProgress, NamedRegress};
}
