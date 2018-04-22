using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
	[SerializeField]
	private Text coinText = null;
	public int coinCounter = 0;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void UpdateCoinCounter ()
	{
		if (coinText != null) {
			coinCounter++;
			coinText.text = ": " + coinCounter.ToString ();
		}
	}
}
