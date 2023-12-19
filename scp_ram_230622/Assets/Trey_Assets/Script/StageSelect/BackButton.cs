using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackButton : StageSelect_sim
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
        ButtonFlag[4] = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        ButtonFlag[4] = true;
    }

    private void OnMouseExit()
    {
        ButtonFlag[4] = false;
    }

    private void OnMouseDown()
    {
        MouseClickNext.Play();
        //Invoke("LoadScene", 0.2f);
        //Invoke("NextScene", 0.2f);
        calc.NextScene("Title");

        //-------------------------------------------------------------181115
        for (int i = 0; i < 60; i++)
        {
            SubStoryFlag[i] = false;
            SubStoryChoice.SetActive(false);
            Stage_1_ButtonCollider.SetActive(true);
            Stage_2_ButtonCollider.SetActive(true);
            Stage_3_ButtonCollider.SetActive(true);
            Stage_4_ButtonCollider.SetActive(true);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Title");
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
        async = SceneManager.LoadSceneAsync("Title");

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
