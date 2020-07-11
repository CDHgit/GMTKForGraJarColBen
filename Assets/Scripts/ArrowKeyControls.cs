using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyControls : MonoBehaviour
{
    public float thrust; // the thrust value associated with movement keys = acceleration (mass eq)
    public Rigidbody2D rb; // the rigidbody coomponent on this mech 

    public bool active;
    public float maxSpeed; // max speed is used to cap the speed of the mech

    bool forceApplied; // force applied is used to check if input is applied to this mech
    float scaleFactor; // scale factor is used to limit the max speed
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();;
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Force applied is used to detect if any of the directions are input
        forceApplied = false; 
        //Detect if this is the active mech to be controlled else velocity zero (for now)
        if(active)
        {
            //W up
            if (Input.GetKey ("w")) {
                //transofrm.* is a RELATIVE direction AFAIK, might need to be changed to a vector later
                rb.AddForce(transform.up * thrust);
                forceApplied = true;
                // Debug.Log("pressed w");
            }
            //S down
            if (Input.GetKey ("s")) {
                rb.AddForce(-transform.up * thrust);
                forceApplied = true;
                // Debug.Log("pressed s");
            }
            //D right
            if (Input.GetKey ("d")) {
                rb.AddForce(transform.right * thrust);
                forceApplied = true;
                // Debug.Log("pressed d");
            }
            //A left
            if (Input.GetKey ("a")) {
                rb.AddForce(-transform.right * thrust);
                forceApplied = true;
                // Debug.Log("pressed a");
            }
        }
        // If none of the directions are input then set the speed to 0
        if (!forceApplied) {
            // set to zero
            rb.velocity = Vector3.zero;
        }
        // Debug.Log(rb.velocity.magnitude);
        // Speed capping code
        // TODO: This actually caps speed at pre force (I think), meaning the actual speed is higher
        // we could do some math to ensure that this speed is capped taking the force into account in order
        // to match the top speed and stop diagonals from being sqrt 2 faster.
        if (rb.velocity.magnitude > maxSpeed) {
            // Debug.Log("maxed speed on mech");
            scaleFactor = maxSpeed/rb.velocity.magnitude;
            rb.velocity = rb.velocity * new Vector2(scaleFactor,scaleFactor);
        }
     }
}
