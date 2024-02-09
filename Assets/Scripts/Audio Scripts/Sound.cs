using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    public enum AudioTypes
    {
        SFX, 
        Music,
        ClimateFX,
        Master
    }

    public AudioTypes audioType;

    [HideInInspector] public AudioSource audioSource;

    public AudioClip audioClip;
    public string audioClipName;
    public bool isLoop;
    public bool playOnAwake;
    [Range(0,1)] public float volume = 0.5F;
}
