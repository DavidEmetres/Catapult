using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour {

	[SerializeField] Image button1Fill;
	[SerializeField] Image button2Fill;
	[SerializeField] Image button3Fill;
	[SerializeField] Image button4Fill;
	[SerializeField] Transform option1;
	[SerializeField] Transform option2;
	[SerializeField] Transform option3;
	[SerializeField] Transform option4;
	[SerializeField] GameObject particleMotion;
	[SerializeField] Image blackFade;

	bool button1 = false;
	bool entering = false;
	Transform target;

	void Start () {
	
	}
	
	void Update () {
		if (button1)
			button1Fill.fillAmount += 0.05f;
		else if (!button1)
			button1Fill.fillAmount = 0f;

		else if (ArduinoInput.button2)
			button2Fill.fillAmount += 0.05f;
		else if (!ArduinoInput.button2)
			button2Fill.fillAmount = 0f;

		else if (ArduinoInput.button3)
			button3Fill.fillAmount += 0.05f;
		else if (ArduinoInput.button3)
			button3Fill.fillAmount = 0f;

		else if (ArduinoInput.button4)
			button4Fill.fillAmount += 0.05f;
		else if (ArduinoInput.button4)
			button4Fill.fillAmount = 0f;

		if (Input.GetKey (KeyCode.A))
			button1 = true;
		else
			button1 = false;

		CheckSceneChange ();

		if (entering) {
			Vector3 relativePos = target.position - Camera.main.transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			Camera.main.transform.rotation = Quaternion.Lerp (Camera.main.transform.rotation, rotation, 0.05f);

			Camera.main.transform.Translate (Vector3.forward);

			blackFade.color = new Vector4 (blackFade.color.r, blackFade.color.g, blackFade.color.b, blackFade.color.a + 0.017f);
		}
	}

	void CheckSceneChange() {
		if (button1Fill.fillAmount == 1f) {
			button1Fill.enabled = false;
			target = option1;
			particleMotion.SetActive (true);
			entering = true;
		}

		if (button2Fill.fillAmount == 1f) {
			button2Fill.enabled = false;
			target = option2;
			particleMotion.SetActive (true);
			entering = true;
		}

		if (button3Fill.fillAmount == 1f) {
			button3Fill.enabled = false;
			target = option3;
			particleMotion.SetActive (true);
			entering = true;
		}

		if (button4Fill.fillAmount == 1f) {
			button4Fill.enabled = false;
			target = option4;
			particleMotion.SetActive (true);
			entering = true;
		}
	}
}
