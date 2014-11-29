using UnityEngine;

public class Swing : MonoBehaviour
{
	bool isSwinging = false;
	bool isReturning = false;
	Weapon playerWeapon;
	float rotationAmount = 0.0f;
	public float RotationPerUpdate = 2.0f;

	void Start()
	{
		playerWeapon = GetComponentInChildren<Weapon>();
	}

	void Update()
	{
		if (isSwinging)
		{
			if (rotationAmount >= -180.0f && !isReturning)
			{
				transform.Rotate(new Vector3(0, -RotationPerUpdate, 0.0f));
				rotationAmount -= RotationPerUpdate;
			}
			else
			{
				isReturning = true;
				transform.Rotate(new Vector3(0, RotationPerUpdate, 0.0f));
				rotationAmount += RotationPerUpdate;
				if (rotationAmount >= 0)
				{
					isReturning = false;
					isSwinging = false;
				}
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) && !TestCamera.Instance.IsInConversation)
			{
				isSwinging = true;
			}
		}
	}
}
