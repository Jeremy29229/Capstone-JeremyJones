using UnityEngine;

public class Movement : MonoBehaviour
{
	private float moveSpeed = 50.0f;
	private float rotationSpeed = 150.0f;

	GameObject c;

	void Start()
	{
		c = GameObject.Find("Main Camera");
	}

	void Update()
	{
		if (Input.GetKey("a"))
		{
			gameObject.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
		}

		if (Input.GetKey("w"))
		{
			//camera.renderer.
			//gameObject.transform.localPosition += new Vector3(1.0f, 0, 0);
			//gameObject.transform.position += c.camera.transform.rotation.;
			//gameObject.rigidbody.AddForce(speed, 0, 0);

			gameObject.transform.position += gameObject.transform.rotation * new Vector3(moveSpeed * Time.deltaTime, 0, 0);
		}

		if (Input.GetKey("d"))
		{
			//gameObject.transform.position += -c.camera.transform.position * speed;
			gameObject.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
		}
		
		if (Input.GetKey("s"))
		{
			gameObject.transform.position -= gameObject.transform.rotation * new Vector3(moveSpeed * Time.deltaTime, 0, 0);
		}
	}
}
