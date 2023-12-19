using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class simdata_predata : MonoBehaviour {

    //---------------------
    //ゲーム全般

    public static AudioSource MouseEnter;
    public static AudioSource MouseClickNext;
    //public static AudioSource MouseClickBack;

    //　非同期動作で使用するAsyncOperation
    public static AsyncOperation async;
    //　シーンロード中に表示するUI画面
    public static GameObject LoadUI;
    //　読み込み率を表示するスライダー
    public static Slider LoadSlider;
    public static CanvasRenderer LoadBackGround;


    //ゲーム進行フラグ
    public static bool[] MainStoryFlag = new bool [60];
    public static bool[] SubStoryFlag = new bool [60];
    public static bool[] StageClearFlag = new bool[60];

    public static string LastScene;
    public static GameObject[] ClearText = new GameObject[60];
    //---------------------

    public static Button BackButton;
    public static CanvasRenderer BackButtonImage;

    //------------------------------------1811115

    public static GameObject Donna_Image;

    public static bool[] Choice = new bool [7];
    public static Text[] ChoiceText = new Text[7];

    // 0 = English, 1 = Chinese, 2 = Japanese
    public static int Language = 2;

}
