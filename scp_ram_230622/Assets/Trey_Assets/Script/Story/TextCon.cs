using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class TextCon : MonoBehaviour
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
    private int currentLine = 0;
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


    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        ActorFlag = new bool[ActorList.Length];
        ActorSide = new bool[ActorList.Length];

        CSVName = "NovelSampleScenario";
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
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < height && Input.GetMouseButtonDown(0))
            {
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

        
    }


    void SetNextLine()
    {
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

        currentText = csvDatas[currentLine][1];
        currentName = csvDatas[currentLine][0];

        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;


    }

    //Actorのシミュレーション
    private void FixedUpdate()
    {
        for (int i = 0; i < ActorList.Length; i++)
        {
            if (ActorFlag[i])
            {
                if (ActorSide[i] == false && ActorList[i].transform.position.x < -4.8)
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
}
