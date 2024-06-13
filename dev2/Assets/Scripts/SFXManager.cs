using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects;

    public void PlaySFX(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].Play();
        //soundEffects[sfxToPlay].PlayOneShot();
    }

    public AudioClip collectGem;
    public void PlaySFXPitched(int sfxToPlay)
    {
        if(sfxToPlay == 9)
        {
            soundEffects[sfxToPlay].PlayOneShot(collectGem);
        }
        else
        {
            soundEffects[sfxToPlay].pitch = Random.Range(.5f, 1f);
            PlaySFX(sfxToPlay);
        }
    }
}
