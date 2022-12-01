using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class AudioSettings : MonoBehaviour  //Interactua con los audioMixers
    {
        private const string volumeKey = "Volumen_Musica";
        private const string sfxKey = "Volumen_SFX";
        private const string masterKey = "Volumen_Master";

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


