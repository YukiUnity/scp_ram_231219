using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStory0_Info : Info_sim {

    public int ID = 0;

    private void OnMouseEnter()
    {
        if (StageClearFlag[ID])
        {
            MouseEnter.Play();
            ButtonFlag_Sub_Enter[ID] = true;
        }
    }

    private void OnMouseExit()
    {
        if (StageClearFlag[ID])
        {
            ButtonFlag_Sub_Enter[ID] = false;
        }
    }

    private void OnMouseDown()
    {
        if (StageClearFlag[ID])
        {
            MouseClickNext.Play();
            Invoke("NextScene", 0.2f);
        }
    }
    private void OnMouseUp()
    {
        ButtonFlag_Sub_Click[ID] = false;
    }

    public void NextScene()
    {
        ButtonFlag_Sub_Click[ID] = true;
        LastScene = "Info";
        calc.NextScene("SubStory" + ID);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
