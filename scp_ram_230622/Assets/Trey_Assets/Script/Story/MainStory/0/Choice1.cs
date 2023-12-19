using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Choice1 : MainStory0
{

    public static bool EnterFlag;

    void Start()
    {
        EnterFlag = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        EnterFlag = true;
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        EnterFlag = false;
    }

    private void OnMouseDown()
    {
        MainStory0.choice[1] = true;
        MouseClickNext.Play();
        Debug.Log("choice1" + MainStory0.choice[1]);
    }

    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (Choice1.GetAlpha() < 1f)
            {
                Choice1.SetAlpha(Choice1.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Choice1.GetAlpha() > 0f)
            {
                Choice1.SetAlpha(Choice1.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
}
