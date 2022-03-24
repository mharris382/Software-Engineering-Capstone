using UnityEngine;

[CreateAssetMenu(menuName = "Core/Create New Level", fileName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public int levelID;
    public string startSceneName;
}