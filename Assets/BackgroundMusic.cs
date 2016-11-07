using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public static BackgroundMusic Instance;

	void Start () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else
			Destroy (this.gameObject);
	}
	
	void Update () {
		
	}
}
