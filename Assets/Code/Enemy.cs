using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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

		if (this.transform.position.z == end)
			Die ();
	}

	void Move() {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	public void Die() {
		manager.EnemyIsDead ();
		Destroy(this.gameObject);
	}
}
