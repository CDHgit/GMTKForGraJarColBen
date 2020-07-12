using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeIn : MonoBehaviour
{
    public float secToIn=4;
    public Image slave;
    // Start is called before the first frame update
    void Start()
    {
        slave.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > secToIn){
            slave.enabled=true;
        }
    }
}
