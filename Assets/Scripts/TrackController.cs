using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackController : MonoBehaviour {

    public int trackSize = 16; //Number of beats each track keeps at a time.
    public float bpm = 128f;
    KeyCode track1Key= KeyCode.U, track2Key= KeyCode.I, track3Key= KeyCode.O;

    public Context context; 
    public Image trackImageBase;
    static int trackTextureSize = 128;
    Vector3 initTrackPos;
    private float pixelsPerFrame;
    static float heightOfBelt = 154f;
    static int size = 3;
    static float secPerBeat;
    private Track[] tracks = new Track[size];


    // Start is called before the first frame update
    void Start () {
        secPerBeat = GameObject.Find("Audio Source").GetComponent<SongTimer>().secPerBeat;
        initTrackPos = trackImageBase.GetComponent<RectTransform>().localPosition;
        Debug.Log("size" + context.mech1);
        tracks[0] = new Track(trackSize, new Action[]{new RocketFireAction()});
        for (int i = 1; i < size; i++) {
            tracks[i] = new Track (trackSize, new Action[]{new TestAction()});
        }
        setTrack(context.mech1, 0);
        setTrack(context.mech2, 1);
        setTrack(context.mech3, 2);
       
    }
    public void setTrack(GameObject mech, int trackNum){
        foreach (Track t in tracks){
            t.removeMech(mech);
        }
        tracks[trackNum].addMech(mech);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(track1Key)){
            setTrack(context.getCurMech(), 0);
        } else if (Input.GetKey(track2Key)){
            setTrack(context.getCurMech(), 1);
        } else if (Input.GetKey(track3Key)){
            setTrack(context.getCurMech(), 2);
        }
        float pixToMove = Time.deltaTime / secPerBeat * heightOfBelt;
        trackImageBase.GetComponent<RectTransform>().localPosition += new Vector3(0,(float)pixToMove,0); 
    }
    void onBeat(){
        foreach (Track t in tracks){
            t.runBeat();
        }
        updateTrackUI();

    }
    void updateTrackUI () {
        Debug.Log("Track base image update");
        trackImageBase.GetComponent<RectTransform>().localPosition = initTrackPos;
    }

}