using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Track {
    public int size; // Beats in the queue
    public System.Type[] possibleActions; //potential actions to pull from
    private Queue actions = new Queue (); //action queue
    private List<GameObject> mechs = new List<GameObject>();
    private int trackIdx;
    public Track (int size, int tIdx, System.Type[] possibleActions) {
        this.trackIdx = tIdx;
        this.possibleActions=possibleActions;
        this.size = size;
        for (int i = 0; i < size; i++) {
            addAction (i, trackIdx);
        }
    }
    public void addMech(GameObject mech){
        mechs.Add(mech);
    }
    public void removeMech(GameObject mech){
        mechs.Remove(mech);
    }
    /**
     * Runs the next beat
     */
    public void runBeat () {
        consumeAction ();
        addAction(this.size-1, this.trackIdx);
    }
    
    /**
     * Sets the possible actions this Track can pull from
     */
    public void setPossibleActions (System.Type[] actions) {
        this.possibleActions = actions;
    }

    /**
     * Adds a random action to the end of our loaded actions
     */
    private void addAction (int actionIdx, int trackIdx) {
        Type actionType = possibleActions[UnityEngine.Random.Range (0, possibleActions.Length)];
        actions.Enqueue(Activator.CreateInstance(actionType, actionIdx, trackIdx));
    }
    /**
     * Runs the next action in the queue
     */
    private void consumeAction () {
        Action a = (Action) actions.Dequeue ();
        foreach (GameObject m in mechs) {
            a.performAction (m);
        }
    }
    public void UpdateActionsUI (float pixToMove) {
        foreach(Action actionItem in actions){
            //Move the actions down on each fixedUpdate
        }
    }
}