using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene: MonoBehaviour
{
    [SerializeField]
    private float duration = 1.0f;
    [SerializeField]
    private int sceneIndex = 0;
    [SerializeField]
    private bool hasButton = false;

    //check if the scene should be loaded automatically or wait for a button click
    private void Start()
    {
        if (!hasButton)
        {
            StartCoroutine((Loading()));
        }
    }

    //call this on button press
    public void onStartClick ()
	{
        if (hasButton)
        {
            StartCoroutine((Loading()));
        }
	}

    //load scene after few seconds
	IEnumerator Loading ()
	{
		yield return new WaitForSeconds (duration);
		SceneManager.LoadScene (sceneIndex);
	}
}
