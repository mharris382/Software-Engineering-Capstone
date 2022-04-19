
using System;
using System.Collections;
using UnityEngine;

namespace AudioEvents
{
    public struct AudioEventHandler : IDisposable
    {
        public bool IsPlaying { get; private set; }
        public bool IsFinished => !IsPlaying || Time.time > _endTime;

        private float _endTime;
        private Action cleanup;
        public AudioEventHandler(AudioEventSource source)
        {
            if (source.AudioEvent == null)
            {
                IsPlaying = false;
                throw new NullReferenceException("Cannot create audio event handler on <b>AudioEventSource</b> without first setting <b>AudioEvent</b> ");
            }
            IsPlaying = true;
            
            //play the audio event
            Play(source.Source, source.AudioEvent);
            source.StartCoroutine(UpdateSound(source.Source, source.AudioEvent));
            cleanup = () =>
            {
                source.StopAllCoroutines();
                GameObject.Destroy(source.gameObject);
            };
            
            
            //for non looping calculate time that clip will finish            
            var clipDuration = source.Source.clip.length;
            var seconds = clipDuration / Mathf.Abs(source.Source.pitch);
            _endTime = source.AudioEvent.looping ? float.MaxValue : Time.time + seconds;
        }

        static void Play(AudioSource Source, AudioEvent audioEvent)
        {
            audioEvent.ApplyToAudioSource(Source);
            Source.Play();
        }
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        static IEnumerator UpdateSound(AudioSource Source, AudioEvent audioEvent)
        {
            Play(Source, audioEvent);
            if (audioEvent.looping)
            {
                while (Source.isPlaying && Source.clip != null && Source.pitch!=0)
                {
                    var clipDuration = Source.clip.length;
                    var seconds = clipDuration / Mathf.Abs(Source.pitch);
                    
                    yield return new WaitForSeconds(seconds);
                    Play(Source, audioEvent);
                }
            }
            else
            {
                var clipDuration = Source.clip.length;
                var seconds = clipDuration / Mathf.Abs(Source.pitch);
                yield return new WaitForSeconds(seconds);
            }
        }
        
        public void Dispose()
        {
            //if already disposed don't do anything
            if (!IsPlaying) return;
            IsPlaying = false;
            
            //call cleanup
            cleanup?.Invoke();
            cleanup = null;
        }
    }
}