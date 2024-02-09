using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider climateSlider;
    public static float masterVolume { get; private set; }
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }
    public static float climateVolume { get; private set; }

    private void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("masterSaved");
        masterSlider.value = masterVolume;
        musicVolume = PlayerPrefs.GetFloat("musicSaved");
        musicSlider.value = musicVolume;
        soundEffectsVolume = PlayerPrefs.GetFloat("sFXSaved");
        soundEffectsSlider.value = soundEffectsVolume;
        climateVolume = PlayerPrefs.GetFloat("climateSaved");
        climateSlider.value = climateVolume;
    }

    public void OnMasterSliderValueChange(float value)
    {
        masterVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("masterSaved", masterVolume);
    }
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("musicSaved", musicVolume);
    }
    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("sFXSaved", soundEffectsVolume);
    }
    public void OnClimateSliderValueChange(float value)
    {
        climateVolume = value;
        AudioManager.Instance.UpdateMixerVolume();
        PlayerPrefs.SetFloat("climateSaved", climateVolume);
    }

}
