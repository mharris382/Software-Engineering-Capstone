using System.Collections;
using System.Collections.Generic;
using Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerSaveSlot = 0;

    [SerializeField] private int defaultLevelID = 0;
    [SerializeField]
    private GameObject playerSaveLoader;
    [SerializeField]
    private LevelData levelData;

    
    public int CurrentLevel
    {
        get => levelData.levelID;
        set
        {
            if (levelData == null || levelData.levelID != value)
            {
                ChangeLevels(value);
            }
        }
    }

    private IPlayerSaveLoad _playerSaveLoad;
    public IPlayerSaveLoad PlayerSaveLoader
    {
        get
        {
            if (_playerSaveLoad == null)
            {
                if (playerSaveLoader == null)
                {
                    playerSaveLoader = GameObject.FindGameObjectWithTag("Player");
                    if(playerSaveLoader == null)Debug.LogError("NO PLAYER FOUND IN SCENE");
                    OpenMainMenu();
                    return null;
                }
                _playerSaveLoad = playerSaveLoader.GetComponent<IPlayerSaveLoad>();
                if (_playerSaveLoad == null)
                {
                    Debug.LogError("<b>PLAYER GAMEOBJECT MISSING SAVE_LOAD COMPONENT!</b>", playerSaveLoader);
                }
            }
            return _playerSaveLoad;
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


    private GameState _state;

    public GameState State
    {
        get => _state;
        set
        {
            if (value != _state)
            {
                _state = value;
                OnGameStateChanged?.Invoke(_state);
            }
        }
    }
    public static event Action<GameState> OnGameStateChanged;
    
    private SaveSlot _slot;
    private SaveSlot Slot
    {
        get
        {
            if (_slot == null)
            {
                _slot= new SaveSlot(Mathf.Clamp(playerSaveSlot, 0, 2));//3 slots [0,1,2]
            }
            return _slot;
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
            if (levelData)
            {
                LoadLevel();
            }
        }
    }

    
    private void LoadLevel()
    {
        if (playerSaveSlot < 0)
        {
            Debug.LogError("No player save slot selected");
            OpenMainMenu();
            return;
        }

        if (levelData == null)
        {
            levelData = Levels.GetLevel(defaultLevelID);
        }
        StartCoroutine(LoadWaitForScene());
    }

    IEnumerator LoadWaitForScene()
    {
        var op = SceneManager.LoadSceneAsync(levelData.startSceneName);
        while (op.isDone == false)
        {
            yield return null;
        }

        yield return null;
        var player =GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "No object tagged as player in scene");
        var loader = player.GetComponent<IPlayerSaveLoad>();
        Debug.Assert(loader != null, "No loader on player in scene");
        _playerSaveLoad = loader;
        SaveGame();
        SaveCheckpoint();
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

    public void LoadLastCheckpoint()
    {
        Slot.LoadCheckpointGameState().ApplyState(PlayerSaveLoader);
    }

    public void PlayerReachedCheckpoint()
    {
        var state = GetCurrentGameState();
        state.playerMana = 1;
        state.playerHealth = 1;
        Slot.SaveCheckpointGameState(state);
    }

    public void SaveCheckpoint()
    {
        var state = GetCurrentGameState();
        Slot.SaveCheckpointGameState(state);
    }
    
    public void SaveGame()
    {
        if (playerSaveSlot < 0)
        {
            Debug.LogError("No player save slot selected");
            return;
        }
        var state = GetCurrentGameState();
        Slot.SaveCurrentGameState(state);
    }

    SavedGameState GetCurrentGameState()
    {
        var state = new SavedGameState();
        state.SaveState(this.PlayerSaveLoader);
        state.level = levelData.levelID;
        return state;
    }

    public void ChangeLevels(int newLevel)
    {
        levelData = Levels.GetLevel(newLevel);
        ChangeLevels(levelData);
    }

    public void ChangeLevels(LevelData levelData)
    {
        LoadLevel();
        SaveCheckpoint();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            LoadLastCheckpoint();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SaveGame();
            SaveCheckpoint();
        }
    }
}

public enum GameState
{
    MainMenu,
    Loading,
    Playing,
    Paused,
}