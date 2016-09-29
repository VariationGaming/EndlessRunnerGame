using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;

	public float scoreCount;
	public float highScoreCount;

	public float pointsPerSecond;

	public bool scoreIncreasing;

	void Start () {
		if (PlayerPrefs.HasKey("HighScore")){
			highScoreCount = PlayerPrefs.GetFloat ("HighScore");
		}
	}

	void Update () {

		if (scoreIncreasing) {
			scoreCount += pointsPerSecond * Time.deltaTime;
		}
		if (scoreCount > highScoreCount) {
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", highScoreCount);
		}
		scoreText.text = "Score: " + (int)scoreCount;
		highScoreText.text = "High Score: " + (int)highScoreCount;
	}

	public void AddScore(int pointsToAdd){
		scoreCount += pointsToAdd;
	}
}
