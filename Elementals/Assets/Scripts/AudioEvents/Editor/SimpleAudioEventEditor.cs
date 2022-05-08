using AudioEvents;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
#endif

[CustomEditor(typeof(AudioEvent))]
#if ODIN_INSPECTOR
public class SimpleAudioEventEditor : OdinEditor
{
    protected override void OnEnable()
    {
        Initialize();
        base.OnEnable();
    }
 protected override void OnDisable()
    {
        Cleanup();
        base.OnDisable();
    }
#else
public class SimpleAudioEventEditor : Editor
{
    protected  void OnEnable()
    {
        Initialize();
    }
    protected void OnDisable()
    {
        Cleanup();
    }
#endif
    private AudioSource previewSource;
    void Initialize()
    {
        previewSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview",
                HideFlags.HideAndDontSave,
                typeof(AudioSource))
            .GetComponent<AudioSource>();
    }
    
    new void Cleanup()
    {
        DestroyImmediate(previewSource.gameObject);
        
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.FindProperty("monoClips");
        serializedObject.FindProperty("stereoClips");
        DrawPreviewButton();
    }

    void DrawPreviewButton()
    {
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            var simpleTarget = ((AudioEvent)target);
            simpleTarget.Play(previewSource);

        }
        EditorGUI.EndDisabledGroup();
    }
}

#endif