using UnityEngine;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// An NPC's single dialog.
	/// </summary>
	public class NeoDialog : MonoBehaviour
	{
		private NeoCorrespondence _correspondence;

		[HideInInspector]
		public NeoCorrespondence Correspondence
		{
			get
			{
				if (_correspondence == null)
				{
					if (GetComponentInParent<NeoDialogResponse>() != null)
					{
						_correspondence = GetComponentInParent<NeoDialogResponse>().Correspondence;
					}
					else if(GetComponentInParent<NeoConversation>() != null)
					{
						_correspondence = GetComponentInParent<NeoConversation>().Correspondence;
					}
					else
					{
						Debug.LogException(new UnityException("NeoDialog's parent object does not contain a NeoDialogResponse or NeoConversation."));
					}
				}
				return _correspondence;
			}
			private set
			{
				_correspondence = value;
			}
		}

		/// <summary>
		/// "Text of the NPC's dialog."
		/// </summary>
		[Tooltip("Text of the NPC's dialog.")]
		public string NPCDialog = "";
	}
}
