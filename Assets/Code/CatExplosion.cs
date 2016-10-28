using UnityEngine;
using System.Collections;

public class CatExplosion : MonoBehaviour {

	float totalTime;
	SpriteRenderer sprite;

	public float duration;

	void Start() {
		totalTime = Time.time + duration;
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (Time.time >= totalTime)
			Destroy (this.gameObject);

		sprite.color = new Vector4 (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.005f);
	}
}
