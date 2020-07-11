using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechInfo : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    public Track curTrack;
    private Texture2D healthTex;
    void Start()
    {
        healthTex = new Texture2D(1,1);
        healthTex.SetPixel(0,0,Color.green);
        healthTex.wrapMode = TextureWrapMode.Repeat;
    }
    public void setTrack(Track t){
        this.curTrack=t;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
