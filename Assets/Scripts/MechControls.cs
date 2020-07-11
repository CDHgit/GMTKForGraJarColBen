using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControls : MonoBehaviour {
    public float thrust; // the thrust value associated with movement keys = acceleration (mass eq)
    public GameObject rocketPrefab;
    public GameObject laserPrefab;
    public GameObject bulletPrefab;
    internal Rigidbody2D rb; // the rigidbody coomponent on this mech 
    public float dashStrength;
    public float dashLength;
    private Context context;
    private TrackController trackController;

    public bool active;
    bool mechEnabled = true;
    public float maxSpeedConstant; // max speed is used to cap the speed of the mech
    private float maxSpeed;
    bool forceApplied; // force applied is used to check if input is applied to this mech
    float scaleFactor; // scale factor is used to limit the max speed
    // Start is called before the first frame update
    public GameObject topSprite;
    // Start is called before the first frame update
    public float offsetAmount;

    int mechNum;
    Vector2 dashDestination;
    float dashTimer = 0;

    float virusWalkTimer = 0;
    Vector2 force;
    private int targetNum;
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D> ();
        context = GameObject.Find ("ContextManager").GetComponent<Context> ();
        trackController = GameObject.Find ("TrackController").GetComponent<TrackController> ();
        targetNum = (Random.Range (1, 3) + mechNum) % 3; //choose a random mech that isn't this one
    }
    public void startDash () {
        if (this.mechEnabled) {

            GameObject target = context.mechList[targetNum];
            float angle;
            angle = HelperFunctions.getAngleBetween (this.gameObject, target);
            angle +=90;
            angle = angle * Mathf.PI / 180f;
            dashDestination = new Vector2 (-Mathf.Sin (angle), Mathf.Cos (angle));
            dashTimer = dashLength;
        }
    }

    public void fireRocket () {
        if (this.mechEnabled) {

            GameObject target = context.mechList[targetNum];

            float angle;

            angle = HelperFunctions.getAngleBetween (this.gameObject, target);
            GameObject rocket = Instantiate (rocketPrefab, rb.position + .5f * new Vector2 (-Mathf.Sin (angle / 180f * Mathf.PI), Mathf.Cos (angle / 180f * Mathf.PI)), Quaternion.Euler (0, 0, angle));
            rocket.SendMessage ("initBullet", angle);
            rocket.SendMessage ("setParent", this.gameObject);
        }

    }
    public void fireLaser () {
        float angle;
        if (mechEnabled) {
            GameObject curMech = context.getCurMech ();

            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween (this.gameObject, target);
            float angRad = angle / 180f * Mathf.PI;
            GameObject laser = Instantiate (laserPrefab,
                this.transform.position + offsetAmount * new Vector3 (-Mathf.Sin (angRad), Mathf.Cos (angRad), 0),
                Quaternion.Euler (0, 0, angle));
            laser.SendMessage ("initBullet", angle);
            laser.SendMessage ("setParent", this.gameObject);
        }
    }

    public void fireBullet()
    {
        
        if (mechEnabled)
        {
            StartCoroutine(bulletCoruoutine());   
        }
    }

    IEnumerator bulletCoruoutine()
    {
        float angle;
        // Shoot random burst
        int burst = Random.Range(2, 5);
        for (int i = 0; i < burst; i++)
        {
            GameObject curMech = context.getCurMech();
            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween(this.gameObject, target);
            float angRad = angle / 180f * Mathf.PI;

            GameObject bullet = Instantiate(bulletPrefab,
                this.transform.position + offsetAmount * new Vector3(-Mathf.Sin(angRad), Mathf.Cos(angRad), 0),
                Quaternion.Euler(0, 0, angle));
            bullet.SendMessage("initBullet", angle);
            bullet.SendMessage("setParent", this.gameObject);

            yield return new WaitForSeconds(0.25f);

        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        GameObject curMech = context.getCurMech ();

        GameObject target = context.mechList[targetNum];
        topSprite.transform.rotation = Quaternion.Euler (0, 0, HelperFunctions.getAngleBetween (this.gameObject, target));
        //Force applied is used to detect if any of the directions are input
        forceApplied = false;
        //Detect if this is the active mech to be controlled else velocity zero (for now)
        if (active && mechEnabled) {
            maxSpeed = maxSpeedConstant;

            if (dashTimer > 0) { // we are dashing/ dash is on cooldown
                rb.AddForce (dashDestination * dashStrength);
                dashTimer -= Time.deltaTime;

            } else if (mechEnabled) {
                //W up
                if (Input.GetKey ("w")) {
                    //transofrm.* is a RELATIVE direction AFAIK, might need to be changed to a vector later
                    rb.AddForce (transform.up * thrust);
                    forceApplied = true;
                    // Debug.Log("pressed w");
                }
                //S down
                if (Input.GetKey ("s")) {
                    rb.AddForce (-transform.up * thrust);
                    forceApplied = true;
                    // Debug.Log("pressed s");
                }
                //D right
                if (Input.GetKey ("d")) {
                    rb.AddForce (transform.right * thrust);
                    forceApplied = true;
                    // Debug.Log("pressed d");
                }
                //A left
                if (Input.GetKey ("a")) {
                    rb.AddForce (-transform.right * thrust);
                    forceApplied = true;
                    // Debug.Log("pressed a");
                }
            }
        } else if (mechEnabled) {
            // mech ai when uncontrolled
            maxSpeed = maxSpeedConstant * 0.3f;

            if (virusWalkTimer > 0) {
                // we are dashing/ dash is on cooldown
                rb.AddForce (force);
                virusWalkTimer -= Time.deltaTime;
                forceApplied = true;
            } else {
                GameObject targetMech = context.getCurMech ();
                Vector2 distance = targetMech.transform.position - this.gameObject.transform.position;
                Vector2 randomAngle = Random.insideUnitCircle * distance.magnitude * 1.5f;
                force = (distance + randomAngle) * thrust;

                virusWalkTimer = Random.Range (0, 3);
            }

        }
        // If none of the directions are input then set the speed to 0
        if (!forceApplied) {
            // set to zero
            rb.velocity = Vector2.zero;
        }
        // Debug.Log(rb.velocity.magnitude);
        // Speed capping code
        // TODO: This actually caps speed at pre force (I think), meaning the actual speed is higher
        // we could do some math to ensure that this speed is capped taking the force into account in order
        // to match the top speed and stop diagonals from being sqrt 2 faster.
        if (rb.velocity.magnitude > maxSpeed) {
            // Debug.Log("maxed speed on mech");
            scaleFactor = maxSpeed / rb.velocity.magnitude;
            rb.velocity *= new Vector2 (scaleFactor, scaleFactor);
        }

        // Set bottom to direction of velocity
        if (rb.velocity != Vector2.zero) {
            float angle = Mathf.Atan2 (rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
            this.gameObject.transform.GetChild (1).gameObject.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }
    }
    void setMechNum1 (int mechNum) {
        this.mechNum = mechNum;
    }
    public void setActive (bool active) {
        this.active = active;
    }
    void onBeat (int beatNum) {
        if (!this.active) {
            //possibly change ai track
            int randnum = Random.Range (0, 100);
            if (randnum <= 25) {
                int track = Random.Range(0, 3);
                trackController.setTrack(this.gameObject, track);
            }
        }

        if (beatNum % 2 == 0){
            target = context.mechList[(Random.Range(1,3)+mechNum)%3]; //choose a random mech that isn't this one
        }
    }
    public bool getMechEnabledStatus()
    {
        return this.mechEnabled;
    }

    public void setMechEnabledStatus (bool setMechEnabled) {
        this.mechEnabled = setMechEnabled;
    }
}