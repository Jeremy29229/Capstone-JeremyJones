using UnityEngine;
using D_Quester;

public class DingoScene : MonoBehaviour
{
	bool triggered = false;
	void Start()
	{
		triggered = false;
	}

	void Update()
	{
		if (GetComponent<RewardableBool>().Value && !triggered)
		{
			triggered = true;
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("DeadDingo"))
			{
				foreach(Transform child in g.transform)
				{
					child.gameObject.SetActive(true);
				}
			}

			GameObject.Find("DingoSpawner").SetActive(false);

			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Dingo"))
			{
				g.SetActive(false);
			}
		}
	}
}
