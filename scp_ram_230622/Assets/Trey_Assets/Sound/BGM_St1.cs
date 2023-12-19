using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_St1 : MonoBehaviour
{

    public AudioClip intro;
    public AudioClip loop;
    public float delay = 0.1f;

    AudioSource[] audioSource = new AudioSource[2];

    // Use this for initialization
    void Start()
    {
        GameObject intro_2 = new GameObject("Player");
        GameObject BGM_2 = new GameObject("Player");


        audioSource[0] = intro_2.AddComponent<AudioSource>();
        audioSource[1] = BGM_2.AddComponent<AudioSource>();

        if (intro != null)
        {
            audioSource[0].playOnAwake = false;
            audioSource[0].clip = intro;
            audioSource[0].PlayScheduled(AudioSettings.dspTime + delay);
            if (loop != null)
            {
                audioSource[1].playOnAwake = false;
                audioSource[1].clip = loop;
                audioSource[1].loop = true;
                audioSource[1].PlayScheduled(AudioSettings.dspTime + delay + intro.length);
            }
        }
    }
}