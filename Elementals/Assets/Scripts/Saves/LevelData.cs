using UnityEngine;

[CreateAssetMenu(menuName = "Create New Level", fileName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public int levelID;
    public string startSceneName;
}