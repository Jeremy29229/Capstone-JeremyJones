﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace D_Quester
{
	/// <summary>
	/// Allows a game object to be interacted with. GameObject must also have another script component that implements IIteractable.
	/// </summary>
	public class Interactable : MonoBehaviour
	{
		private IInteractable behavior;
		public float InteractionRadius = 2.0f;
		public KeyCode InteractionKey = KeyCode.E;
		public string PlayerObjectName = "Player";
		private GameObject Player;
		public bool IsActive = true;
		private Canvas UI;
		public string Action;
		public string InteractableName;
		public string AdditionalInformation = "";

		void Start()
		{
			UI = (Canvas)GameObject.Find("InteractionGUI").GetComponent(typeof(Canvas));
			UI.enabled = false;
			Player = GameObject.Find(PlayerObjectName);

			if (Player == null)
			{
				throw new UnassignedReferenceException("Player must be defined");
			}

			behavior = (IInteractable)gameObject.GetComponent("IInteractable");

			if (behavior == null)
			{
				throw new UnassignedReferenceException("A script that implements IInteractable must be a component in the same GameObject as this script.");
			}

			GameObject.Find("InteractionManager").GetComponent<InteractionManager>().Interactables.Add(gameObject);
		}

		public void InteractionUpdate()
		{
			if (Vector3.Distance(Player.transform.position, gameObject.transform.position) <= InteractionRadius)
			{
				UI.enabled = true;
				if (Input.GetKeyDown(InteractionKey))
				{
					behavior.InteractWith();
				}
			}
			else
			{
				UI.enabled = false;
			}
		}

		public void updateGUIText()
		{
			UI.GetComponentInChildren<Text>().text = "Press " + InteractionKey.ToString() + " to " + Action + " " + InteractableName;
			if (!string.IsNullOrEmpty(AdditionalInformation))
			{
				UI.GetComponentInChildren<Text>().text += " " + AdditionalInformation;
			}

			UI.enabled = false;
		}
	}
}