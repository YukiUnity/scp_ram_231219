using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect_sim : simdata_predata
{


    public static Button Stage_1_Button;
    public static Button Stage_2_Button;
    public static Button Stage_3_Button;
    public static Button Stage_4_Button;
    public static Button BackButton;

    public static CanvasRenderer Title_Tube;
    public static CanvasRenderer Title_Name;

    public static CanvasRenderer Stage_1_ButtonImage;
    public static CanvasRenderer Stage_2_ButtonImage;
    public static CanvasRenderer Stage_3_ButtonImage;
    public static CanvasRenderer Stage_4_ButtonImage;
    public static CanvasRenderer BackButtonImage;

    public static bool[] ButtonFlag = new bool [5];

    public static GameObject SubStoryChoice;

    public static GameObject Stage_1_ButtonCollider;
    public static GameObject Stage_2_ButtonCollider;
    public static GameObject Stage_3_ButtonCollider;
    public static GameObject Stage_4_ButtonCollider;

    //---------------------------------------------------------------190105
    public static CanvasRenderer Choice0;
    public static CanvasRenderer Choice1;
    //---------------------------------------------------------------

    // Use this for initialization
    public static void InitScene()
    {
        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();
        //MouseClickBack = GameObject.Find("MousClickBackSE").GetComponent<AudioSource>(); 

        /*
        Stage_1_Button = GameObject.Find("Stage_1_Button").GetComponent<Button>();
        Stage_1_Button.onClick.AddListener(Stage_1_Click);
        Stage_2_Button = GameObject.Find("Stage_2_Button").GetComponent<Button>();
        Stage_2_Button.onClick.AddListener(Stage_2_Click);
        Stage_3_Button = GameObject.Find("Stage_3_Button").GetComponent<Button>();
        Stage_3_Button.onClick.AddListener(Stage_3_Click);
        Stage_4_Button = GameObject.Find("Stage_4_Button").GetComponent<Button>();
        Stage_4_Button.onClick.AddListener(Stage_4_Click);
        BackButton = GameObject.Find("BackButton").GetComponent<Button>();
        BackButton.onClick.AddListener(BackClick);
        */

        /*
        Title_Tube = GameObject.Find("Title_Tube").GetComponent<CanvasRenderer>();
        Title_Tube.SetAlpha(0);
        Title_Name = GameObject.Find("Title_Name").GetComponent<CanvasRenderer>();
        Title_Name.SetAlpha(0);
        */

        Stage_1_ButtonImage = GameObject.Find("Stage_1_ButtonImage").GetComponent<CanvasRenderer>();
        Stage_1_ButtonImage.SetAlpha(0);
        Stage_2_ButtonImage = GameObject.Find("Stage_2_ButtonImage").GetComponent<CanvasRenderer>();
        Stage_2_ButtonImage.SetAlpha(0);
        Stage_3_ButtonImage = GameObject.Find("Stage_3_ButtonImage").GetComponent<CanvasRenderer>();
        Stage_3_ButtonImage.SetAlpha(0);
        Stage_4_ButtonImage = GameObject.Find("Stage_4_ButtonImage").GetComponent<CanvasRenderer>();
        Stage_4_ButtonImage.SetAlpha(0);
        BackButtonImage = GameObject.Find("BackButtonImage").GetComponent<CanvasRenderer>();
        BackButtonImage.SetAlpha(0);

        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        for (int i = 0; i < 2; i++)
        {
            ClearText[i] = GameObject.Find("Canvas").transform.Find("StageClearText").transform.Find("StageClear" + i).gameObject;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------181115
        SubStoryChoice = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject;

        Stage_1_ButtonCollider = GameObject.Find("Stage_1_ButtonImage");
        Stage_1_ButtonCollider.SetActive(true);
        Stage_2_ButtonCollider = GameObject.Find("Stage_2_ButtonImage");
        Stage_2_ButtonCollider.SetActive(true);
        Stage_3_ButtonCollider = GameObject.Find("Stage_3_ButtonImage");
        Stage_3_ButtonCollider.SetActive(true);
        Stage_4_ButtonCollider = GameObject.Find("Stage_4_ButtonImage");
        Stage_4_ButtonCollider.SetActive(true);

        for (int i = 0; i < Choice.Length; i++)
        {
            Choice[i] = false;
        }

        ChoiceText[2] = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject.transform.Find("ChoiceTitleText").GetComponent<Text>();
        ChoiceText[0] = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject.transform.Find("Choice0Text").GetComponent<Text>();
        ChoiceText[1] = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject.transform.Find("Choice1Text").GetComponent<Text>();

        switch (Language)
        {
            case (0):
                ChoiceText[0].text = "Yes";
                ChoiceText[1].text = "No";
                ChoiceText[2].text = "You have won a new sub-story!\nDo you want to watch it now?";
                break;
            case (1):
                ChoiceText[0].text = "是";
                ChoiceText[1].text = "不";
                ChoiceText[2].text = "你赢了一个新的子故事！\n你想现在看吗？";
                break;
            case (2):
                ChoiceText[0].text = "はい";
                ChoiceText[1].text = "いいえ";
                ChoiceText[2].text = "新しいサブストーリーを獲得しました！\n今すぐ視聴しますか？";
                break;
        }

        //--------------------------------------------------190105
        Choice0 = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject.transform.Find("Mask_UI_Choice_Left").transform.Find("UI_Choice_Left").GetComponent<CanvasRenderer>();
        Choice1 = GameObject.Find("Canvas").transform.Find("SubStoryChoice").gameObject.transform.Find("Mask_UI_Choice_Right").transform.Find("UI_Choice_Right").GetComponent<CanvasRenderer>();

        //--------------------------------------------------

    }

    public static void Stage_1_Enter()
    {
        if (ButtonFlag[0])
        {
            if (Stage_1_ButtonImage.GetAlpha() < 1f)
            {
                Stage_1_ButtonImage.SetAlpha(Stage_1_ButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Stage_1_ButtonImage.GetAlpha() > 0f)
            {
                Stage_1_ButtonImage.SetAlpha(Stage_1_ButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }

    public static void Stage_2_Enter()
    {
        if (ButtonFlag[1])
        {
            if (Stage_2_ButtonImage.GetAlpha() < 1f)
            {
                Stage_2_ButtonImage.SetAlpha(Stage_2_ButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Stage_2_ButtonImage.GetAlpha() > 0f)
            {
                Stage_2_ButtonImage.SetAlpha(Stage_2_ButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }

    public static void Stage_3_Enter()
    {
        if (ButtonFlag[2])
        {
            if (Stage_3_ButtonImage.GetAlpha() < 1f)
            {
                Stage_3_ButtonImage.SetAlpha(Stage_3_ButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Stage_3_ButtonImage.GetAlpha() > 0f)
            {
                Stage_3_ButtonImage.SetAlpha(Stage_3_ButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }

    public static void Stage_4_Enter()
    {
        if (ButtonFlag[3])
        {
            if (Stage_4_ButtonImage.GetAlpha() < 1f)
            {
                Stage_4_ButtonImage.SetAlpha(Stage_4_ButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Stage_4_ButtonImage.GetAlpha() > 0f)
            {
                Stage_4_ButtonImage.SetAlpha(Stage_4_ButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }

    public static void BackEnter()
    {
        if (ButtonFlag[4])
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
    }

    public static void SceneManagement()
    {
        for(int i = 0; i < 60; i++)
        {
            if(MainStoryFlag[i])
            {
                MainStoryFlag[i] = false;
                LastScene = "StageSelect";
                calc.NextScene("MainStory" + i);
            }

            if(StageClearFlag[i])
            {
                ClearText[i].SetActive(true);
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (SubStoryFlag[i] && !LoadUI.activeSelf)
            {
                Stage_1_ButtonCollider.SetActive(false);
                Stage_2_ButtonCollider.SetActive(false);
                Stage_3_ButtonCollider.SetActive(false);
                Stage_4_ButtonCollider.SetActive(false);

                SubStoryChoice.SetActive(true);
                if (Choice[0])
                {
                    SubStoryFlag[i] = false;
                    LastScene = "StageSelect";
                    calc.NextScene("SubStory" + i);
                }
                if(Choice[1])
                {
                    SubStoryFlag[i] = false;
                    SubStoryChoice.SetActive(false);
                    Stage_1_ButtonCollider.SetActive(true);
                    Stage_2_ButtonCollider.SetActive(true);
                    Stage_3_ButtonCollider.SetActive(true);
                    Stage_4_ButtonCollider.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    public static void UpdateScene()
    {
        Stage_1_Enter();
        Stage_2_Enter();
        Stage_3_Enter();
        Stage_4_Enter();
        BackEnter();

        SceneManagement();

        /*
        currenttime += 0.02f;
        float waittime = 3f;

        if (currenttime >= waittime)
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
        */


    }
}