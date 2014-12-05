using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace D_Quester
{
	/// <summary>
	/// Handles the displaying of conversations and the behind the scenes changes associated with the dialog response options.
	/// </summary>
	public class HierConversationManager : MonoBehaviour
	{
		/// <summary>
		/// Delegate for dialog responses.
		/// </summary>
		/// <param name="responseName"></param>
		[HideInInspector]
		public delegate void ResponseDelegate(string responseName);

		/// <summary>
		/// Event for dialog responses.
		/// </summary>
		[HideInInspector]
		public event ResponseDelegate ResponseEvent;

		/// <summary>
		/// Notifies all subscribes of the dialog response selected by name.
		/// </summary>
		/// <param name="responseName">Name of the dialog response.</param>
		[HideInInspector]
		public virtual void OnResponseEvent(string responseName)
		{
			if (ResponseEvent != null)
			{
				ResponseEvent(responseName);
			}
		}

		/// <summary>
		/// Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.
		/// </summary>
		[Tooltip("Name of the GameObject that represents the player. The player must have a component of type Inventory for this component to function correctly.")]
		public string PlayerObjectName = "Player";

		private Canvas UI;
		private GameObject[] buttons;
		private GameObject[] oldButtons;
		private Text[] responseTextDisplay;
		private Text npcText;
		private Text npcName;
		private Inventory inventory;
		private GameObject player;
		private HierDialog last;
		private int optionCount = 0;
		private bool ignoreSelection = false;

		void Start()
		{

			UI = gameObject.GetComponent<Canvas>();
			UI.enabled = false;

			player = GameObject.Find(PlayerObjectName);
			inventory = player.GetComponent<Inventory>();

			buttons = new GameObject[4];
			buttons[0] = GameObject.Find("HierDialog1Button");
			buttons[1] = GameObject.Find("HierDialog2Button");
			buttons[2] = GameObject.Find("HierDialog3Button");
			buttons[3] = GameObject.Find("HierDialog4Button");

			oldButtons = new GameObject[4];
			oldButtons[0] = GameObject.Find("Dialog1Button");
			oldButtons[1] = GameObject.Find("Dialog2Button");
			oldButtons[2] = GameObject.Find("Dialog3Button");
			oldButtons[3] = GameObject.Find("Dialog4Button");

			responseTextDisplay = new Text[4];
			responseTextDisplay[0] = buttons[0].GetComponentInChildren<Text>();
			responseTextDisplay[1] = buttons[1].GetComponentInChildren<Text>();
			responseTextDisplay[2] = buttons[2].GetComponentInChildren<Text>();
			responseTextDisplay[3] = buttons[3].GetComponentInChildren<Text>();

			npcText = GameObject.Find("NPCText").GetComponent<Text>();
			npcName = GameObject.Find("NPCName").GetComponent<Text>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				UI.enabled = false;
				Screen.lockCursor = true;
				TestCamera.Instance.IsInConversation = false;

				if (last != null)
				{
					last.Correspondence.GetComponent<Interactable>().IsActive = true;
				}
			}
		}

		/// <summary>
		/// Process the current NPC dialog in a conversation and display all associated dialog responses.
		/// </summary>
		/// <param name="d">Current dialog being processed.</param>
		public void ProcessDialog(HierDialog d)
		{
			foreach (GameObject g in oldButtons)
			{
				g.SetActive(false);
			}

			if (d == null)
			{
				UI.enabled = false;
				Screen.lockCursor = true;
				TestCamera.Instance.IsInConversation = false;
				if (last != null)
				{
					last.Correspondence.GetComponent<Interactable>().IsActive = true;
				}
			}
			else
			{
				optionCount = 0;
				last = d;
				UI.enabled = true;
				ignoreSelection = false;

				for (int i = 0; i < buttons.Length; i++)
				{
					buttons[i].SetActive(false);
				}

				List<HierDialogResponse> responses = new List<HierDialogResponse>();
					//d.GetComponentsInChildren<HierDialogResponse>();

				foreach (Transform child in d.transform)
				{
					if (child.parent == d.transform)
					{
						responses.Add(child.GetComponent<HierDialogResponse>());
					}
				}

				for (int i = 0; i < responses.Count && i < buttons.Length; i++)
				{
					if (responses[i] != null && IsDialogVisiable(responses[i]))
					{
						optionCount++;
						buttons[i].SetActive(true);
						buttons[i].GetComponentInChildren<Text>().text = responses[i].Text;
						npcText.text = "\"" + d.NPCDialog + "\"";
						npcName.text = d.Correspondence.GetComponent<Interactable>().InteractableName;
					}
				}

				if (optionCount == 0)
				{
					buttons[0].SetActive(true);
					buttons[0].GetComponentInChildren<Text>().text = "<Leave>";
					npcText.text = "\"" + d.NPCDialog + "\"";
					npcName.text = d.Correspondence.GetComponent<Interactable>().InteractableName;
					ignoreSelection = true;
				}
			}
		}

		/// <summary>
		/// Processes dialog response selected by the player.
		/// </summary>
		/// <param name="choiceIndex">The index of the dialog response selected by the player.</param>
		public void ProcessDialog(int choiceIndex)
		{
			HierDialogResponse choice = null;
			HierDialog next = null;
			//HierDialogResponse[] responses = new //last.GetComponentsInChildren<HierDialogResponse>();

			List<HierDialogResponse> responses = new List<HierDialogResponse>();

			foreach (Transform child in last.transform)
			{
				if (child.parent == last.transform)
				{
					responses.Add(child.GetComponent<HierDialogResponse>());
				}
			}

			if (!ignoreSelection && choiceIndex < responses.Count)
			{
				choice = responses[choiceIndex];
				OnResponseEvent(choice.DialogResponseName);
				if (!ignoreSelection)
				{
					choice.NumTimesSelected++;
				}
				next = choice.GetComponentInChildren<HierDialog>();

				if (choice != null && !string.IsNullOrEmpty(choice.NewConversationName))
				{
					choice.Correspondence.CurrentConversationName = choice.NewConversationName;
				}

				if (choice != null && choice.ItemRequirements != null)
				{
					foreach (var item in choice.ItemRequirements)
					{
						if (item.AreItemsTaken)
						{
							inventory.Remove(item.ItemName, item.Amount);
						}
					}
				}
			}

			ProcessDialog(next);
		}

		private bool IsDialogVisiable(HierDialogResponse d)
		{
			return d != null
				&& HasRequiredItems(d)
				&& HasRequiredQuestNodeState(d)
				&& HasCorrectSelectionState(d);
		}

		private bool HasRequiredItems(HierDialogResponse d)
		{
			bool hasItems = true;

			if (d.ItemRequirements != null)
			{
				foreach (var itemRequirement in d.ItemRequirements)
				{
					var item = inventory.items.FirstOrDefault(x => x.ItemName == itemRequirement.ItemName);

					if (item == null || item.Quantity < itemRequirement.Amount)
					{
						hasItems = false;
					}
				}
			}

			return hasItems;
		}

		private bool HasRequiredQuestNodeState(HierDialogResponse d)
		{
			bool isMet = true;

			if (d.Quest_Requirement != null && !d.Quest_Requirement.IsMet())
			{
				isMet = false;
			}

			return isMet;
		}

		private bool HasCorrectSelectionState(HierDialogResponse d)
		{
			return !d.IsOneTimeOption || d.NumTimesSelected == 0;
		}
	}
}