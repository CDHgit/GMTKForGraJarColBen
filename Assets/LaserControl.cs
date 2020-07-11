using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{

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
        rigidBody = GetComponent<Rigidbody2D> ();

    }

    void setParent (GameObject o) {
        parent = o;
    }
    // Update is called once per frame
    void Update () {
        mTransform.rotation = Quaternion.Euler (0, 0, travelAngle);
        if (Time.time - startTime > lifeSpanSecs) {
            explode ();
        }
    }
    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject != parent) {
            explode ();
        }
    }
    void explode () {
        Destroy (this.gameObject);
    }
}

