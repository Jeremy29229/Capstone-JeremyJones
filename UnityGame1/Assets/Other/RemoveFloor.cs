using UnityEngine;
using D_Quester;

public class RemoveFloor : MonoBehaviour
{
	bool isTrigger = false;
	RewardableBool floor;

	void Start()
	{
		floor = GetComponent<RewardableBool>();
	}

	void Update()
	{
		if (floor.Value && !isTrigger)
		{
			GameObject.Find("Terrain").SetActive(false);
			isTrigger = true;
		}
	}
}
