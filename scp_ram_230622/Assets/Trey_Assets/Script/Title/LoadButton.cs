using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : Title_sim
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
        calc.NextScene("StageSelect");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (LoadButtonImage.GetAlpha() < 1f)
            {
                LoadButtonImage.SetAlpha(LoadButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (LoadButtonImage.GetAlpha() > 0f)
            {
                LoadButtonImage.SetAlpha(LoadButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
}
