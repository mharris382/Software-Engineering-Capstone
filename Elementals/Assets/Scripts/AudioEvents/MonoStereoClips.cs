using UnityEngine;

namespace AudioEvents
{
    [CreateAssetMenu(fileName = "New MonoStereo Audio Clip", menuName = "Feedback/New Mono-Stereo Audio Clips", order = 0)]
    public class MonoStereoClips : ScriptableObject
    {
        [SerializeField] AudioClip[] monoClips;
        [SerializeField] AudioClip[] stereoClips;

        public AudioClip[] Clips
        {
            get
            {
                //TODO: return either mono or stereo version
                return stereoClips;
            }
        }
    }
}