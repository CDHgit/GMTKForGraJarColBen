using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
    
    Vector3 startActionOrigin = new Vector3(-716, 322, -100);
    Vector3 trackIndexTransition = new Vector3(104, 0, 0);
    Vector3 actionIndexTransition = new Vector3(0, 154, 0);
    public string spriteName; // For use of finding the ritght icon
    public Sprite actionIcon; // For use of displaying the right icon

    public abstract void performAction (GameObject o);
    //Constructor
    public Action(int actionIdx, int trackIdx){
        GameObject actionPrefab = Resources.Load("Assets/Prefabs/Action") as GameObject;
        actionIcon = Resources.Load<Sprite>(spriteName);
        Vector3 actionPosition = new Vector3(-12f, -12f, -99f);
        GameObject newAction = Instantiate(actionPrefab, actionPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("ActionTrackCanvas").transform);
        newAction.GetComponent<SpriteRenderer>().sprite = actionIcon;
        newAction.GetComponent<RectTransform>().localPosition = startActionOrigin + (actionIdx * actionIndexTransition) + (trackIdx * trackIndexTransition);
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

