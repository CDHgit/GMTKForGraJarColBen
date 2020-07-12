using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {
    
    Vector3 startActionOrigin = new Vector3(502.5f, 308, -100);
    Vector3 trackIndexTransition = new Vector3(104, 0, 0);
    Vector3 actionIndexTransition = new Vector3(0, -154, 0);
    public virtual string spriteName{get {return null;}}    
    public Sprite actionIcon; // For use of displaying the right icon

    private GameObject conveyorAction;
    public abstract void performAction (GameObject o);
    public void deleteAction()
    {
        Destroy(this.conveyorAction);
    }
    //Empty Constructor
    public Action(){}
    //Constructor
    public Action(float actionIdx, int trackIdx)
    {
        // Get the action prefab from the resources folder 
        GameObject actionPrefab = Resources.Load("Prefabs/Action") as GameObject;
        // Get the sprite from the resources folder using a property of the implementing actions
        actionIcon = Resources.Load<Sprite>("Icons/" + this.spriteName);
        // Debug.Log("ActionIcon: " + actionIcon);
        // Debug.Log("SpriteName: " + spriteName);
        // placeholder position for start
        Vector3 actionPosition = new Vector3(-12f, -12f, -99f);
        // placeholder transform to put me in the canvas
        Transform canvasTransform = GameObject.FindGameObjectWithTag("ActionTrackCanvas").transform;
        // add the new action to game
        this.conveyorAction = Instantiate(actionPrefab, actionPosition, Quaternion.identity, canvasTransform);
        // set the sprite for the icon child
        GameObject iconChild = this.conveyorAction.transform.Find("ActionIcon").gameObject;
        iconChild.GetComponent<SpriteRenderer>().sprite = actionIcon;
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
    public EmptyAction(float actionIdx, int trackIdx) : base(){}
    public override void performAction(GameObject o)
    {
        //Do Nothing
    }
}
public class TestAction : Action 
{
    public TestAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        // //Debug.Log ("hello"); 
    }
}
public class DashAction : Action 
{
    public override string spriteName{get {return "Dash";}}
    public DashAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        o.SendMessage("startDash");
    }

}
public class RocketFireAction : Action 
{ 
    public override string spriteName{get {return "Non-Homing Missile";}}
    public RocketFireAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction (GameObject o) {
        o.SendMessage("fireRocket");
    }
}


public class LaserAction : Action
{
    public override string spriteName{get {return "Yellow Laser";}}
    public LaserAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        o.SendMessage("fireLaser");
    }
}
public class BulletAction : Action
{
    public override void performAction(GameObject o)
    {
        o.SendMessage("fireBullet");
    }
}

public class HealAction : Action
{
    public override string spriteName{get {return "Healing";}}
    public HealAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeHealth");
    }
}

public class AntivirusAction : Action
{
    public override string spriteName{get {return "Antivirus Progress Icon";}}
    public AntivirusAction(float actionIdx, int trackIdx) : base(actionIdx, trackIdx){}
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeAntivirus");
    }
}

