using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtActive : MonoBehaviour
{
    private Context contextObject;
    private GameObject curMech;

    private GameObject bullet;
    // Start is called before the first frame update
    public float DegRange;
    private static Vector3 Vec3Z = new Vector3(0,0,1);
    public GameObject BulletPrefab;
    void Start()
    {
        contextObject = GameObject.Find("ContextManager").GetComponent<Context>();
    }
    public void Update()
    {   
        curMech = contextObject.getCurMech();
        if(gameObject != curMech){
            // Get pos of me
            Vector3 myPos = gameObject.transform.position;
            // Get vector from this object to the target
            Vector3 vectorToGameObject = curMech.transform.position - myPos;
            int DegDither = Random.Range(-(int)DegRange,(int)DegRange);
            // Dont worry about it unless this breaks
            // Add rand angle
            Vector3 ditheredUnitToGameObject = Quaternion.AngleAxis(DegDither, Vec3Z) * vectorToGameObject;
            // Make unit vector
            ditheredUnitToGameObject = ditheredUnitToGameObject/ditheredUnitToGameObject.magnitude;
            // Create bullet ignore collision, add force 2 shoot
            bullet = Instantiate(BulletPrefab, myPos,  Quaternion.LookRotation(Vector3.forward, vectorToGameObject));
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.GetComponent<Rigidbody2D>().AddForce(ditheredUnitToGameObject * 1);
        }
    }
}
