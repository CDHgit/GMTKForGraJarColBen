using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineDemo : MonoBehaviour
{

    public Vector2[] pointAs;
    public Vector2[] pointBs;


    void Start() {
        pointAs=new Vector2[3];
        pointBs=new Vector2[3];
    }
    void Update()
    {
        //Creating a dotted line
        DottedLine.DottedLine.Instance.DrawDottedLine(pointAs[0], pointBs[0], 0);

        //Creating another dotted line
        DottedLine.DottedLine.Instance.DrawDottedLine(pointAs[1], pointBs[1], 1);

        DottedLine.DottedLine.Instance.DrawDottedLine(pointAs[2], pointBs[2], 2);
    }
}
