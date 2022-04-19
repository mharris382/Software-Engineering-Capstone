using UnityEngine;

namespace AudioEvents
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        private static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                    if(_instance == null){
                        var go = new GameObject("{AUDIO MANAGER}");
                        _instance = go.AddComponent<AudioManager>();
                    }
                }
                return _instance;
            }
        }

        [SerializeField]
        private AudioEventSource defaultAudioSourcePrefab;

        public static AudioEventSource DefaultAudioSourcePrefab => Instance.GetDefaultAudioSourcePrefab();
        
        
        
        private AudioEventSource GetDefaultAudioSourcePrefab()
        {
            if (defaultAudioSourcePrefab == null)
            {
                var go = new GameObject("Default Audio Source Prefab", typeof(AudioSource));
                go.transform.SetParent(transform);
                defaultAudioSourcePrefab = go.AddComponent<AudioEventSource>();
            }
            return defaultAudioSourcePrefab;
        } 
    }
}