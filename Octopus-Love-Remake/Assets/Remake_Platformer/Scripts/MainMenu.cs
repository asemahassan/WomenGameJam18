using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
  

    // Use this for initialization
    void Start()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1); //remake   
    }

    void Update()
    {
    }
}

