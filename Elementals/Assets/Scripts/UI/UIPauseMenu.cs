using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [Range(1,3)]
    [SerializeField] private int currentLevel = 1;
    private bool _paused;

    public UnityEvent<bool> onGamePaused;
    public UnityEvent onMenuOpened;
    public void ResumeGame()
    {
        _paused = false;
        Time.timeScale = 1;
        onGamePaused?.Invoke(false);
    }

    public void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0;
        onGamePaused?.Invoke(true);
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    IEnumerator Start()
    {
        ResumeGame();
        while (enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _paused = !_paused;
                if (!_paused) ResumeGame();
                else PauseGame();
                yield return new WaitForSecondsRealtime(1);
            }
            yield return null;
        }
    }
    

}


