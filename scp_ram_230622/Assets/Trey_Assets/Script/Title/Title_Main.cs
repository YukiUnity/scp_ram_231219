using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Title_Main : simdata_predata
{

    // Use this for initialization
    void Start()
    {
        Title_sim.InitScene();
        //DrawObj.DrawScene();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Title_sim.UpdateScene();
    }
}