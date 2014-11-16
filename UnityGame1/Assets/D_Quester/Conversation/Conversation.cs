using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// Holds the starting dialog for a conversation with an NPC.
	/// </summary>
	public class Conversation : MonoBehaviour
	{
		/// <summary>
		/// The beginning dialog for a conversation with an NPC.
		/// </summary>
		[Tooltip("The beginning dialog for a conversation with an NPC.")]
		public Dialog Beginning;
	}
}
