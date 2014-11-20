using UnityEngine;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// An NPC's single dialog.
	/// </summary>
	public class HierDialog : MonoBehaviour
	{
		private HierCorrespondence _correspondence;

		[HideInInspector]
		public HierCorrespondence Correspondence
		{
			get
			{
				if (_correspondence == null)
				{
					if (GetComponentInParent<HierDialogResponse>() != null)
					{
						_correspondence = GetComponentInParent<HierDialogResponse>().Correspondence;
					}
					else if(GetComponentInParent<HierConversation>() != null)
					{
						_correspondence = GetComponentInParent<HierConversation>().Correspondence;
					}
					else
					{
						Debug.LogException(new UnityException("HierDialog's parent object does not contain a HierDialogResponse or HierConversation."));
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
