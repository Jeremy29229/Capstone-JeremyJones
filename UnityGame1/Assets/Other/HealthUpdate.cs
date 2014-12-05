using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
	Hurtable playerHealth;
	Image playerImage;

	void Start()
	{
		playerHealth = GameObject.Find("Player").GetComponent<Hurtable>();
		playerImage = GameObject.Find("PlayerImage").GetComponent<Image>();
	}

	void Update()
	{
		GetComponent<Text>().text = "Health: " + (int)(playerHealth.currentHealth / playerHealth.MaxHealth * 100) +"%";

		if (playerHealth.currentHealth <= float.Epsilon)
		{
			Application.LoadLevel(0);
		}
		else
		{
			playerImage.fillAmount = playerHealth.currentHealth / playerHealth.MaxHealth;
		}
	}
}
