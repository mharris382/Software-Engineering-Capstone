using System;
using System.Collections;
using UnityEngine;

namespace AudioEvents
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEventSource : MonoBehaviour
    {
        
        private AudioSource _source;
        public AudioSource Source
        {
            get
            {
                if (_source == null)
                {
                    _source = GetComponent<AudioSource>();
                }
                return _source;
            }
        }

        [SerializeField] private AudioEvent audioEvent;
        [SerializeField] private bool playOnStart;
        public AudioEvent AudioEvent
        {
            get => audioEvent;
            set
            {
                audioEvent = value;
            }
        }

        public void Play()
        {
            if (AudioEvent.looping)
            {
                Source.loop = true;
            }
            else
            {
                Source.loop = false;
            }
            AudioEvent.ApplyToAudioSource(Source);
            Source.Play();
        }

        internal  void UpdateAudioSource()
        {
            AudioEvent.ApplyToAudioSource(Source);
        }

        private void OnEnable()
        {
            if (playOnStart && audioEvent != null)
            {
                Play();
            }
        }
    }
}