using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{
    public Transform Target;
    public Transform Origin;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    float totalFlightTime;
    float horizontalVelocityMagnitude;
    float verticalVelocityMagnitude;

    Transform Projectile;
    private Transform myTransform;
    Catapult catapultScript;

    void Awake()
    {
        
    }

    public void Shoot()
    {
        Debug.Log("SHOOT");
        catapultScript = GetComponent<Catapult>();
        Projectile = catapultScript.rock.transform;
        myTransform = Origin;
        StartCoroutine(SimulateProjectile());
    }
    private Vector3 findInitialVelocity(Vector3 startPosition, Vector3 finalPosition, float maxHeightOffset = 7f, float rangeOffset = 0.11f)
    {
        // get our return value ready. Default to (0f, 0f, 0f)
        Vector3 newVel = new Vector3();

        // Find the direction vector without the y-component
        Vector3 direction = new Vector3(finalPosition.x, 0f, finalPosition.z) - new Vector3(startPosition.x, 0f, startPosition.z);

        // Find the distance between the two points (without the y-component)
        float range = direction.magnitude;

        // Add a little bit to the range so that the ball is aiming at hitting the back of the rim.
        // Back of the rim shots have a better chance of going in.
        // This accounts for any rounding errors that might make a shot miss (when we don't want it to).
        //range += rangeOffset;

        // Find unit direction of motion without the y component
        Vector3 unitDirection = direction.normalized;

        // Find the max height
        // Start at a reasonable height above the hoop, so short range shots will have enough clearance to go in the basket
        // without hitting the front of the rim on the way up or down.
        float maxYPos = finalPosition.y + maxHeightOffset;


        // check if the range is far enough away where the shot may have flattened out enough to hit the front of the rim
        // if it has, switch the height to match a 45 degree launch angle
        float minRange = 14.17f;
        if (range <= minRange)
            maxYPos = range / 2f;
        //maxYPos = range / 2f;

        // find the initial velocity in y direction
        newVel.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * (maxYPos - startPosition.y));
        verticalVelocityMagnitude = newVel.y;

        // find the total time by adding up the parts of the trajectory
        // time to reach the max
        float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - startPosition.y) / Physics.gravity.y);

        // time to return to y-target
        float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - finalPosition.y) / Physics.gravity.y);

        // add them up to find the total flight time
        totalFlightTime = timeToMax + timeToTargetY;

        // find the magnitude of the initial velocity in the xz direction
        horizontalVelocityMagnitude = range / totalFlightTime;

        // use the unit direction to find the x and z components of initial velocity
        newVel.x = horizontalVelocityMagnitude * unitDirection.x;
        newVel.z = horizontalVelocityMagnitude * unitDirection.z;
        Debug.Log(range);

        return newVel;
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        //Target.position = Target.position - new Vector3(0, -, 0);
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);
        

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector3 velocity = findInitialVelocity(Projectile.position, Target.position);
        

        // Calculate flight time.
        //float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        //Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < totalFlightTime)
        {

            //Projectile.Translate(0, (Vy-Physics.gravity.y) * Time.deltaTime, -horizontalVelocityMagnitude * Time.deltaTime);
            Transform smooth = Projectile;
            float smoothFactor = 0.2f;
            //smooth.Translate(velocity*Time.deltaTime, Space.World);
            //Projectile.position = Vector3.MoveTowards(Projectile.position, smooth.position, 0.1f);

            smooth.Translate(velocity*Time.deltaTime, Space.World);
            smooth.position = Vector3.Lerp(Projectile.position, smooth.position, smoothFactor);
            Projectile.position = Vector3.MoveTowards(Projectile.position, smooth.position, 10.0f*Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        
    }
}