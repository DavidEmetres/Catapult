using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProyectileBehaviour : MonoBehaviour {

	List<GameObject> enemiesInRange = new List<GameObject>();

	void Start () {
	
	}
	
	void Update () {
		if (transform.position.y <= 0f) {
			Explosion ();
		}
	}

	void Explosion() {
		if (enemiesInRange.Count > 0) {
			foreach (GameObject e in enemiesInRange) {
				e.GetComponent<Enemy> ().Die ();
			}
			enemiesInRange.Clear ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			enemiesInRange.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy") {
			if(enemiesInRange.Count > 0)
				enemiesInRange.Remove (other.gameObject);
		}
	}
}
