using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simEffect : simdata {

	// Use this for initialization
	void Start () {

    }
	

    public static void HPGageEffect ()
    {
        //------------------------------------181106
        //プレイヤーHPゲージの減少エフェクト
        float playerHPtime = 0;
        float enemyHPtime = 0;

        float HPtargettime = 3f;

        if (enemyHPEffectGage >= enemyHP)
        {
            enemyHPtime += Time.deltaTime;
            //Debug.Log("EnemyLastHP" + lastenemyHP);
            //Debug.Log("EnemyLastDamage" + enemyDamageSave);
            //enemyHPEffectGage = (float)lastenemyHP - (enemyDamageSave * (HPtargettime / enemyHPtime));
            //enemyHPEffectGage = (float)lastenemyHP - ((float)enemyDamageSave * Time.deltaTime);
            //                                   HP最大値 - 現在のHP = ダメージ総数
            enemyHPEffectGage = enemyHPEffectGage - (((float)enemyMaxHP - enemyHP) / 37f);
            enemyHPGage.value = enemyHPEffectGage / enemyMaxHP;

        }
        if(enemyHPEffectGage < enemyHP)
        {
            enemyHPtime = 0;
            enemyHPEffectGage = enemyHPEffectGage + (((float)enemyHP - enemyHPEffectGage) / 37f);
            enemyHPGage.value = enemyHPEffectGage / enemyMaxHP;

        }

        if (playerHPEffectGage >= (float)playerHP)
        {
            playerHPtime += Time.deltaTime;
            //lastplayerHP = playerHP;
            //playerHPEffectGage = (float)lastplayerHP - (playerDamageSave * (HPtargettime / playerHPtime));
            //                                       HP最大値 - 現在のHP = ダメージ総数
            playerHPEffectGage = playerHPEffectGage - (((float)playerMaxHP - playerHP) / 37f);
            playerHPGage.value = playerHPEffectGage / playerMaxHP;

        }
        if(playerHPEffectGage < playerHP)
        {
            playerHPtime = 0;
            playerHPEffectGage = playerHPEffectGage + (((float)playerMaxHP - playerHPEffectGage) / 37f);
            playerHPGage.value = playerHPEffectGage / playerMaxHP;
        }


       
    }

    public static void FadeIO(CanvasRenderer obj, bool Fadein, float targettime)
    {
        //フェードイン
        if (Fadein)
        {
            float LastAlpha = obj.GetAlpha();
            obj.SetAlpha(LastAlpha + (1 / (targettime * 50)));
        }
        //フェードアウト
        else
        {
            float LastAlpha = obj.GetAlpha();
            obj.SetAlpha(LastAlpha - (1 / (targettime * 50)));
            //Debug.Log("Fadeout");
        }
    }

    public static void DamageText ()
    {
        
        for (int i = 0; i < AllTextNumber; i++)
        {
            //if (playerDamageSave[i] > 0)
            //{
                if (playerDamageTextWait[i])
                {
                    playerDamageTextObj[i].transform.position = new Vector3(playerDamageTextObj[i].transform.position.x, 6.77f, playerDamageTextObj[i].transform.position.z);

                    FadeIO(playerDamageTextA[i], true, 0.2f);
                    Debug.Log("playerrrrrrrrrrrrrrr" + playerDamageCount);
                    if (playerDamageTextA[i].GetAlpha() >= 1)
                    {
                        playerDamageTextWait[i] = false;
                    }
                }
                else
                {
                    if (playerDamageTextA[i].GetAlpha() > 0)
                    {
                        playerDamageTextObj[i].transform.position = new Vector3(playerDamageTextObj[i].transform.position.x, playerDamageTextObj[i].transform.position.y + (2f / 50f), playerDamageTextObj[i].transform.position.z);
                        FadeIO(playerDamageTextA[i], false, 1f);
                    }
                    else
                    {
                        //playerDamageSave[i] = 0;
                        //playerDamageTextWait[i] = true;
                    }
                }
            //}

            //if (enemyDamageSave[i] > 0)
            //{
                if (enemyDamageTextWait[i])
                {
                    enemyDamageTextObj[i].transform.position = new Vector3(enemyDamageTextObj[i].transform.position.x, 3.72f, enemyDamageTextObj[i].transform.position.z);

                    FadeIO(enemyDamageTextA[i], true, 0.2f);

                    Debug.Log("enemyyyyyyyyyyyyy" + enemyDamageCount);
                    if (enemyDamageTextA[i].GetAlpha() >= 1)
                    {
                        enemyDamageTextWait[i] = false;
                    }
                }
                else
                {
                    if (enemyDamageTextA[i].GetAlpha() > 0)
                    {
                        enemyDamageTextObj[i].transform.position = new Vector3(enemyDamageTextObj[i].transform.position.x, enemyDamageTextObj[i].transform.position.y + (2f / 50f), enemyDamageTextObj[i].transform.position.z);
                        FadeIO(enemyDamageTextA[i], false, 1f);
                    }
                    else
                    {
                        //enemyDamageSave[i] = 0;
                        //enemyDamageTextWait[i] = true;
                    }
                }
            //}
        }
        

        /*
        if (playerDamageSave[playerDamageCount] > 0)
        {
            if (playerDamageTextWait[playerDamageCount])
            {
                playerDamageTextObj[playerDamageCount].transform.position = new Vector3(playerDamageTextObj[playerDamageCount].transform.position.x, 6.77f, playerDamageTextObj[playerDamageCount].transform.position.z);

                FadeIO(playerDamageTextA[playerDamageCount], true, 0.2f);
                if (playerDamageTextA[playerDamageCount].GetAlpha() >= 1)
                {
                    playerDamageTextWait[playerDamageCount] = false;
                }
            }
            else
            {
                playerDamageTextObj[playerDamageCount].transform.position = new Vector3(playerDamageTextObj[playerDamageCount].transform.position.x, playerDamageTextObj[playerDamageCount].transform.position.y + (2f / 50f), playerDamageTextObj[playerDamageCount].transform.position.z);
                FadeIO(playerDamageTextA[playerDamageCount], false, 1f);
            }
        }

        if (enemyDamageSave[enemyDamageCount] > 0)
        {
            if (enemyDamageTextWait[enemyDamageCount])
            {
                enemyDamageTextObj[enemyDamageCount].transform.position = new Vector3(enemyDamageTextObj[enemyDamageCount].transform.position.x, 6.72f, enemyDamageTextObj[enemyDamageCount].transform.position.z);

                FadeIO(enemyDamageTextA[enemyDamageCount], true, 0.2f);

                //Debug.Log("position" + enemyDamageTextObj[enemyDamageCount].transform.position.y);
                if (enemyDamageTextA[enemyDamageCount].GetAlpha() >= 1)
                {
                    enemyDamageTextWait[enemyDamageCount] = false;
                }
            }
            else
            {
                {
                    enemyDamageTextObj[enemyDamageCount].transform.position = new Vector3(enemyDamageTextObj[enemyDamageCount].transform.position.x, enemyDamageTextObj[enemyDamageCount].transform.position.y + (2f / 50f), enemyDamageTextObj[enemyDamageCount].transform.position.z);
                    FadeIO(enemyDamageTextA[enemyDamageCount], false, 1f);
                }
            }
        }
        */
        
    }
    
    public static void BattleLog()
    {
        float waittime = 3f;
        if (BattleLogQue)
        {

                //FadeIO(enemyAttackText.canvasRenderer, true, 0.2f);
                enemyAttackText.canvasRenderer.SetAlpha(1);
                if (enemyAttackText.canvasRenderer.GetAlpha() >= 1)
                {
                    BattleLogQue = false;
                }

            
        }
        else
        {
            BattleLogCurrenttime += 0.02f;
            if (waittime <= BattleLogCurrenttime)
            {
                if (enemyAttackText.canvasRenderer.GetAlpha() > 0)
                {
                    FadeIO(enemyAttackText.canvasRenderer, false, 1f);
                }
                else
                {
                    //BattleLogWait = !BattleLogWait;
                }
            }
        }
    }

    public static void DmageEffect()
    {
        float waittime = 0.37f;

        if(isPlayerDamaged && waittime >= PlayerDamageTime)
        {
            PlayerDamageTime += 0.02f;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 33));
            PlayerImage.SetAlpha(level);
        }

        if(waittime < PlayerDamageTime)
        {
            isPlayerDamaged = false;
            if (PlayerImage.GetAlpha() < 1)
            {
                PlayerImage.SetAlpha(1);
            }
        }

        if (isEnemyDamaged && waittime >= EnemyDamageTime)
        {
            EnemyDamageTime += 0.02f;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 33));
            EnemyImage.SetAlpha(level);
        }

        if (waittime < EnemyDamageTime)
        {
            isEnemyDamaged = false;
            if (EnemyImage.GetAlpha() < 1)
            {
                EnemyImage.SetAlpha(1);
            }
        }
    }

    // Updateを0.02秒毎に行う
    void FixedUpdate()
    {
        DmageEffect();
        HPGageEffect();

        //---------------------------------------ダメージテキスト保留中
        //DamageText();
        //----------------------------------------

        BattleLog();

        enemySkillCDText.text = "" + enemyAttackCD;

        playerHPText.text = playerHP + "/" + playerMaxHP;
        enemyHPText.text = enemyHP + "/" + enemyMaxHP;

        

    }
}
