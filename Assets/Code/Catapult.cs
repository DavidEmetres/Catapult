using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Catapult : MonoBehaviour {

    ThrowSimulation throwScript;

    float turnInputValue = 0;
    public float maxAngle = 40;
    public float turnSpeed = 180f;
    Rigidbody playerRigidbody;
	//TridimensionalParabolic parabolicMov;

    public GameObject rock;
    GameObject lanzadera;
    Transform rockSpawn;

    public GameObject rockPrefab;
    public ShotCat rockScript;
	public Sprite thrownSprite;
	SpriteRenderer rockSprite;

    public Image recharge;
    public Transform targetCanvas;
    public Transform target;

    Animator anim;
    float force = 0;
    float chargeAmount = 0;
    public float maxDistance = -25.2f;
    float forceIncrease = -0.01f;

    // Use this for initialization
    void Start () {
        playerRigidbody = transform.parent.GetComponent<Rigidbody>();
        rockSpawn = GameObject.FindGameObjectWithTag("rockSpawn").transform;
        anim = GetComponent<Animator>();

        rock = Instantiate(rockPrefab, rockSpawn.position, transform.rotation) as GameObject;
        rockScript = rock.GetComponent<ShotCat>();
		rockSprite = rock.GetComponentInChildren<SpriteRenderer> ();
		rockSprite.GetComponent<Animator> ().SetTrigger ("charge");
		//parabolicMov = rock.GetComponent<TridimensionalParabolic> ();

        throwScript = GetComponent<ThrowSimulation>();


    }

    // Update is called once per frame
    void Update () {

        if (ArduinoInput.button4/*Input.GetKey("a")*/)
        {
            turnInputValue = -1f;
        }
        if (ArduinoInput.button1/*Input.GetKey("d")*/)
        {
            turnInputValue = 1f;
        }
        if (!ArduinoInput.button1 && !ArduinoInput.button4/*!Input.GetKey("a") && !Input.GetKey("d")*/)
        {
            turnInputValue = 0f;
        }
        if (ArduinoInput.button2/*Input.GetKey("f")*/)
        {
            //firstTime = Time.time;
            force += forceIncrease;
            if (force > 1.1 || force<0)
            {
                forceIncrease =forceIncrease* -1;
            }
            updateTarget();


        }
        if (ArduinoInput.button3/*Input.GetKey("t")*/)
        {
			if (!rockScript.thrown && rock != null)
            {
                
                Shoot();
                    
                //rock = null;
            }
            
                
        }
		if(rock == null && chargeAmount >= 1)
		{
			Recharge();
		}
		if(rockScript.thrown)
		{
			chargeAmount += 0.01f;
			if (chargeAmount > 1)
				chargeAmount = 1;
			recharge.fillAmount = chargeAmount;
		}
        Turn();
        


    }
    void updateTarget()
    {
        Vector3 newPos = new Vector3(-0.7f, -1.9f, maxDistance * force-6);
        targetCanvas.localPosition = newPos;
    }
    void Recharge()
    {
        

        //Destroy(rock.gameObject);
        rock = Instantiate(rockPrefab, rockSpawn.position, transform.rotation) as GameObject;
        rockScript = rock.GetComponent<ShotCat>();
		rockSprite = rock.GetComponentInChildren<SpriteRenderer> ();
		rockSprite.GetComponent<Animator> ().SetTrigger ("charge");
		//parabolicMov = rock.GetComponent<TridimensionalParabolic> ();

        //force = 0;
        //updateTarget();



    }
    void Shoot()
    {
        if (!rockScript.thrown && force!=0)
        {
			rockSprite.GetComponent<Animator> ().SetTrigger ("throw");
			rockSprite.sprite = thrownSprite;
			chargeAmount = 0;
            rockScript.thrown = true;
            //throwScript.Shoot();
			GetComponent<TridimensionalParabolic>().Projectile = rock.transform;
			GetComponent<TridimensionalParabolic>().Target = target.transform;
			GetComponent<TridimensionalParabolic> ().Shoot ();
            anim.SetTrigger("throw");
            //force = 0;
            //updateTarget();

        }


    }
    void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation
        if (transform.eulerAngles.y < 180)
        {
            if (transform.eulerAngles.y > 180 - maxAngle && turnInputValue < 0)
            {

                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
                if (transform.eulerAngles.y < 180 - maxAngle)
                {
                    transform.rotation = Quaternion.Euler(0f, 180-maxAngle, 0f);
                }

            }
            if (turnInputValue > 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
            }
        }
        else
        {
            if (transform.eulerAngles.y < 180 + maxAngle && turnInputValue > 0)
            {
                playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
                if (transform.eulerAngles.y>180 +maxAngle)
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
