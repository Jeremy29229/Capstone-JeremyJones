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
	GameObject sword;
	GameObject swordSheathed;

	bool isWeaponActive = true;

	void Start()
	{
		UI = GameObject.Find("PlayerGUI");
		hurtable = GetComponent<Hurtable>();
		sword = GameObject.Find("jcSword");
		swordSheathed = GameObject.Find("cSword_");
		swordSheathed.SetActive(false);
		thwakerTrigger = GameObject.Find("jcSword").GetComponent<MeshCollider>();
		thwakerGraphic = GameObject.Find("jcSword").GetComponent<MeshRenderer>();
		bool isWeaponActive = true;
	}

	void Update()
	{
		int percentHealth = (int)(100 * (hurtable.currentHealth / hurtable.MaxHealth));
		//UI.GetComponentInChildren<Text>().text = "Health: " + percentHealth + "%";

		if (Input.GetKeyDown("l") && !TestCamera.Instance.IsInConversation)
		{
			if (isWeaponActive)
			{
				sword.SetActive(false);
				swordSheathed.SetActive(true);
			}
			else
			{
				sword.SetActive(true);
				swordSheathed.SetActive(false);
			}

			isWeaponActive = !isWeaponActive;
		}
	}
#pragma warning restore 0414, 0219
}
