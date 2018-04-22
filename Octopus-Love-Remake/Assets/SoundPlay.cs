using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{

	[SerializeField]
	AudioSource firstMs = null;

	[SerializeField]
	AudioSource secondMs = null;
	public float timeOutForFirtMusic = 5;

	[SerializeField]
	Complete completeCheck = null;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine ((playSecondTheme ()));
	}

	IEnumerator playSecondTheme ()
	{

		yield return new WaitForSeconds (timeOutForFirtMusic);
		firstMs.Stop ();
		firstMs.enabled = false;
		secondMs.enabled = true;
		secondMs.Play ();
	}


	void Update ()
	{
		if (completeCheck.hasComplete) {
			if (secondMs.enabled == true) {
				secondMs.enabled = false;
				secondMs.Stop ();
			}
		
			if (firstMs.enabled == true) {
				firstMs.Stop ();
				firstMs.enabled = false;
			}
		}
	}
}
