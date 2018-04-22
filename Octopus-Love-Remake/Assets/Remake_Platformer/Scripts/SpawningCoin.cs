using UnityEngine;
using System.Collections;

public class SpawningCoin : MonoBehaviour
{
	private GameObject _coin = null;
	private int coinCount = 5;
	private float offsetX = 0;
	private float coinRadius = 0;

	void Start ()
	{
		
		coinCount = Random.Range (1, coinCount);
		if (_coin == null) {
			_coin = Resources.Load ("EURCoin") as GameObject;
			coinRadius = _coin.GetComponent<CircleCollider2D> ().radius;
		}
		offsetX = this.GetComponent<BoxCollider2D> ().offset.x;
		//Debug.Log ("Offset X: " + offsetX);

		Spawn ();
	}

	public void Spawn ()
	{
		for (int i = 0; i < coinCount; i++) {

			//instantiate method
			GameObject spawnCoin = Instantiate (_coin, Vector3.zero, Quaternion.identity);
			spawnCoin.transform.SetParent (this.transform);
			spawnCoin.transform.localPosition = new Vector3 (offsetX + (i * coinRadius), 1.5f, 0);
		}
	}
}
