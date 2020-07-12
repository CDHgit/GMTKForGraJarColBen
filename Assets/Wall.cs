using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    int health = 1;
    // Start is called before the first frame update
    void Start () {

    }
    void changeHealth (int delta) {
        health += delta;
    }

    // Update is called once per frame
    void Update () {
        if (health <= 0) {
            this.gameObject.SetActive (false);
        }
    }
}