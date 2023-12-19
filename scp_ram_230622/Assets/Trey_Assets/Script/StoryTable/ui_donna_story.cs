using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_donna_story : storytable_sim
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
            if (Donna.transform.localScale.x < 1.07f)
            {
                Donna.transform.localScale = new Vector3(Donna.transform.localScale.x + 0.08f, Donna.transform.localScale.y + 0.08f, Donna.transform.localScale.z + 0.08f);
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
            if (Donna.transform.localScale.x > 1)
            {
                Donna.transform.localScale = new Vector3(Donna.transform.localScale.x - 0.08f, Donna.transform.localScale.y - 0.08f, Donna.transform.localScale.z - 0.08f);
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
