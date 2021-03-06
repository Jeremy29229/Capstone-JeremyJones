﻿
namespace D_Quester
{
	/// <summary>
	/// Must be implemented by any script that works with Interactable. Used to define what should happen with a player successful attempts to interact with the GameObject.
	/// </summary>
	public interface IInteractable
	{
		/// <summary>
		/// Gets called by Interactable script when a player attempts to interact with the GameObject.
		/// </summary>
		void InteractWith();
	}
}
