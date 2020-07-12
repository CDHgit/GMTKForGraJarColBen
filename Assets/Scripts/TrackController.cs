using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackController : MonoBehaviour {

    public int trackSize = 16; //Number of beats each track keeps at a time.
    public float bpm = 128f;
    KeyCode track1Key = KeyCode.U, track2Key = KeyCode.I, track3Key = KeyCode.O;

    private Context context;
    public Image conveyorImageBase;
    static int trackTextureSize = 128;
    Vector3 initTrackPos;
    private float pixelsPerFrame;
    private GameObject target;
    static float heightOfBelt = 154f;
    static float secPerBeat;
    static int size = 3;
    public static float pxPerBeatIncrement;
    private Track[] tracks = new Track[size];

    // Start is called before the first frame update
    void Start () 
    {
        context = GameObject.Find ("ContextManager").GetComponent<Context> ();

        secPerBeat = GameObject.Find ("Audio Source").GetComponent<SongTimer> ().secPerBeat;
        pxPerBeatIncrement = secPerBeat / heightOfBelt;
        initTrackPos = conveyorImageBase.GetComponent<RectTransform>().localPosition;
        tracks[0] = new Track(trackSize, 0, new System.Type[]{typeof(RocketFireAction)});
        tracks[1] = new Track(trackSize, 1, new System.Type[]{typeof(RocketFireAction)});
        tracks[2] = new Track(trackSize, 2, new System.Type[]{typeof(RocketFireAction)});
        for (int i = 0; i < 3; i++) {
            setTrack (context.mechList[i], i);
        }
    }
    public void setTrack(GameObject mech, int trackNum) {
        foreach (Track t in tracks) {
            t.removeMech (mech);
        }
        tracks[trackNum].addMech (mech);
    }
    // Update is called once per physics tick
    void FixedUpdate () {
        // Switch track based on key input
        if (Input.GetKey(track1Key)) {
            setTrack (context.getCurMech (), 0);
        } else if (Input.GetKey (track2Key)) {
            setTrack (context.getCurMech (), 1);
        } else if (Input.GetKey (track3Key)) {
            setTrack (context.getCurMech (), 2);
        }
        float pixToMove = Time.deltaTime / pxPerBeatIncrement;
        //Update track Actions UI
        tracks[0].UpdateActionsUI(pixToMove);
        tracks[1].UpdateActionsUI(pixToMove);
        tracks[2].UpdateActionsUI(pixToMove);
        //Move the track image repeatedly so that it loops
        conveyorImageBase.GetComponent<RectTransform>().localPosition += new Vector3(0,(float)pixToMove,0); 
    }
    void onBeat () {
        foreach (Track t in tracks) {
            t.runBeat ();
        }
        resetConveyorUI();
    }
    void resetConveyorUI() {
        conveyorImageBase.GetComponent<RectTransform>().localPosition = initTrackPos;
    }
}