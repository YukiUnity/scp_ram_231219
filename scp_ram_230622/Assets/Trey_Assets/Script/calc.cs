using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class calc : MonoBehaviour {

    public static calc instance;
    public void Start()
    {
        instance = this;
    }

    public static bool HitTest (Vector3 obj,Vector3 mouse)
    {
        mouse.z = 40f;
        //Debug.Log(Camera.main.ScreenToWorldPoint(mouse));
        //Debug.Log(distanceAtoB(obj, Camera.main.ScreenToWorldPoint(mouse)));
        if (distanceAtoB(obj,Camera.main.ScreenToWorldPoint(mouse)) < 1.2) return true;
        //if (distanceAtoB(obj, mouse) < 0.0000002f) return true;
        else return false;
    }

    public static Vector3 directionAtoB (Vector3 obj, Vector3 mouse, Vector3 dir)
    {
        /*
        dir.x = mouse.x - obj.x;
        dir.y = mouse.y - obj.y;
        dir.z = mouse.z - obj.z;
        */
        dir.x = obj.x - mouse.x;
        dir.y = obj.y - mouse.y;
        dir.z = obj.z - mouse.z;
        return dir;
    }

    public static double distanceAtoB (Vector3 obj, Vector3 mouse)
    {
        Vector3 dir = new Vector3(0,0,0);
        dir = directionAtoB(obj, mouse, dir);
        return VectorNorm(dir);
    }

    public static double VectorNorm(Vector3 dir)
    {
        //Debug.Log("dir" + dir);
        double Sqrt = 0;
        Sqrt = System.Math.Sqrt(dir.x * dir.x + dir.y * dir.y + dir.z * dir.z);
        //Debug.Log("Sqrt" + Sqrt);
        return Sqrt;
    }

    //フェードイン・アウト関数   CanvasRenderer   インかアウト  何秒で  obj専用のcurrentTime型
    public static void FadeIO (CanvasRenderer obj, bool flag, float time, float currentTime)
    {
        //フェードイン
        if (flag)
        {
            if (obj.GetAlpha() < 1)
            {
                currentTime += Time.deltaTime;
                obj.SetAlpha(time / currentTime);
                //フェードが完了後、currentTimeを初期化
                if(obj.GetAlpha() >= 1)
                {
                    currentTime = 0;
                }
            }
        }
        //フェードアウト
        else
        {
            if (obj.GetAlpha() > 0)
            {
                currentTime += Time.deltaTime;
                obj.SetAlpha(1 - (time / currentTime));
                //フェードが完了後、currentTimeを初期化
                if (obj.GetAlpha() <= 0)
                {
                    currentTime = 0;
                }
            }
        }
    }

    public static void NextScene(string name)
    {
        //　ロード画面UIをアクティブにする
        simdata_predata.LoadUI.SetActive(true);

        //　コルーチンを開始
        instance.StartCoroutine("LoadData",name);
    }

    public IEnumerator LoadData(string name)
    {
        // シーンの読み込みをする
        simdata_predata.async = SceneManager.LoadSceneAsync(name);

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!simdata_predata.async.isDone)
        {
            var progressVal = Mathf.Clamp01(simdata_predata.async.progress / 0.9f);
            simdata_predata.LoadSlider.value = progressVal;
            //simdata_predata.LoadFill.fillAmount = progressVal;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 2));
            simdata_predata.LoadBackGround.SetAlpha(level);
            yield return null;
        }
    }

    //-----------------------------------------------------------------181115
    public static void DelayCall(float waittime)
    {
        instance.StartCoroutine("DelayMethod",waittime);
    }

    public IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    //-----------------------------------------------------------------

    private void Update()
    {
        instance = calc.instance;
    }
}
