using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour {

    private readonly string[] mechs = {"Mech1", "Mech2", "Mech3" };

    public int switchCooldownBeats = 8;
    int beatsToReady = 0; //beats left in cooldown
    int curMechIdx = 0;
    KeyCode mech1Key = KeyCode.J, mech2Key = KeyCode.K, mech3Key = KeyCode.L; //Private key codes JKL
    public List<GameObject> mechList; // Mech list used to update the active
    MechControls mechKeyControlsScript;
    bool[] mechsEnabled = new bool[3] {true, true, true};
    // Start is called before the first frame update
    void Start () {
        // Create a list of mechs to iterate through later for easier updating
        mechList = new List<GameObject> ();
        foreach (string s in mechs) {
            mechList.Add(GameObject.Find(s));
        }


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

    public GameObject getCurMech(){
        return mechList[curMechIdx];
    }
    /**
     * Switch which mech is being controlled, checks if mech is enabled
     */
    private void switchMech (int mechNum) {
        Debug.Assert (mechNum >= 0 && mechNum < 3, "Mechnum should be in the range [1,3] but was: " + mechNum);
        // Debug.Log("Switching mech to " + mechNum);
        //Set the active mech to true and the others to false
        if (beatsToReady <= 0 && curMechIdx != mechNum && mechsEnabled[mechNum-1]) {
            beatsToReady = switchCooldownBeats;
            mechList[curMechIdx].SendMessage("setActive", false);
            curMechIdx = mechNum;
            mechList[curMechIdx].SendMessage("setActive", true);
        }
        else
        {
            print("Mech Switch Failed");
        }

    }
    public void onBeat (int beatNum) {
        if (beatsToReady > 0) {
            beatsToReady--;
        }
    }
}