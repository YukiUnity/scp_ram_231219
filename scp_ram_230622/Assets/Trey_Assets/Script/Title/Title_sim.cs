using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_sim : simdata_predata
{

    public static Button StartButton;
    public static Button LoadButton;
    public static Button InfoButton;
    public static Button SystemButton;
    public static Button ExitButton;

    public static CanvasRenderer Title_Tube;
    public static CanvasRenderer Title_Name;

    public static CanvasRenderer StartButtonImage;
    public static CanvasRenderer LoadButtonImage;
    public static CanvasRenderer InfoButtonImage;
    public static CanvasRenderer SystemButtonImage;
    public static CanvasRenderer ExitButtonImage;

    //--------------------------------------------------------------------181115
    public static Text TitleText;
    public static Text StartButtonText;
    public static Text LoadButtonText;
    public static Text InfoButtonText;
    public static Text SystemButtonText;
    public static Text ExitButtonText;

    //--------------------------------------------------------------------------

    public static float currenttime;

    public static bool LuxFlag;

    // Use this for initialization
    public static void InitScene()
    {
        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();
        //MouseClickBack = GameObject.Find("MousClickBackSE").GetComponent<AudioSource>(); 

        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        StartButton.onClick.AddListener(StartClick);
        LoadButton = GameObject.Find("LoadButton").GetComponent<Button>();
        LoadButton.onClick.AddListener(LoadClick);
        InfoButton = GameObject.Find("InfoButton").GetComponent<Button>();
        InfoButton.onClick.AddListener(InfoClick);
        SystemButton = GameObject.Find("SystemButton").GetComponent<Button>();
        SystemButton.onClick.AddListener(SystemClick);
        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        ExitButton.onClick.AddListener(ExitClick);

        Title_Tube = GameObject.Find("Title_Tube").GetComponent<CanvasRenderer>();
        Title_Tube.SetAlpha(0);
        Title_Name = GameObject.Find("Title_Name").GetComponent<CanvasRenderer>();
        Title_Name.SetAlpha(0);

        StartButtonImage = GameObject.Find("StartButtonImage").GetComponent<CanvasRenderer>();
        StartButtonImage.SetAlpha(0);
        LoadButtonImage = GameObject.Find("LoadButtonImage").GetComponent<CanvasRenderer>();
        LoadButtonImage.SetAlpha(0);
        InfoButtonImage = GameObject.Find("InfoButtonImage").GetComponent<CanvasRenderer>();
        InfoButtonImage.SetAlpha(0);
        SystemButtonImage = GameObject.Find("SystemButtonImage").GetComponent<CanvasRenderer>();
        SystemButtonImage.SetAlpha(0);
        ExitButtonImage = GameObject.Find("ExitButtonImage").GetComponent<CanvasRenderer>();
        ExitButtonImage.SetAlpha(0);

        LuxFlag = false;

        //--------------------------------------------181113
        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        //---------------------------------------------181115
        TitleText = GameObject.Find("TitleText").GetComponent<Text>();
        StartButtonText = GameObject.Find("StartButtonText").GetComponent<Text>();
        LoadButtonText = GameObject.Find("LoadButtonText").GetComponent<Text>();
        InfoButtonText = GameObject.Find("InfoButtonText").GetComponent<Text>();
        SystemButtonText = GameObject.Find("SystemButtonText").GetComponent<Text>();
        ExitButtonText = GameObject.Find("ExitButtonText").GetComponent<Text>();

        switch (Language)
        {
            case (0):
                TitleText.text = "MAGNET SCOPE";
                StartButtonText.text = "Start Game";
                LoadButtonText.text = "Load Game";
                //InfoButtonText.text = "Info";
                InfoButtonText.text = "Closed";
                //SystemButtonText.text = "System";
                SystemButtonText.text = "Closed";
                ExitButtonText.text = "Exit";

                break;
            case (1):
                TitleText.text = "观察仪器";
                StartButtonText.text = "开始游戏";
                LoadButtonText.text = "加载游戏";
                //InfoButtonText.text = "信息";
                InfoButtonText.text = "信息";
                //SystemButtonText.text = "组态";
                SystemButtonText.text = "组态";
                ExitButtonText.text = "出口";

                break;
            case (2):
                TitleText.text = "MAGNET SCOPE";
                StartButtonText.text = "最初から";
                LoadButtonText.text = "続きから";
                //InfoButtonText.text = "情報";
                InfoButtonText.text = "Closed";
                //SystemButtonText.text = "各種設定";
                SystemButtonText.text = "Closed";
                ExitButtonText.text = "戻る";

                break;
        }


    }

    public static  void StartClick()
    {
        //SceneManager.LoadScene("StageSelect");
    }

    public static void LoadSceneStart()
    {

    }

    public static void LoadClick()
    {

    }

    public static void InfoClick()
    {
        //calc.NextScene("Info");
    }

    public static void SystemClick()
    {

    }

    public static void ExitClick()
    {

    }

    // Update is called once per frame
    public static void UpdateScene()
    {
        currenttime += 0.02f;
        float waittime = 3f;

        if(currenttime >= waittime)
        {
            if (LuxFlag)
            {
                if (Title_Tube.GetAlpha() > 0f)
                {
                    Title_Tube.SetAlpha(Title_Tube.GetAlpha() - (1f / 100f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() - (1f / 100f));
                }
                else
                {
                    LuxFlag = false;
                    currenttime = 0;
                }
            }
            else
            {
                if (Title_Tube.GetAlpha() < 1f)
                {
                    Title_Tube.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                    //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
                }
                else
                {
                    Debug.Log("Flag");
                    LuxFlag = true;
                }
            }
        }
    }
}