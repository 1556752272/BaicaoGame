using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource sfxAudio;

    public void Awake()
    {
        instance = this;

        //初始化lastPlayTime，list存储上次播放时间
        for (int i = 0;i < soundEffects.Length; i++)
        {
            lastPlayTime.Add(0);
        }
    }


    public AudioSource[] soundEffects;

    public void PlaySFX(int sfxToPlay)
    {
        //soundEffects[sfxToPlay].Stop();
        //soundEffects[sfxToPlay].Play();

        //播放最小间隔，cd为0.1
        if ((Time.time - lastPlayTime[sfxToPlay]) > 0.05)
        {
            lastPlayTime[sfxToPlay] = Time.time;
            soundEffects[sfxToPlay].PlayOneShot(soundEffects[sfxToPlay].clip);
        }
    }
    //list储存上次播放时间
    List<float> lastPlayTime = new List<float>();
    public void PlaySFXPitched(int sfxToPlay)
    {
        //soundEffects[sfxToPlay].pitch = Random.Range(.5f, 1f);
        PlaySFX(sfxToPlay);
    }
}
