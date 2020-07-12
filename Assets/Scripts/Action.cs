using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {
    
    Vector3 startActionOrigin = new Vector3(-716, 322, -100);
    Vector3 trackIndexTransition = new Vector3(104, 0, 0);
    Vector3 actionIndexTransition = new Vector3(0, -154, 0);
    public string spriteName; // For use of finding the ritght icon
    public Sprite actionIcon; // For use of displaying the right icon

    private GameObject conveyorAction;
    public abstract void performAction (GameObject o);
    public void deleteAction()
    {
        Destroy(this.conveyorAction);
    }
    //Constructor
    public Action(int actionIdx, int trackIdx)
    {
        // Get the action prefab from the resources folder 
        GameObject actionPrefab = Resources.Load("Prefabs/Action") as GameObject;
        // Get the sprite from the resources folder(need to move them over for now)
        actionIcon = Resources.Load<Sprite>(spriteName);
        // placeholder position for start
        Vector3 actionPosition = new Vector3(-12f, -12f, -99f);
        // placeholder transform to put me in the canvas
        Transform canvasTransform = GameObject.FindGameObjectWithTag("ActionTrackCanvas").transform;
        // add the new action to game
        this.conveyorAction = Instantiate(actionPrefab, actionPosition, Quaternion.identity, canvasTransform);
        // set the sprite for the icon
        this.conveyorAction.GetComponent<SpriteRenderer>().sprite = actionIcon;
        // set the start position based on the action index and track index
        this.conveyorAction.GetComponent<RectTransform>().localPosition = startActionOrigin + (actionIdx * actionIndexTransition) + (trackIdx * trackIndexTransition);
        // ahaha how are you that small
        this.conveyorAction.GetComponent<RectTransform>().localScale = new Vector3(100, 100, 1);
    }
    public RectTransform getRectTrans()
    {
        return this.conveyorAction.GetComponent<RectTransform>();
    }
    
}
public class EmptyAction : Action
{
    public EmptyAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        //Do Nothing
    }
}
public class TestAction : Action 
{
    public TestAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        // Debug.Log ("hello"); 
    }
}
public class DashAction : Action 
{
    public DashAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        o.SendMessage("startDash");
    }

}
public class RocketFireAction : Action 
{ 
     public RocketFireAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        o.SendMessage("fireRocket");
    }
}
public class LaserAction : Action 
{
    public LaserAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        o.SendMessage("shootAt");
    }
}

public class HealAction : Action
{
    public HealAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeHealth");
    }
}

public class AntivirusAction : Action
{
    public AntivirusAction(int actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeAntivirus");
    }
}

