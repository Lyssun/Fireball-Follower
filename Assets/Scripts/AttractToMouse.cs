using UnityEngine;

/*
 * Attract the linked Object to the positon 
 * of the mouse or of the finger on the screen.
 * 
 * The object would be slowly attracted at first,
 * and possibly rotate around the position
 * but with the decrease velocity effect it should be ok.
 * 
 * The attract force is calculated like this:
 * a = direction * Gravity Constant * mass of the Object attracted * strength * dist
 */
public class AttractToMouse : MonoBehaviour {

    // The Object that would be attracted.
    public GameObject obj;

    // The gravity constant used during the gravity.
    public float gravityConstant = 6.67408f;

    // Value used during the gravity strength calc.
    public float strength = 5f;

    // Distance from the screen where the object would be attracted to.
    public float distance = 8f;

    // minimum distance to be attracted 
    public float attractDist = 0.05f;

    // minimum distance  to be slowed and stopped
    public float stopDist = 0.01f;

    // To know if the velocity would be decreased when the object is far from the position.
    public bool decrease = true;

    // Rigidbody of the object.
    private Rigidbody rb;

    void Start()
    {
        // Used in Update()
        rb = obj.GetComponent<Rigidbody>();
        // Security change, if the stopDist is higher than the attractDist, 
        // the object would be attracted very slowly (if it can move.)
        if( attractDist < stopDist)
        {
            float tmp = attractDist;
            attractDist = stopDist;
            stopDist = tmp;
        }
    }

    void Update()
    {
        // Create a ray from the position of the mouse (or the finger for phone)
        // and get the vector3 at the distance on the line
        Vector3 pos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(distance);
        // Calculate the offset between the position and the position of the object
        Vector3 offset = pos - obj.transform.position;
        // calculate the distance between the position and the position of the object
        float dist = offset.sqrMagnitude;

        //if the object isn't near...
        if (dist > attractDist)
        { 
            // if activated , decrease the object's velocity to get a controlled motion
            if(decrease)
                rb.velocity *= 0.9f;
            /*
             * Represent the attract force.
             * We add a force by combining the direction,
             * the mass of the object, the gravityConstant, the distance and the strength.
             */
            rb.AddForce(dist * strength  * (gravityConstant * offset.normalized * rb.mass) );
        }
        //if the object is near
        else if (dist >= stopDist)
            // Decrease and negate velocity of the object to prevent
            // it from passing trought the pos.
            rb.velocity *= -0.9f;
    }
}
