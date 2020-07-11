using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Action {
    void performAction ();
}
public class TestAction : Action {
    public void performAction () {
       // Debug.Log ("hello");
    }
}