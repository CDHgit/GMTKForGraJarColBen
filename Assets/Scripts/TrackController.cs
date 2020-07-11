using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public int trackSize = 16; //Number of beats each track keeps at a time.
    public float bpm = 128f;
    private static int size = 3;
    private Track[] tracks = new Track[size];


    // Start is called before the first frame update
    void Start () {
        for (int i = 0; i < size; i++) {
            tracks[i] = new Track (trackSize);
        }
        
    }

    // Update is called once per frame
    void Update () {
        
    }
    void onBeat(){
        foreach (Track t in tracks){
            t.runBeat();
        }
    }
}