using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{

	private Transform cameraMain = null;
	private Vector3 groundPos = Vector3.zero;
	private Vector3 myFixPos = Vector3.zero;
	// Use this for initialization
	void Start ()
	{
		cameraMain = Camera.main.transform;
		groundPos = cameraMain.transform.position;
		myFixPos = this.transform.position;
		
	}
	// Update is called once per frame
	void Update ()
	{

		if (cameraMain.position.y > groundPos.y) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y
			- cameraMain.position.y, this.transform.position.z);
		} else if (cameraMain.position.y < groundPos.y) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y
			+ cameraMain.position.y, this.transform.position.z);
		} else {
			this.transform.position = myFixPos;
		}
	}
}
