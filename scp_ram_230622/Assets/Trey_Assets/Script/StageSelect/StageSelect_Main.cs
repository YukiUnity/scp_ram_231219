using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StageSelect_Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StageSelect_sim.InitScene();
        //DrawObj.DrawScene();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StageSelect_sim.UpdateScene();
    }
}