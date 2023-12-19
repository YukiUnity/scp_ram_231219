using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Main : MonoBehaviour {

    // Use this for initialization
    void Start () {
        sim.InitScene();
        //DrawObj.DrawScene();
        //-------------------------------------------181110
    }
	
	// Update is called once per frame
	void Update () {
        sim.UpdateScene();
        DrawObj.InitiarizeScene();
        DrawObj.DrawScene();
    }

}
