using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControls : MonoBehaviour {
    public float thrust; // the thrust value associated with movement keys = acceleration (mass eq)
    public GameObject rocketPrefab;

    public bool laserStopped = false;

    public GameObject laserPrefab;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public GameObject empPrefab;
    public GameObject minePrefab;
    public GameObject explosionParticles;
    public GameObject shieldPrefab;

    [HideInInspector]
    public bool invulnerable = false;

    internal Rigidbody2D rb; // the rigidbody coomponent on this mech 
    public float dashStrength;
    public float dashLength;
    private Context context;
    private TrackController trackController;

    public bool active;
    public bool mechEnabled = true;
    public float maxSpeedConstant; // max speed is used to cap the speed of the mech
    private float maxSpeed;
    bool forceApplied; // force applied is used to check if input is applied to this mech
    float scaleFactor; // scale factor is used to limit the max speed
    // Start is called before the first frame update
    public GameObject topSprite;
    // Start is called before the first frame update
    public float offsetAmount;
    public DottedLineDemo dottedLine;
    int mechNum;
    Vector2 dashDestination;
    float dashTimer = 0;
    float virusWalkTimer = 0;
    Vector2 force;
    private int targetNum;

    void Start () {
        curMaxSpeed=maxSpeedConstant;
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
            angle += 90;
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
            // Add cone of fire
            angle += Random.Range(-15, 15);
            GameObject rocket = Instantiate (rocketPrefab, rb.position + .5f * new Vector2 (-Mathf.Sin (angle / 180f * Mathf.PI), Mathf.Cos (angle / 180f * Mathf.PI)), Quaternion.Euler (0, 0, angle));
            rocket.SendMessage ("initBullet", angle);
            rocket.SendMessage ("setParent", this.gameObject);
        }

    }
    public void fireLaser () {
        //Debug.Log("Firin laser " + laserStopped + " " +mechNum);
        if (laserStopped) {
            return;
        }

        laserStopped = true;
        float angle;
        if (mechEnabled) {
            GameObject curMech = context.getCurMech ();

            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween (this.gameObject, target);
            // Add cone of fire
            angle += Random.Range(-10, 10);

            float angRad = angle / 180f * Mathf.PI;
            GameObject laser = Instantiate (laserPrefab,
                this.transform.position + offsetAmount * new Vector3 (-Mathf.Sin (angRad), Mathf.Cos (angRad), 0),
                Quaternion.Euler (0, 0, angle));
            laser.SendMessage ("initBullet", angle);
            laser.SendMessage ("setParent", this.gameObject);
        }
    }

    public void fireBullet () {

        if (mechEnabled) {
            StartCoroutine (bulletCoruoutine ());
        }
    }
    void destroyWall(){
        //DO NOTHING
    }
    IEnumerator bulletCoruoutine () {
        float angle;
        // Shoot random burst
        int burst = Random.Range (2, 5);
        for (int i = 0; i < burst; i++) {
            GameObject curMech = context.getCurMech ();
            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween(this.gameObject, target);

            // Add cone of fire
            angle += Random.Range(-20, 20);

            float angRad = angle / 180f * Mathf.PI;

            GameObject bullet = Instantiate(bulletPrefab, rb.position + .5f * new Vector2(-Mathf.Sin(angle / 180f * Mathf.PI), Mathf.Cos(angle / 180f * Mathf.PI)), Quaternion.Euler(0, 0, angle));
            bullet.SendMessage("initBullet", angle);
            bullet.SendMessage("setParent", this.gameObject);

            yield return new WaitForSeconds (0.25f);

        }
    }
    public void throwGrenade()
    {
        float angle;
        if (mechEnabled)
        {
            GameObject curMech = context.getCurMech();

            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween(this.gameObject, target);
            // Add cone of fire
            angle += Random.Range(-90, 90);

            float angRad = angle / 180f * Mathf.PI;
            GameObject grenade = Instantiate(grenadePrefab,
                this.transform.position + offsetAmount * new Vector3(-Mathf.Sin(angRad), Mathf.Cos(angRad), 0),
                Quaternion.Euler(0, 0, angle));
            grenade.SendMessage("initLob", angle);
            grenade.SendMessage("setParent", this.gameObject);
        }
    }

    public void throwEMP()
    {
        float angle;
        if (mechEnabled)
        {
            GameObject curMech = context.getCurMech();

            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween(this.gameObject, target);
            // Add cone of fire
            angle += Random.Range(-90, 90);

            float angRad = angle / 180f * Mathf.PI;
            GameObject EMP = Instantiate(empPrefab,
                this.transform.position + offsetAmount * new Vector3(-Mathf.Sin(angRad), Mathf.Cos(angRad), 0),
                Quaternion.Euler(0, 0, angle));
            float[] args = {angle, 1};
            EMP.SendMessage("initLob", args);
            EMP.SendMessage("setParent", this.gameObject);
        }
    }

    public void throwMine()
    {
        float angle;
        if (mechEnabled)
        {
            GameObject curMech = context.getCurMech();

            GameObject target = context.mechList[targetNum];
            angle = HelperFunctions.getAngleBetween(this.gameObject, target);
            // Add cone of fire
            angle += Random.Range(-180, 180);

            float angRad = angle / 180f * Mathf.PI;
            GameObject mine = Instantiate(minePrefab,
                this.transform.position + offsetAmount * new Vector3(-Mathf.Sin(angRad), Mathf.Cos(angRad), 0),
                Quaternion.Euler(0, 0, angle));
            mine.SendMessage("initLob", angle);
            mine.SendMessage("setParent", this.gameObject);
        }
    }

    public void shield()
    {
        if (mechEnabled)
        {
            StartCoroutine(shieldCoruoutine());
        }
        
    }

    IEnumerator shieldCoruoutine()
    {
        GameObject shieldObj = Instantiate(shieldPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        shieldObj.transform.parent = gameObject.transform;
        invulnerable = true;
        yield return new WaitForSeconds(2f);
        invulnerable = false;
        Destroy(shieldObj);
    }

    void heal()
    {
        MechInfo mInfo = this.gameObject.GetComponent<MechInfo>();
        mInfo.changeHealth(25);
    }

    void laserEnd () {
        laserStopped = false;
    }

    public void empEffect()
    {
        StartCoroutine(empEffected());
    }

    IEnumerator empEffected()
    {
        this.setMechEnabledStatus(false);
        yield return new WaitForSeconds(3f);
        this.setMechEnabledStatus(true);
    }

    void dead(){
        context.dead++;
        dottedLine.pointBs[mechNum] = GetComponent<Rigidbody2D> ().position;
        this.gameObject.SetActive (false);
        setMechEnabledStatus (false);
        GameObject.Instantiate(explosionParticles, this.transform.position, Quaternion.Euler(0, 0, 0));
    }

    void goalAchieved () {
        context.goal++;
        this.gameObject.SetActive (false);
        setMechEnabledStatus (false);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (this.mechEnabled && !context.mechList[targetNum].GetComponent<MechControls> ().mechEnabled) {
            targetNum = (Random.Range (1, 3) + mechNum) % 3; //choose a random mech that isn't this one
        }
        GameObject curMech = context.getCurMech ();
        GameObject target = context.mechList[targetNum];

        if (!laserStopped)
            topSprite.transform.rotation = Quaternion.Euler (0, 0, HelperFunctions.getAngleBetween (this.gameObject, target));
        //Force applied is used to detect if any of the directions are input
        forceApplied = false;
        //Detect if this is the active mech to be controlled else velocity zero (for now)

        dottedLine.pointAs[mechNum] = GetComponent<Transform> ().position;
        dottedLine.pointBs[mechNum] = target.GetComponent<Transform> ().position;
        //Debug.Log ("LASER " + mechNum + " " + laserStopped);
        if (active && mechEnabled && !laserStopped) {
            maxSpeed = curMaxSpeed;

            if (dashTimer > 0) { // we are dashing/ dash is on cooldown
                rb.AddForce (dashDestination * dashStrength);
                dashTimer -= Time.deltaTime;

            } else if (mechEnabled) {
                //W up
                if (Input.GetKey ("w")) {
                    //transofrm.* is a RELATIVE direction AFAIK, might need to be changed to a vector later
                    rb.AddForce (transform.up * thrust);
                    forceApplied = true;
                    // //Debug.Log("pressed w");
                }
                //S down
                if (Input.GetKey ("s")) {
                    rb.AddForce (-transform.up * thrust);
                    forceApplied = true;
                    // //Debug.Log("pressed s");
                }
                //D right
                if (Input.GetKey ("d")) {
                    rb.AddForce (transform.right * thrust);
                    forceApplied = true;
                    // //Debug.Log("pressed d");
                }
                //A left
                if (Input.GetKey ("a")) {
                    rb.AddForce (-transform.right * thrust);
                    forceApplied = true;
                    // //Debug.Log("pressed a");
                }
            }
        } else if (mechEnabled && !laserStopped) {
            // mech ai when uncontrolled
            maxSpeed = curMaxSpeed * 0.3f;

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
        if (!forceApplied || laserStopped) {
            // set to zero
            rb.velocity = Vector2.zero;
        }
        // //Debug.Log(rb.velocity.magnitude);
        // Speed capping code
        // TODO: This actually caps speed at pre force (I think), meaning the actual speed is higher
        // we could do some math to ensure that this speed is capped taking the force into account in order
        // to match the top speed and stop diagonals from being sqrt 2 faster.
        if (rb.velocity.magnitude > maxSpeed) {
            // //Debug.Log("maxed speed on mech");
            scaleFactor = maxSpeed / rb.velocity.magnitude;
            rb.velocity *= new Vector2 (scaleFactor, scaleFactor);
        }

        // Set bottom to direction of velocity
        if (rb.velocity != Vector2.zero) {
            float angle = Mathf.Atan2 (rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
            this.gameObject.transform.GetChild (1).gameObject.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }
    }
    float curMaxSpeed;
    public void reduceSpeed () {
        curMaxSpeed=maxSpeedConstant/2;
    }
    public void restoreSpeed() {
        curMaxSpeed=maxSpeedConstant;
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
                int track = Random.Range (0, 3);
                trackController.setTrack (this.gameObject, track);
            }
        }

        if ((this.mechEnabled && beatNum % 2 == 0)) {
            targetNum = (Random.Range (1, 3) + mechNum) % 3; //choose a random mech that isn't this one
        }
    }
    public bool getMechEnabledStatus () {
        return this.mechEnabled;
    }

    public void setMechEnabledStatus (bool setMechEnabled) {
        this.mechEnabled = setMechEnabled;
    }
}