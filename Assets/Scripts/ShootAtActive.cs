using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtActive : MonoBehaviour {
    private Context contextObject;
    private GameObject curMech;
    public GameObject topSprite;
    private GameObject bullet;
    // Start is called before the first frame update
    public float DegRange;
    public float offsetAmount;
    private static Vector3 Vec3Z = new Vector3 (0, 0, 1);
    public GameObject BulletPrefab;
    void Start () {
        contextObject = GameObject.Find ("ContextManager").GetComponent<Context> ();
    }
    void Update () {
        curMech = contextObject.getCurMech ();
        if (this.gameObject == curMech) {
            topSprite.transform.rotation = Quaternion.Euler (0, 0, HelperFunctions.getAngleToMouse (this.gameObject));
        } else {
            topSprite.transform.rotation = Quaternion.Euler (0, 0, HelperFunctions.getAngleBetween (this.gameObject, curMech));
        }
    }
    public void shootAt () {
        float angle;
        if (curMech.GetComponent<MechControls> ().mechEnabled) {
            curMech = contextObject.getCurMech ();
            angle = HelperFunctions.getAngleBetween (this.gameObject, contextObject.getCurMech ());
            float angRad = angle / 180f * Mathf.PI;
            GameObject laser = Instantiate (BulletPrefab,
            this.transform.position + offsetAmount * new Vector3 (-Mathf.Sin (angRad), Mathf.Cos (angRad), 0),
            Quaternion.Euler (0, 0, angle));
            laser.SendMessage ("initBullet", angle);
            laser.SendMessage ("setParent", this.gameObject);
        }
    }
}