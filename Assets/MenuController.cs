using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Play() {
        SceneManager.LoadScene("Level 2");
    }
    public void Credits(){
        SceneManager.LoadScene("Credits");
    }
    public void Menu(){
        SceneManager.LoadScene("Menu");
    }
    public void Tutorial1()
    {
        SceneManager.LoadScene("Tutorial 1");
    }
    public void Tutorial2()
    {
        SceneManager.LoadScene("Tutorial 2");
    }
    public void Tutorial3()
    {
        SceneManager.LoadScene("Tutorial 3");
    }
    public void Quit(){ 
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
