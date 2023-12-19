using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ExitButton : Title_sim
{

    public static bool EnterFlag;

    // Use this for initialization
    void Start()
    {
        EnterFlag = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        EnterFlag = true;
    }

    private void OnMouseExit()
    {
        EnterFlag = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        Invoke("LoadScene", 0.6f);
    }

    void LoadScene()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (ExitButtonImage.GetAlpha() < 1f)
            {
                ExitButtonImage.SetAlpha(ExitButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (SystemButtonImage.GetAlpha() > 0f)
            {
                ExitButtonImage.SetAlpha(ExitButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
}
