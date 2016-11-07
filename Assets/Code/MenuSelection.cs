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
	[SerializeField] Animator titleAnim;

	bool button1 = false;
	bool entering = false;
	Transform target;
	int option;

	void Start () {
		
	}

	void Update () {
		if (titleAnim.GetCurrentAnimatorStateInfo (0).IsName ("TitleAnimation") &&
		    titleAnim.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {
			if (ArduinoInput.button1) {
				button1Fill.fillAmount += 0.05f;
				button2Fill.fillAmount = 0f;
				button3Fill.fillAmount = 0f;
				button4Fill.fillAmount = 0f;
			} else if (ArduinoInput.button2) {
				button2Fill.fillAmount += 0.05f;
				button1Fill.fillAmount = 0f;
				button3Fill.fillAmount = 0f;
				button4Fill.fillAmount = 0f;
			} else if (ArduinoInput.button3) {
				button3Fill.fillAmount += 0.05f;
				button2Fill.fillAmount = 0f;
				button1Fill.fillAmount = 0f;
				button4Fill.fillAmount = 0f;
			} else if (ArduinoInput.button4) {
				button4Fill.fillAmount += 0.05f;
				button2Fill.fillAmount = 0f;
				button3Fill.fillAmount = 0f;
				button1Fill.fillAmount = 0f;
			} else {
				button4Fill.fillAmount = 0f;
				button2Fill.fillAmount = 0f;
				button3Fill.fillAmount = 0f;
				button1Fill.fillAmount = 0f;
			}
		}

		CheckSceneChange ();

		if (titleAnim.GetBool ("entering")) {
			Vector3 relativePos = target.position - Camera.main.transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			Camera.main.transform.rotation = Quaternion.Lerp (Camera.main.transform.rotation, rotation, 0.05f);

			Camera.main.transform.Translate (Vector3.forward);

			blackFade.color = new Vector4 (blackFade.color.r, blackFade.color.g, blackFade.color.b, blackFade.color.a + 0.025f);

			if (Vector3.Distance(Camera.main.transform.position, target.position) <= 5f) {
				switch (option) {
					case 1:
						Application.LoadLevel ("Nivel");
						break;
					case 2:
						Application.LoadLevel ("Controls");
						break;
					case 3:
						Application.LoadLevel ("Credits");
						break;
					case 4:
						//Application.LoadLevel ("");
						break;
				}
			}
		}
	}

	void CheckSceneChange() {
		if (button1Fill.fillAmount == 1f) {
			option = 1;
			button1Fill.enabled = false;
			target = option1;
			particleMotion.SetActive (true);
			titleAnim.SetBool ("entering", true);
		}

		if (button2Fill.fillAmount == 1f) {
			option = 2;
			button2Fill.enabled = false;
			target = option2;
			particleMotion.SetActive (true);
			titleAnim.SetBool ("entering", true);
		}

		if (button3Fill.fillAmount == 1f) {
			option = 3;
			button3Fill.enabled = false;
			target = option3;
			particleMotion.SetActive (true);
			titleAnim.SetBool ("entering", true);
		}

		/*if (button4Fill.fillAmount == 1f) {
			option = 4;
			button4Fill.enabled = false;
			target = option4;
			particleMotion.SetActive (true);
			titleAnim.SetBool ("entering", true);
		}*/
	}
}
