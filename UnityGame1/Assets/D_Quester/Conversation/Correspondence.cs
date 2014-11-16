using UnityEngine;

namespace D_Quester
{
	/// <summary>
	/// Holds the conversation the NPC will use when the player interacts with them. Can be changed with a dialog response or other script.
	/// </summary>
	public class Correspondence : MonoBehaviour
	{
		/// <summary>
		/// Current conversation the NPC will have with the player.
		/// </summary>
		[Tooltip("Current conversation the NPC will have with the player.")]
		public Conversation Current;
	}
}
