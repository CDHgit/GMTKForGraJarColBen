using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }
    void OnTriggerEnter2D (Collider2D collision) {
        //Debug.Log("las hit");
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag ("Destructable")) {

            //Debug.Log("las damage");
            collisionObject.GetComponent<MechControls> ().reduceSpeed ();
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        Debug.Log("awefaew");
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag ("Destructable")) {
            //Debug.Log("las damage");
            collisionObject.GetComponent<MechControls> ().restoreSpeed ();
        }
    }
    // Update is called once per frame
    void Update () {

    }
}