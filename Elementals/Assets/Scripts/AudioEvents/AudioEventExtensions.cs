using UnityEngine;

namespace AudioEvents
{
    public static class AudioEventExtensions
    {
        
        public static AudioEventHandler PlayAudioEvent(this Transform t, AudioEvent audioEvent)
        {
            var source = GetNewSource(audioEvent);
            if (audioEvent.looping)
            {
                source.transform.SetParent(t.parent, false);
            }
            else
            {
                source.transform.position = t.position;
            }
            
            return new AudioEventHandler(source);
        }

        private static AudioEventSource GetNewSource(AudioEvent audioEvent)
        {
            var source = GameObject.Instantiate(AudioManager.DefaultAudioSourcePrefab);
            source.AudioEvent = audioEvent;
            return source;
        }
    }
}