using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;

public class ArduinoInput : MonoBehaviour{

	SerialPort stream;
	Thread readThread;
	string value;
	bool threadRunning = true;

	public static bool button1;
	public static bool button2;
	public static bool button3;
	public static bool button4;

	void Start () {
		string[] ports = SerialPort.GetPortNames ();

		stream = new SerialPort(ports[0], 9600);

		stream.Open ();
		readThread = new Thread (new ThreadStart(ReadInput));
		readThread.Start ();
	}

	void Update() {
		Debug.Log (button1 + ", " + button2 + ", " + button3 + ", " + button4);
	}

	void OnApplicationQuit() {
		threadRunning = false;
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
