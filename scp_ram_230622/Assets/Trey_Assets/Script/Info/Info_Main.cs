using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_Main : simdata_predata {

	// Use this for initialization
	void Start () {
        Info_sim.InitScene();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Info_sim.UpdateScene();
	}
}
