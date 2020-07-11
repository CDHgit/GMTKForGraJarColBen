using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public int trackSize = 16; //Number of beats each track keeps at a time.
    public float bpm = 128f;
    KeyCode track1Key= KeyCode.U, track2Key= KeyCode.I, track3Key= KeyCode.O;

    public Context context; 
    static int size = 3;
    private Track[] tracks = new Track[size];


    // Start is called before the first frame update
    void Start () {
        for (int i = 0; i < size; i++) {
            tracks[i] = new Track (trackSize);
        }
        context.mech1.SendMessage("setTrack", tracks[0]);
        context.mech2.SendMessage("setTrack", tracks[0]);
        context.mech3.SendMessage("setTrack", tracks[0]);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(track1Key)){
            context.getCurMech().SendMessage("setTrack", tracks[0]);
        } else if (Input.GetKey(track2Key)){
            context.getCurMech().SendMessage("setTrack", tracks[1]);
        } else if (Input.GetKey(track3Key)){
            context.getCurMech().SendMessage("setTrack", tracks[2]);
        }
    }
    void onBeat(){
        foreach (Track t in tracks){
            // t.runBeat();
        }
    }
}