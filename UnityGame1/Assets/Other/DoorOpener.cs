using UnityEngine;
using D_Quester;

public class DoorOpener : MonoBehaviour
{
	bool wasTrigger = false;
	void Start()
	{

	}

	void Update()
	{
		if (GameObject.Find("WallQuestInfo").GetComponent<RewardableBool>().Value && !wasTrigger)
		{
			wasTrigger = true;

			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Door"))
			{
				g.transform.Rotate(new Vector3(0, 90, 0));
			}
		}
	}
}
