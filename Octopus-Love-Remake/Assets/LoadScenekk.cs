using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenekk : MonoBehaviour
{
	public float dur = 2.0f;
	public int sceneIndex = 3;
	// Use this for initialization
	void Start ()
	{

		StartCoroutine ((LoadSceneASS ()));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator LoadSceneASS ()
	{
		yield return new WaitForSeconds (dur);
		SceneManager.LoadScene (sceneIndex);
	}
}
