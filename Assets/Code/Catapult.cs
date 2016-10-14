using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    float turnInputValue = 0;
    public float maxAngle = 40;
    Quaternion initialRotation;
    public float turnSpeed = 180f;
    Rigidbody playerRigidbody;
    float angle;

    float firstTime = 0f;
    GameObject rock;

    public GameObject rockPrefab;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        initialRotation = playerRigidbody.rotation;

        rock = Instantiate(rockPrefab, transform.position+new Vector3(0,0.5f,0), transform.rotation) as GameObject;

    }
	
	// Update is called once per frame
	void Update () {
        if (firstTime != 0f)
        {
            //waiting for second button for shooting

            if (ArduinoInput.button1)
            {
                Shoot(firstTime, Time.time);
                firstTime = 0;
            }

        }
        else
        {
            if (ArduinoInput.button4)
            {
                turnInputValue = -1f;
            }
            if (ArduinoInput.button1)
            {
                turnInputValue = 1f;
            }
            if (!ArduinoInput.button1 && !ArduinoInput.button4)
            {
                turnInputValue = 0f;
            }
            if (ArduinoInput.button2)
            {
                firstTime = Time.time;
            }
            if (ArduinoInput.button3)
            {
                Recharge();
            }

            Turn();
        }
        


    }
    void Recharge()
    {
        Destroy(rock);
        rock = Instantiate(rockPrefab, transform.position + new Vector3(0, 2f, 0), transform.rotation) as GameObject;
    }
    void Shoot(float firstTime, float timeNow)
    {
        float elapsedTime = timeNow - firstTime;
        Debug.Log("Elapsed time: " + elapsedTime);
        float force;

        if (elapsedTime < 0.3f)
        {
            force = 3;
        }
        else if(elapsedTime < 0.6f)
        {
            force = 2f;
        }
        else
        {
            force = 1;
        }

        if (elapsedTime < 6)
        {
            Vector3 vectorForce = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            vectorForce.Normalize();
            rock.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force*100);
            rock.GetComponent<Rigidbody>().AddForce(new Vector3(0,force*100, 0));
        }
        

    }
    void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation
        angle = Quaternion.Angle(initialRotation, playerRigidbody.rotation);
        if (transform.eulerAngles.y > 180)
        {
            if (transform.eulerAngles.y > 360 - maxAngle && turnInputValue < 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
                if (transform.eulerAngles.y < 360 - maxAngle)
                {
                    transform.rotation = Quaternion.Euler(0f, -maxAngle, 0f);
                }

            }
            if (turnInputValue > 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
            }
        }
        else
        {
            if (transform.eulerAngles.y < maxAngle && turnInputValue > 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
                if (transform.eulerAngles.y > maxAngle)
                {
                    transform.rotation = Quaternion.Euler(0f, maxAngle, 0f);
                }

            }
            if (turnInputValue < 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
            }
        }
    }
}
