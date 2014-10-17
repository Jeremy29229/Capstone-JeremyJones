using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
	public float Damage = 1;

	private List<Hurtable> targets;

	void Start()
	{
		targets = new List<Hurtable>();
	}

	void Update()
	{
		foreach (var target in targets)
		{
			target.currentHealth -= Damage * Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (!c.isTrigger)
		{
			Hurtable target = c.gameObject.GetComponent<Hurtable>();

			if (target != null)
			{
				targets.Add(target);
			}
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (!c.isTrigger)
		{
			Hurtable target = c.gameObject.GetComponent<Hurtable>();

			if (target != null)
			{
				targets.Remove(target);
			}
		}
	}
}
