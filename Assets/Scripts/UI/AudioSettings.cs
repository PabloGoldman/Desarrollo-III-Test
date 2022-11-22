using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class AudioSettings : MonoBehaviour  //Interactua con los audioMixers
    {
        public AudioMixer audioMixer;
        public AudioMixer SFXMixer;

        private const string volumeKey = "Volume";
        private const string sfxKey = "SFX";
        private const string masterKey = "Master";

        public void SetMusicVolume(float volume)
        {
            AkSoundEngine.SetRTPCValue(volumeKey, volume);
        }

        public void SetSFXVolume(float volume)
        {
            AkSoundEngine.SetRTPCValue(sfxKey, volume);
        }

        public void SetMasterVolume(float volume)
        {
            AkSoundEngine.SetRTPCValue(masterKey, volume);
        }
    }
}


