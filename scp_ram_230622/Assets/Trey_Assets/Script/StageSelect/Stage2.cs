using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage2 : StageSelect_sim
{

    public static bool EnterFlag;

    // Use this for initialization
    void Start()
    {
        ButtonFlag[1] = false;
    }

    private void OnMouseEnter()
    {
                MouseEnter.Play();
                ButtonFlag[1] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag[1] = false;
    }

    private void OnMouseDown()
    {
                MouseClickNext.Play();
        //calc.NextScene("StoryTable");
        Invoke("LoadScene", 0.2f);
    }

    void LoadScene()
    {
        //SceneManager.LoadScene("Stage_2_1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
