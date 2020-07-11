using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Action {
    void performAction (GameObject o);
}
public class TestAction : Action {
    public float getAngleToMouse(GameObject o){
        Vector3 pos = o.GetComponent<Transform> ().position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 vec_to_mouse = new Vector2 (mousePos.x - pos.x, mousePos.y - pos.y);
        float angleBetween = Vector2.SignedAngle (new Vector2 (0, 1), vec_to_mouse);
        
    }
    public void performAction (GameObject o) {
        // Debug.Log ("hello"); 
    }
}
public class DashAction : Action {
    public void performAction (GameObject o) {
        
    }

}