using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Info_sim : simdata_predata {

    public static float starttime;

    public static bool[] ButtonFlag_Tab_Enter = new bool[10];
    public static bool[] ButtonFlag_Tab_Enter_Flag = new bool[10];
    public static bool[] ButtonFlag_Tab_Click = new bool[10];

    public static bool[] ButtonFlag_Sub_Enter = new bool[100];
    public static bool[] ButtonFlag_Sub_Enter_Flag = new bool[100];
    public static bool[] ButtonFlag_Sub_Click = new bool[100];

    public static CanvasRenderer SubStory0ButtonImage;
    public static CanvasRenderer SubStory1ButtonImage;

    public static GameObject SubStory0Text;
    public static GameObject SubStory1Text;

    // Use this for initialization
    public static void InitScene () {
        starttime = 0;

        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();
        //MouseClickBack = GameObject.Find("MousClickBackSE").GetComponent<AudioSource>(); 

        BackButtonImage = GameObject.Find("BackButtonImage").GetComponent<CanvasRenderer>();
        BackButtonImage.SetAlpha(0);

        SubStory0ButtonImage = GameObject.Find("SubStory0ButtonImage").GetComponent<CanvasRenderer>();
        SubStory0ButtonImage.SetAlpha(0);
        SubStory1ButtonImage = GameObject.Find("SubStory1ButtonImage").GetComponent<CanvasRenderer>();
        SubStory1ButtonImage.SetAlpha(0);

        SubStory0Text = GameObject.Find("SubStory0Text");
        SubStory0Text.SetActive(false);
        SubStory1Text = GameObject.Find("SubStory1Text");
        SubStory1Text.SetActive(false);

        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        for (int i = 0; i < 60; i++)
        {
            //ClearText[i] = GameObject.Find("Canvas").transform.Find("StageClear" + i).gameObject;
        }

        for(int i =0; i < 10;i++)
        {
            ButtonFlag_Tab_Enter[i] = false;
            ButtonFlag_Tab_Enter_Flag[i] = false;
            ButtonFlag_Tab_Click[i] = false;
        }

        for(int i =0; i< 100; i++)
        {
            ButtonFlag_Sub_Enter[i] = false;
            ButtonFlag_Sub_Enter_Flag[i] = false;
            ButtonFlag_Sub_Click[i] = false;
        }
    }

    public static void BackButtonSim()
    {
        if(ButtonFlag_Tab_Enter[4] && !ButtonFlag_Tab_Enter_Flag[4])
        {
            //MouseEnter.Play();
            ButtonFlag_Tab_Enter_Flag[4] = true;
        }

        if(!ButtonFlag_Tab_Enter[4])
        {
            ButtonFlag_Tab_Enter_Flag[4] = false;
        }

        if (ButtonFlag_Tab_Enter[4])
        {
            if (BackButtonImage.GetAlpha() < 1f)
            {
                BackButtonImage.SetAlpha(BackButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (BackButtonImage.GetAlpha() > 0f)
            {
                BackButtonImage.SetAlpha(BackButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }

        if(ButtonFlag_Tab_Click[4])
        {
            //calc.NextScene("Title");
        }
    }

    public static void SubStory0Sim()
    {
        int ID = 0;

        if (StageClearFlag[ID])
        {
            SubStory0Text.SetActive(true);

            if (ButtonFlag_Sub_Enter[ID] && !ButtonFlag_Sub_Enter_Flag[ID])
            {
                ButtonFlag_Sub_Enter_Flag[ID] = true;
            }

            if (!ButtonFlag_Sub_Enter[ID])
            {
                ButtonFlag_Sub_Enter_Flag[ID] = false;
            }

            if (ButtonFlag_Sub_Enter[ID])
            {
                if (SubStory0ButtonImage.GetAlpha() < 1f)
                {
                    SubStory0ButtonImage.SetAlpha(SubStory0ButtonImage.GetAlpha() + (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
            }
            else
            {
                if (SubStory0ButtonImage.GetAlpha() > 0f)
                {
                    SubStory0ButtonImage.SetAlpha(SubStory0ButtonImage.GetAlpha() - (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
            }

            if (ButtonFlag_Sub_Click[ID])
            {
                //calc.NextScene("SubStory0");
            }
        }
    }

    public static void SubStory1Sim()
    {
        int ID = 1;

        if (StageClearFlag[ID])
        {
            SubStory1Text.SetActive(true);

            if (ButtonFlag_Sub_Enter[ID] && !ButtonFlag_Sub_Enter_Flag[ID])
            {
                ButtonFlag_Sub_Enter_Flag[ID] = true;
            }

            if (!ButtonFlag_Sub_Enter[ID])
            {
                ButtonFlag_Sub_Enter_Flag[ID] = false;
            }

            if (ButtonFlag_Sub_Enter[ID])
            {
                if (SubStory1ButtonImage.GetAlpha() < 1f)
                {
                    SubStory1ButtonImage.SetAlpha(SubStory1ButtonImage.GetAlpha() + (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
            }
            else
            {
                if (SubStory1ButtonImage.GetAlpha() > 0f)
                {
                    SubStory1ButtonImage.SetAlpha(SubStory1ButtonImage.GetAlpha() - (1f / 1f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
            }

            if (ButtonFlag_Sub_Click[ID])
            {
                //calc.NextScene("SubStory0");
            }
        }
    }

    // Update is called once per frame
    public static void UpdateScene ()
    {

            BackButtonSim();
            SubStory0Sim();

    }
}
