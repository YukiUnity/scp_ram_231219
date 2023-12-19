using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton_Info : Info_sim {

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        ButtonFlag_Tab_Enter[4] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag_Tab_Enter[4] = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        Invoke("NextScene", 0.2f);
    }

    public void NextScene()
    {
        ButtonFlag_Tab_Click[4] = true;
        calc.NextScene("Title");
    }


    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
