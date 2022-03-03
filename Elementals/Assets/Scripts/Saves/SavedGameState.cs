using System;
using UnityEngine;

[Serializable]
public class SavedGameState
{
    public int level;
    public float posX;
    public float posY;
    public float playerMana;      //saved as a percentage of health rather than absolute value
    public float playerHealth;    //saved as a percentage of health rather than absolute value
    public string playerElement;

    public void SaveState(IPlayerSaveLoad saveFrom)
    {
        level = GameManager.Instance.CurrentLevel;
        posX = saveFrom.Position.x;
        posY = saveFrom.Position.y;
        playerHealth = saveFrom.HealthPercent;
        playerMana = saveFrom.ManaPercent;
        playerElement = saveFrom.PlayerElement;
    }

    public void ApplyState(IPlayerSaveLoad loadTo)
    {
        GameManager.Instance.CurrentLevel = level;
        loadTo.Position = new Vector2(posX, posY);
        loadTo.HealthPercent = playerHealth;
        loadTo.ManaPercent = playerMana;
        loadTo.PlayerElement = playerElement;
    }

    public static SavedGameState NewGameInitialState =>
        new SavedGameState()
        {
            level = 0,
           posX = 0,
           posY = 0,
            playerHealth = 1,
            playerMana = 1,
            playerElement = ""
        };

    public bool HasPlayerPickedFirstElement() => !string.IsNullOrEmpty(playerElement);
}