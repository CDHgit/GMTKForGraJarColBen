using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitboxControl : MonoBehaviour {
    public float lifeSpanSecs = 0.3f; 
    float startTime;
    int damage;

    // Start is called before the first frame update
    void Start () {
        
    }

    void init (int DAM)
    {
        damage = DAM;
        this.gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D collider = this.GetComponent<CircleCollider2D>();
        collider.radius = 0.6f;
        collider.isTrigger = true;

        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update () {

        if (Time.time - startTime > Mathf.Abs(lifeSpanSecs)) {
            Destroy(this.gameObject);
        } 
    }
    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Destructable"))
        {
            collision.gameObject.GetComponent<MechInfo>().changeHealth(-damage);
            Destroy(this.gameObject);
        }
    }
}