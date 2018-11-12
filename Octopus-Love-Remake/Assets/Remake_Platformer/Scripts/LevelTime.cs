using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTime : MonoBehaviour {
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private float timeLeft = 60.0f;
    [SerializeField]
    private Text timerText = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            // ... destroy the player.
            if (player != null)
            {
                Destroy(player.gameObject);
            }
            // ... reload the level.
            StartCoroutine("ReloadGame");
        }
        else
        {
            if (timerText != null)
                timerText.text = timeLeft.ToString("F1") + " s";
        }
    }

    IEnumerator ReloadGame()
    {
        // ... pause briefly
        yield return new WaitForSeconds(1.0f);
        // ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
