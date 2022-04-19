using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class UIGraphicsSettings : MonoBehaviour
{
    private const string PREFS_GRAPHICS_QUALITY_SETTINGS = "QualitySettingPreference";
    private const string PREFS_RESOLUTION = "ResolutionPreference";
    private const string PREFS_TEXTURE_QUALITY = "TextureQualityPreference";
    private const string PREFS_ANTI_ALAISING = "AntiAliasingPreference";
    private const string PREFS_FULLSCREEN = "FullscreenPreference";
    
    
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private TMP_Dropdown textureDropdown;
    [SerializeField]
    private TMP_Dropdown aaDropdown;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    public bool alwaysSaveSettings;
    Resolution[] resolutions;

    private void Awake()
    {
        InitResolutions();
    }




    public void Quit()
    {
        SaveSettings();
        Application.Quit();
    }

    void InitResolutions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + 
                            resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt(PREFS_GRAPHICS_QUALITY_SETTINGS, qualityDropdown.value);
        PlayerPrefs.SetInt(PREFS_RESOLUTION, resolutionDropdown.value);
        PlayerPrefs.SetInt(PREFS_TEXTURE_QUALITY, textureDropdown.value);
        PlayerPrefs.SetInt(PREFS_ANTI_ALAISING, aaDropdown.value);
        PlayerPrefs.SetInt(PREFS_FULLSCREEN, Convert.ToInt32(Screen.fullScreen));
    }
    
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey(PREFS_GRAPHICS_QUALITY_SETTINGS))
            SetQualityDropdown(PlayerPrefs.GetInt(PREFS_GRAPHICS_QUALITY_SETTINGS));
        else
            SetQualityDropdown(3);
        if (PlayerPrefs.HasKey(PREFS_RESOLUTION))
            SetResolutionDropdown(PlayerPrefs.GetInt(PREFS_RESOLUTION));
        else
            SetResolutionDropdown(currentResolutionIndex);
        if (PlayerPrefs.HasKey(PREFS_TEXTURE_QUALITY))
            SetTextureDropdown(PlayerPrefs.GetInt(PREFS_TEXTURE_QUALITY));
        else
            SetTextureDropdown(0);
        if (PlayerPrefs.HasKey(PREFS_ANTI_ALAISING))
            SetAADropdown(PlayerPrefs.GetInt(PREFS_ANTI_ALAISING));
        else
            SetAADropdown(1);
        if (PlayerPrefs.HasKey(PREFS_FULLSCREEN))
            SetFullscreenToggle(Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_FULLSCREEN)));
        else
            SetFullscreenToggle(true);
    }

   
    public void SetFullscreenToggle(bool enabled)
    {
        Screen.fullScreen = enabled;
    }
   
    void SetResolutionDropdown(int value)
    {
        if (resolutionDropdown != null)
        {
            resolutionDropdown.value = value;
        }
    }
    void SetTextureDropdown(int value)
    {
        if (textureDropdown != null)
            textureDropdown.value = value;
    }
    void SetAADropdown(int value)
    {
        if (aaDropdown != null)
            aaDropdown.value = value;
    }
    void SetQualityDropdown(int value)
    {
        if(qualityDropdown!=null)
            qualityDropdown.value = value;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        if(alwaysSaveSettings)
            SaveSettings();
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        SetQualityDropdown(6);
        if(alwaysSaveSettings)
            SaveSettings();
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        SetQualityDropdown(6);
        if(alwaysSaveSettings)
            SaveSettings();
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) // if the user is not using 
            //any of the presets
            //QualitySettings.SetQualityLevel(qualityIndex);
        switch (qualityIndex)
        {
            case 0: // quality level - very low
                SetTextureDropdown(3);
                SetAADropdown(0);
                Debug.Log("Switching to very low quality");
                break;
            case 1: // quality level - low
                SetTextureDropdown(2);
                SetAADropdown(0);
                Debug.Log("Switching to low quality");
                break;
            case 2: // quality level - medium
                SetTextureDropdown(1);
                SetAADropdown(0);
                Debug.Log("Switching to medium quality");
                break;
            case 3: // quality level - high
                SetTextureDropdown(0);
                SetAADropdown(0);
                Debug.Log("Switching to high quality");
                break;
            case 4: // quality level - very high
                SetTextureDropdown(0);
                SetAADropdown(1);
                Debug.Log("Switching to very high quality");
                break;
            case 5: // quality level - ultra
                SetTextureDropdown(0);
                SetAADropdown(2);
                Debug.Log("Switching to ultra quality");
                break;
        }
        SetQualityDropdown(qualityIndex);
        if(alwaysSaveSettings)
            SaveSettings();
    }
    
   
}


