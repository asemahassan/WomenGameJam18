using UnityEngine;
using System.Collections;

public class SpawningCoin : MonoBehaviour
{
	public GameObject[] _platforms = null;
	public GameObject _coin = null;

	private int coinCount = 10;

	void Start ()
	{
		coinCount = Random.Range (1, coinCount);
	}

	public void Spawn ()
	{
		
		_platforms = GameObject.FindGameObjectsWithTag ("Platforms");

	
		for (int i = 0; i < coinCount; i++) {
			//instantiate method
			GameObject spawnCoin = Instantiate (_coin, Vector3.zero, Quaternion.identity);


		}
	}
}
