using UnityEngine;
using System.Collections;

public class PickUpPoints : MonoBehaviour {

	public int givenScore;
	private ScoreManager theScoreManager;

	private AudioSource coinSound;

	void Start () {
		theScoreManager = FindObjectOfType <ScoreManager> ();
		coinSound = GameObject.Find ("CoinSound").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.name == "Player") {
			theScoreManager.AddScore (givenScore);
			gameObject.SetActive (false);

			if (coinSound.isPlaying) {
				coinSound.Stop ();
				coinSound.Play ();
			} else {
				coinSound.Play ();
			}
		}
	}
}
