using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider backgroundMusicSlider;
    public Slider soundEffectsSlider;

    void Start()
    {

        if (PlayerPrefs.GetInt("SetFirstTimeVolume") == 0)
        {
            PlayerPrefs.SetInt("SetFirstTimeVolume", 1);
            masterSlider.value = .3f;
            backgroundMusicSlider.value = .25f;
            soundEffectsSlider.value = .25f;
        }
        else
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume");
            soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
        }

    }

    public void SetMasterVolume()
    {
        SetVolume("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        SetVolume("BackgroundMusicVolume", backgroundMusicSlider.value);
    }

    public void SetSFXVoume()
    {
        SetVolume("SoundEffectsVolume", soundEffectsSlider.value);
    }


    void SetVolume(string name, float value)
    {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0)
        {
            volume = -80;
        }
        audioMixer.SetFloat(name, volume);
        PlayerPrefs.SetFloat(name, value);
    }
}
