using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource soundSource;

    [Range(0,1)] public  float  musicMinVol;
    [Range(0,1)] public float musicMaxVol;
    [Range(0, 1)] public float fadeTime;

    public AudioClip bounce;
    public AudioClip buttonClick;
    public AudioClip dead;
    public AudioClip normalBreak;
    public AudioClip win;
    private void Start()
    {
        fade(0);
    }
    public void NormalBreakSound()
    {
        soundSource.PlayOneShot(normalBreak);
    }
    public void fade(int i)
    {
        if(i==0)
        {
            musicSource.DOFade(musicMinVol, fadeTime);
        }
        else
        {
            musicSource.DOFade(musicMaxVol, fadeTime);
        }
    }
    public void BounceSound()
    {
        soundSource.PlayOneShot(bounce);
    }
    public void ButtonClickSound()
    {
        soundSource.PlayOneShot(buttonClick);
    }
    public void DeadSound()
    {
        soundSource.PlayOneShot(dead);
    }
    public void WinSound()
    {
        soundSource.PlayOneShot(win);
    }
}
