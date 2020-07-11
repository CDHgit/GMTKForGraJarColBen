using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {
    public abstract void performAction (GameObject o);
    public static float getAngleToMouse(GameObject o){
        Vector3 pos = o.GetComponent<Transform> ().position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 vec_to_mouse = new Vector2 (mousePos.x - pos.x, mousePos.y - pos.y);
        float angleBetween = Vector2.SignedAngle (new Vector2 (0, 1), vec_to_mouse);
        return angleBetween;
    }
}
public class TestAction : Action {
    
    public override void performAction (GameObject o) {
        // Debug.Log ("hello"); 
    }
}
public class DashAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("startDash", Action.getAngleToMouse(o));
    }

}
public class RocketFireAction : Action { 
    public override void performAction (GameObject o) {
        o.SendMessage("fireRocket", Action.getAngleToMouse(o));
    }
}
public class LaserAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("shootAt");
    }

}