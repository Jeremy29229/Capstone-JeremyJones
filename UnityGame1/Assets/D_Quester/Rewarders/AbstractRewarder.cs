using UnityEngine;

/// <summary>
/// Abstract Rewarder class used to allow easy collection creation. Do not directly inherit from.
/// </summary>
public abstract class Rewarder : MonoBehaviour
{
	public abstract void Reward();
}
