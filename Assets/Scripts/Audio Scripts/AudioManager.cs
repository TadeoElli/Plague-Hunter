using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup masterMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sFXMixerGroup;
    [SerializeField] private AudioMixerGroup climateFXMixerGroup;


    [SerializeField] private Sound[] sounds;


    private void Awake()
    {
        Instance = this;

        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.isLoop;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.volume = s.volume;


            switch (s.audioType)
            {
                case Sound.AudioTypes.Music:
                    s.audioSource.outputAudioMixerGroup = musicMixerGroup;
                    break;

                case Sound.AudioTypes.SFX:
                    s.audioSource.outputAudioMixerGroup = sFXMixerGroup;
                    break;

                case Sound.AudioTypes.ClimateFX:
                    s.audioSource.outputAudioMixerGroup = climateFXMixerGroup;
                    break;

                case Sound.AudioTypes.Master:
                    s.audioSource.outputAudioMixerGroup = masterMixerGroup;
                    break;
            }

            if (s.playOnAwake)
            {
                s.audioSource.Play();
            }
        }
    }

    public void PlayClipByName(string clipName)
    {
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.audioClipName == clipName);

        if (soundToPlay != null)
        {
            soundToPlay.audioSource.Play();
        } else
        {
            Debug.Log($"No encontre el audioclip a reproducir {clipName}");
        }
    }

    public void StopClipByName(string clipName)
    {
        Sound soundToStop = Array.Find(sounds, dummySound => dummySound.audioClipName == clipName);
        
        if (soundToStop != null)
        {
            soundToStop.audioSource.Stop();
        }
        else
        {
            Debug.Log($"No encontre el audioclip a detener {clipName}");
        }
    }

    public void UpdateMixerVolume()
    {
        masterMixerGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(AudioMenuManager.masterVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioMenuManager.musicVolume) * 20);
        sFXMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(AudioMenuManager.soundEffectsVolume) * 20);
        climateFXMixerGroup.audioMixer.SetFloat("ClimateFXVolume", Mathf.Log10(AudioMenuManager.climateVolume) * 20);
    }
}
