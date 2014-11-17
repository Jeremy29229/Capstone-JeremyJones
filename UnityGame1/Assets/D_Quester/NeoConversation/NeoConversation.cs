using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// Represent the start of a conversation and the root of a dialog tree.
	/// </summary>
	public class NeoConversation : MonoBehaviour
	{
		private NeoCorrespondence _correspondence;

		[HideInInspector]
		public NeoCorrespondence Correspondence
		{
			get
			{
				if (_correspondence == null)
				{
					_correspondence = GetComponentInParent<NeoCorrespondence>();
				}
				return _correspondence;
			}
			private set
			{
				_correspondence = value;
			}
		}
	}
}
