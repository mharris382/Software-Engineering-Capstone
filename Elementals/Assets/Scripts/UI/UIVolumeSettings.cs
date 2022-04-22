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
    private const string PARAM_UI =  "Volume_Effects";

    private const string PREFS_MUTE = "MuteAllAudio";
    private const string PREFS_MASTER = "MasterVolume";
    private const string PREFS_MUSIC = "MusicVolume";
    private const string PREFS_EFFECTS = "EffectsVolume";
    private const string PREFS_UI = "UIVolume";


    private float[] range;

   


    private void Awake()
    {
        InitRange();
    }

    private void Start()
    {
        
        LoadSettings();
    }

    #region [SAVE LOAD]

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(PREFS_MASTER,SliderToDecible(masterVolumeSlider.value) );
        PlayerPrefs.SetFloat(PREFS_MUSIC, SliderToDecible(musicVolumeSlider.value));
        PlayerPrefs.SetFloat(PREFS_EFFECTS, SliderToDecible(effectsVolumeSlider.value));
    }

    public void LoadSettings()
    {
        float LoadVolume(string pref)
        {
            float defaultVolume = Mathf.Log10(1) * 20;
            if (PlayerPrefs.HasKey(pref))
            {
                return PlayerPrefs.GetFloat(pref);
               
            }
            Debug.Log($"Player Prefs missing {pref}");
            PlayerPrefs.SetFloat(pref, defaultVolume);
            return defaultVolume;
        }

        var masterVolume = LoadVolume(PREFS_MASTER);
        var musicVolume = LoadVolume(PREFS_MUSIC);
        var effectsVolume = LoadVolume(PREFS_EFFECTS);
        
        SetMasterVolumeSlider(DecibleToSlider(masterVolume));
        SetMusicVolumeSlider(DecibleToSlider(musicVolume));
        SetEffectsVolumeSlider(DecibleToSlider(effectsVolume));
        
        SetMasterVolumeDec(masterVolume);
        SetMusicVolumeDec(musicVolume);
        SetEffectsVolumeDec(effectsVolume);
    }
    
    

    #endregion
    
    void InitRange()
    {
        range = new float[]
        {
            Mathf.Log10(0.0001f) * 20,
            Mathf.Log10(1) * 20
        };
    }

    float DecibleToSlider(float decibleValue)
    {
        return Mathf.InverseLerp(range[0], range[1], decibleValue);
    }
    

    float SliderToDecible(float sliderValue)
    {
        return Mathf.Lerp(range[0], range[1], sliderValue);
    }

    #region SLIDERS SET

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

    #endregion

    #region MUTE

    void SetMuteToggle(bool isOn)
    {
        if (muteToggle != null) muteToggle.isOn = isOn;
    }
    public void SetMuteEnabled(bool mute)
    {
        SaveSettings();
    }

    #endregion

    
    public void SetMasterVolumeFromSlider(float sliderValue)
    {
        SetMasterVolumeDec(SliderToDecible(sliderValue));
        SaveSettings();
    }

    public void SeMusicVolumeFromSlider(float sliderValue)
    {
        SetMusicVolumeDec(SliderToDecible(sliderValue));
        SaveSettings();
    }

    public void SetEffectsVolumeFromSlider(float sliderValue)
    {
        SetEffectsVolumeDec(SliderToDecible(sliderValue));
        SaveSettings();
    }

    #region SET DEC

    public void SetMasterVolumeDec(float value)
    {
        SetVolumeOnMixer(PARAM_MASTER, value);
    }
    public void SetMusicVolumeDec(float value)
    {
        SetVolumeOnMixer(PARAM_MUSIC, value);
    }
    public void SetEffectsVolumeDec(float value)
    {
        SetVolumeOnMixer(PARAM_EFFECTS, value);
    }

    void SetVolumeOnMixer(string paramName, float dec)
    {
        SetMuteEnabled(false);
        SetMuteToggle(false);
        mixer.SetFloat(paramName, dec);
    }

    #endregion
}
