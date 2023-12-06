using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    private bool _skipTutorial;
    public int firstLevel = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _skipTutorial = true;
            StartGame();
        }
    }

    public void StartTutorial()
    {
        if (!_skipTutorial)
        {
            SceneManager.LoadScene("Tutorial Level");
        }
        else
        {
            StartGame();
        }
    }
    
    public void SetEnableSkipTutorial(bool skipTutorial)
    {
        _skipTutorial = skipTutorial;
    }
}
