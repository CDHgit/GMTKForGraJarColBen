using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public GameObject mech1, mech2, mech2;
    private GameObject curMech = mech1;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    /**
     * Switch between mechs based on what key was pressed
     */
    void switchContext (int mechNum) {
        
        switch (mechNum) {
            case 1:
                curMech = mech1;
                break;
            case 2:
                curMech = mech2;
                break;
            case 3:
                curMech = mech3;
                break;
            default:
                break;
        }
    }
}