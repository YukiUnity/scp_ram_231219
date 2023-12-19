using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DrawObj : simdata {

    simdata simdata = new simdata();
    public static void InitiarizeScene()
    {
        for(int i = 0; i < simdata.ObjList.Count; i++)
        {
            Destroy(simdata.ObjList[i]);
        }

        simdata.ObjList.Clear();
        /*
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps * 2 - 3; k++)
            {
                if (i % 2 == 0)
                {
                    if ((k + 1) < simdata.N_Maps)
                    {
                            Destroy(GameObject.Find("MapObj(Clone)"));
                    }
                }
                else
                {
                    Destroy(GameObject.Find("MapObj(Clone)"));
                }
            }
        }
        */
    }


    public static void DrawScene ()
    {
        /*
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            for (int k = 0; k < simdata.N_Maps; k++)
            {
                Instantiate(simdata.MapObj[i, k], simdata.MapObjPos[i,k], Quaternion.identity);
            }
        }
        */

       

        //マップの描画
        //マップの描画で使っている式を当たり判定にも使うことで描画との同期も可能
        for (int i = 0; i < simdata.N_Maps; i++)
        {
            //180825 -1に変更
            for (int k = 0; k < simdata.N_Maps - 1; k++)
            {
                if (i % 2 == 0)
                {
                    if (k <= simdata.N_Maps)
                    {
                        if (csvDatas[i][k] == "0")
                        {
                            if (DebugMode)
                            {
                                simdata.MapObjEdgeAndPointB[i, k] = Instantiate(simdata.MapObjEdgeAndPoint[i, k], simdata.MapObjPos[i, k], Quaternion.identity) as GameObject;
                                simdata.ObjList.Add(simdata.MapObjEdgeAndPointB[i, k]);
                            }
                        }
                        else
                        {
                            simdata.MapObjEdgeAndPointB[i, k] = Instantiate(simdata.MapObjEdgeAndPoint[i, k], simdata.MapObjPos[i, k], Quaternion.identity) as GameObject;
                            simdata.ObjList.Add(simdata.MapObjEdgeAndPointB[i, k]);
                        }
                    }
                }
                else
                {
                    if (simdata.csvDatas[i][k] == "0")
                    {
                        if (DebugMode)
                        {
                            simdata.MapObjEdgeAndPointB[i, k] = Instantiate(simdata.MapObjEdgeAndPoint[i, k], simdata.MapObjPos[i, k], Quaternion.identity) as GameObject;
                            simdata.ObjList.Add(simdata.MapObjEdgeAndPointB[i, k]);
                        }

                    }
                    else
                    {
                        simdata.MapObjEdgeAndPointB[i, k] = Instantiate(simdata.MapObjEdgeAndPoint[i, k], simdata.MapObjPos[i, k], Quaternion.identity) as GameObject;
                        simdata.ObjList.Add(simdata.MapObjEdgeAndPointB[i, k]);
                    }
                    if (simdata.csvDatasB[i][k] == "0")
                    {
                        if (DebugMode)
                        {
                            simdata.MapObjArea[i, k] = Instantiate(simdata.MapObjArea[i, k], simdata.MapObjPosArea[i, k] = new Vector3(simdata.MapObjPos[i, k].x + 0.68f, simdata.MapObjPos[i, k].y, simdata.MapObjPos[i, k].z), Quaternion.identity) as GameObject;
                            simdata.ObjList.Add(simdata.MapObjArea[i, k]);
                        }

                    }
                    else
                    {
                        simdata.MapObjArea[i, k] = Instantiate(simdata.MapObjArea[i, k], simdata.MapObjPosArea[i, k] = new Vector3(simdata.MapObjPos[i, k].x + 0.68f, simdata.MapObjPos[i, k].y, simdata.MapObjPos[i, k].z), Quaternion.identity) as GameObject;
                        simdata.ObjList.Add(simdata.MapObjArea[i, k]);
                        //-----------------------------------------------------------181105
                        MapAreaCount += 1;
                    }
                }
            }
        }

    }

}
