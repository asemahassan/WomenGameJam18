using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private AudioSource click;
  

    // Use this for initialization
    void Start()
    {

    }

	public void LoadGame(int sceneNum)
    {
			Debug.Log("Scene loaded: " + sceneNum);
			SceneManager.LoadScene(sceneNum);
    }

	public void QuitGame(){
	
		Application.Quit ();
	}
	public void Onclick()
	{
		if(click!=null)
			click.Play();
	}


    void Update()
    {
    }
}

