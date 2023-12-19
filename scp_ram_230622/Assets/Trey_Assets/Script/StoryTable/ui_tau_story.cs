using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui_tau_story : storytable_sim
{

    public static bool EnterFlag;

    void Start()
    {
        EnterFlag = false;
    }

    private void OnMouseEnter()
    {
        if (StageClearFlag[0])
        {
            MouseEnter.Play();
            EnterFlag = true;
            Debug.Log("Enter");
        }
    }

    private void OnMouseExit()
    {
        EnterFlag = false;
    }

    private void OnMouseDown()
    {
        if (StageClearFlag[0])
        {
            //MainStory1.choice[0] = true;
            MouseClickNext.Play();
            Debug.Log("choice1" + MainStory1.choice[0]);
        }
    }

    void FixedUpdate()
    {
        if (StageClearFlag[0])
        {
            if (EnterFlag)
            {
                if (Tau.transform.localScale.x < 1.07f)
                {
                    Tau.transform.localScale = new Vector3(Tau.transform.localScale.x + 0.08f, Tau.transform.localScale.y + 0.08f, Tau.transform.localScale.z + 0.08f);
                }
                /*
                if (Choice0.GetAlpha() < 1f)
                {
                    Choice0.SetAlpha(Choice0.GetAlpha() + (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
                */
            }
            else
            {
                if (Tau.transform.localScale.x > 1)
                {
                    Tau.transform.localScale = new Vector3(Tau.transform.localScale.x - 0.08f, Tau.transform.localScale.y - 0.08f, Tau.transform.localScale.z - 0.08f);
                }
                /*
                if (Choice0.GetAlpha() > 0f)
                {
                    Choice0.SetAlpha(Choice0.GetAlpha() - (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
                */
            }
        }
        else
        {

        }
    }
}
