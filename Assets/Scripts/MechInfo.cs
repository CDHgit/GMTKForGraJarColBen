using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechInfo : MonoBehaviour
{
    public int health;
    public Slider healthSlider;
    public int antivirusProgress;
    public Slider antivirusSlider;
    public Context context;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        GameObject curMech = context.getCurMech();
        if (curMech == this.gameObject)
        {
            antivirusProgress = antivirusProgress++;
        }
        antivirusSlider.value = antivirusProgress;
        //print(Time.timeSinceLevelLoad);
        print(health);
    }

    void Heal(int healValue)
    {
        health = health + healValue;
    }

    void AntivirusHeal(int antivirusHealValue)
    {
        antivirusProgress = antivirusProgress + antivirusHealValue;
    }

}
