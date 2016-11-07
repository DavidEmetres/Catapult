using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	[SerializeField] GameObject enemy;
	[SerializeField] Text scoreText;
	[SerializeField] GameObject gameOver;
	[SerializeField] GameObject[] livesImages;
	float time;
	float nextRespawnTime;
	int counter;
	int difficulty;
	int score;
	int currentScore;
	int lives;

	public float respawnDepth;
	public float end;
	public float minSide;
	public float maxSide;
	public float speedMin;
	public float speedMax;
	public float minRespawnTime;
	public float maxRespawnTime;

	void Start() {
		time = 0f;
		counter = 0;
		difficulty = 1;
		lives = 7;
		nextRespawnTime = time + Random.Range (minRespawnTime, maxRespawnTime);

		GameObject music = GameObject.Find ("BackgroundMusic");
		Destroy (music);
	}

	void Update () {
		time += Time.deltaTime;
		score = counter * 50;

		if (currentScore < score) {
			currentScore++;
			scoreText.text = currentScore.ToString();
		}

		if (time >= nextRespawnTime) {
			RespawnEnemy ();
			nextRespawnTime = time + Random.Range (minRespawnTime, maxRespawnTime);
		}

		if (Input.GetKeyDown (KeyCode.F1))
			UpgradeDifficulty ();
	}

	void RespawnEnemy() {
		Vector3 pos = new Vector3 (Random.Range (minSide, maxSide), 0.8f, respawnDepth);
		GameObject instance = Instantiate (enemy, pos, Quaternion.identity) as GameObject;
		Enemy e = instance.GetComponent<Enemy> ();
		e.Initialize (this, Random.Range(speedMin, speedMax), end);
	}

	void UpgradeDifficulty() {
		difficulty++;

		switch (difficulty) {
		case 2:
			speedMax = 1.5f;
			maxRespawnTime = 3.5f;
			break;
		case 3:
			speedMin = 1f;
			break;
		case 4:
			minRespawnTime = 2.5f;
			maxRespawnTime = 3f;
			break;
		case 5:
			speedMax = 2f;
			break;
		}
	}

	public void EnemyIsDead() {
		counter++;

		switch (counter) {
		case 20:
			UpgradeDifficulty ();
			break;
		case 70:
			UpgradeDifficulty ();
			break;
		case 130:
			UpgradeDifficulty ();
			break;
		case 180:
			UpgradeDifficulty ();
			break;
		}
	}

	public void EnemyEnded() {
		lives--;
		Destroy (livesImages [lives].gameObject);

		if (lives == 0) {
			lives = -1;
			StartCoroutine (GameOver());
		}
	}

	IEnumerator GameOver() {
		gameOver.SetActive (true);
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("MainMenu");
	}

	void OnGUI() {

	}
}
