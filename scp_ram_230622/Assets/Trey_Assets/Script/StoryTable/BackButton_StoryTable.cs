using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackButton_StoryTable : storytable_sim
{

    public static bool EnterFlag;

    void Start()
    {
        EnterFlag = false;
    }

    private void OnMouseEnter()
    {
        MouseEnter.Play();
        EnterFlag = true;
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        EnterFlag = false;
    }

    private void OnMouseDown()
    {
        //MouseClickNext.Play();
        //Invoke("LoadScene", 0.2f);
        MouseClickNext.Play();
        Invoke("NextScene", 0.2f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void NextScene()
    {
        //　ロード画面UIをアクティブにする
        LoadUI.SetActive(true);

        //　コルーチンを開始
        StartCoroutine("LoadData");

    }


    IEnumerator LoadData()
    {
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("StageSelect");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            LoadSlider.value = progressVal;
            yield return null;
        }
    }


    void FixedUpdate()
    {
        if (EnterFlag)
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
}
