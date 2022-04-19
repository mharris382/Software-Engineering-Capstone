using System;
using UnityEngine;

namespace AudioEvents
{
    public static class AudioSettingsHelper
    {
        public enum OutputAudioDeviceType
        {
            Headphones = 1,
            Speakers = 2,
            SurroundSound = 3
        }

        public const string PREF_OUTPUT_DEVICE = "Output_Audio_Device_Type";

        public static OutputAudioDeviceType GetPlayerPrefOutputDevice()
        {
            if (!PlayerPrefs.HasKey(PREF_OUTPUT_DEVICE))
            {
                PlayerPrefs.SetInt(PREF_OUTPUT_DEVICE, 1);
            }
            int deviceType = PlayerPrefs.GetInt(PREF_OUTPUT_DEVICE);
            deviceType = Mathf.Clamp(deviceType, 1, 3);
            return (OutputAudioDeviceType) deviceType;
        }

        public static void SetPlayerPrefsOutputDevice(OutputAudioDeviceType deviceType)
        {
            PlayerPrefs.SetInt(PREF_OUTPUT_DEVICE, (int)deviceType);
        }
        
        public static bool DeviceTypeUsesStereoSound(OutputAudioDeviceType deviceType)
        {
            switch (deviceType)
            {
                case OutputAudioDeviceType.Headphones:
                case OutputAudioDeviceType.SurroundSound:
                    return true;
                case OutputAudioDeviceType.Speakers:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null);
            }
        }
    }
}