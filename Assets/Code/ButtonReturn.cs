using UnityEngine;
using System.Collections;

public class ButtonReturn : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		if (ArduinoInput.button1 || ArduinoInput.button2 || ArduinoInput.button3 || ArduinoInput.button4) {
			LoadScene ();
		}
	}

	public void LoadScene()
	{
		Application.LoadLevel ("MainMenu");
	}
}
