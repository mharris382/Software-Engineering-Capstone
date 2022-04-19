using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIVolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;


    [SerializeField]
    private Toggle muteToggle;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    
    
    private const string PARAM_MASTER = "Volume_Master";
    private const string PARAM_MUSIC =  "Volume_Music";
    private const string PARAM_EFFECTS =  "Volume_Effects";

    private const string PREFS_MUTE = "MuteAllAudio";
    private const string PREFS_MASTER = "MasterVolume";
    private const string PREFS_MUSIC = "MasterVolume";
    private const string PREFS_EFFECTS = "MasterVolume";


    private void Start()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        if(PlayerPrefs.HasKey(PREFS_MASTER))
            PlayerPrefs.SetFloat(PREFS_MASTER, GetMasterVolume());
        if(PlayerPrefs.HasKey(PREFS_MUSIC))
            PlayerPrefs.SetFloat(PREFS_MUSIC, GetMusicVolume());
        if(PlayerPrefs.HasKey(PREFS_EFFECTS))
            PlayerPrefs.SetFloat(PREFS_EFFECTS, GetEffectsVolume());

    }

    public void LoadSettings()
    {
        float LoadVolume(string pref, float defaultVolume)
        {
            return PlayerPrefs.HasKey(pref) ? PlayerPrefs.GetFloat(pref) : defaultVolume;
        }

        float dv = Mathf.Log10(1) * 20;
        SetMasterVolumeSlider(LoadVolume(PREFS_MASTER, dv));
        SetMusicVolumeSlider(LoadVolume(PREFS_MUSIC, dv));
        SetEffectsVolumeSlider(LoadVolume(PREFS_EFFECTS, dv));
    }

    float GetMusicVolume() => musicVolumeSlider.value;
    float GetEffectsVolume() => effectsVolumeSlider.value;
    float GetMasterVolume() => masterVolumeSlider.value;

    void UpdateAllGUI()
    {
        SetMasterVolumeSlider(GetMasterVolume());
        SetEffectsVolumeSlider(GetEffectsVolume());
        SetMusicVolumeSlider(GetMusicVolume());
        SetMuteToggle(false);
    }
    
    void SetMusicVolumeSlider(float value)
    {
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = value;
    }
    void SetMasterVolumeSlider(float value)
    {
        if (masterVolumeSlider != null) 
            masterVolumeSlider.value = value;
    }
    void SetEffectsVolumeSlider(float value)
    {
        if(effectsVolumeSlider!=null)
            effectsVolumeSlider.value = value;
    }

    void SetMuteToggle(bool isOn)
    {
        if (muteToggle != null) muteToggle.isOn = isOn;
    }
    public void SetMuteEnabled(bool mute)
    {
        SaveSettings();
    }
    
    public void SetMasterVolumeFromSlider(float sliderValue) => SetMasterVolume(Mathf.Log10(sliderValue)*20);
    public void SeMusicVolumeFromSlider(float sliderValue) => SetMusicVolume(Mathf.Log10(sliderValue)*20);
    public void SetEffectsVolumeFromSlider(float sliderValue) => SetEffectsVolume(Mathf.Log10(sliderValue)*20);

    public void SetMasterVolume(float value)
    {
       SetVolumeOnMixer(PARAM_MASTER, value);
       SaveSettings();
    }
    public void SetMusicVolume(float value)
    {
        SetVolumeOnMixer(PARAM_MUSIC, value);
        SaveSettings();
    }
    public void SetEffectsVolume(float value)
    {
        SetVolumeOnMixer(PARAM_EFFECTS, value);
        SaveSettings();
    }

    void SetVolumeOnMixer(string paramName, float value)
    {
        SetMuteEnabled(false);
        SetMuteToggle(false);
        mixer.SetFloat(paramName, value);
    }
}
