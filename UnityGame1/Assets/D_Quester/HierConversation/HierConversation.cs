using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace D_Quester
{
	/// <summary>
	/// Represent the start of a conversation and the root of a dialog tree.
	/// </summary>
	public class HierConversation : MonoBehaviour
	{
		private HierCorrespondence _correspondence;

		[HideInInspector]
		public HierCorrespondence Correspondence
		{
			get
			{
				if (_correspondence == null)
				{
					_correspondence = GetComponentInParent<HierCorrespondence>();
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
