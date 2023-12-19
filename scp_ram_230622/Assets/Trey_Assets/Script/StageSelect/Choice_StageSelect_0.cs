﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Choice_StageSelect_0 : StageSelect_sim
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
        Choice[0] = true;
        MouseClickNext.Play();
        Debug.Log("choice0" + Choice[0]);
    }

    void FixedUpdate()
    {
        if (EnterFlag)
        {
            if (Choice0.GetAlpha() < 1f)
            {
                Choice0.SetAlpha(Choice0.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Choice0.GetAlpha() > 0f)
            {
                Choice0.SetAlpha(Choice0.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
}
