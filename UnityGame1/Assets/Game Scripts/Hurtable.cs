using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Hurtable : MonoBehaviour
{
	public float MaxHealth = 1;
	public float currentHealth;

	/// <summary>
	/// Delegate for picking up items.
	/// </summary>
	/// <param name="itemName">Name of the picked up item.</param>
	[HideInInspector]
	public delegate void PickupDelegate(string itemName);

	/// <summary>
	/// Event for picking up items.
	/// </summary>
	[HideInInspector]
	public event PickupDelegate PickupEvent;



	/// <summary>
	/// Called whenever an item is picked up.
	/// </summary>
	/// <param name="name">Name of the picked up item.</param>
	[HideInInspector]
	public virtual void OnPickUpEvent(string name)
	{
		if (PickupEvent != null)
		{
			PickupEvent(name);
		}
	}

	void Start()
	{
		currentHealth = MaxHealth;
	}

	void Update()
	{
		if (currentHealth < float.Epsilon)
		{
			IDeath target = (IDeath)gameObject.GetComponent(typeof(IDeath));

			if (target != null)
			{
				OnPickUpEvent(gameObject.name);
				target.Die();
			}
		}
	}

	//void OnMouseEnter()
	//{
	//	UI.GetComponent<Canvas>().enabled = true;
	//	monsterName.text = gameObject.name;
	//	health.text = "Health: " + currentHealth;
	//}

	//void OnMouseExit()
	//{
	//	UI.GetComponent<Canvas>().enabled = false;
	//}
}
