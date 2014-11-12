using UnityEngine;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// 
	/// </summary>
	public class Dialog : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public string NPCDialog;

		/// <summary>
		/// 
		/// </summary>
		[Tooltip("")]
		public DialogResponse[] Responses = new DialogResponse[4];

		private Interactable npc;

		void Start()
		{
			npc = (GetComponent<Interactable>()) ? GetComponent<Interactable>() : null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetNPCName()
		{
			return npc.InteractableName;
		}
	}
}
