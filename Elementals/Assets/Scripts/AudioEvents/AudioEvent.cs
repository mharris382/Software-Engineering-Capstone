#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AudioEvents
{
    [CreateAssetMenu(menuName ="Feedback/New Audio Event")]
    public class AudioEvent : ScriptableObject
    {
#if ODIN_INSPECTOR
    [ListDrawerSettings(Expanded =true)]
#endif
        public bool looping;
        
        [SerializeField]
        private AudioClip[] clips = new AudioClip[0];


        [SerializeField]
        private RangedFloat volume = new RangedFloat(0.5f, 1);

        [MinMaxRange(0, 2f), SerializeField]
        private RangedFloat pitch = new RangedFloat(1, 1);

        [SerializeField,  MinMaxRange(0f, 1000f)]
        private RangedFloat distance = new RangedFloat(1, 1000);



        [SerializeField] private MonoStereoClips monoStereoClips;

        
        
        
        [SerializeField]
        private AudioMixerGroup mixer;

        public void Play(AudioSource source)
        {
            ApplyToAudioSource(source);
            
            source.Play();
        }

        public void ApplyToAudioSource(AudioSource source)
        {
            source.outputAudioMixerGroup = mixer;

            int clipIndex = Random.Range(0, clips.Length);
            source.clip = clips[clipIndex];

            source.pitch = Random.Range(pitch.min, pitch.max);
            source.volume = Random.Range(volume.min, volume.max);

            source.minDistance = distance.min;
            source.maxDistance = distance.max;

            source.loop = looping;
        }
    }
}