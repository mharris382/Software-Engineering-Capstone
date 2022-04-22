using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplaySettings : MonoBehaviour
{
    private const string PREFS_USE_GAMEPAD = "UseGamepad";
    private const string PREFS_CONFINE_CURSOR = "ConfineCursor";

    void SaveSettings()
    {
        bool useGamepad = useGamepadToggle != null && useGamepadToggle.isOn;
        bool confineCursor = cursorConfinedToggle == null || cursorConfinedToggle.isOn;
        SaveBool(PREFS_USE_GAMEPAD, useGamepad);
        SaveBool(PREFS_CONFINE_CURSOR, confineCursor);
    }
    void LoadSettings()
    {
        bool useGamepad =     LoadBool(PREFS_USE_GAMEPAD, false);
        bool confineCursor = LoadBool(PREFS_CONFINE_CURSOR, true);
        SetCursorConfinedToggle(confineCursor);
        SetCursorConfined(confineCursor);
        SetUseGamepadToggle(useGamepad);
        SetUseGamepad(useGamepad);
    }
    void SaveBool(string key, bool value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }

    bool LoadBool(string key, bool defaultValue = false)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int i = PlayerPrefs.GetInt(key);
            return i > 0;
        }
        PlayerPrefs.SetInt(key, defaultValue ? 1 : 0);
        return defaultValue;
    }


    private void Awake()
    {
        LoadSettings();
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }


    public Toggle cursorConfinedToggle;
    public Toggle useGamepadToggle;


    void SetCursorConfinedToggle(bool isOn)
    {
        SetToggle(cursorConfinedToggle, isOn);
    }

    void SetUseGamepadToggle(bool isOn)
    {
        SetToggle(useGamepadToggle, isOn);
    }
    
    
    void SetToggle(Toggle control, bool isOn)
    {
        if (control)
        {
            control.isOn = isOn;
        }
    }

   public void SetUseGamepad(bool useGamepad)
    {
        
    }

 public  void SetCursorConfined(bool isConfined)
    {
        Cursor.lockState = isConfined ? CursorLockMode.Confined : CursorLockMode.None;
    }
}
