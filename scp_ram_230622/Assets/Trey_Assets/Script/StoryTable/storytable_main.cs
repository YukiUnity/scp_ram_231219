using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storytable_main : simdata_predata
{

    // Use this for initialization
    void Start()
    {
        storytable_sim.InitScene();
        //DrawObj.DrawScene();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        storytable_sim.UpdateScene();
    }
}
