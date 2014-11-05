using UnityEngine;
using System;

namespace D_Quester
{
	public class DialogResponse : MonoBehaviour
	{
		public string DialogResponseName = "";
		public Dialog Resulting;
		public Conversation NewCurrent;
		public string Text = "";
		public bool IsOneTimeOption = false;
		public int NumTimesSelected = 0;
		public ItemRequirement[] Items;
		public QuestRequirement requirement;
	}
}
