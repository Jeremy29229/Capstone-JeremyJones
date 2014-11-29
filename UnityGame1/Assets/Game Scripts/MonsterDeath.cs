using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterDeath : MonoBehaviour, IDeath
{
	public GameObject spawner;
	public Droper droper;

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

		Destroy(gameObject.transform.parent.gameObject);

	}
}
