using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
  

    // Use this for initialization
    void Start()
    {

    }

	public void LoadGame(int sceneNum)
    {
			Debug.Log("Scene loaded: " + sceneNum);
			SceneManager.LoadScene(sceneNum);
    }

    void Update()
    {
    }
}

