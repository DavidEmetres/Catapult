using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject enemyScore;
	EnemyManager manager;
	float speed;
	float end;

	public void Initialize(EnemyManager manager, float speed, float end) {
		this.manager = manager;
		this.speed = speed;
		this.end = end;
	}

	void Update() {
		Move ();

		if (this.transform.position.z <= end)
			Ended ();
	}

	void Move() {
		transform.Translate (-1f * Vector3.forward * speed * Time.deltaTime);
	}

	void Ended() {
		manager.EnemyEnded ();
		Destroy (this.gameObject);
	}

	public void Die() {
		GameObject obj = Instantiate (enemyScore, this.transform.position, Quaternion.identity) as GameObject;
		obj.transform.position = this.transform.position;
		manager.EnemyIsDead ();
		Destroy(this.gameObject);
	}
}
