using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Hurtable : MonoBehaviour
{
	public float MaxHealth = 1;
	public float currentHealth;

	private GameObject UI;

#pragma warning disable 0414
	private Text monsterName;
	private Text health;
#pragma warning restore 0414

	void Start()
	{
		currentHealth = MaxHealth;
		UI = GameObject.Find("NameDisplayManager");
		UI.GetComponent<Canvas>().enabled = true;
		monsterName = GameObject.Find("MonsterNameText").GetComponent<Text>();
		health = GameObject.Find("HealthText").GetComponent<Text>();
		UI.GetComponent<Canvas>().enabled = false;
	}

	void Update()
	{
		if (currentHealth < float.Epsilon)
		{
			IDeath target = (IDeath)gameObject.GetComponent(typeof(IDeath));

			if (target != null)
			{
				target.Die();
			}
		}
	}

	//void OnMouseEnter()
	//{
	//	UI.GetComponent<Canvas>().enabled = true;
	//	monsterName.text = gameObject.name;
	//	health.text = "Health: " + currentHealth;
	//}

	//void OnMouseExit()
	//{
	//	UI.GetComponent<Canvas>().enabled = false;
	//}
}
