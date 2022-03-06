using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//TODO: create unit tests for this class
public class SaveSlot
{
    private const string MAIN_SAVE_FILENAME = "SaveGame";
    private int _slotNumber;
    private string _slotName;
    
    //path to the game state data to use when exit/resume game
    private string _gameStateFilePath;
    
    //path to the game state data to use when player dies or manually reloads to last checkpoint
    private string _checkpointStateFilePath;
    private string _saveDirectoryPath;

    private BinaryFormatter _binaryFormatter;

    public BinaryFormatter Formatter => _binaryFormatter ??= new BinaryFormatter(); 

    public SaveSlot(int slotNumber)
    {
        _slotNumber = slotNumber;
        _slotName = $"Save_{slotNumber}";
        _saveDirectoryPath = Path.Combine(Application.persistentDataPath, _slotName);
        if (!Directory.Exists(_saveDirectoryPath))
        {
            Directory.CreateDirectory(_saveDirectoryPath);
        }
        _gameStateFilePath = $"{_saveDirectoryPath}/game_state.dat";
        _checkpointStateFilePath = $"{_saveDirectoryPath}/load_state.dat";
        
    }


    public SavedGameState LoadCheckpointGameState()
    {
        return LoadGameState(_gameStateFilePath);
    }

    public SavedGameState LoadCurrentGameState()
    {
        return LoadGameState(_gameStateFilePath);
    }

    public void SaveCheckpointGameState(SavedGameState checkpointGameState)
    {
        SaveGameState(_checkpointStateFilePath, checkpointGameState);
    }

    public void SaveCurrentGameState(SavedGameState savedGameState)
    {
        SaveGameState(_gameStateFilePath, savedGameState);
    }

    private void SaveGameState(string path, SavedGameState state)
    {
        if (state == null)
        {
            Debug.LogWarning("Tried to save null game state");
            return;
        }
        using (FileStream dataStream = new FileStream(path, FileMode.Create))
        {
            Formatter.Serialize(dataStream, state);
        }
    }

    private SavedGameState LoadGameState(string path)
    {
        if (!File.Exists(path))
        {
            SaveGameState(path, SavedGameState.NewGameInitialState);
        }

        SavedGameState state;
        using (FileStream dataStream = new FileStream(path, FileMode.OpenOrCreate))
        {
            state = (SavedGameState)Formatter.Deserialize(dataStream);
        }
        return state;
    }

    private void DeleteSaveData(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }


 

    //each ID gets 2 files a state file and a checkpoint file
    public void SaveSlotData<T>(string dataID, T data)
    {
        string path = $"{_saveDirectoryPath}/{dataID}.dat";
        using (FileStream dataStream = new FileStream(path, FileMode.Create))
        {
            Formatter.Serialize(dataStream, data);
        }
    }
    
    public T LoadSaveData<T>(string dataID)
    {
        string path = $"{_saveDirectoryPath}/{dataID}.dat";
        T data;
        using (FileStream dataStream = new FileStream(path, FileMode.Create))
        {
             data = (T) Formatter.Deserialize(dataStream);
        }
        return data;

    }
    
    public void SaveSlotDataAsCheckpoint<T>(string dataID, T data)
    {
        string path = $"{_saveDirectoryPath}/{dataID}_CP.dat";
        using (FileStream dataStream = new FileStream(path, FileMode.Create))
        {
            Formatter.Serialize(dataStream, data);
        }
    }
    
    public T LoadCheckpointSaveData<T>(string dataID)
    {
        string path = $"{_saveDirectoryPath}/{dataID}_CP.dat";
        T data;
        using (FileStream dataStream = new FileStream(path, FileMode.Create))
        {
            data = (T) Formatter.Deserialize(dataStream);
        }
        return data;

    }
}