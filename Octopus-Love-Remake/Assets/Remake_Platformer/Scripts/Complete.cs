﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour
{

	[SerializeField]
	AudioClip heartClip = null;
	GameObject particleSystem = null;

	[SerializeField]
	GameObject octoMale = null;

	[SerializeField]
	Sprite independentFrame = null;

	[SerializeField]
	SpriteRenderer player = null;

	public bool hasComplete = false;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if (other.tag == "Player" && !hasComplete) {


			//enable octopus male
			if (octoMale != null) {
				octoMale.SetActive (true);
				particleSystem = octoMale.transform.Find ("HeartParticles").gameObject;
			}

			//play heart particles
			if (particleSystem != null)
				particleSystem.GetComponent <ParticleSystem> ().Play ();
			//play heart sound
			AudioSource.PlayClipAtPoint (heartClip, this.transform.position);

			//change player frame to freedom
			player.sprite = independentFrame;
			player.transform.GetComponent <Animator> ().enabled = false;


			GameObject hh = GameObject.Find ("ui_healthDisplay");
			if (hh != null) {
				hh.SetActive (false);
			}
			//hasComplete = false;
			StartCoroutine (quit ());
		}
	}

	IEnumerator quit ()
	{

		yield return new WaitForSeconds (5.0f);
		Application.Quit ();
	}

}
