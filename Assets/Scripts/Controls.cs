using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public GameObject mech1, mech2, mech3;
    private GameObject curMech;
    // Start is called before the first frame update
    void Start () {
        curMech = mech1;
    }

    // Update is called once per frame
    void Update () {

    }

    /**
     * Switch which mech is being controlled
     */
    void switchContext (int mechNum) {
        Debug.Assert(mechNum > 0 && mechNum < 4, "Mechnum should be in the range [1,3] but was: " + mechNum);
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