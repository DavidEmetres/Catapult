using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1f)
			Destroy (this.transform.parent.gameObject);
	}
}
