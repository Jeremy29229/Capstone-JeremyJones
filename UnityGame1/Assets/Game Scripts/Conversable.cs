using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversable : MonoBehaviour, IInteractable
{
	private Conversation conversation;
	private ConversationManager cm;
	//private GameObject player;

	void Start()
	{
		//player = GameObject.Find("Player");
		conversation = gameObject.GetComponent<Conversation>();
		cm = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
	}

	public void InteractWith()
	{
		cm.ProcessDialog(conversation.start);
	}
}
