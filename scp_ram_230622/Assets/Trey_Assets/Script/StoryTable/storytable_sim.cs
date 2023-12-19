using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storytable_sim : simdata_predata
{
    public static GameObject Donna;
    public static GameObject Tau;
    public static GameObject Rephe;
    public static GameObject Yameu;

    public static GameObject Filo;
    public static Texture2D Unknown;

    // Use this for initialization
    public static void InitScene()
    {
        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();

        Donna = GameObject.Find("Donna");
        Tau = GameObject.Find("Tau");
        Rephe = GameObject.Find("Rephe");
        Yameu = GameObject.Find("Yameu");
        Filo = GameObject.Find("Filo");

        Unknown = Resources.Load("TChara_black") as Texture2D;

        BackButtonImage = GameObject.Find("BackButtonImage").GetComponent<CanvasRenderer>();

        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        switch (Language)
        {
            case (0):
                

                break;
            case (1):
                

                break;
            case (2):
                

                break;
        }

        if (StageClearFlag[0] == false)
        {
            Tau.GetComponent<Image>().sprite = Sprite.Create(Unknown, new Rect(0, 0, Unknown.width, Unknown.height), Vector2.zero);
        }

        if (StageClearFlag[1] == false)
        {
            Rephe.GetComponent<Image>().sprite = Sprite.Create(Unknown, new Rect(0, 0, Unknown.width, Unknown.height), Vector2.zero);
        }

        if (StageClearFlag[2] == false)
        {
            Yameu.GetComponent<Image>().sprite = Sprite.Create(Unknown, new Rect(0, 0, Unknown.width, Unknown.height), Vector2.zero);
        }
    }


    // Update is called once per frame
    public static void UpdateScene()
    {
        
    }
}
