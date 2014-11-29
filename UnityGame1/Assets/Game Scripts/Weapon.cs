using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
	public float DamagePerSecond = 1.0f;
	public bool HasConstantDamage = true;

	public float DamagePerHit = 0.25f;

	private List<Hurtable> targets;
	private GameObject theThing;
	private GameObject flame;
	//ParticleSystem e;

	void Start()
	{
		targets = new List<Hurtable>();
		if (GetComponentInChildren<ParticleSystem>() != null)
		{

			//e = GetComponentInChildren<ParticleSystem>();
			//e.enableEmission = false;
		}

		if(transform.FindChild("Flame") != null)
		{
			flame = transform.FindChild("Flame").gameObject;
			foreach (ParticleSystem ps in flame.GetComponentsInChildren<ParticleSystem>())
			{
				ps.enableEmission = false;
			}
		}
		
		
	}

	void Update()
	{
		if (HasConstantDamage)
		{
			foreach (var target in targets)
			{
				target.currentHealth -= DamagePerSecond * Time.deltaTime;
			}

			if (theThing == null)
			{
				//e.enableEmission = false;
				if (flame != null)
				{
					//flame.SetActive(false);

					foreach (Transform child in flame.transform)
					{
						if (child.gameObject.GetComponent("EllipsoidParticleEmitter") != null)
						{
							var ps = child.gameObject.GetComponent("EllipsoidParticleEmitter");
							ps.particleEmitter.emit = false;
						}
					}
					foreach (var light in flame.GetComponentsInChildren<Light>())
					{
						light.enabled = false;
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (HasConstantDamage)
		{
			if (!c.isTrigger)
			{
				Hurtable target = c.gameObject.GetComponent<Hurtable>();

				if (target != null)
				{
					targets.Add(target);
				}

				if (flame != null)
				{
					foreach (Transform child in flame.transform)
					{
						if (child.gameObject.GetComponent("EllipsoidParticleEmitter") != null)
						{
							var ps = child.gameObject.GetComponent("EllipsoidParticleEmitter");
							ps.particleEmitter.emit = true;
						}
					}
					foreach (var light in flame.GetComponentsInChildren<Light>())
					{
						light.enabled = true;
					}
				}

				//if (e != null)
				//{
				//	e.enableEmission = true;
				//}

				theThing = c.gameObject;
			}
		}
		else
		{
			Hurtable target = c.gameObject.GetComponent<Hurtable>();

			if (target != null)
			{
				target.currentHealth -= DamagePerHit;
			}
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (HasConstantDamage)
		{
			if (!c.isTrigger)
			{
				Hurtable target = c.gameObject.GetComponent<Hurtable>();

				if (target != null)
				{
					targets.Remove(target);
				}

				if (flame != null)
				{
					foreach (Transform child in flame.transform)
					{
						if (child.gameObject.GetComponent("EllipsoidParticleEmitter") != null)
						{
							var ps = child.gameObject.GetComponent("EllipsoidParticleEmitter");
							ps.particleEmitter.emit = false;
						}
					}
					foreach (var light in flame.GetComponentsInChildren<Light>())
					{
						light.enabled = false;
					}
				}
			}
		}
		else
		{

		}
	}
}
