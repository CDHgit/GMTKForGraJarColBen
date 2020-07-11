using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions {
    public static float getAngleToMouse(GameObject o){
        Vector3 pos = o.GetComponent<Transform> ().position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 vec_to_mouse = new Vector2 (mousePos.x - pos.x, mousePos.y - pos.y);
        float angleBetween = Vector2.SignedAngle (new Vector2 (0, 1), vec_to_mouse);
        return angleBetween;
    }

    public static float getAngleBetween(GameObject from, GameObject to) {
        Vector3 pos = from.GetComponent<Transform> ().position;
        Vector2 pos2 =  to.GetComponent<Transform>().position;
        Vector2 vecBetween = new Vector2 (pos2.x - pos.x, pos2.y - pos.y);
        float angleBetween = Vector2.SignedAngle (new Vector2 (0, 1), vecBetween);
        return angleBetween;
    }
}