using UnityEngine;
using System.Collections;

public class SpawningCoin : MonoBehaviour
{
	public GameObject _platforms=null;
	public GameObject _coins=null;
	public void Spawn()
	{
		
		_coins = GameObject.FindGameObjectsWithTag("coins");

	
		for (int i = 0; i < 10; i++) {
			//instantiate method
			GameObject spawnCoin = Instantiate (_coins, Vector3.zero, Quaternion.identity);

		}
	}
}
