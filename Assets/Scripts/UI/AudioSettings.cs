using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game
{
    public class AudioSettings : MonoBehaviour  //Interactua con los audioMixers
    {
        private const string volumeKey = "Volumen_Musica";
        private const string sfxKey = "Volumen_SFX";
        private const string masterKey = "Volumen_Master";

        public Slider thisSlider;
        public float masterVolume;
        public float musicVolume;
        public float SFXVolume;

        public void SetSpecificVolume(string whatValue)
        {
            float sliderValue = thisSlider.value;

            switch (whatValue)
            {
                case "Master":
                    masterVolume = thisSlider.value;
                    AkSoundEngine.SetRTPCValue(masterKey, masterVolume);
                    break;
                case "Music":
                    musicVolume = thisSlider.value;
                    AkSoundEngine.SetRTPCValue(volumeKey, musicVolume);
                    break;
                case "Sounds":
                    SFXVolume = thisSlider.value;
                    AkSoundEngine.SetRTPCValue(sfxKey, SFXVolume);
                    break;
                default:
                    break;
            }
        }

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


