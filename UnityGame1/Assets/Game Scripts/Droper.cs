using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Droper : MonoBehaviour
{
	public GameObject[] drops;
	public float[] dropRate;

	public void Drop()
	{
		for (int i = 0; i < drops.Length; i++)
		{
			if (Random.value <= dropRate[i])
			{
				Instantiate(drops[i], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y / 2, gameObject.transform.position.z), Quaternion.identity);
				break;
			}
		}
	}
}
