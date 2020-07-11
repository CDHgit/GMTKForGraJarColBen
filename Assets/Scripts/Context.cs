using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour {
    public GameObject mech1, mech2, mech3;

    public int switchCooldownBeats = 8;
    int beatsToReady = 0; //beats left in cooldown
    int curMechIdx = 0;
    KeyCode mech1Key = KeyCode.J, mech2Key = KeyCode.K, mech3Key = KeyCode.L; //Private key codes JKL
    List<GameObject> mechList; // Mech list used to update the active
    ArrowKeyControls mechKeyControlsScript;
    // Start is called before the first frame update
    void Start () {
        // Create a list of mechs to iterate through later for easier updating
        mechList = new List<GameObject> ();
        mechList.Add (mech1);
        mechList.Add (mech2);
        mechList.Add (mech3);
        // This doesn't work right now might need to trigger it or have a 3 state maybe
        // switchMech(0);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown (mech1Key)) {
            switchMech (0);
        } else if (Input.GetKeyDown (mech2Key)) {
            switchMech (1);
        } else if (Input.GetKeyDown (mech3Key)) {
            switchMech (2);
        }
    }

    /**
     * Switch which mech is being controlled
     */
    private void switchMech (int mechNum) {
        Debug.Assert (mechNum >= 0 && mechNum < 3, "Mechnum should be in the range [1,3] but was: " + mechNum);
        // Debug.Log("Switching mech to " + mechNum);
        //Set the active mech to true and the others to false
        if (beatsToReady <= 0 && curMechIdx != mechNum) {
            beatsToReady = switchCooldownBeats;

            for (int mechIdx = 0; mechIdx < 3; mechIdx++) {
                mechKeyControlsScript = mechList[mechIdx].GetComponent<ArrowKeyControls> ();
                if (mechNum == mechIdx) {
                    mechKeyControlsScript.active = true;
                    curMechIdx = mechNum;
                } else {
                    mechKeyControlsScript.active = false;
                }
            }

        }

    }
    public void onBeat (int beatNum) {
        if (beatsToReady > 0) {
            beatsToReady--;
        }
    }
}