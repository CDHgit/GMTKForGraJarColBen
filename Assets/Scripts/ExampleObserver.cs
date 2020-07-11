using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleObserver : MonoBehaviour
{
    SpriteRenderer spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spRenderer= GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {

    }
    void onBeat(int beatNum){
        spRenderer.flipY = !spRenderer.flipY;
    }
}
