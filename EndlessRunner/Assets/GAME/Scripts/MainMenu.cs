using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string playLevel;

	public void PlayGame(){
		SceneManager.LoadScene(playLevel);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
