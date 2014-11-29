using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour
{
	public GameObject player;
	public Vector3 velocity;
	public bool IsInConversation = false;

	private float horizontal;
	private float vertical;
	float jumpHeight = 300.0f;

	private Vector3 MovementVector { get; set; }

	public static TestController Instance;

	void Awake()
	{
		Instance = this;
		TestCamera.checkForCameraCreation();
	}

	void LateUpdate()
	{
		if (!IsInConversation)
		{
			if (Camera.main == null)
			{
				Debug.LogError("There needs to be a main camera for the third-person camera");
			}
			else
			{
				if (inputTaken())
				{

					horizontal = Input.GetAxisRaw("Horizontal");
					vertical = Input.GetAxisRaw("Vertical");
					TestMotor.Instance.MovementVector = new Vector3(horizontal, 0.0f, vertical);

					if (Input.GetKeyDown(KeyCode.Space))
					{
						if (CanJump())
						{
							Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
							rig.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
						}
					}
					TestMotor.Instance.UpdateMotor();
				}
			}
		}
	}

	private bool CanJump()
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		if (Physics.Linecast(player.transform.position + new Vector3(0.0f, 0.2f, 0.0f), player.transform.position - new Vector3(0.0f, 0.2f, 0.0f), out hit))
		{
			if (!hit.collider.isTrigger) result = true;
		}
		return result;
	}

	private bool inputTaken()
	{
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl));
	}
}
