using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	[SerializeField] GameObject enemy;
	float time;
	float nextRespawnTime;
	int counter;
	int difficulty;

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
		nextRespawnTime = time + Random.Range (minRespawnTime, maxRespawnTime);
	}

	void Update () {
		time += Time.deltaTime;

		if (time >= nextRespawnTime) {
			RespawnEnemy ();
			nextRespawnTime = time + Random.Range (minRespawnTime, maxRespawnTime);
		}

		if (Input.GetKeyDown (KeyCode.F1))
			UpgradeDifficulty ();
	}

	void RespawnEnemy() {
		Vector3 pos = new Vector3 (Random.Range (minSide, maxSide), 1f, respawnDepth);
		GameObject instance = Instantiate (enemy, pos, Quaternion.identity) as GameObject;
		Enemy e = instance.GetComponent<Enemy> ();
		e.Initialize (this, Random.Range(speedMin, speedMax), end);
	}

	void UpgradeDifficulty() {
		difficulty++;

		switch (difficulty) {
		case 2:
			speedMax = 4f;
			maxRespawnTime = 1f;
			break;
		case 3:
			speedMin = 3f;
			break;
		case 4:
			minRespawnTime = 0.2f;
			maxRespawnTime = 0.7f;
			break;
		case 5:
			maxRespawnTime = 0.5f;
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

	void OnGUI() {
		GUI.Label (new Rect (0, 0, 100, 100), "" + time);
		GUI.Label (new Rect (100, 0, 100, 100), "" + counter);
		GUI.Label (new Rect (200, 0, 100, 100), "" + difficulty);
	}
}
