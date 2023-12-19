using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StartButton : Title_sim {

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
    void Start () {
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
        MainStoryFlag[0] = true;
        MouseClickNext.Play();
        //Invoke("LoadScene", 0.2f);
        Invoke("NextScene", 0.2f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("StageSelect");
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
        async = SceneManager.LoadSceneAsync("StageSelect");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
	    if(EnterFlag)
        {
            if (Title_sim.StartButtonImage.GetAlpha() < 1f)
            {
                Title_sim.StartButtonImage.SetAlpha(Title_sim.StartButtonImage.GetAlpha() + (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
        else
        {
            if (Title_sim.StartButtonImage.GetAlpha() > 0f)
            {
                Title_sim.StartButtonImage.SetAlpha(Title_sim.StartButtonImage.GetAlpha() - (1f / 1f));
                //Title_Name.SetAlpha(Title_Tube.GetAlpha() + (1f / 100f));
            }
        }
    }
}
