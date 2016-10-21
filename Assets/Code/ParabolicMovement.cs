using UnityEngine;
using System.Collections;

public class ParabolicMovement : MonoBehaviour {

	float tf;
	float yo;
	float vo;
	float vox;
	float voy;
	float t = 0f;
	float x;
	float y;

	public float xf = 30f;
	public float angle = 30f;

	void Start () {
		angle = Mathf.Deg2Rad * angle;
		yo = transform.position.y;
		tf = Mathf.Sqrt ((yo + xf * Mathf.Tan(angle)) / 4.9f);
		vo = xf / (Mathf.Cos (angle) * tf);
		vox = vo * Mathf.Cos(angle);
		voy = vo * Mathf.Sin (angle);
	}

	void Update () {
		if (t <= tf) {
			t += Time.deltaTime;

			x = vox * t;
			y = yo + voy * t - (4.9f * Mathf.Pow (t, 2));

			transform.position = new Vector3 (x * Mathf.Cos(transform.eulerAngles.y + 270f), y, x * Mathf.Abs(Mathf.Sin(transform.eulerAngles.y + 270f)));
			//transform.localPosition = new Vector3 (transform.localPosition.x, y, x);
		}

		Debug.Log (xf);
	}
}
