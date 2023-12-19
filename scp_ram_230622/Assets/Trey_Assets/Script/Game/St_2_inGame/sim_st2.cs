using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class sim_st2 : simdata //MonoBehaviour から変更181029
{
    public static void InitScene()
    {
        CSVName = "MapData_St2_190205";
        CSVNameB = "MapData_Area_St2_190205";
        height = 0;
        heightB = 0;

        // Resouces/CSV下のCSV読み込み 
        simdata.csvFile = Resources.Load("CSV/" + simdata.CSVName + simdata.CSVNumber) as TextAsset;
        StringReader reader = new StringReader(simdata.csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // 配列に格納
            simdata.csvDatas.Add(line.Split(';'));
            // 行数加算
            simdata.height++;
        }

        // Resouces/CSV下のCSV読み込み (エリア用)
        simdata.csvFileB = Resources.Load("CSV/" + simdata.CSVNameB + simdata.CSVNumberB) as TextAsset;
        StringReader readerB = new StringReader(simdata.csvFileB.text);

        while (readerB.Peek() > -1)
        {
            string lineB = readerB.ReadLine();
            // 配列に格納
            simdata.csvDatasB.Add(lineB.Split(';'));
            // 行数加算
                simdata.heightB++;
        }

        //180825追加、面を別変数を用いて別関数で配置
        //PlaceArea();

        //なにに使ってるか分からない？
        /*for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps * 2 + 2; k++)
            {
                if (i % 2 == 0)
                {
                    if (k < simdata.N_Maps)
                    {
                        simdata.MapSprite[i,k] = simdata.MapObj[i,k].GetComponent<SpriteRenderer>();
                    }
                }
                else
                {
                    simdata.MapSprite[i, k] = simdata.MapObj[i, k].GetComponent<SpriteRenderer>();
                }
            }
        }*/

        //変数初期化
        //-----------------------------------181030
        StartFlag = false;
        starttime = 0;
        StartMusic = GameObject.Find("8-BitDubstepIV").GetComponent<AudioSource>(); //----------------------------------------------------------------------------ステージによって変わる
        EndFlag = false;
        endtime = 0;
        EndMusicWin = GameObject.Find("Groove_It_Now").GetComponent<AudioSource>(); //----------------------------------------------------------------------------ステージによって変わる

        //----------------------------------181116
        PointDrawSE = GameObject.Find("PointDrawSE").GetComponent<AudioSource>();
        PointBackSE = GameObject.Find("PointDrawSE").GetComponent<AudioSource>();
        AreaTakeSE = GameObject.Find("AreaTakeSE").GetComponent<AudioSource>();

        //----------------------------------

        StageID = 0; //----------------------------------------------------------------------------ステージによって変わる

        ResultScreen = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject;

        LoadUI = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        LoadSlider = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        LoadBackGround = GameObject.Find("Canvas").transform.Find("LoadingScreen").gameObject.transform.Find("Image_light").gameObject.GetComponent<CanvasRenderer>();

        DebugMode = false;

        //-----------------------------------181110
        GlitchFx._intensity = 1;

        MapStartPos = new Vector3(-25, 12, 0); //----------------------------------------------------------------------------ステージによって変わる

        NonTerritory = 0;
        BlankTerritory = 1;
        PostPlayerTerritory = 2;
        PlayerTerritory = 3;
        EnemyTerritory = 4;

        PlaceEdgeAndPoint();
        PlaceArea();
        InitMap();

        pastMapConditionB[0, 0] = 10;
        pastMapConditionB[0, 1] = 10;

        for (int j = 0; j < N_Maps * 3; j++)
        {
            for (int i = 0; i < simdata.N_Maps; i++)
            {

                for (int k = 0; k < simdata.N_Maps - 1; k++)
                {

                    pastMapCondition[j, i, k] = BlankTerritory;
                }
            }
        }

        Camera1 = GameObject.Find("Main_Camera");

        //-----------------------------------181105
        Orthographic = Camera1.GetComponent<Camera>();

        //-----------------------------------181118
        MainCanvas = GameObject.Find("Canvas");

        PlayerImage = GameObject.Find("Player").GetComponent<CanvasRenderer>();
        EnemyImage = GameObject.Find("Enemy").GetComponent<CanvasRenderer>();

        isPlayerDamaged = false;
        isEnemyDamaged = false;
        //-----------------------------------

        playerHPGage = GameObject.Find("playerHPGage").GetComponent<Slider>();
        enemyHPGage = GameObject.Find("enemyHPGage").GetComponent<Slider>();

        enemyAttackText = GameObject.Find("enemyAttackText").GetComponent<Text>();
        enemyNextAttackText = GameObject.Find("enemyNextAttackText").GetComponent<Text>();

        enemySkillCDText = GameObject.Find("enemySkillCD").GetComponent<Text>();

        playerHPText = GameObject.Find("playerHPText").GetComponent<Text>();
        enemyHPText = GameObject.Find("enemyHPText").GetComponent<Text>();

        //マップ初期化
        MapAreaCount = 0;
        pastDoCount = 1;

        //HP初期化
        LevelSystem.PlayerInfo();

        enemyName = "ATA-0"; //----------------------------------------------------------------------------ステージによって変わる
        enemyMaxHP = 160; //----------------------------------------------------------------------------ステージによって変わる
        enemyAP = 10; //----------------------------------------------------------------------------ステージによって変わる

        playerHP = playerMaxHP;
        enemyHP = enemyMaxHP;
        playerDamage = 0;
        enemyDamage = 0;
        playerDamageCount = 0;
        enemyDamageCount = 0;
        PlayerDamageSE = GameObject.Find("PlayerDamageSE").GetComponent<AudioSource>();

        MapAreaCount_PL = 0;
        MapAreaCount_EN = 0;

        isDrawing = false;
        isAround = false;

        //敵スキル初期化
        ChargeSkillEffect[0] = GameObject.Find("ChargeSkillEffect" + 0).GetComponent<ParticleSystem>();
        ChargeSkillEffect[1] = GameObject.Find("ChargeSkillEffect" + 1).GetComponent<ParticleSystem>();
        ChargeSkillEffect[2] = GameObject.Find("ChargeSkillEffect" + 2).GetComponent<ParticleSystem>();

        enemySkillNumber = (int)Random.Range(0.3f, 1.4f); //----------------------------------------------------------------------------ステージによって変わる
        switch (enemySkillNumber) //----------------------------------------------------------------------------ステージによって変わる
        {
            case 0:
                enemyAttackCD = (int)Random.Range(3.6f, 4.4f); ;
                break;
            case 1:
                enemyAttackCD = (int)Random.Range(3.6f, 6.4f); ;
                break;

        }

        //----------------------------------------------------------181116
        SkillEffectTime = 0;
        EnemySkillSE[0] = GameObject.Find("HackSE").GetComponent<AudioSource>();
        EnemySkillSE[1] = GameObject.Find("ChargeSE").GetComponent<AudioSource>();

        MapAreaCount_PL = 0;
        MapAreaCount_EN = 0;
        TerritoryCount();

        //----------------------------------------------------------181106
        playerHPEffectGage = playerHP;
        enemyHPEffectGage = enemyHP;

        //---------------------------------------------------------------------------ダメージテキスト保留中
        /*
        for (int i = 0; i < AllTextNumber; i++)
        {
            playerDamageTextObj[i] = GameObject.Find("playerDamageText (" + i + ")");
            playerDamageText[i] = playerDamageTextObj[i].GetComponent<Text>();
            playerDamageTextA[i] = playerDamageTextObj[i].GetComponent<CanvasRenderer>();
            playerDamageTextA[i].SetAlpha(0);
            playerDamageSave[i] = playerDamage;
            playerDamageTextWait[i] = false;

            enemyDamageTextObj[i] = GameObject.Find("enemyDamageText (" + i + ")");
            enemyDamageText[i] = enemyDamageTextObj[i].GetComponent<Text>();
            enemyDamageTextA[i] = enemyDamageTextObj[i].GetComponent<CanvasRenderer>();
            enemyDamageTextA[i].SetAlpha(0);
            enemyDamageSave[i] = enemyDamage;
            enemyDamageTextWait[i] = false;
        }
        */
        //------------------------------------------------------------------------------

        BattleLogQue = false;
        BattleLogCurrenttime = 0;
        BattleLogWait = true;

        //---------------------------------------------------------181204
        Rank = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("Rank").GetComponent<Text>();
        Power = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("Power").GetComponent<Text>();
        Technique = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("Technique").GetComponent<Text>();
        Speed = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("Speed").GetComponent<Text>();

        ResultTitle = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("ResultTitle").GetComponent<Text>();
        RankTitle = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("RankTitle").GetComponent<Text>();
        PowerTitle = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("PowerTitle").GetComponent<Text>();
        TechniqueTitle = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("TechniqueTitle").GetComponent<Text>();
        SpeedTitle = GameObject.Find("Canvas").transform.Find("ResultScreen").gameObject.transform.Find("SpeedTitle").GetComponent<Text>();

        //--------------------------------------------------------Language設定
        switch (Language)
        {
            case (0):
                ResultTitle.text = "Mission Complete!";
                RankTitle.text = "Total Rank";
                PowerTitle.text = "Power";
                TechniqueTitle.text = "Technique";
                SpeedTitle.text = "Speed";
                break;
            case (1):
                ResultTitle.text = "任务完成!";
                RankTitle.text = "总体评价";
                PowerTitle.text = "功率";
                TechniqueTitle.text = "技术";
                SpeedTitle.text = "速度";
                break;
            case (2):
                ResultTitle.text = "ミッションコンプリート!";
                RankTitle.text = "総合評価";
                PowerTitle.text = "攻撃性";
                TechniqueTitle.text = "生存性";
                SpeedTitle.text = "スピード";
                break;
        }

        //--------------------------------------------------190105
        MouseEnter = GameObject.Find("MouseEnterSE").GetComponent<AudioSource>();
        MouseClickNext = GameObject.Find("MouseClickNextSE").GetComponent<AudioSource>();

        BackButtonImage = GameObject.Find("BackButtonImage").GetComponent<CanvasRenderer>();

        //--------------------------------------------------
    }

    public static void PlaceEdgeAndPoint()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //初期化
        for (int i = 0; i < simdata.N_Maps; i++)
        {

            for (int k = 0; k < simdata.N_Maps - 1; k++)
            {

                simdata.MapCondition[i, k] = BlankTerritory;
            }
        }


        //線と点の配置(17~116まで)
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps - 1; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlag)
                        {
                            simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("PointBlack");
                            //
                            //simdata.MapObj[i, k].name = "MapObj";
                            simdata.ObjCount++;

                            simdata.MapObjClick[i, k] = simdata.MapObjEdgeAndPoint[i, k].GetComponent<CircleCollider2D>();
                            MapObjFlag = !MapObjFlag;
                        }
                        else
                        {
                            simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeUnderBlack");
                            MapObjFlag = !MapObjFlag;
                        }
                        simdata.MapObjEdgeAndPoint[i, k].name = "MapObj";
                    }
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeRightBlack");
                        MapObjFlagB = !MapObjFlagB;
                    }
                    else
                    {
                        simdata.MapObjEdgeAndPoint[i, k] = Resources.Load<GameObject>("EdgeLeftBlack");
                        MapObjFlagB = !MapObjFlagB;
                        break;
                    }
                    simdata.MapObjEdgeAndPoint[i, k].name = "MapObj";
                }
            }
        }

        float x = 1.36f;
        float y = 1.15f;
        bool MapObjFlagD = true;

        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps - 1; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlagC)
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 30);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) - x, simdata.MapStartPos.y + (y * -i), 30);
                                Debug.Log("Array2");
                            }
                        }
                        else
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + x, simdata.MapStartPos.y + (y * -i), 30);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 30);

                            }
                        }
                    }
                }
                else
                {
                    simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + 0.68f, simdata.MapStartPos.y + (y * -i), 30);
                    MapObjFlagD = !MapObjFlagD;
                }
            }
            if (i % 2 == 0)
            {
                MapObjFlagC = !MapObjFlagC;
            }
        }
    }

    public static void PlaceArea()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //初期化
        for (int i = 0; i < simdata.N_Maps; i++)
        {

            for (int k = 0; k < simdata.N_Maps; k++)
            {

                simdata.MapAreaCondition[i, k] = NonTerritory;
            }
        }

        //面の配置(173~116まで)
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        simdata.MapObjArea[i, k] = Resources.Load<GameObject>("AreaDownBlack");
                        MapObjFlagB = !MapObjFlagB;
                    }
                    else
                    {
                        simdata.MapObjArea[i, k] = Resources.Load<GameObject>("AreaUpBlack");
                        MapObjFlagB = !MapObjFlagB;
                    }
                    simdata.MapObjArea[i, k].name = "MapObj";
                }
            }
        }



        float x = 1.36f;
        float y = 1.15f;
        bool MapObjFlagD = true;



        /*
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0)
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlagC)
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) - x, simdata.MapStartPos.y + (y * -i), 0);
                                Debug.Log("Array2");
                            }
                        }
                        else
                        {
                            if (i % 4 == 0)
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + x, simdata.MapStartPos.y + (y * -i), 0);
                            }
                            else
                            {
                                simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k), simdata.MapStartPos.y + (y * -i), 0);

                            }
                        }
                    }
                }
                else
                {
                    simdata.MapObjPos[i, k] = new Vector3(simdata.MapStartPos.x + (x * k) + 0.68f, simdata.MapStartPos.y + (y * -i), 0);
                    MapObjFlagD = !MapObjFlagD;
                }
            }
            if (i % 2 == 0)
            {
                MapObjFlagC = !MapObjFlagC;
            }
        }
        */
    }

    public static void InitMap()
    {
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            //180825 -1に変更
            for (int k = 0; k < simdata.N_Maps - 1; k++)
            {
                if (i % 2 == 0)
                {
                    if (k <= simdata.N_Maps)
                    {
                        if (simdata.csvDatas[i][k] == "1")
                        {
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapCondition[i, k] = BlankTerritory;

                        }
                        else
                        {
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapCondition[i, k] = NonTerritory;
                        }
                    }
                }
                else
                {
                    if (simdata.csvDatas[i][k] == "1")
                    {
                        //-------------------------------------------------------------------------------------------------------------181107
                        MapCondition[i, k] = BlankTerritory;

                    }
                    else
                    {
                        //-------------------------------------------------------------------------------------------------------------181107
                        MapCondition[i, k] = NonTerritory;
                    }

                    switch (csvDatasB[i][k])
                    {
                        case "0":
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapAreaCondition[i, k] = NonTerritory;
                            break;
                        case "1":
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapAreaCondition[i, k] = BlankTerritory;
                            break;
                        case "3":
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapAreaCondition[i, k] = PlayerTerritory;
                            break;
                        case "4":
                            //-------------------------------------------------------------------------------------------------------------181107
                            MapAreaCondition[i, k] = EnemyTerritory;
                            break;
                    }
                }
            }
        }
    }

    /*--------------------------------ジャッジ用181030------------------------------------*/
    //ジャッジ用関数
    public static void checkJudge()
    {

        //陣取り判定、Judgeリセット

        if (Judge == 1)
        {
            Debug.Log("Judge Start");

            //---------------------------------------------------181116
            AreaTakeSE.Play();
            //---------------------------------------------------------------181114
            MapAreaCount_PL = 0;
            MapAreaCount_EN = 0;
            TerritoryCount();

            isDrawing = false;

            //最小値、最大値を計算し、陣取り
            for (int y = 0; y < N_Maps; y++)
            {
                for (int x = 0; x < N_Maps * 2; x++)
                {
                    //現y軸で、2の値を見つけた場合
                    if (MapCondition[y, x] == PlayerTerritory)
                    {
                        //現y軸で初めてのxを記録
                        if (mincount == 0)
                        {
                            min = x;
                            mincount = 1;

                        }

                        //最大値を記録
                        if (x >= max)
                        {
                            max = x;
                        }

                        //イレギュラーの最小値を記録
                        if (x <= min)
                        {
                            min = x;
                        }

                    }

                }

                //1216追記
                if (y % 2 == 0)
                {

                }

                else
                {
                    //現y軸の面をひっくり返す
                    for (judgex = min; judgex < max; judgex++)
                    {
                        if (MapAreaCondition[y, (int)judgex] == BlankTerritory)
                        {
                            //1215変更
                            MapAreaCondition[y, (int)judgex] = PlayerTerritory;
                            count++;
                            enemyDamage += playerAP + MapAreaCount_PL;
                            /*

                            if (AreaTake[y, judgex] == 5)
                           {

                            Damage();

                        　　エリア側で自身が5の値でひっくり返ったのを確認した場合、Damageの値を入力する

                          　write("Damage = &n, Damage")

                            (HP - Damage;)

                            エリアから入力されていたダメージをリセット
                            Damage = 0;

                            }
                             */
                        }
                        //1223変更
                        if (MapAreaCondition[y, (int)judgex] == PostPlayerTerritory)
                        {
                            MapAreaCondition[y, (int)judgex] = PlayerTerritory;
                            count++;

                            //---------------------------------------------------181106
                            enemyDamage += playerAP + MapAreaCount_PL;
                            currentPower++;
                        }

                        if (MapAreaCondition[y, (int)judgex] == EnemyTerritory)
                        {
                            //1215変更
                            MapAreaCondition[y, (int)judgex] = PlayerTerritory;
                            count++;
                            playerDamage += enemyAP + MapAreaCount_EN;
                            currentTechnique += 20f;
                        }
                    }
                }


                //エッジもひっくり返す
                for (int judgex = min; judgex <= max; judgex++)
                {
                    if (MapCondition[y, judgex] == PostPlayerTerritory)
                    {
                        MapCondition[y, judgex] = BlankTerritory;
                        count++;
                    }

                    //1215追記
                    if (MapCondition[y, judgex] == PlayerTerritory)
                    {
                        MapCondition[y, judgex] = BlankTerritory;
                        count++;
                    }
                }

                //ジャッジ終了後値を初期化
                CurrentPoint.y = N_Maps;
                CurrentPoint.x = N_Maps;
                FirstPoint.y = N_Maps;
                FirstPoint.x = N_Maps;
                //LastPoint.y = N_Maps;
                //LastPoint.x = N_Maps;
                pastDoCount = 1;



                //現y軸の処理終了後、数値初期化
                max = 0;
                min = 0;
                mincount = 0;

            }

            //判定終了後 1 の場所を初期化
            for (int y = 0; y < N_Maps; y++)
            {
                for (int x = 0; x < N_Maps; x++)
                {
                    if (MapCondition[y, x] == PostPlayerTerritory)
                    {
                        MapCondition[y, x] = BlankTerritory;
                    }


                }
            }

            count = 0;
            score = 0;
            Judge = 0;

            /*
            for (y = 0; y <= 10; y++)
            {
                for (x = 0; x <= 10; x++)
                {
                    //現y軸で、2の値を見つけた場合

                    if (CanTake2[y, x] == 2)
                    {
                        CanTake2[y, x] = 3;

                    }

                }
            }
            */


            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j < N_Maps; j++)
                {
                    for (int k = 0; k < N_Maps; k++)
                    {
                        pastMapCondition[i, j, k] = BlankTerritory;

                    }
                }
            }

            dealDamage();
        }
    }

    public static void dealDamage()
    {
        //現在使っていないため緑保留181030

        playerDamage5s = 0;
        enemyDamage5s = 0;

        if (playerDamage > 0)
        {


            //味方へのダメージ
            playerDamageSave[playerDamageCount] = playerDamage;
            lastplayerHP = playerHP;
            playerHP -= playerDamage;
            //--------------------------------------------------181106
            //------------------------------------------------------------------------------ダメージテキスト保留中
            /*
            playerDamageText[playerDamageCount].text = playerDamage + "Damage";
            playerDamageCount++;
            playerDamageSave[playerDamageCount] = 0;
            playerDamageTextWait[playerDamageCount] = true;
            playerDamageTextObj[playerDamageCount].transform.position = new Vector3(playerDamageTextObj[playerDamageCount].transform.position.x, 6.77f, playerDamageTextObj[playerDamageCount].transform.position.z);
            */
            //--------------------------------------------------------------------------------------


            //使ってないため保留181105
            //playerDamageAlpha = 1;
            //playerDamageTextA.SetAlpha(playerDamageAlpha);

            PlayerDamageTime = 0;
            isPlayerDamaged = true;
            PlayerDamageSE.Play();
        }


        if (enemyDamage > 0)
        {


            //敵へのダメージ
            enemyDamageSave[enemyDamageCount] = enemyDamage;
            lastenemyHP = enemyHP;
            enemyHP -= enemyDamage;
            //--------------------------------------------------181106
            //------------------------------------------------------------------------------ダメージテキスト保留中
            /*
            enemyDamageText[enemyDamageCount].text = enemyDamage + "Damage";

            enemyDamageCount++;
            enemyDamageSave[playerDamageCount] = 0;
            enemyDamageTextWait[playerDamageCount] = true;
            enemyDamageTextObj[enemyDamageCount].transform.position = new Vector3(enemyDamageTextObj[enemyDamageCount].transform.position.x, 6.72f, enemyDamageTextObj[enemyDamageCount].transform.position.z);
            */
            //--------------------------------------------------------------------------------------



            //使ってないため保留181105
            //enemyDamageAlpha = 1;
            //enemyDamageTextA.SetAlpha(enemyDamageAlpha);

            EnemyDamageTime = 0;
            isEnemyDamaged = true;
        }

        //ダメージ値リセット
        playerDamage = 0;
        enemyDamage = 0;
        Debug.Log("PlayerHP" + playerHP);

        Debug.Log("EnemyHP" + enemyHP);

        //--------------------------------181105
        //playerHPGage.value = (float)playerHP / 100f;
        //enemyHPGage.value = (float)enemyHP / 100f;
    }

    /*------------------------------------------------------------------------------------*/
    //演算用関数
    public static void clickPoint(int y, int x)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        //クリックされた際、未取得なら自らを"2"に
        //------------------------------------------------------181114
        if (!isDrawing)
        {
            if (simdata.MapCondition[ya, xa] == BlankTerritory)
            {


                //バックアップ保存
                for (y = 0; y < N_Maps; y++)
                {
                    for (x = 0; x < N_Maps * 2; x++)
                    {
                        simdata.DefaultMapCondition[y, x] = simdata.MapCondition[y, x];

                        //0415
                        simdata.pastMapCondition[simdata.pastDoCount, y, x] = simdata.MapCondition[y, x];
                    }
                }

                simdata.MapCondition[ya, xa] = PlayerTerritory;

                //初期座標入力

                simdata.FirstPoint.y = ya;
                simdata.FirstPoint.x = xa;

                simdata.CurrentPoint.y = ya;
                simdata.CurrentPoint.x = xa;

                //0415変記
                simdata.pastPoint[simdata.pastDoCount] = simdata.CurrentPoint;
                //↑変更後
                /*simdata.pastMapConditionB[simdata.pastDoCount,0] = (int)CurrentPoint.y;
                returnCanTakeB[returnCount][1] = (int)CurrentPoint.x;*/


                //クリックされた際、周囲の辺を"1"に

                for (y = ya - 1; y <= ya + 1; y++)
                {
                    for (x = xa - 1; x < xa + 1; x++)
                    {
                        if (simdata.MapCondition[y, x] == BlankTerritory)
                        {
                            simdata.MapCondition[y, x] = PostPlayerTerritory;
                        }

                    }
                }

                if (simdata.MapCondition[ya, xa + 1] == BlankTerritory)
                {
                    simdata.MapCondition[ya, xa + 1] = PostPlayerTerritory;
                }

                //----------------------------------------------------181105---
                enemyAttackCD -= 1;

                //----------------------------------------------------181114
                isDrawing = true;
                //---------------------------------------------------181116
                PointDrawSE.Play();
            }
        }

    }

    //----------------------------------------20181030------------------

    public static void enterPoint(int y, int x)
    {
        int ya = 0;
        int xa = 0;
        int y2 = 0;
        int x2 = 0;
        int resety = 0;
        int resetx = 0;

        ya = y;
        xa = x;

        int currentY = 0;
        int currentX = 0;

        /*----------------------------------------------"待った"判定-------------------------------------------------*/
        //1215追記　待ったの値を計算する
        //Debug.Log(pastDoCount);

        //3次元→2次元はジャグ配列を使うと解決？http://bbs.wankuma.com/index.cgi?mode=al2&namber=75399&KLOG=127
        //1215　座標値専用
        returnY = pastMapConditionB[pastDoCount - 1, 0];
        returnX = pastMapConditionB[pastDoCount - 1, 1];

        //LastPointの時
        if (ya == returnY && xa == returnX)
        {
            //FirstPointの場合、全てリセット
            if (FirstPoint.y == ya && FirstPoint.x == xa)
            {
                for (y = 0; y < N_Maps; y++)
                {
                    for (x = 0; x < N_Maps * 2; x++)
                    {
                        MapCondition[y, x] = DefaultMapCondition[y, x];

                        CurrentPoint.y = N_Maps;
                        CurrentPoint.x = N_Maps;
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < N_Maps; j++)
                            {
                                for (int k = 0; k < N_Maps; k++)
                                {
                                    //1215追記
                                    pastMapCondition[i, j, k] = BlankTerritory;

                                }
                            }
                        }

                        pastDoCount = 1;
                    }
                }
                //---------------------------------------------------181114
                isDrawing = false;
                //---------------------------------------------------181116
                PointDrawSE.Play();
            }

            //FirstPointではない場合１つ前にもどる
            else
            {
                for (y = 0; y < N_Maps; y++)
                {
                    for (x = 0; x < N_Maps * 2; x++)
                    {
                        //1215追記
                        MapCondition[y, x] = pastMapCondition[pastDoCount - 1, y, x];

                        pastMapCondition[pastDoCount, y, x] = BlankTerritory;

                        CurrentPoint.y = ya;
                        CurrentPoint.x = xa;
                    }
                }

                pastDoCount -= 1;
                Debug.Log(pastDoCount);

                //---------------------------------------------------181116
                PointDrawSE.Play();
            }
        }

        /*
        //LastPointの場合、待った実行
        if (DataBase2.LastPoint.y == ya && DataBase2.LastPoint.x == xa)
        {
            //FirstPointの場合、全てリセット
            if (DataBase2.FirstPoint.y == ya && DataBase2.FirstPoint.x == xa)
            {
                for (y = 0; y <= 10; y++)
                {
                    for (x = 0; x <= 10; x++)
                    {
                        DataBase2.CanTake2[y, x] = DataBase2.DefaultCanTake[y, x];

                        DataBase2.CurrentPoint.y = N_MAPS;
                        DataBase2.CurrentPoint.x = N_MAPS;
                    }
                }

            }
            
            //FirstPointではない場合１つ前にもどる
            else
            {
                for (y = 0; y <= 10; y++)
                {
                    for (x = 0; x <= 10; x++)
                    {

                        DataBase2.CanTake2[y, x] = DataBase2.SavedCanTake[y, x];

                        DataBase2.CurrentPoint.y = ya;
                        DataBase2.CurrentPoint.x = xa;

                    }
                }
            }
            
        }
        */

        /*-----------------------------”待った”以外------------------------------------*/

        else
        {
            if (MapCondition[ya, xa] == PlayerTerritory)
            {

                //ジャッジするか判定

                if (FirstPoint.y == ya && FirstPoint.x == xa)
                {
                    //-------------------------------------------------------------181114
                    for (y = ya - 1; y <= ya + 1; y++)
                    {
                        for (x = xa - 1; x < xa + 1; x++)
                        {
                            if (simdata.MapCondition[y, x] == PostPlayerTerritory)
                            {
                                isAround = true;
                            }

                        }
                    }

                    if (simdata.MapCondition[ya, xa + 1] == PostPlayerTerritory)
                    {
                        isAround = true;
                    }
                    //----------------------------------------------------------------
                    if (isAround)
                    {
                        if (CurrentPoint.y == ya && CurrentPoint.x == xa)
                        {
                        }

                        else
                        {

                            if (MapCondition[ya, xa + 1] == PostPlayerTerritory)
                            {
                                MapCondition[ya, xa] = PlayerTerritory;
                                MapCondition[ya, xa + 1] = PlayerTerritory;

                                //----------------------------------------------------181105---
                                enemyAttackCD -= 1;
                            }

                            else
                            {
                                for (y = ya - 1; y <= ya + 1; y++)
                                {
                                    for (x = xa - 1; x <= xa; x++)
                                    {
                                        if (y == ya && x == xa)
                                        { }

                                        else
                                        {

                                            if (MapCondition[y, x] == PostPlayerTerritory)
                                            {
                                                MapCondition[ya, xa] = PlayerTerritory;
                                                MapCondition[y, x] = PlayerTerritory;

                                                //----------------------------------------------------181105---
                                                enemyAttackCD -= 1;

                                            }

                                        }
                                    }
                                }

                            }


                            //現在地入力

                            CurrentPoint.y = 0;
                            CurrentPoint.x = 0;

                            Judge = 1;
                            Debug.Log("Hello");
                        }
                    }
                    //----------------------------------------------------------181114
                    isAround = false;
                }

            }



            // 点が１かつ、周りの辺のどれかが取得可能なら、点を２に
            // 該当辺を２に
            // 周囲の辺を1に

            if (MapCondition[ya, xa] == PostPlayerTerritory)
            {
                //1215追記
                pastMapConditionB[pastDoCount, 0] = (int)CurrentPoint.y;
                pastMapConditionB[pastDoCount, 1] = (int)CurrentPoint.x;

                //バックアップ保存
                for (y = 0; y < N_Maps; y++)
                {
                    for (x = 0; x < N_Maps * 2; x++)
                    {
                        //181030使ってない？
                        //SavedCanTake[y, x] = MapCondition[y, x];
                        pastMapCondition[pastDoCount, y, x] = MapCondition[y, x];
                    }

                }
                pastDoCount++;
                Debug.Log(pastDoCount);

                //右横が 1 なら点を 2 に
                if (MapCondition[ya, xa + 1] == PostPlayerTerritory)
                {
                    MapCondition[ya, xa] = PlayerTerritory;
                    MapCondition[ya, xa + 1] = PlayerTerritory;

                    //現在地
                    CurrentPoint.y = ya;
                    CurrentPoint.x = xa;

                    //----------------------------------------------------181105---
                    enemyAttackCD -= 1;
                }

                //周囲のどれかが 1 なら、点を 2 に
                else
                {

                    for (y = ya - 1; y <= ya + 1; y++)
                    {
                        for (x = xa - 1; x <= xa; x++)
                        {
                            if (y == ya && x == xa)
                            { }

                            else
                            {

                                if (MapCondition[y, x] == PostPlayerTerritory)
                                {
                                    MapCondition[ya, xa] = PlayerTerritory;
                                    MapCondition[y, x] = PlayerTerritory;

                                    //現在地
                                    CurrentPoint.y = ya;
                                    CurrentPoint.x = xa;

                                    //----------------------------------------------------181105---
                                    enemyAttackCD -= 1;

                                }

                            }
                        }
                    }

                }

                //1215追記　周りの辺を 1 に
                if (MapCondition[ya, xa + 1] == BlankTerritory)
                {
                    MapCondition[ya, xa + 1] = PostPlayerTerritory;
                }

                for (y2 = ya - 1; y2 <= ya + 1; y2++)
                {
                    for (x2 = xa - 1; x2 <= xa; x2++)
                    {
                        if (MapCondition[y2, x2] == BlankTerritory)
                        {
                            MapCondition[y2, x2] = PostPlayerTerritory;
                        }

                    }
                }

                //1215追記　3 の場合 1 に
                if (MapCondition[ya, xa + 1] == EnemyTerritory)
                {
                    MapCondition[ya, xa + 1] = PostPlayerTerritory;
                }

                for (y2 = ya - 1; y2 <= ya + 1; y2++)
                {
                    for (x2 = xa - 1; x2 <= xa; x2++)
                    {
                        if (MapCondition[y2, x2] == EnemyTerritory)
                        {
                            MapCondition[y2, x2] = PostPlayerTerritory;
                        }

                    }
                }

                //---------------------------------------------------181116
                PointDrawSE.Play();
            }


            //--------------------------------------------------------------------------181107
            //辺はジャッジ毎にリセットするので、この項は保留
            /*
            //1215追記　　2になっていたら
            if (CurrentPoint.y == ya && CurrentPoint.x == xa)
            {
            }
            else
            {
                if (MapCondition[ya, xa] >= PlayerTerritory)
                {
                    //171215追記
                    pastMapConditionB[pastDoCount,0] = (int)CurrentPoint.y;
                    pastMapConditionB[pastDoCount,1] = (int)CurrentPoint.x;

                    //バックアップ保存
                    for (y = 0; y <= 10; y++)
                    {
                        for (x = 0; x <= 10; x++)
                        {
                            //181030使ってない？
                            //SavedCanTake[y, x] = MapCondition[y, x];
                            pastMapCondition[pastDoCount,y, x] = MapCondition[y, x];
                        }

                    }
                    pastDoCount++;



                    //既に周りに 1 の辺があるか確認
                    if (MapCondition[ya, xa + 1] == PostPlayerTerritory)
                    {
                        takeCount = 1;
                        MapCondition[ya, xa + 1] = PlayerTerritory;
                        MapCondition[ya, xa] = PlayerTerritory;
                    }
                    else
                    {
                        for (int i = ya - 1; i <= ya + 1; i++)
                        {
                            for (int j = xa - 1; j <= xa; j++)
                            {
                                //自分の時はなにもしない
                                if (i == ya && j == xa)
                                {
                                }
                                //他のとき
                                else
                                {
                                    //他でみつけたとき
                                    if (MapCondition[y, x] == PostPlayerTerritory)
                                    {
                                        takeCount = 1;
                                        MapCondition[i, j] = PlayerTerritory;
                                        if (ya == i && ya == j)
                                        {
                                            MapCondition[i, j] = PlayerTerritory;
                                        }
                                        else
                                        {
                                            MapCondition[i, j] = PlayerTerritory;
                                        }
                                    }
                                }


                            }
                        }
                    }

                    // 1215 2 や 3 をとることはありえないのでこれは使わない 
                    // 1　があったとき自らを 1 に。周りも 1 に
                    if (takeCount == 1)
                    {

                        Debug.Log("takeCount");

                        //周囲の辺を1に

                        if (MapCondition[ya, xa + 1] >= BlankTerritory)
                        {
                            MapCondition[ya, xa + 1] = PostPlayerTerritory;
                        }


                        for (y2 = ya - 1; y2 <= ya + 1; y2++)
                        {
                            for (x2 = xa - 1; x2 <= xa; x2++)
                            {
                                if (MapCondition[y2, x2] >= 1)
                                {
                                    if (ya == y2 && ya == xa)
                                    {
                                        MapCondition[y2, x2] = PlayerTerritory;
                                    }
                                    else
                                    {
                                        MapCondition[y2, x2] = PostPlayerTerritory;
                                    }
                                }
                            }
                        }

                        CurrentPoint.y = ya;
                        CurrentPoint.x = xa;

                        takeCount = 0;
                    }


                    CurrentPoint.y = ya;
                    CurrentPoint.x = xa;
                }
            }
            */
        }

    }

    public static void checkEdges(int y, int x)
    {

        int ya = 0;
        int xa = 0;
        int y2 = 0;
        int x2 = 0;
        int count = 0;

        int[] resetPoint = new int[5];

        ya = y;
        xa = x;


        /*---------------------------------------予測線リセット---------------------------------------------------*/

        //LastPointの時に 1 の辺を 0 にリセット

        //CurrentPointの場合はリセットしない
        if (CurrentPoint.y == ya && CurrentPoint.x == xa)
        {
        }

        else
        {
            //1215追記
            returnY = pastMapConditionB[pastDoCount - 1, 0];
            returnX = pastMapConditionB[pastDoCount - 1, 1];

            if (ya == returnY && xa == returnX)
            {
                //LastPointの時のみリセット
                if (count <= 0)
                {
                    //xa + 1 が1のときに、他の１をリセット
                    if (MapCondition[ya, xa + 1] == PostPlayerTerritory)
                    {
                        MapCondition[y, x + 1] = BlankTerritory;
                    }

                    //xa + 1、自分以外が2の時に、他の１をリセット
                    for (y = ya - 1; y <= ya + 1; y++)
                    {
                        for (x = xa - 1; x <= xa; x++)
                        {

                            //自分の時はなにもしない

                            if (y == ya && x == xa)
                            {

                            }

                            //他のとき
                            else
                            {

                                //他でみつけたとき
                                if (MapCondition[y, x] == PostPlayerTerritory)
                                {
                                    //xa + 1以外
                                    MapCondition[y, x] = BlankTerritory;

                                    //countを削除

                                }
                            }
                        }
                    }
                }
            }
        }


        //171215追記　点のリセット
        int resetCount = 0;

        //1215追記
        returnY = pastMapConditionB[pastDoCount - 1, 0];
        returnX = pastMapConditionB[pastDoCount - 1, 1];

        //LastPointはリセットしない
        if (ya == returnY && xa == returnX)
        {

        }

        else
        {
            if (MapCondition[ya, xa + 1] == PostPlayerTerritory)
            {
                resetCount = 1;
            }

            else
            {
                for (y = ya - 1; y <= ya + 1; y++)
                {
                    for (x = xa - 1; x <= xa; x++)
                    {
                        //自分の時はなにもしない
                        if (y == ya && x == xa)
                        {

                        }
                        else
                        {
                            if (MapCondition[y, x] == PostPlayerTerritory)
                            {
                                resetCount = 1;
                            }
                        }


                    }

                }
            }

            //周りに2がある時も実行
            if (MapCondition[ya, xa + 1] == PlayerTerritory)
            {
                resetCount = 1;
            }

            else
            {
                for (y = ya - 1; y <= ya + 1; y++)
                {
                    for (x = xa - 1; x <= xa; x++)
                    {
                        //自分の時はなにもしない

                        if (y == ya && x == xa)
                        {

                        }
                        else
                        {
                            if (MapCondition[y, x] == PlayerTerritory)
                            {
                                resetCount = 1;
                            }
                        }
                    }
                }
            }


            if (resetCount == 0)
            {
                if (MapCondition[ya, xa] == PostPlayerTerritory)
                {
                    MapCondition[ya, xa] = BlankTerritory;
                }
            }

            resetCount = 0;

        }

    }
    public static void AllcheckEdges()
    {
        for (int y = 1; y < N_Maps - 1; y++)
        {
            for (int x = 1; x < N_Maps * 2 - 1; x++)
            {
                if (y % 2 == 0)
                {
                    if (y % 4 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            checkEdges(y, x);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        if (x % 2 == 0)
                        {
                        }
                        else
                        {
                            checkEdges(y, x);
                        }
                    }
                }
                else
                {
                }
            }
        }
    }

    //----------------------------------------20181105------------------
    //敵スキル用関数
    public static void enemySkill()
    {
        //次のスキル表示
        switch (Language)
        {
            case (0):
                switch (enemySkillNumber)
                {
                    case 0:
                        enemyNextAttackText.text = "Next: Charge";
                        break;
                    case 1:
                        enemyNextAttackText.text = "Next: Hack";
                        break;


                }
                break;
            case (1):
                switch (enemySkillNumber)
                {
                    case 0:
                        enemyNextAttackText.text = "下一个: 攻击";
                        break;
                    case 1:
                        enemyNextAttackText.text = "下一个: 黑客";
                        break;


                }
                break;
            case (2):
                switch (enemySkillNumber)
                {
                    case 0:
                        enemyNextAttackText.text = "Next: 突進";
                        break;
                    case 1:
                        enemyNextAttackText.text = "Next: ハック";
                        break;


                }
                break;
        }

        //Debug.Log(MapAreaCount);
        if (enemyAttackCD == 0)
        {
            //スキルに合わせた効果を発動
            switch (enemySkillNumber)
            {
                case 0:
                    switch (Language)
                    {
                        case (0):
                            enemyAttackText.text = enemyName + "'s Charge!";
                            break;
                        case (1):
                            enemyAttackText.text = enemyName + "的攻击!";
                            break;
                        case (2):
                            enemyAttackText.text = enemyName + "の突進!";
                            break;
                    }
                    //-------------------------------------------------------181106ダメージ計算とか
                    playerDamage = MapAreaCount_EN;
                    playerDamageSave[playerDamageCount] = playerDamage;
                    lastplayerHP = playerHP;
                    playerHP -= playerDamage;
                    playerDamage = 0;

                    EnemySkillSE[1].Play();
                    Observable.Timer(System.TimeSpan.FromSeconds(0.38f)).Subscribe(_ => PlayerDamageTime = 0);
                    Observable.Timer(System.TimeSpan.FromSeconds(0.38f)).Subscribe(_ => isPlayerDamaged = true);
                    Observable.Timer(System.TimeSpan.FromSeconds(0.38f)).Subscribe(_ => PlayerDamageSE.Play());
                    //playerDamageSave[playerDamageCount] = playerDamage;
                    //lastplayerHP = playerHP;
                    // playerHP -= playerDamage;

                    //playerDamage = 0;
                    //-----------------------------------------------------------------------------------------ダメージテキスト保留中
                    /*
                        playerDamageText[playerDamageCount].text = playerDamage + "Damage";
                        playerDamageCount++;
                        playerDamageSave[playerDamageCount] = 0;
                        playerDamageTextWait[playerDamageCount ] = true;
                        playerDamageTextObj[playerDamageCount].transform.position = new Vector3(playerDamageTextObj[playerDamageCount].transform.position.x, 6.77f, playerDamageTextObj[playerDamageCount].transform.position.z);
                        */
                    //------------------------------------------------------------------------------------------

                    //--------------------------------------------------------
                    Debug.Log("Charge");

                    //--------------------------------------------------181118エフェクト
                    Observable.Timer(System.TimeSpan.FromSeconds(0)).Subscribe(_ => ChargeSkillEffect[0].Play());
                    Observable.Timer(System.TimeSpan.FromSeconds(0.1f)).Subscribe(_ => ChargeSkillEffect[1].Play());
                    Observable.Timer(System.TimeSpan.FromSeconds(0.17f)).Subscribe(_ => ChargeSkillEffect[2].Play());

                    //Vector3(-309.6f, 52f, 10f)
                    //--------------------------------------------------

                    //次のスキル振り分け
                    enemySkillNumber = (int)Random.Range(0.3f, 1.4f);
                    break;

                case 1:
                    int HackTile = 0;
                    int i = 0;
                    int[,] PastHackTile = new int[N_Maps, N_Maps];
                    int HackTargetCount = 0;

                    switch (Language)
                    {
                        case (0):
                            enemyAttackText.text = enemyName + "'s Hack!";
                            break;
                        case (1):
                            enemyAttackText.text = enemyName + "的黑客!";
                            break;
                        case (2):
                            enemyAttackText.text = enemyName + "のハック!";
                            break;
                    }

                    //総エリア枚数からランダム値をとる
                    HackTile = (int)Random.Range(-0.4f, 108.4f);
                    Debug.Log(HackTile);

                    //-----------------------------------------------181116
                    EnemySkillSE[0].Play();
                    SkillEffectTime = 0;
                    GlitchFx._intensity = 0.1f;


                    //-----------------------------------------------181114
                    for (int y = 0; y < N_Maps; y++)
                    {
                        for (int x = 0; x < N_Maps - 1; x++)
                        {
                            if (MapCondition[y, x] == PlayerTerritory)
                            {
                                //-------------------------------------------------------------181114
                                for (int ya = y - 1; ya <= y + 1; ya++)
                                {
                                    //            x - 2 !
                                    for (int xa = x - 2; xa < x + 1; xa++)
                                    {
                                        if (csvDatasB[ya][xa] == "1" && MapAreaCondition[ya, xa] == BlankTerritory)
                                        {
                                            PastHackTile[ya, xa] = 1;
                                        }
                                    }
                                }

                                if (MapCondition[y, x + 1] == PlayerTerritory)
                                {
                                    if (csvDatasB[y][x] == "1" && MapAreaCondition[y, x] == BlankTerritory)
                                    {
                                        PastHackTile[y, x] = 1;
                                    }
                                }
                                //----------------------------------------------------------------
                            }
                        }
                    }

                    for (int y = 0; y < N_Maps; y++)
                    {
                        for (int x = 0; x < N_Maps - 1; x++)
                        {
                            if (PastHackTile[y, x] == 1)
                            {
                                HackTargetCount++;
                            }
                        }
                    }

                    HackTile = (int)Random.Range(0.5f, (float)HackTargetCount - 1);

                    //-----------------------------------------------
                    for (int y = 0; y < N_Maps; y++)
                    {
                        for (int x = 0; x < N_Maps - 1; x++)
                        {
                            if (y % 2 == 0)
                            {
                                if (x <= simdata.N_Maps)
                                {
                                }
                            }
                            else
                            {
                                if (csvDatasB[y][x] == "1" && PastHackTile[y, x] == 1)
                                {
                                    if (HackTile == i)
                                    {
                                        MapAreaCondition[y, x] = EnemyTerritory;
                                        Debug.Log("HackComplete");
                                    }
                                    //HackTileの数値になるまで+
                                    i++;

                                }
                            }
                        }
                    }
                    //--------------------------------------181114 past script
                    /*
                    for (int y = 0; y < N_Maps; y++)
                    {
                        for (int x = 0; x < N_Maps - 1; x++)
                        {
                            if (y % 2 == 0)
                            {
                                if (x <= simdata.N_Maps)
                                {
                                }
                            }
                            else
                            {
                                if (csvDatasB[y][x] == "1")
                                {
                                    if (MapAreaCondition[y, x] != EnemyTerritory)
                                    {
                                        if (HackTile == i)
                                        {
                                            MapAreaCondition[y, x] = EnemyTerritory;
                                            Debug.Log("HackComplete");
                                        }
                                        //HackTileの数値になるまで+
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    */
                    Debug.Log("Hack");

                    //次のスキル振り分け
                    enemySkillNumber = (int)Random.Range(0.3f, 1.4f);
                    break;

            }

            //次のスキルに合わせてカウントダウン数を調整
            switch (enemySkillNumber)
            {
                case 0:
                    enemyAttackCD = (int)Random.Range(3.6f, 6.4f); ;
                    break;
                case 1:
                    enemyAttackCD = (int)Random.Range(3.6f, 6.4f); ;
                    break;


            }

            BattleLogCurrenttime = 0;
            BattleLogQue = true;
        }
    }

    public static void enemySkillRandom()
    {
        //スキルに合わせた効果を発動

        //次のスキルに合わせてカウントダウン数を調整

    }

    //----------------------------------------20181029-----------------

    //-----エッジの状態から隣接する点を取得
    public static void takePoint(int y, int x)
    {

        int ya = 0;
        int xa = 0;
        int y2 = 0;
        int x2 = 0;

        ya = y;
        xa = x;

        if (MapCondition[ya, xa] == PostPlayerTerritory)
        {
            if (CurrentPoint.y == ya && CurrentPoint.x == x + 1)
            {
                if (MapCondition[ya, x - 1] == BlankTerritory)
                {
                    MapCondition[ya, x - 1] = PostPlayerTerritory;
                }

                if (MapCondition[ya, x - 1] == EnemyTerritory)
                {
                    MapCondition[ya, x - 1] = PostPlayerTerritory;
                }
            }

            if (CurrentPoint.y == ya && CurrentPoint.x == x - 1)
            {
                if (MapCondition[ya, x + 1] == BlankTerritory)
                {
                    MapCondition[ya, x + 1] = PostPlayerTerritory;
                }

                if (MapCondition[ya, x + 1] == EnemyTerritory)
                {
                    MapCondition[ya, x + 1] = PostPlayerTerritory;
                }
            }
            /*
            if (DataBase2.CanTake2[ya, x + 1] == 0)
            {
                DataBase2.CanTake2[ya, x + 1] = 1;
            }
            if (DataBase2.CanTake2[ya, x - 1] == 0)
            {
                DataBase2.CanTake2[ya, x - 1] = 1;
            }
            */
        }

    }

    public static void takeLeftPoint(int y, int x)
    {

        int ya = 0;
        int xa = 0;
        int y2 = 0;
        int x2 = 0;

        ya = y;
        xa = x;

        if (MapCondition[ya, xa] == PostPlayerTerritory)
        {
            if (CurrentPoint.y == ya - 1 && CurrentPoint.x == xa + 1)
            {
                if (MapCondition[ya + 1, xa] == BlankTerritory)
                {
                    MapCondition[ya + 1, xa] = PostPlayerTerritory;
                }

                if (MapCondition[ya + 1, xa] == EnemyTerritory)
                {
                    MapCondition[ya + 1, xa] = PostPlayerTerritory;
                }
            }

            if (CurrentPoint.y == ya + 1 && CurrentPoint.x == xa)
            {
                if (MapCondition[ya - 1, xa + 1] == BlankTerritory)
                {
                    MapCondition[ya - 1, xa + 1] = PostPlayerTerritory;
                }

                if (MapCondition[ya - 1, xa + 1] == EnemyTerritory)
                {
                    MapCondition[ya - 1, xa + 1] = PostPlayerTerritory;
                }
            }
        }
    }

    public static void takeRightPoint(int y, int x)
    {
        int ya = 0;
        int xa = 0;
        int y2 = 0;
        int x2 = 0;

        ya = y;
        xa = x;

        if (MapCondition[ya, xa] == PostPlayerTerritory)
        {
            if (CurrentPoint.y == ya - 1 && CurrentPoint.x == xa)
            {
                if (MapCondition[y + 1, xa + 1] == BlankTerritory)
                {
                    MapCondition[y + 1, xa + 1] = PostPlayerTerritory;
                }

                if (MapCondition[y + 1, xa + 1] == EnemyTerritory)
                {
                    MapCondition[y + 1, xa + 1] = PostPlayerTerritory;
                }
            }

            if (CurrentPoint.y == ya + 1 && CurrentPoint.x == xa + 1)
            {
                if (MapCondition[y - 1, xa] == BlankTerritory)
                {
                    MapCondition[y - 1, xa] = PostPlayerTerritory;
                }

                if (MapCondition[y - 1, xa] == EnemyTerritory)
                {
                    MapCondition[y - 1, xa] = PostPlayerTerritory;
                }
            }

        }
    }

    public static void AlltakePoint()
    {

        for (int y = 0; y < N_Maps; y++)
        {
            for (int x = 0; x < N_Maps * 2; x++)
            {
                if (y % 2 == 0)
                {
                    if (y % 4 == 0)
                    {
                        if (x % 2 == 0)
                        {

                        }
                        else
                        {
                            takePoint(y, x);
                            takeLeftPoint(y, x);
                            takeRightPoint(y, x);
                        }
                    }
                    else
                    {
                        if (x % 2 == 0)
                        {
                            takePoint(y, x);
                            takeLeftPoint(y, x);
                            takeRightPoint(y, x);
                        }
                        else { }
                    }
                }
                else
                {
                    takePoint(y, x);
                    takeLeftPoint(y, x);
                    takeRightPoint(y, x);
                }
            }
        }

        /*
        for (y = 0; y < N_Maps; y++)
        {
            for (x = 0; x < N_Maps; x++)
            {
                takePoint(y, x);
                takeLeftPoint(y, x);
                takeRightPoint(y, x);
            }
        }
        */
    }

    //-------------------------------------181114

    public static void TerritoryCount()
    {
        for (int y = 0; y < N_Maps; y++)
        {
            for (int x = 0; x < N_Maps - 1; x++)
            {
                if (MapAreaCondition[y, x] == PlayerTerritory)
                {
                    MapAreaCount_PL++;
                }
                if (MapAreaCondition[y, x] == EnemyTerritory)
                {
                    MapAreaCount_EN++;
                }
            }
        }
    }
    //-----------------------------------------
    //-----------------------------------------------------------------

    public static void changePointSprite(int y, int x)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        // SpriteRenderのspriteを設定済みの他のspriteに変更

        // 例) HoldSpriteに変更

        if (simdata.MapCondition[ya, xa] == NonTerritory)
        {
            //simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointBlack");
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointRed");
        }

        if (simdata.MapCondition[ya, xa] == BlankTerritory)
        {
            //simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointBlack");
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointBlack");
        }

        if (simdata.MapCondition[ya, xa] == PostPlayerTerritory)
        {
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointGreen");
        }

        if (simdata.MapCondition[ya, xa] == PlayerTerritory)
        {
            //simdata.MapSprite[y, x].sprite = Resources.Load<Sprite>("PointBlue");
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointBlue");
            //Debug.Log("Blue" +ya +xa+ simdata.MapCondition[ya,xa]);
        }

        if (simdata.MapCondition[ya, xa] == EnemyTerritory)
        {
            simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("PointRed");
        }

    }

    public static void changeEdgeSprite(int y, int x, int i)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        // SpriteRenderのspriteを設定済みの他のspriteに変更

        // 例) HoldSpriteに変更

        if (simdata.MapCondition[ya, xa] == NonTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderRed");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftRed");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightRed");
                    break;
            }
        }

        if (simdata.MapCondition[ya, xa] == BlankTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderBlack");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftBlack");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightBlack");
                    break;
            }
        }

        if (simdata.MapCondition[ya, xa] == PostPlayerTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderGreen");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftGreen");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightGreen");
                    break;
            }
            //Debug.Log("Green" + ya + xa + simdata.MapCondition[ya, xa]);
        }

        if (simdata.MapCondition[ya, xa] == PlayerTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderBlue");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftBlue");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightBlue");
                    break;
            }
            //Debug.Log("Blue" + ya + xa + simdata.MapCondition[ya, xa]);
        }

        if (simdata.MapCondition[ya, xa] == EnemyTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeUnderRed");
                    break;
                case (1):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeLeftRed");
                    break;
                case (2):
                    simdata.MapObjEdgeAndPoint[ya, xa] = Resources.Load<GameObject>("EdgeRightRed");
                    break;
            }
        }
    }
    public static void AllchangeEdgeSprite()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        //0415移植テスト
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            //180825 -1に変更
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0) //偶数の場合は線と点のみの列、奇数の場合はエッジ2種類とエリアの列
                {
                    //180825 < から <=に変更
                    if (k <= simdata.N_Maps)
                    {
                        if (MapObjFlag)
                        {
                            changePointSprite(i, k);

                            MapObjFlag = !MapObjFlag; //MapObjFlagのOn/Offで点とエッジを区別
                        }
                        else
                        {
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeUnderBlack");
                            changeEdgeSprite(i, k, 0);
                            MapObjFlag = !MapObjFlag;
                        }
                    }
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        changeEdgeSprite(i, k, 2);
                        MapObjFlagB = !MapObjFlagB; //MapObjFlagBの数でエッジの左右とエリアを区別
                    }
                    else
                    {
                        changeEdgeSprite(i, k, 1);
                        MapObjFlagB = !MapObjFlagB;
                    }
                    /*
                        case (0):
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeRightBlack");
                            changeEdgeSprite(i, k, 2);
                            MapObjFlagB++; //MapObjFlagBの数でエッジの左右とエリアを区別
                            break;
                        case (1):
                            //simdata.MapObj[i, k] = Resources.Load<GameObject>("AreaDownBlack");

                            MapObjFlagB++;
                            break;
                        case (2):
                            // simdata.MapObj[i, k] = Resources.Load<GameObject>("EdgeLeftBlack");
                            changeEdgeSprite(i, k, 1);
                            MapObjFlagB++;
                            break;
                        case (3):
                           // simdata.MapObj[i, k] = Resources.Load<GameObject>("AreaUpBlack");
                            MapObjFlagB = 0;
                            break;
                        default:
                            MapObjFlagB = 0;
                            break;
                    */

                }
            }
        }
    }

    public static void changeAreaSprite(int y, int x, int i)
    {
        int ya = 0;
        int xa = 0;

        ya = y;
        xa = x;

        if (simdata.MapAreaCondition[ya, xa] == NonTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaDownRed");
                    break;
                case (1):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaUpRed");
                    break;
            }
        }

        if (simdata.MapAreaCondition[ya, xa] == BlankTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaDownBlack");
                    break;
                case (1):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaUpBlack");
                    break;
            }
        }

        if (simdata.MapAreaCondition[ya, xa] == PostPlayerTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaDownGreen");
                    break;
                case (1):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaUpGreen");
                    break;
            }
            //Debug.Log("Green" + ya + xa + simdata.MapAreaCondition[ya, xa]);
        }

        if (simdata.MapAreaCondition[ya, xa] == PlayerTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaDownBlue");
                    break;
                case (1):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaUpBlue");
                    break;
            }
            //Debug.Log("Blue" + ya + xa + simdata.MapAreaCondition[ya, xa]);
        }

        if (simdata.MapAreaCondition[ya, xa] == EnemyTerritory)
        {
            switch (i)
            {
                case (0):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaDownRed");
                    break;
                case (1):
                    simdata.MapObjArea[ya, xa] = Resources.Load<GameObject>("AreaUpRed");
                    break;
            }
        }
    }
    public static void AllchangeAreaSprite()
    {
        bool MapObjFlag = true;
        bool MapObjFlagB = true;
        bool MapObjFlagC = true;

        for (int i = 0; i < simdata.N_Maps; i++)
        {
            //180825 -1に変更
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                if (i % 2 == 0) //偶数の場合は線と点のみの列、奇数の場合はエッジ2種類とエリアの列
                {
                }

                else
                {
                    if (MapObjFlagB)
                    {
                        changeAreaSprite(i, k, 0);
                        MapObjFlagB = !MapObjFlagB;
                    }
                    else
                    {
                        changeAreaSprite(i, k, 1);
                        MapObjFlagB = !MapObjFlagB;
                    }
                }
            }
        }
    }

    /*-----------------------------------------------------------------------------------*/
    //キーバインド
    public static void Keybind()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Camera1.transform.position.y < 17)
            {
                Vector3 pastPos;
                pastPos = Camera1.transform.position;
                Camera1.transform.position = new Vector3(pastPos.x, pastPos.y + 0.5f, pastPos.z);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Camera1.transform.position.y > -17)
            {
                Vector3 pastPos;
                pastPos = Camera1.transform.position;
                Camera1.transform.position = new Vector3(pastPos.x, pastPos.y - 0.5f, pastPos.z);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Camera1.transform.position.x > -17)
            {
                Vector3 pastPos;
                pastPos = Camera1.transform.position;
                Camera1.transform.position = new Vector3(pastPos.x - 0.5f, pastPos.y, pastPos.z);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Camera1.transform.position.x < 17)
            {
                Vector3 pastPos;
                pastPos = Camera1.transform.position;
                Camera1.transform.position = new Vector3(pastPos.x + 0.5f, pastPos.y, pastPos.z);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Orthographic.orthographicSize > 3)
            {
                float pastSize;
                pastSize = Orthographic.orthographicSize;
                Orthographic.orthographicSize = pastSize - (Input.GetAxis("Mouse ScrollWheel") * 3.5f);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Orthographic.orthographicSize < 20)
            {
                float pastSize;
                pastSize = Orthographic.orthographicSize;
                Orthographic.orthographicSize = pastSize + (-1 * Input.GetAxis("Mouse ScrollWheel") * 3.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            DebugMode = !DebugMode;
            Debug.Log("DebugMode = " + DebugMode);
            if (DebugMode)
            {
                playerHP = playerMaxHP;
                enemyHP = 0;
            }
        }

    }


    /*-----------------------------------------------------------------------------------*/
    //ゲームクリアフラグ
    public static void MissionCompleteCheck()
    {
        if (enemyHP <= 0)
        {
            if (playerHP > 0)
            {
                if (!EndFlag)
                {
                    EndFlag = true;
                    GlitchFx._intensity = 1f;
                    EndMusicWin.Play();
                }
                StartMusic.volume -= 0.5f;
                endtime += 0.02f;
                GlitchFx._intensity = GlitchFx._intensity - (endtime / 0.6f);
                ResultScreen.SetActive(true);

                //-----------------------------------Power
                if ((float)enemyHP / (float)enemyMaxHP * 100 >= 40)
                {
                    Power.text = "C";
                }
                else if ((float)enemyHP / (float)enemyMaxHP * 100 >= 20)
                {
                    Power.text = "B";
                }
                else if ((float)enemyHP / (float)enemyMaxHP * 100 >= 10)
                {
                    Power.text = "A";
                }
                else if ((float)enemyHP / (float)enemyMaxHP * 100 < 10)
                {
                    Power.text = "S";
                }

                //-----------------------------------Speed
                if (starttime <= 120)
                {
                    Power.text = "S";
                }
                else if (starttime <= 170)
                {
                    Power.text = "A";
                }
                else if (starttime <= 220)
                {
                    Power.text = "B";
                }
                else if (starttime > 220)
                {
                    Power.text = "C";
                }

                //-----------------------------------Technique
                Debug.Log(playerHP + "+" + playerMaxHP);
                Debug.Log((float)playerHP / (float)playerMaxHP * 100);

                if ((float)playerHP / (float)playerMaxHP * 100 >= 60f)
                {
                    Technique.text = "S";
                }
                else if ((float)playerHP / (float)playerMaxHP * 100 >= 50f)
                {
                    Technique.text = "A";
                }
                else if ((float)playerHP / (float)playerMaxHP * 100 >= 30f)
                {
                    Technique.text = "B";
                }
                else if ((float)playerHP / (float)playerMaxHP * 100 < 30f)
                {
                    Technique.text = "C";
                }

                //-----------------------------------Rank
                int RankPoint = 0;

                switch (Power.text)
                {
                    case ("S"):
                        RankPoint += 40;
                        break;
                    case ("A"):
                        RankPoint += 30;
                        break;
                    case ("B"):
                        RankPoint += 20;
                        break;
                    case ("C"):
                        RankPoint += 10;
                        break;
                }

                switch (Speed.text)
                {
                    case ("S"):
                        RankPoint += 40;
                        break;
                    case ("A"):
                        RankPoint += 30;
                        break;
                    case ("B"):
                        RankPoint += 20;
                        break;
                    case ("C"):
                        RankPoint += 10;
                        break;
                }

                switch (Technique.text)
                {
                    case ("S"):
                        RankPoint += 40;
                        break;
                    case ("A"):
                        RankPoint += 30;
                        break;
                    case ("B"):
                        RankPoint += 20;
                        break;
                    case ("C"):
                        RankPoint += 10;
                        break;
                }

                if (RankPoint >= 110)
                {
                    Rank.text = "S";
                }
                else if (RankPoint >= 80)
                {
                    Rank.text = "A";
                }
                else if (RankPoint >= 50)
                {
                    Rank.text = "B";
                }
                else if (RankPoint < 50)
                {
                    Rank.text = "C";
                }
                //-------------------------------------end

                if (!StageClearFlag[StageID])
                {
                    MainStoryFlag[StageID + 1] = true;
                    SubStoryFlag[StageID] = true;
                    StageClearFlag[StageID] = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    calc.NextScene("StageSelect");
                }
            }

            else
            {

            }
        }
        else if (playerHP <= 0)
        {
            if (!EndFlag)
            {
                EndFlag = true;
                GlitchFx._intensity = 1f;
                EndMusicWin.Play();
            }
            StartMusic.volume -= 0.5f;
            endtime += 0.02f;
            GlitchFx._intensity = GlitchFx._intensity - (endtime / 0.6f);
            ResultScreen.SetActive(true);

            //-----------------------------------Power
            if ((float)enemyHP / (float)enemyMaxHP * 100 >= 40)
            {
                Power.text = "C";
            }
            else if ((float)enemyHP / (float)enemyMaxHP * 100 >= 20)
            {
                Power.text = "B";
            }
            else if ((float)enemyHP / (float)enemyMaxHP * 100 >= 10)
            {
                Power.text = "A";
            }
            else if ((float)enemyHP / (float)enemyMaxHP * 100 < 10)
            {
                Power.text = "S";
            }

            //-----------------------------------Speed
            if (starttime <= 120)
            {
                Power.text = "S";
            }
            else if (starttime <= 170)
            {
                Power.text = "A";
            }
            else if (starttime <= 220)
            {
                Power.text = "B";
            }
            else if (starttime > 220)
            {
                Power.text = "C";
            }

            //-----------------------------------Technique
            Debug.Log(playerHP + "+" + playerMaxHP);

            if (playerHP / playerMaxHP * 100 >= 60f)
            {
                Technique.text = "S";
            }
            else if ((float)playerHP / (float)playerMaxHP * 100 >= 50f)
            {
                Technique.text = "A";
            }
            else if ((float)playerHP / (float)playerMaxHP * 100 >= 30f)
            {
                Technique.text = "B";
            }
            else if ((float)playerHP / (float)playerMaxHP * 100 < 30f)
            {
                Technique.text = "C";
            }

            //-----------------------------------Rank
            int RankPoint = 0;

            switch (Power.text)
            {
                case ("S"):
                    RankPoint += 40;
                    break;
                case ("A"):
                    RankPoint += 30;
                    break;
                case ("B"):
                    RankPoint += 20;
                    break;
                case ("C"):
                    RankPoint += 10;
                    break;
            }

            switch (Speed.text)
            {
                case ("S"):
                    RankPoint += 40;
                    break;
                case ("A"):
                    RankPoint += 30;
                    break;
                case ("B"):
                    RankPoint += 20;
                    break;
                case ("C"):
                    RankPoint += 10;
                    break;
            }

            switch (Technique.text)
            {
                case ("S"):
                    RankPoint += 40;
                    break;
                case ("A"):
                    RankPoint += 30;
                    break;
                case ("B"):
                    RankPoint += 20;
                    break;
                case ("C"):
                    RankPoint += 10;
                    break;
            }

            if (RankPoint >= 110)
            {
                Rank.text = "S";
            }
            else if (RankPoint >= 80)
            {
                Rank.text = "A";
            }
            else if (RankPoint >= 50)
            {
                Rank.text = "B";
            }
            else if (RankPoint < 50)
            {
                Rank.text = "C";
            }
            //-------------------------------------end

            if (Input.GetMouseButtonDown(0))
            {
                calc.NextScene("StageSelect");
            }
        }
    }

    /*-----------------------------------------------------------------------------------*/
    //ゲーム内の更新をここで行う

    public static void UpdateScene()
    {
        MissionCompleteCheck();

        starttime += 0.02f;
        SkillEffectTime += 0.02f;

        //---------------------------------------------------------181110
        if (!EndFlag && !StartFlag)
        {
            GlitchFx._intensity = GlitchFx._intensity - (starttime / 100f);
        }
        //---------------------------------------------------------181116
        if (!EndFlag && StartFlag)
        {
            GlitchFx._intensity = GlitchFx._intensity - (SkillEffectTime / 6f);
        }

        if (starttime >= 2.1f)
        {
            StartFlag = true;
        }

        if (StartFlag && !EndFlag)
        {
            bool MapObjFlag = true;
            bool MapObjFlagB = true;
            bool MapObjFlagC = true;

            //クリックの判定OnMOuseDown
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < simdata.N_Maps; i++)
                {
                    for (int k = 0; k < simdata.N_Maps; k++)
                    {
                        if (i % 2 == 0)
                        {
                            if (MapObjFlag && calc.HitTest(simdata.MapObjPos[i, k], Input.mousePosition))
                            {
                                //クリックでかつポイント座標に近い場合

                                Debug.Log("Click" + simdata.number);
                                simdata.number++;

                                //0415移植テスト
                                clickPoint(i, k);
                                //simdata.MapCondition[i, k] = 2;

                                MapObjFlag = !MapObjFlag;
                            }
                            else
                            {
                                MapObjFlag = !MapObjFlag;
                            }
                        }
                        else { }
                    }
                }
            }

            MapObjFlag = true;

            //マウスが上を通った判定OnMouseEnter
            for (int i = 0; i < simdata.N_Maps; i++)
            {
                for (int k = 0; k < simdata.N_Maps; k++)
                {
                    if (i % 2 == 0)
                    {
                        if (MapObjFlag && calc.HitTest(simdata.MapObjPos[i, k], Input.mousePosition))
                        {
                            //Debug.Log("Enter");
                            MapObjFlag = !MapObjFlag;

                            //0415移植テスト
                            enterPoint(i, k);
                        }
                        else
                        {
                            MapObjFlag = !MapObjFlag;
                        }
                    }
                }
            }
            //キー操作
            Keybind();

        }
        //敵スキル
        enemySkill();

        //エフェクト
        //simEffect.FixedUpdate();

        //現在地以外のエッジの削除------------181030
        AllcheckEdges();

        //------------181029
        AlltakePoint();
        //-------------
        //------------181030
        checkJudge();

        //-------------
        //---------------------------エッジと点のスプライト変更
        AllchangeEdgeSprite();

        //--------------------------面のスプライト変更
        AllchangeAreaSprite();


    }
}
