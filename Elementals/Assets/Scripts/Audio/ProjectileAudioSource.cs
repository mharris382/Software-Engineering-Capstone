using System;
using UnityEngine;

namespace AudioManager
{
    
    public class ProjectileAudio : MonoBehaviour
    {
        public AudioSource spawnAudio;

        public AudioSource destroyedAudio;

        private void Start()
        {
            PrepareAudioGameObjects();
            spawnAudio.Play();

            void PrepareAudioGameObjects()
            {
                spawnAudio.transform.SetParent(transform, false);
                destroyedAudio.transform.SetParent(transform, false);
                destroyedAudio.Stop();
            }

        }

        private void OnDestroy()
        {
            CleanUpSpawnAudio();
            SetupDestroyAudio();
            

            void CleanUpSpawnAudio()
            {
                if (spawnAudio == null) return;
                if (!spawnAudio.loop)
                {
                    spawnAudio.transform.SetParent(null);
                }
            }

            void SetupDestroyAudio()
            {
                if (destroyedAudio == null) return;
                destroyedAudio.transform.SetParent(null);
                destroyedAudio.Play();
            }
        }
    }
}