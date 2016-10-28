using UnityEngine;
using System.Collections;

public class ParabolicMovement : MonoBehaviour {

	float tf;
	float yo;
	float vo;
	float voz;
	float voy;
	float vox;
	float t = 0f;
	float z;
	float y;
	float x;
	float xo;
	float zo;
	Vector3 direction;
	Vector3 unitDirection;

	public float zf;
	public float xf;
	public float angle = 30f;
	public bool shooted = false;

	void Start () {

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.F1))
			Shoot ();

		if (shooted) {
			if (t <= tf) {
				t += Time.deltaTime;

				z = zo + voz * t;
				//x = xo + voz * t;
				y = yo + voy * t - (4.9f * Mathf.Pow (t, 2));

				transform.position = new Vector3 (transform.position.x, y, z);
			}
		}
	}

	public void Shoot() {
		angle = Mathf.Deg2Rad * angle;
		yo = transform.position.y;
		xo = transform.position.x;
		zo = transform.position.z;
		tf = Mathf.Sqrt ((yo + xf * Mathf.Tan(angle)) / 4.9f);
		vo = xf / (Mathf.Cos (angle) * tf);
		voz = vo * Mathf.Cos(angle);
		voy = vo * Mathf.Sin (angle);
		shooted = true;
	}
}
