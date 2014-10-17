using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
	void Start()
	{
		if (gameObject.GetComponent<Interactable>() != null)
		{
			npcName = gameObject.GetComponent<Interactable>().InteractableName;
		}
	}

	public string npcName;

	public string npcText;

	public string[] responseText;

	public Dialog[] responseObject;

	public D_Quester.QuestRequirement[] requirements;

	//public InventoryItem t;
}
