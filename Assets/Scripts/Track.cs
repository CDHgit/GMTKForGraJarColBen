using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track {
    public int size; // Beats in the queue
    public Action[] possibleActions = new Action[] { new TestAction () }; //potential actions to pull from
    private Queue actions = new Queue (); //action queue

    public Track (int size) {
        this.size = size;
        for (int i = 0; i < size; i++) {
            addAction ();
        }
    }

    /**
     * Runs the next beat
     */
    public void runBeat () {
        consumeAction ();
        addAction ();
    }

    /**
     * Sets the possible actions this Track can pull from
     */
    public void setPossibleActions (Action[] actions) {
        this.possibleActions = actions;
    }

    /**
     * Adds a random action to the end of our loaded actions
     */
    private void addAction () {
        actions.Enqueue (possibleActions[Random.Range (0, possibleActions.Length)]);
    }
    /**
     * Runs the next action in the queue
     */
    private void consumeAction () {
        ((Action) actions.Dequeue ()).performAction ();
    }

}