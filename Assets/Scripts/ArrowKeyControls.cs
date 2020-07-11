using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyControls : MonoBehaviour
{
    public float thrust;
    public Rigidbody2D rb;

    public float maxSpeed;
    // Start is called before the first frame update

    bool forceApplied = false;
    float scaleFactor;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();;
    }

    // Update is called once per frame
    void FixedUpdate () {
        forceApplied = false;
        if (Input.GetKey ("w")) {
            rb.AddForce(transform.up * thrust);
            forceApplied = true;
            // Debug.Log("pressed w");
        }
        if (Input.GetKey ("s")) {
            rb.AddForce(-transform.up * thrust);
            forceApplied = true;
            // Debug.Log("pressed s");
        }
        if (Input.GetKey ("d")) {
            rb.AddForce(transform.right * thrust);
            forceApplied = true;
            // Debug.Log("pressed d");
        }
        if (Input.GetKey ("a")) {
            rb.AddForce(-transform.right * thrust);
            forceApplied = true;
            // Debug.Log("pressed a");
        }
        if (!forceApplied) {
            rb.velocity = Vector3.zero;
        }
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > maxSpeed) {
            Debug.Log("maxed speed on mech");
            scaleFactor = maxSpeed/rb.velocity.magnitude;
            rb.velocity = rb.velocity * new Vector2(scaleFactor,scaleFactor);
        }
     }
}
