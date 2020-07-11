using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float thrust = 1;
    public float lifeSpanSecs = 5;
    public float initialSpeed = 10;

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
        rigidBody.velocity = initialSpeed * new Vector2 (-Mathf.Sin (travelAngle * Mathf.PI / 180f), Mathf.Cos (travelAngle * Mathf.PI / 180f));

    }

    void setParent (GameObject o) {
        parent = o;
    }
    // Update is called once per frame
    void Update () {
        rigidBody.AddForce (thrust * new Vector2 (-Mathf.Sin (travelAngle * Mathf.PI / 180f), Mathf.Cos (travelAngle * Mathf.PI / 180f)));
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