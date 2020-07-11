using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour {
    public GameObject mech1, mech2, mech3;
    KeyCode mech1Key = KeyCode.J, mech2Key = KeyCode.K, mech3Key = KeyCode.L;
    GameObject[] mechList;
    ArrowKeyControls mechKeyControlsScript;
    private int curMechIdx, prevMechIdx;
    // Start is called before the first frame update
    void Start () {
        mechList[0] = mech1;
        mechList[1] = mech2;
        mechList[2] = mech3;
        prevMechIdx= 0;
        curMechIdx = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(mech1Key)){
            switchMech(0);
        } else if (Input.GetKeyDown(mech2Key)){
            switchMech(1);
        } else if (Input.GetKeyDown(mech3Key)){
            switchMech(2);
        }
    }

    /**
     * Switch which mech is being controlled
     */
    private void switchMech (int mechNum) {
        Debug.Assert(mechNum >= 0 && mechNum < 3, "Mechnum should be in the range [1,3] but was: " + mechNum);
        for (int mechIdx = 0; mechIdx < 3; mechIdx ++)
        prevMechControls = mechList[prevMechIdx].GetComponent<ArrowKeyControls>();
        prevMechControls.active = false;
        curMechControls = mechList[curMechIdx].GetComponent<ArrowKeyControls>();
    }
}