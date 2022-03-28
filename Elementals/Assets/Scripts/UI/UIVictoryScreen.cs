using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR

#endif
public class UIVictoryScreen : MonoBehaviour
{
  

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public class UIButtons : MonoBehaviour
{
    
    
    
}

#if UNITY_EDITOR
public class UIButtonsEditor
{
    
}
#endif

[Serializable]
public struct ButtonInfo
{
    [SerializeField] private string name;
    [SerializeField] private bool overrideText;
    [SerializeField] private string text;
    public UnityEvent OnPressed;

    public string Name => GetStringSafe(name, "UNNAMED BUTTON");
    public string Text => overrideText ? GetStringSafe(text, name) : name;
    

    private string GetStringSafe(string str, string fallback) => string.IsNullOrEmpty(name) ? fallback : name;
}