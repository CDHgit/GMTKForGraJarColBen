using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public int damage = 5;
    public float thrust = 1;
    public float lifeSpanSecs = 5;
    public float initialSpeed = 10;
    public float maxSpeed = 20;
    public float armTime = 1;
    public bool explosive = false;
    public GameObject explosionParticles = null;
    public GameObject explosionHitbox = null;

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
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D (Collider2D collision) {
        GameObject collisionObject = collision.gameObject;
        if (collision.gameObject.name.Contains("Water") || collision.gameObject.name.Contains("Lava")){
            return;
        }
        if (Time.time - startTime > armTime || collisionObject!=parent) {
            if (!explosive && collisionObject.CompareTag("Destructable"))
            {
                collisionObject.SendMessage("changeHealth", -damage);
            }
            else if (explosive)
            {
                collisionObject.SendMessage("changeHealth", -damage);
                //summon explosion hitbox and effect

                GameObject.Instantiate(explosionParticles, this.transform.position, Quaternion.Euler(0, 0, 0));


                GameObject hit = GameObject.Instantiate(explosionHitbox, this.transform.position, Quaternion.Euler(0, 0, 0));

                hit.SendMessage("init", new int[] {damage, 0});


                SoundMixer.PlaySound("Explosion", 0.1f);

            }

            Destroy(this.gameObject);
        }
    }
}