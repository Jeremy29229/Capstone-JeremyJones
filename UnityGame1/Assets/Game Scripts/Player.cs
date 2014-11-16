using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
#pragma warning disable 0414, 0219
	GameObject UI;
	Hurtable hurtable;
	MeshCollider thwakerTrigger;
	MeshRenderer thwakerGraphic;

	bool isWeaponActive = true;

	void Start()
	{
		UI = GameObject.Find("PlayerGUI");
		hurtable = GetComponent<Hurtable>();
		thwakerTrigger = GameObject.Find("thwaker").GetComponent<MeshCollider>();
		thwakerGraphic = GameObject.Find("thwaker").GetComponent<MeshRenderer>();
	}

	void Update()
	{
		int percentHealth = (int)(100 * (hurtable.currentHealth / hurtable.MaxHealth));
		//UI.GetComponentInChildren<Text>().text = "Health: " + percentHealth + "%";

		if (Input.GetKeyDown("l"))
		{
			if (isWeaponActive)
			{
				thwakerTrigger.enabled = false;
				thwakerGraphic.enabled = false;
			}
			else
			{
				thwakerTrigger.enabled = true;
				thwakerGraphic.enabled = true;
			}

			isWeaponActive = !isWeaponActive;
		}
	}
#pragma warning restore 0414, 0219
}
