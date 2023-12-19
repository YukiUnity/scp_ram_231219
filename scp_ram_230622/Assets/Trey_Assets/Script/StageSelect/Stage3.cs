using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage3 : StageSelect_sim
{

    public static bool EnterFlag;

    // Use this for initialization
    void Start()
    {
        ButtonFlag[2] = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        ButtonFlag[2] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag[2] = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        Invoke("LoadScene", 0.2f);
    }

    void LoadScene()
    {
        //SceneManager.LoadScene("Stage_3_1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
