﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace D_Quester
{
	public class InteractionManager : MonoBehaviour
	{
		public List<GameObject> Interactables;
		public string PlayerObjectName = "Player";

		private GameObject player;
		private Canvas UI;

		void Start()
		{
			UI = (Canvas)GameObject.Find("InteractionGUI").GetComponent(typeof(Canvas));
			player = GameObject.Find(PlayerObjectName);
		}

		void Update()
		{
			GameObject closest = null;
			float minDistance = float.MaxValue;

			foreach (var v in Interactables)
			{
				if (v == null)
				{
					Interactables.Remove(v);
					break;
				}
				else
				{
					float currentDistance = Vector3.Distance(player.transform.position, v.transform.position);
					if (currentDistance < minDistance && v.GetComponent<Interactable>().IsActive)
					{
						minDistance = currentDistance;
						closest = v;
					}
				}
			}

			if (closest == null)
			{
				UI.enabled = false;
			}
			else
			{
				closest.GetComponent<Interactable>().updateGUIText();
				closest.GetComponent<Interactable>().InteractionUpdate();
			}
		}
	}
}