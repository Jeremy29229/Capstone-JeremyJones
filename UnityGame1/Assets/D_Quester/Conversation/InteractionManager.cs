using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace D_Quester
{
	public class InteractionManager : MonoBehaviour
	{
		/// <summary>
		/// List of the all existing interactables that the placer can see if they are close enough to them. Do not directly add to this list. Use the Interactable script.
		/// </summary>
		[Tooltip("List of the all existing interactables that the placer can see if they are close enough to them. Do not directly add to this list. Use the Interactable script.")]
		public List<GameObject> Interactables;

		/// <summary>
		/// Name of the GameObject the player is in.
		/// </summary>
		[Tooltip("Name of the GameObject the player is in.")]
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
