using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem_Main_1 : MonoBehaviour
{

    [SerializeField] AudioSource[] SoundList;
    [SerializeField] int[] SoundTriger;
    [SerializeField] bool[] SoundFlag;
    [SerializeField] bool[] isSE;

    int number = 0;

    void Update()
    {
        for (int i = 0; i < SoundList.Length; i++)
        {


            if (MainStory1.currentLine == SoundTriger[i])
            {
                if (SoundFlag[i])
                {
                    if (SoundList[i].isPlaying)
                    {

                    }
                    else
                    {
                        SoundList[i].Play();
                        number++;
                        if (isSE[i])
                        {
                            SoundFlag[i] = false;
                        }
                    }
                }
                else
                {
                    if (SoundList[i].isPlaying)
                    {
                        if (!isSE[i])
                        {
                            SoundList[i].Stop();
                            number++;
                        }
                    }
                }
            }
        }
    }
}


