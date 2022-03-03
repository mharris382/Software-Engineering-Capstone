using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    public class SaveSlot
    {
        private const string MAIN_SAVE_FILENAME = "SaveGame";
        private int _slotNumber;
        private string _slotName;
        private string _mainSaveFilePath;
        private string _saveDirectoryPath;
        public SaveSlot(int slotNumber)
        {
            _slotNumber = slotNumber;
            _slotName = $"Save_{slotNumber}";
            _saveDirectoryPath = Path.Combine(Application.persistentDataPath, _slotName);
        }
    }

   
    
    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    var go = new GameObject("GAME_MANAGER");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
    }

    
    
    

    public void OpenMainMenu()
    {
        //TODO: load the main menu scene
    }

    public void SaveAndExitToMainMenu()
    {
        SaveGame();
        OpenMainMenu();
    }

    public void SaveAndExitApplication()
    {
        SaveGame();
        ExitApplication();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        //TODO: create save game
    }
}


[Serializable]
public class SaveData
{
    public string level;
        
    public Vector2 lastCheckpoint;
    public Vector2 position;
        
    public float playerMana;
    public float playerHealth;
        
    
}

