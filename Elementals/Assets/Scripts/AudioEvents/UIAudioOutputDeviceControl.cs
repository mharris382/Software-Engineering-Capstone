using UnityEngine;

namespace AudioEvents
{
    public class UIAudioOutputDeviceControl : MonoBehaviour
    {
        public void SetAudioDevice(AudioSettingsHelper.OutputAudioDeviceType deviceType)
        {
            AudioSettingsHelper.SetPlayerPrefsOutputDevice(deviceType);
        }
    }
}