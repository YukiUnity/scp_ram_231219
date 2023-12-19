using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1 : StageSelect_sim
{

    public static bool EnterFlag;

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;
    //　読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;

    // Use this for initialization
    void Start()
    {
        ButtonFlag[0] = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        ButtonFlag[0] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag[0] = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        calc.NextScene("Stage_0_0");
        //Invoke("LoadScene", 0.2f);
        //Invoke("NextScene", 0.2f);
    }

    void LoadScene()
    {
        //SceneManager.LoadScene("Stage_1_1");
    }


    public void NextScene()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        //　コルーチンを開始
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("Stage_0_0");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
