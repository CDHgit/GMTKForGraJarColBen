using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SongTimer : MonoBehaviour
{
    public AudioSource song;
    public float bpm = 130; //bpm of the song
    public float startOffset = 0; //number of seconds that pass before the first beat
    public GameObject[] observers; //list of all objects that want to be notified when the beat falls


    float secPerBeat; //number of seconds that pass between beats
    float songPos; //number of seconds since the first beat
    float songStart; //time when the song start was recorded
    int lastBeat=0; //the last beat number
    void Start()
    {
        secPerBeat = 60f/bpm;
        songStart = (float)AudioSettings.dspTime;
        song.Play();
	}

	void Update ()
	{
        
        songPos = (float)(AudioSettings.dspTime - songStart - startOffset);
		if (songPos/secPerBeat > lastBeat){
            foreach (GameObject o in observers) {
                o.SendMessage("onBeat", lastBeat);
            }
            lastBeat++;
        }
		
	
    }
}
