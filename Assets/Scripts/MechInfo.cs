using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechInfo : MonoBehaviour
{
    public int health;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
    }

}
