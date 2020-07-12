using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    public float dps;
    // Start is called before the first frame update
    void Start () {

    }

    float totalDamage = 0;
        void OnTriggerStay2D(Collider2D collision)
 {
//     Debug.Log("stay" + totalDamage+ " "+ Time.deltaTime);
        GameObject collisionObject = collision.gameObject;
        totalDamage += Time.deltaTime * dps;
        if (collisionObject.CompareTag ("Destructable")) {
            collisionObject.GetComponent<MechInfo> ().changeHealth (-(int)totalDamage);
            totalDamage = totalDamage - (int)totalDamage;
        }
    }
    // Update is called once per frame
    void Update () {

    }
}