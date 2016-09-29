using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;

	public PlayerControl thePlayer;
	private ScoreManager theScoreManager;
	private Vector3 playerStartPoint;

	private PlatformDestroyer[] platformList;

	public DeathMenu theDeathScreen;


	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		theScoreManager = FindObjectOfType<ScoreManager> ();;
	}

	void Update () {
		
	}

	public void RestartGame(){
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false); // Disappear
		theDeathScreen.gameObject.SetActive(true);
	}

	public void Reset (){
		theDeathScreen.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();

		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint; // Restart the game
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true); // Make him appear again
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	}
		
}
