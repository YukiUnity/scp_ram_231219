using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class main_st2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        sim_st2.InitScene();
        //DrawObj.DrawScene();
        //-------------------------------------------181110
    }

    // Update is called once per frame
    void Update()
    {
        sim_st2.UpdateScene();
        DrawObj.InitiarizeScene();
        DrawObj.DrawScene();
    }

}
