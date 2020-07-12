using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {
    public int damage = 2;
    public float thrust = 1;
    public float lifeSpanSecs = 5;

    public float maxSpeed = 20;
    Rigidbody2D rigidBody;
    Transform mTransform;
    float travelAngle;
    float startTime;
    GameObject parent;
    // Start is called before the first frame update
    void Start () {
        mTransform = GetComponent<Transform> ();
        startTime = Time.time;
    }
    void initBullet (float fl) {
        travelAngle = fl;
        Debug.Log("The laser be existin tho");
        rigidBody = GetComponent<Rigidbody2D> ();
    }

    void setParent (GameObject o) {
        parent = o;
    }
    // Update is called once per frame
    void Update () {
        if (Time.time - startTime > lifeSpanSecs) {
            explode ();
        }
    }
    void OnTriggerEnter2D (Collider2D collision) {
        GameObject collisionObject = collision.gameObject;
        if (collision.gameObject != parent) {
            if (collisionObject.CompareTag ("Destructable")) {
                collisionObject.GetComponent<MechInfo> ().changeHealth (-damage);
            }
        }
    }
    void explode () {
        parent.SendMessage ("laserEnd");
        Destroy (this.gameObject);
    }
}