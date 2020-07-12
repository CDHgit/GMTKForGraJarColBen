﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbedControl : MonoBehaviour {
    public int damage = 15;
    public float thrust = 1;
    public float lifeSpanSecs = 5; // Is negative if the object lands and stays persistent
    public float initialSpeed = 5;
    public float maxSpeed = 5;
    public float rotationSpeed = 1;
    public float maxSize = 1.5f;
    public GameObject explosionParticles = null;
    public GameObject explosionHitbox = null;

    Rigidbody2D rigidBody;
    Transform mTransform;
    float travelAngle;
    float rotation;
    float startTime;
    float lobSpeed;
    Vector3 originalScale;
    bool armed = false;
    GameObject parent;
    // Start is called before the first frame update
    void Start () {
        mTransform = GetComponent<Transform> ();
        
        startTime = Time.time;
        lobSpeed = Mathf.PI / Mathf.Abs(lifeSpanSecs);
        originalScale = transform.localScale;
    }
    void initLob (float fl) {
        travelAngle = fl;
        rotation = travelAngle;
        rigidBody = GetComponent<Rigidbody2D> ();
        rigidBody.velocity = initialSpeed * new Vector2 (-Mathf.Sin (travelAngle * Mathf.PI / 180f), Mathf.Cos (travelAngle * Mathf.PI / 180f));

    }

    void setParent (GameObject o) {
        parent = o;
    }
    // Update is called once per frame
    void Update () {

        if (Time.time - startTime > Mathf.Abs(lifeSpanSecs)) {
            if (lifeSpanSecs > 0)
                explode ();
            else
            {
                if (!armed)
                {
                    armed = true;
                    rigidBody.velocity = Vector2.zero;
                    this.gameObject.AddComponent<CircleCollider2D>();
                    CircleCollider2D collider = GetComponent<CircleCollider2D>();
                    collider.isTrigger = true;
                    rigidBody.mass = 100000;
                }
            }
        } else
        {
            rigidBody.AddForce(thrust * new Vector2(-Mathf.Sin(travelAngle * Mathf.PI / 180f), Mathf.Cos(travelAngle * Mathf.PI / 180f)));
            rotation += rotationSpeed;
            mTransform.rotation = Quaternion.Euler(0, 0, rotation);

            float lobScale = (Mathf.Sin((Time.time - startTime) * lobSpeed)) * (maxSize - 1) + 1;
            transform.localScale = new Vector3(lobScale * originalScale.x, lobScale * originalScale.y, originalScale.z);
        }
    }
    void OnTriggerEnter2D (Collider2D collision) {
        if (armed)
        {
            explode();
        }
    }
    void explode () {

        //summon explosion hitbox and effect
        GameObject.Instantiate(explosionParticles, this.transform.position, Quaternion.Euler(0, 0, 0));
        GameObject hit = GameObject.Instantiate(explosionHitbox, this.transform.position, Quaternion.Euler(0, 0, 0));
        hit.SendMessage("init", damage);
        Destroy (this.gameObject);
    }
}