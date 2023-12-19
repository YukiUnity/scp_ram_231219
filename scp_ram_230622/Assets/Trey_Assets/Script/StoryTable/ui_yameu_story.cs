using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_yameu_story : storytable_sim
{

    public static bool EnterFlag;

    void Start()
    {
        EnterFlag = false;
    }

    private void OnMouseEnter()
    {
        if (StageClearFlag[2])
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
        if (StageClearFlag[2])
        {
            //MainStory1.choice[0] = true;
            MouseClickNext.Play();
            Debug.Log("choice1" + MainStory1.choice[0]);
        }
    }

    void FixedUpdate()
    {
        if (StageClearFlag[2])
        {
            if (EnterFlag)
            {
                if (Yameu.transform.localScale.x < 1.07f)
                {
                    Yameu.transform.localScale = new Vector3(Yameu.transform.localScale.x + 0.08f, Yameu.transform.localScale.y + 0.08f, Yameu.transform.localScale.z + 0.08f);
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
                if (Yameu.transform.localScale.x > 1)
                {
                    Yameu.transform.localScale = new Vector3(Yameu.transform.localScale.x - 0.08f, Yameu.transform.localScale.y - 0.08f, Yameu.transform.localScale.z - 0.08f);
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
    }
}
