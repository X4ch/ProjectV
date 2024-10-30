using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Car audio")]
    public AudioSource engineIdle;
    public AudioSource engineLow;
    public AudioSource engineMid;
    public AudioSource engineFast;
    public AudioSource carHorn;
    public AudioSource drift;

    [Header("Environment audio")]
    public AudioSource voidOut;
    public AudioSource brokenCrate;

    [Header("Menu sounds")]
    public AudioSource menuClick;
    public AudioSource audio3;
    public AudioSource audio2;
    public AudioSource audio1;
    public AudioSource audioGo;

    private float volumeLevel;


    public void PlayEngineIdle()
    {
        if (!engineIdle.isPlaying) { engineIdle.Play(); }
    }

    public void PlayEngineLow()
    {
        if (!engineLow.isPlaying) { engineLow.Play(); }
    }

    public void PlayEngineMid()
    {
        if (!engineMid.isPlaying) { engineMid.Play(); }
    }

    public void PlayEngineFast()
    {
        if (!engineFast.isPlaying) { engineFast.Play(); }
    }

    public void PlayHorn()
    {
        if (!carHorn.isPlaying) { carHorn.Play(); }
    }

    public void PlayDrift()
    {
        if (!drift.isPlaying) { drift.Play(); }
    }

    public void PlayVoidOut()
    {
        if (!voidOut.isPlaying) { voidOut.Play(); }
    }

    public void PlayBrokenCrate()
    {
        if (!brokenCrate.isPlaying) { brokenCrate.Play(); }
    }

    public void PlayMenuClick()
    {
        if (!menuClick.isPlaying) { menuClick.Play(); }
    }

    public void PlayAudio3()
    {
        if (!audio3.isPlaying) { audio3.Play(); }
    }

    public void PlayAudio2()
    {
        if (!audio2.isPlaying) { audio2.Play(); }
    }

    public void PlayAudio1()
    {
        if (!audio1.isPlaying) { audio1.Play(); }
    }
    public void PlayAudioGo()
    {
        if (!audioGo.isPlaying) { audioGo.Play(); }
    }

    public void Update()
    {
        volumeLevel = PlayerPrefs.GetFloat("Volume");
        Debug.Log("Volume:" + volumeLevel);
        foreach(var audio in gameObject.GetComponents<AudioSource>())
        {
            audio.volume = volumeLevel;
        }
    }
}
