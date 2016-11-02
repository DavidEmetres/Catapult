using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;

public class ArduinoInput : MonoBehaviour{

	SerialPort stream;
	Thread readThread;
	string value;
	bool threadRunning = false;

	public static bool button1;
	public static bool button2;
	public static bool button3;
	public static bool button4;

	void Start () {
		string[] ports = SerialPort.GetPortNames ();

		if (ports.Length > 0) {
			stream = new SerialPort (ports [0], 9600);

			stream.Open ();
			stream.ReadTimeout = 1;

			/*if (stream.IsOpen) {
				threadRunning = true;
				readThread = new Thread (new ThreadStart (ReadInput));
				readThread.Start ();
			}*/
		}
	}

	void Update() {
		if (stream != null) {
			if (stream.IsOpen && !threadRunning) {
				threadRunning = true;
				readThread = new Thread (new ThreadStart (ReadInput));
				readThread.Start ();
			}
		}

		if (Input.GetKey (KeyCode.Alpha4))
			button1 = true;
		else
			button1 = false;

		if (Input.GetKey (KeyCode.Alpha3))
			button2 = true;
		else
			button2 = false;

		if (Input.GetKey (KeyCode.Alpha2))
			button3 = true;
		else
			button3 = false;

		if (Input.GetKey (KeyCode.Alpha1))
			button4 = true;
		else
			button4 = false;
	}

	void OnApplicationQuit() {
		if (stream != null) {
			if (stream.IsOpen) {
				threadRunning = false;
				stream.Close ();
			}
		}
	}

	private void ReadInput() {
		while (threadRunning) {
			if (stream.IsOpen) {
				string a = stream.ReadLine();
				string[] b = a.Split (',');

				if (b [0] == "TRUE")
					button1 = true;
				else
					button1 = false;

				if (b [1] == "TRUE")
					button2 = true;
				else
					button2 = false;

				if (b [2] == "TRUE")
					button3 = true;
				else
					button3 = false;

				if (b [3] == "TRUE")
					button4 = true;
				else
					button4 = false;
			}
		}
	}
}
