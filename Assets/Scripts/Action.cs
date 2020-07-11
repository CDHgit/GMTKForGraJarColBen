using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {
    public abstract void performAction (GameObject o);
    
}
public class TestAction : Action {
    
    public override void performAction (GameObject o) {
        // Debug.Log ("hello"); 
    }
}
public class DashAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("startDash");
    }

}
public class RocketFireAction : Action { 
    public override void performAction (GameObject o) {
        o.SendMessage("fireRocket");
    }
}
public class LaserAction : Action {
    public override void performAction (GameObject o) {
        o.SendMessage("shootAt");
    }
}

public class HealAction : Action
{
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeHealth");
    }
}

public class AntivirusAction : Action
{
    public override void performAction(GameObject o)
    {
        o.SendMessage("changeAntivirus");
    }
}

