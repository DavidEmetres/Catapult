using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProyectileBehaviour : MonoBehaviour {

	List<GameObject> enemiesInRange = new List<GameObject>();
	[SerializeField] GameObject catExplosion;

	void Start () {
	
	}
	
	void Update () {
		if (transform.position.y <= 0.5f) {
			Explosion ();
		}
	}

	void Explosion() {
		if (enemiesInRange.Count > 0) {
			Instantiate (catExplosion, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
			foreach (GameObject e in enemiesInRange) {
				e.GetComponent<Enemy> ().Die ();
			}
			enemiesInRange.Clear ();
		}

		Destroy (this.gameObject);
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
