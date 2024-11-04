using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Car audio")]
    public AudioSource engineIdle;
    public AudioSource carHorn;
    public AudioSource drift;
    public AudioSource wallHit;
    public AudioSource engineTest;

    [Header("Environment audio")]
    public AudioSource voidOut;
    public AudioSource brokenCrate;
    public AudioSource teleport;

    [Header("Menu sounds")]
    public AudioSource menuClick;
    public AudioSource audio3;
    public AudioSource audio2;
    public AudioSource audio1;
    public AudioSource audioGo;

    private float volumeLevel;

    public void PlayEngineTest()
    {
        if (!engineTest.isPlaying) { engineTest.loop = true;  engineTest.Play();} 
    }

    public void ChangeEnginePitch(float pitch)
    {
        engineTest.pitch = pitch;
    }

    public void StopEngineTest()
    {
        if (engineTest.isPlaying) { engineTest.loop = false; engineTest.Stop();}
    }

    public void PlayEngineIdle()
    {
        if (!engineIdle.isPlaying) { engineIdle.Play(); }
    }

    public void PlayHorn()
    {
        if (!carHorn.isPlaying) { carHorn.Play(); }
    }

    public void PlayDrift()
    {
        if (!drift.isPlaying) { drift.loop = true; drift.pitch = 0.9f; drift.Play(); }
    }

    public void StopDrift()
    {
        if (drift.isPlaying) { drift.loop = false; drift.Stop(); }
    }

    public void PlayWallHit()
    {
        if (!wallHit.isPlaying) { wallHit.Play(); }
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

    public void PlayTeleport()
    {
        if (!teleport.isPlaying) { teleport.Play(); }
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
        foreach(var audio in gameObject.GetComponents<AudioSource>())
        {
            var match = Regex.Match(audio.clip.name, "(engine)|(Race)|(Impact)", RegexOptions.IgnoreCase);
            var match2 = Regex.Match(audio.clip.name, "(Skid)", RegexOptions.IgnoreCase);
            //Debug.Log(match.Success + " " + audio.clip.name);
            
            if (match.Success)
            {
                audio.volume = volumeLevel / 3;
            }
            else if (match2.Success)
            {
                audio.volume = volumeLevel / 5;
            }
            else
            {
                audio.volume = volumeLevel;
            }
        }
    }
}
