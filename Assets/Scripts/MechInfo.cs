using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechInfo : MonoBehaviour
{
    public int health;
    private Texture2D healthTex;
    void Start()
    {
        healthTex = new Texture2D(1,1);
        healthTex.SetPixel(0,0,Color.green);
        healthTex.wrapMode = TextureWrapMode.Repeat;
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }

}
