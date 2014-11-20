using UnityEngine;
using D_Quester;
using UnityEngine.UI;

public class Examinable : Interactable
{
	public string MonsterName = "Nope";
	public float Health = 1.0f;

	private IInteractable behavior;
	private GameObject Player;
	private Canvas UI;
	private Hurtable h;



	public override void Start()
	{
		UI = (Canvas)GameObject.Find(InteractionGUIObjectName).GetComponent(typeof(Canvas));
		UI.enabled = false;

		Player = GameObject.Find(PlayerObjectName);
		if (Player == null)
		{
			throw new UnassignedReferenceException("Unable to find player's GameObject. Make sure the PlayerObjectName matches the Player's GameObject name.");
		}

		behavior = (IInteractable)gameObject.GetComponent("IInteractable");
		if (behavior == null)
		{
			throw new UnassignedReferenceException("A script that implements IInteractable must be a component in the same GameObject as this script.");
		}

		GameObject.Find(InteractionManagerObjectName).GetComponent<InteractionManager>().Interactables.Add(gameObject);

		h = GetComponent<Hurtable>();

		base.Start();
	}

	/// <summary>
	/// Called by the InteractionManager component to update the interaction popup when this object is the closest in-range.
	/// </summary>
	public override void updateGUIText()
	{
		UI.GetComponentInChildren<Text>().text = MonsterName + " " + Health;

	}

	void Update()
	{
		Health = h.currentHealth;
	}
}
