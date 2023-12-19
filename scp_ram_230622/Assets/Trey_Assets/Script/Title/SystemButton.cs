using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemButton : Title_sim
{

    public static bool EnterFlag;

    // Use this for initialization
    void Start()
    {
        EnterFlag = false;
    }

    /*
    private void OnMouseEnter()
    {
        MouseEnter.Play();
        EnterFlag = true;
    }

    private void OnMouseExit()
    {
        EnterFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (SystemButtonImage.GetAlpha() < 1f)
            {
                SystemButtonImage.SetAlpha(SystemButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (SystemButtonImage.GetAlpha() > 0f)
            {
                SystemButtonImage.SetAlpha(SystemButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
    */
}
