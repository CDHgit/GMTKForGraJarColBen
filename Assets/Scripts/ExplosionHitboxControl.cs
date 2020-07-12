using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitboxControl : MonoBehaviour {
    public float lifeSpanSecs = 0.3f; 
    float startTime;
    int damage;
    bool emp = false;

    // Start is called before the first frame update
    void Start () {
        
    }

    void init (int[] args)
    {
        damage = args[0];
        this.gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D collider = this.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        if (args[1] == 0)
            collider.radius = 0.6f;
        else
        {
            collider.radius = 1f;
            emp = true;
        }
            

        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update () {

        if (Time.time - startTime > lifeSpanSecs) {
            Destroy(this.gameObject);
        } 
    }
    void OnTriggerEnter2D (Collider2D collision) {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Destructable"))
        {
            if (emp)
            {
                collisionObj.GetComponent<MechControls>().empEffect();
            } 
            else
            {
                collisionObj.GetComponent<MechInfo>().changeHealth(-damage);
            }
            
        }
    }


}