using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtActive : MonoBehaviour
{
    // Start is called before the first frame update
    private ArrowKeyControls arrowControlsScript; 
    void Start()
    {
        arrowControlsScript = gameObject.GetComponent(ArrowKeyControls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
