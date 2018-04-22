using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private AudioSource click;
  

    // Use this for initialization
    IEnumerator wait()
    {
		yield return new WaitForSeconds (1.0f);
    }

	public void LoadGame(int sceneNum)
    {
		StartCoroutine (wait());
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

