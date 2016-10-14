using UnityEngine;
using System.Collections;

public class ShotCat : MonoBehaviour {

    // Use this for initialization

    GameObject catapult;
	void Start () {
        catapult = GameObject.FindGameObjectWithTag("catapult");
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = catapult.transform.rotation;
	
	}
}
