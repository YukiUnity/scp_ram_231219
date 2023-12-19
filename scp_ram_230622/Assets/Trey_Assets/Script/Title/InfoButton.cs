using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : Title_sim
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

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        //Invoke("LoadScene", 0.2f);
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (InfoButtonImage.GetAlpha() < 1f)
            {
                InfoButtonImage.SetAlpha(InfoButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (InfoButtonImage.GetAlpha() > 0f)
            {
                InfoButtonImage.SetAlpha(InfoButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
    */
}
