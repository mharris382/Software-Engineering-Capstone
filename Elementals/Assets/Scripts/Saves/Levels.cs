using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Levels : ScriptableObject
{
    [SerializeField]
    private LevelData[] levels;
    
    
    private static Levels _instance;
    private static Levels Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<Levels>("Levels");
#if UNITY_EDITOR
                Levels asset = ScriptableObject.CreateInstance<Levels>();
                AssetDatabase.CreateAsset(asset, "Assets/Resources/Levels.asset");
                AssetDatabase.SaveAssets();
                string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(LevelData)));
                if (guids.Length == 0)
                {
                    Debug.LogWarning("No level data found in asset database, creating new level data now");
                }
                else
                {
                    
                }
#endif
            }
            return _instance;
        }
    }

    public static int LevelCount => Instance.levels.Length;

    public static LevelData GetLevel(int id)
    {
        try {
            return Instance.levels[id];
        }
        catch (IndexOutOfRangeException e)
        {
            //TODO: ERROR HANDLING
            throw;
        }
    }
}




#if UNITY_EDITOR

[CustomEditor(typeof(Levels))]
public class LevelsEditor : Editor
{
    private SerializedProperty _levelsProperty;
    private void OnEnable()
    {
        _levelsProperty = serializedObject.FindProperty("levels");
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Find All Levels", GUILayout.MinWidth(150), GUILayout.MaxWidth(300)))
        {
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(LevelData)));
            List<LevelData> levels = new List<LevelData>();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var levelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);
                levels.Add(levelData);
            }
            LevelData[] levelArray = levels.ToArray();
            _levelsProperty.arraySize = levelArray.Length;
            for (int i = 0; i < levelArray.Length; i++)
            {
                _levelsProperty.InsertArrayElementAtIndex(i);
                _levelsProperty.GetArrayElementAtIndex(i).objectReferenceValue = levelArray[i];
            }
        }
    base.OnInspectorGUI();
    }
}

#endif