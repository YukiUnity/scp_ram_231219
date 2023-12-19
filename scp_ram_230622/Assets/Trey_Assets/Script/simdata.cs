using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class simdata : simdata_predata {

    

    public static GameObject Camera1;
    public static Camera Orthographic;

    public static int N_Maps = 37;

    //MapObj描画
    public static List<GameObject> ObjList = new List<GameObject>();
    public static GameObject[,] MapObjEdgeAndPoint = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjArea = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjEdgeAndPointB = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static GameObject[,] MapObjAreaB = new GameObject[N_Maps, N_Maps * 2 + 2];
    public static Vector3 MapStartPos = new Vector3(-8, 3, 0);
    public static Vector3[,] MapObjPos = new Vector3[N_Maps, N_Maps * 2 + 2];
    public static Vector3[,] MapObjPosArea = new Vector3[N_Maps, N_Maps * 2 + 2];
    public static int ObjCount = 0;
    public static GameObject obj;

    //MapObjゲーム内
    public static bool StartFlag;
    public static float starttime;
    public static AudioSource StartMusic;
    public static bool EndFlag;
    public static float endtime;
    public static AudioSource EndMusicWin;

    public static int StageID;

    public static bool DebugMode;

    public static int NonTerritory;
    public static int BlankTerritory;
    public static int PostPlayerTerritory;
    public static int PlayerTerritory;
    public static int EnemyTerritory;

    public static SpriteRenderer[,] MapSprite = new SpriteRenderer[N_Maps, N_Maps * 2 + 2];
    public static int[,] MapCondition = new int[N_Maps, N_Maps* 2 + 2];
    public static int[,] DefaultMapCondition = new int[N_Maps, N_Maps * 2 + 2];
    public static int[,,] pastMapCondition = new int[N_Maps * 3,N_Maps, N_Maps * 2 + 2];
    public static int[,] pastMapConditionB = new int[N_Maps * 3, N_Maps * 2 + 2];
    public static Vector2[] pastPoint = new Vector2[N_Maps * 3];
    public static int pastDoCount = 1;

    public static Vector2 FirstPoint;
    public static Vector2 CurrentPoint;

    public static int[,] MapAreaCondition = new int[N_Maps, N_Maps * 2 + 2];

    //---------------181105
    public static int MapAreaCount = 0;

    //---------------181114
    public static int MapAreaCount_PL;
    public static int MapAreaCount_EN;

    public static bool isDrawing;
    public static bool isAround;

    //------------------------------------------------181116
    public static AudioSource PointDrawSE;
    public static AudioSource PointBackSE;
    public static AudioSource AreaTakeSE;

    //----------------------------------------------------181106
    //UI操作
    public static GameObject MainCanvas;

    public static CanvasRenderer PlayerImage;
    public static CanvasRenderer EnemyImage;

    public static bool isPlayerDamaged;
    public static bool isEnemyDamaged;
    public static float PlayerDamageTime;
    public static float EnemyDamageTime;

    public static GameObject ChargeSkillEffectPrefab;
    public static ParticleSystem[] ChargeSkillEffect = new ParticleSystem [10];
    public static string playerName;
    public static string enemyName;

    public static int playerLevel;
    public static int playerExp;
    public static int playerAP;

    public static int enemyAP;

    public static Slider playerHPGage;
    public static Slider enemyHPGage;

    public static float playerHPEffectGage;
    public static float enemyHPEffectGage;

    public static float playerDamageAlpha = 0f;
    public static float enemyDamageAlpha = 0f;
    public static float playerDamage5s = 0f;
    public static float enemyDamage5s = 0f;

    public static int playerDamageCount = 0;
    public static int enemyDamageCount = 0;

    public static int AllTextNumber = 71;

    public static GameObject[] playerDamageTextObj = new GameObject[AllTextNumber];
    public static GameObject[] enemyDamageTextObj = new GameObject[AllTextNumber];
    public static Text[] playerDamageText = new Text[AllTextNumber];
    public static Text[] enemyDamageText = new Text[AllTextNumber];
    public static CanvasRenderer[] playerDamageTextA = new CanvasRenderer[AllTextNumber];
    public static CanvasRenderer[] enemyDamageTextA = new CanvasRenderer[AllTextNumber];

    public static bool[] playerDamageTextWait = new bool[AllTextNumber];
    public static bool[] enemyDamageTextWait = new bool[AllTextNumber];

    public static Text enemyAttackText;
    public static Text enemyNextAttackText;

    public static Text playerSkillCDText;
    public static Text enemySkillCDText;

    public static Text playerHPText;
    public static Text enemyHPText;

    //--------------------------------------------------181106
    public static bool BattleLogQue;
    public static float BattleLogCurrenttime;
    public static bool BattleLogWait;

    //-------------------------------------------------------------------181030---------------------------------------------------
    public static int returnX, returnY = 1;
    public static int takeCount = 0;

    //ジャッジ用
    public static int Judge = 0;
    public static int judgex = 0;
    public static int max = 0;
    public static int min = 0;

    public static int mincount = 0;

    public static int turnCount = 0;
    public static int count = 0;
    public static int score = 0;
    public static int firstBall;

    public static int playerMaxHP = 0;
    public static int playerHP = 0;
    public static int enemyMaxHP = 0;
    public static int enemyHP = 0;

    //----------------------------------------------------181106
    public static int lastplayerHP = 0;
    public static int lastenemyHP = 0;

    public static AudioSource PlayerDamageSE;
    public static AudioSource EnemyDamageSE;
    public static int playerDamage = 0;
    public static int enemyDamage = 0;
    public static int[] playerDamageSave = new int [AllTextNumber];
    public static int[] enemyDamageSave = new int[AllTextNumber];

    //---------------------------------------------------181105
    public static int enemyAttackCD = 0;
    public static int enemySkillNumber = 0;

    //---------------------------------------------------181116
    public static float SkillEffectTime;

    public static AudioSource[] EnemySkillSE = new AudioSource[10];


    //1221 StageSelect リザルト用
    public static int stageResult = 0;

    public static CanvasGroup EndCanvas;
    public static GameObject ResultScreen;
    public static Text Rank;
    public static Text Power;
    public static Text Technique;
    public static Text Speed;

    public static Text ResultTitle;
    public static Text RankTitle;
    public static Text PowerTitle;
    public static Text TechniqueTitle;
    public static Text SpeedTitle;

    public CanvasRenderer EndTitle;

    //1222 リザルト計算用
    public float totalRank;
    public float totalPower;
    public float totalTechnique;
    public float totalSpeed;

    public int totalresultRank;
    public int totalresultPower;
    public int totalresultTechnique;
    public int totalresultSpeed;

    public static float currentPower;
    public static float currentTechnique;
    public static float currentSpeed;

    public float st0Power = 1;  //2クリティカル
    public float st0Technique = 20;  //20ダメージ
    public float st0Speed = 60; //5分

    public float st1Power = 1;  //2クリティカル
    public float st1Technique = 20;  //20ダメージ
    public float st1Speed = 300; //5分

    public static int stage_1 = 0;

    //---------------------------------------------------------------------------------------------------------------------------

    public static CircleCollider2D[,] MapObjClick = new CircleCollider2D[N_Maps, N_Maps* 2 + 2];


    public static int number = 0;

    //csv
    public static string CSVName = "MapData181105v2";
    public static string CSVNumber = "";
    public static TextAsset csvFile; // CSVファイル
    public static List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリス
    public static string StringReader;
    public static int height = 0;

    public static string CSVNameB = "MapData_Area181105v2";
    public static string CSVNumberB = "";
    public static TextAsset csvFileB; // CSVファイル
    public static List<string[]> csvDatasB = new List<string[]>(); // CSVの中身を入れるリス
    public static string StringReaderB;
    public static int heightB = 0;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
