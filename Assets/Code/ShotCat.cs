using UnityEngine;
using System.Collections;

public class ShotCat : MonoBehaviour {

    // Use this for initialization

    GameObject catapult;
    Transform rockSpawn;
    public bool thrown = false;

	void Start () {
        catapult = GameObject.FindGameObjectWithTag("catapult");
        rockSpawn = GameObject.FindGameObjectWithTag("rockSpawn").transform;

    }

    // Update is called once per frame
    void Update () {
        if (!thrown)
        {
            transform.rotation = catapult.transform.rotation;
            transform.position = rockSpawn.position;
        }
        

    }
}
