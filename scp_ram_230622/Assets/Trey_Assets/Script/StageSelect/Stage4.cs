using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage4 : StageSelect_sim
{

    public static bool EnterFlag;

    // Use this for initialization
    void Start()
    {
        ButtonFlag[3] = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        ButtonFlag[3] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag[3] = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        Invoke("LoadScene", 0.2f);
    }

    void LoadScene()
    {
        //SceneManager.LoadScene("Stage_4_1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
