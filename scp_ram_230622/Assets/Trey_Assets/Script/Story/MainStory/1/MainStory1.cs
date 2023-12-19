using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MainStory1 : simdata_predata
{

    public string[] scenarios;
    [SerializeField] Text uiText;
    [SerializeField] Text uiName;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private string currentName = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    public static int currentLine = 0;
    private int lastUpdateCharacter = -1;

    //csv
    string CSVName;
    string CSVNumber;
    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリス
    string StringReader;
    int height = 0;

    //public bool activeActor = false;
    [SerializeField] GameObject[] ActorList;
    [SerializeField] GameObject[] Actor;
    //falseが左
    public bool[] sideActor;
    public bool[] activeActor;
    public int[] trigerEvent;
    private int number = 0;
    private bool[] ActorFlag;
    private bool[] ActorSide;
    private int actor;

    //180903決定音
    [SerializeField] AudioSource Sound;

    //---------------------------------------------------------------181114
    public static bool isLoading = false;

    //---------------------------------------------------------------181115
    public static bool isSelecting = false;

    public static bool[] choice = new bool[2];
    public static int StoryLine = 0;
    public static GameObject SelectableWord0;

    //---------------------------------------------------------------190105
    public static CanvasRenderer Choice0;
    public static CanvasRenderer Choice1;
    //---------------------------------------------------------------


    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        ActorFlag = new bool[ActorList.Length];
        ActorSide = new bool[ActorList.Length];
        //--------------------------------------------------181112
        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        currentLine = 0;
        //--------------------------------------------------1811115
        isLoading = false;
        isSelecting = false;

        for (int i = 0; i < choice.Length; i++)
        {
            choice[i] = false;
        }

        StoryLine = 1;
        SelectableWord0 = GameObject.Find("Canvas").transform.Find("SelectableWord0").gameObject;

        ChoiceText[0] = GameObject.Find("Canvas").transform.Find("SelectableWord0").gameObject.transform.Find("Choice0Text").GetComponent<Text>();
        ChoiceText[1] = GameObject.Find("Canvas").transform.Find("SelectableWord0").gameObject.transform.Find("Choice1Text").GetComponent<Text>();

        //--------------------------------------------------181115
        switch (Language)
        {
            case (0):
                ChoiceText[0].text = "Odd stuffed!?";
                ChoiceText[1].text = "A pet!?";
                CSVName = "MainStory1_EN";
                break;
            case (1):
                ChoiceText[0].text = "有趣的毛绒动物！？";
                ChoiceText[1].text = "宠物！？";
                CSVName = "MainStory1_CN";
                break;
            case (2):
                ChoiceText[0].text = "変なぬいぐるみ！？";
                ChoiceText[1].text = "ペット！？";
                CSVName = "MainStory1_JP";
                break;
        }
        //-------------------------------------------------

        //--------------------------------------------------190105
        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();

        Choice0 = GameObject.Find("Canvas").transform.Find("SelectableWord0").gameObject.transform.Find("Mask_UI_Select_Up").transform.Find("UI_Select_Up").GetComponent<CanvasRenderer>();
        Choice1 = GameObject.Find("Canvas").transform.Find("SelectableWord0").gameObject.transform.Find("Mask_UI_Select_Down").transform.Find("UI_Select_Down").GetComponent<CanvasRenderer>();

        //--------------------------------------------------

        CSVNumber = "";
        // Resouces/CSV下のCSV読み込み 
        csvFile = Resources.Load("CSV/" + CSVName + CSVNumber) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // 配列に格納
            csvDatas.Add(line.Split(','));
            // 行数加算
            height++;
        }

        SetNextLine();
    }

    void Update()
    {



    }


    void SetNextLine()
    {
        //-----------------------------------------------------------------------------------------イベント管理
        if (trigerEvent[number] == currentLine)
        {

            switch (activeActor[number])
            {
                case (false):
                    for (int i = 0; i < ActorList.Length; i++)
                    {
                        if (Actor[number] == ActorList[i])
                        {
                            ActorFlag[i] = false;
                            ActorSide[i] = sideActor[number];

                            //複数イベント処理
                            for (int n = 0; n < trigerEvent.Length; n++)
                            {
                                if (trigerEvent[n] == currentLine)
                                {
                                    for (int k = 0; k < ActorList.Length; k++)
                                    {
                                        if (Actor[n] == ActorList[k])
                                        {
                                            switch (activeActor[n])
                                            {
                                                case (false):
                                                    ActorFlag[k] = false;
                                                    ActorSide[k] = sideActor[n];
                                                    break;
                                                case (true):
                                                    ActorFlag[k] = true;
                                                    ActorSide[k] = sideActor[n];
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {


                        }
                    }
                    break;
                case (true):
                    for (int i = 0; i < ActorList.Length; i++)
                    {
                        if (Actor[number] == ActorList[i])
                        {
                            ActorFlag[i] = true;
                            ActorSide[i] = sideActor[number];

                            //複数イベント処理
                            for (int n = 0; n < trigerEvent.Length; n++)
                            {
                                if (trigerEvent[n] == currentLine)
                                {
                                    for (int k = 0; k < ActorList.Length; k++)
                                    {
                                        if (Actor[n] == ActorList[k])
                                        {
                                            switch (activeActor[n])
                                            {
                                                case (false):
                                                    ActorFlag[k] = false;
                                                    ActorSide[k] = sideActor[n];
                                                    break;
                                                case (true):
                                                    ActorFlag[k] = true;
                                                    ActorSide[k] = sideActor[n];
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {


                        }
                    }
                    break;
                default:
                    break;
            }
            for (int i = 0; i < trigerEvent.Length; i++)
            {
                if (trigerEvent[number + 1] == currentLine)
                {
                    number++;
                }
            }
            number++;
            Debug.Log(ActorFlag[0]);
        }

        //----------------------------------------------------------------------------------------------

        currentText = csvDatas[currentLine][StoryLine];
        currentName = csvDatas[currentLine][StoryLine - 1];

        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;


    }

    //181112
    void ActorSimulation()
    {
        if (currentLine == 2)
        {
            Debug.Log("target height");
        }

        for (int i = 0; i < ActorList.Length; i++)
        {
            if (ActorFlag[i])
            {
                if (ActorSide[i] == false && ActorList[i].transform.position.x < -3.8)
                {
                    ActorList[i].transform.position = new Vector3(ActorList[i].transform.position.x + 0.33f, -1.4f, 0f);
                    if (ActorList[i].GetComponent<SpriteRenderer>().color.a < 1.0f)
                    {
                        ActorList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ActorList[i].GetComponent<SpriteRenderer>().color.a + 0.02f);
                    }

                }

                if (ActorSide[i] == true && ActorList[i].transform.position.x > 4.8)
                {
                    ActorList[i].transform.position = new Vector3(ActorList[i].transform.position.x - 0.33f, -1.4f, 0f);
                    if (ActorList[i].GetComponent<SpriteRenderer>().color.a < 1.0f)
                    {
                        ActorList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ActorList[i].GetComponent<SpriteRenderer>().color.a + 0.02f);
                    }
                }
            }
            else
            {
                if (ActorSide[i] == false && ActorList[i].transform.position.x > -15.0)
                {
                    ActorList[i].transform.position = new Vector3(ActorList[i].transform.position.x - 0.33f, -1.4f, 0f);
                    if (ActorList[i].GetComponent<SpriteRenderer>().color.a > 0.0f)
                    {
                        ActorList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ActorList[i].GetComponent<SpriteRenderer>().color.a - 0.02f);
                    }
                }

                if (ActorSide[i] == true && ActorList[i].transform.position.x < 15.0)
                {
                    ActorList[i].transform.position = new Vector3(ActorList[i].transform.position.x + 0.33f, -1.4f, 0f);
                    if (ActorList[i].GetComponent<SpriteRenderer>().color.a > 0.0f)
                    {
                        ActorList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ActorList[i].GetComponent<SpriteRenderer>().color.a - 0.02f);
                    }
                }
            }
        }
    }

    void SwitchScene()
    {
        if (currentText == "end" && !isLoading)
        {
            calc.NextScene(LastScene);
            isLoading = true;
        }
        /*
        if (currentLine == 38)
        {
            if (Input.GetMouseButtonDown(0) && !isLoading)
            {
                calc.NextScene(LastScene);
                isLoading = true;
                //SceneManager.LoadScene("StageSelect");
            }
        }
        */
    }

    void SelectableWord()
    {
        if (currentLine == 10 && !isSelecting)
        {
            SelectableWord0.SetActive(true);
            isSelecting = true;
        }

        if (choice[0])
        {
            SelectableWord0.SetActive(false);
            isSelecting = false;
            //180903決定音
            //Sound.Play();
            SetNextLine();
            choice[0] = false;
        }

        if (choice[1])
        {
            StoryLine = 3;
            SelectableWord0.SetActive(false);
            isSelecting = false;
            //180903決定音
            //Sound.Play();
            SetNextLine();
            choice[1] = false;
        }
    }

    //Actorのシミュレーション
    private void FixedUpdate()
    {

        if (!isSelecting)
        {
            // 文字の表示が完了してるならクリック時に次の行を表示する
            if (IsCompleteDisplayText)
            {
                if (currentLine < height && Input.GetMouseButtonDown(0))
                {
                    //180903決定音
                    Sound.Play();
                    SetNextLine();
                }
            }
            else
            {
                // 完了してないなら文字をすべて表示する
                if (Input.GetMouseButtonDown(0))
                {
                    timeUntilDisplay = 0;
                }
            }

            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                uiText.text = currentText.Substring(0, displayCharacterCount);
                uiName.text = currentName;

                lastUpdateCharacter = displayCharacterCount;
            }


            ActorSimulation();

            //181112
            SwitchScene();

        }
        SelectableWord();
    }
}
