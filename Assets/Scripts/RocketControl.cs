using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour {
    public float thrust = 1;
    public float lifeSpanSecs= 5;
    Rigidbody2D rigidBody;
    Transform mTransform;
    float travelAngle;
    float startTime;
    // Start is called before the first frame update
    void Start () {
        mTransform = GetComponent<Transform> ();
        rigidBody = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }
    void setDirection(float angle){
        travelAngle = angle;
    }
    // Update is called once per frame
    void Update () {
        rigidBody.AddForce (thrust * new Vector2(-Mathf.Sin(travelAngle*Mathf.PI/180f), Mathf.Cos(travelAngle*Mathf.PI/180f)));
        mTransform.rotation = Quaternion.Euler(0,0, travelAngle);
        if (Time.time - startTime > lifeSpanSecs){
            explode();
        }
    }
    void explode() {
        Destroy(this.rigidBody.gameObject);  
    }
}