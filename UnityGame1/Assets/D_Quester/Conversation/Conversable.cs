using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace D_Quester
{
	public class Conversable : MonoBehaviour, IInteractable
	{
		private Correspondence correspondence;
		private ConversationManager cm;
		private GameObject player;

		void Start()
		{
			player = GameObject.Find("Player");
			correspondence = gameObject.GetComponent<Correspondence>();
			cm = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
		}

		public void InteractWith()
		{
			GetComponent<Interactable>().IsActive = false;
			cm.ProcessDialog(correspondence.Current.Beginning);
		}
	}
}
