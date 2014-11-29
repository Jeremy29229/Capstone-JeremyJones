using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using D_Quester;

public class NPCDeath : MonoBehaviour, IDeath
{
	public GameObject spawner;
	public Droper droper;

	private Interactable interactable;
	private Hurtable hurtable;

	void Start()
	{
		interactable = GetComponentInChildren<Interactable>();
		hurtable = GetComponent<Hurtable>();
	}

	void Update()
	{
		interactable.AdditionalInformation = "(" + hurtable.currentHealth + ")";
	}

	public void Die()
	{
		if (spawner != null)
		{
			DingoSpawner d = spawner.GetComponent<DingoSpawner>();
			d.currentSpawned--;
		}

		if (droper != null)
		{
			droper.Drop();
		}

		Destroy(gameObject);

	}
}
