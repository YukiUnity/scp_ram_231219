using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_rephe_story : storytable_sim
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
        //MainStory1.choice[0] = true;
        MouseClickNext.Play();
        Debug.Log("choice1" + MainStory1.choice[0]);
    }

    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (Rephe.transform.localScale.x < 1.07f)
            {
                Rephe.transform.localScale = new Vector3(Rephe.transform.localScale.x + 0.08f, Rephe.transform.localScale.y + 0.08f, Rephe.transform.localScale.z + 0.08f);
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
            if (Rephe.transform.localScale.x > 1)
            {
                Rephe.transform.localScale = new Vector3(Rephe.transform.localScale.x - 0.08f, Rephe.transform.localScale.y - 0.08f, Rephe.transform.localScale.z - 0.08f);
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
