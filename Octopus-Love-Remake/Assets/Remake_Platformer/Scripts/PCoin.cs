using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCoin : MonoBehaviour
{
	// How much health the crate gives the player.
	public AudioClip collect;
	// The sound of the coin being collected.

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if (other.tag == "Player") {
			// Get a reference to the player health script.
			HUDManager hud = other.GetComponent<HUDManager> ();

			// Increasse the player's health by the health bonus but clamp it at 100.
			hud.UpdateCoinCounter ();

			// Play the collection sound.
			AudioSource.PlayClipAtPoint (collect, transform.position);
				
			// Destroy the coin.
			Destroy (this.gameObject);
		}
	}
}
