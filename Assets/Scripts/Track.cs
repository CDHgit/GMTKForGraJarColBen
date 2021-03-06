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
        this.size = size;
        this.trackIdx = tIdx;
        this.possibleActions=possibleActions;
        // Moved to runBeat for first beat
        // for (int i = 0; i < size; i++) {
        //     addAction ((float)i - .5f, trackIdx);
        // }
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
    public void runBeat (int beatNum) {
        if(beatNum == 1)
        {
            for (int i = 0; i < size; i++) 
            {
                // Adds empty actions for the first 5, random actions after that
                addAction (i, trackIdx, i < 6);
            }
        }
        if(beatNum >= 1)
        {
            consumeAction ();
            addAction(this.size, this.trackIdx);
        }

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
    private void addAction (int actionIdx, int trackIdx, bool isEmptyAction = false) {
        int randIndex = 0;

        if (!isEmptyAction)
        {
            float rand = UnityEngine.Random.Range(0, 100);

            float sum = 0;
            for (int i = 0; i < TrackController.probs.Length; i++)
            {
                sum += TrackController.probs[i];

                if (rand > sum)
                {
                    randIndex = i;
                }
            }
        }
        
        
        // Screw readability all my friends hate readability (if is empty use empty else use random possible action)
        Type actionType = possibleActions[randIndex];
        // Action toEnqueue = Activator.CreateInstance(actionType, actionIdx, trackIdx) as Action;
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
        a.deleteAction();
    }
    public void UpdateActionsUI (float pixToMove) {
        foreach(Action actionItem in actions){
            try{
                RectTransform actionRect = actionItem.getRectTrans();
                if(actionRect != null)
                {
                    actionRect.localPosition += new Vector3(0,(float)pixToMove,0); 
                }
            }catch(NullReferenceException e){} // Deleted before update in race condition
            
        }
    }
}