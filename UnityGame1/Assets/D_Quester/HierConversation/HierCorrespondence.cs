using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds the conversation the NPC will use when the player interacts with them. Can be changed with a dialog response or other script.
	/// </summary>
	public class HierCorrespondence : MonoBehaviour
	{
		/// <summary>
		/// Name of the current conversation the NPC will have with the player.
		/// </summary>
		[Tooltip("Name of the current conversation the NPC will have with the player.")]
		public string CurrentConversationName = "";
	}
}
