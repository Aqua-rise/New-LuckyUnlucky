using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace C__Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Slider menuVolumeSliderSettingsMenu;
        public AudioSource tambourine;
        public AudioSource bass;
        public AudioSource kick;
        public AudioSource drumset;
        public AudioSource marimba;
        public AudioSource guitar;
        public AudioSource numberGen1;
        public AudioSource numberGen2;
        public AudioSource numberGen3;
        public AudioSource numberGen4;
        public AudioSource numberGen5;
        public AudioSource numberGen6;
        public AudioSource numberGen7;
        public AudioSource numberGen8;
        public AudioSource numberGen9;
        public AudioSource gainUpgradeToken;
        
        public TMP_Text AudioVolumeText;

        //These two always start enabled
        private bool _tambourineEnabled = true;
        private bool _bassEnabled = true;
        
        private bool _kickEnabled;
        private bool _drumsetEnabled;
        
        private bool _marimbaEnabled;
        private bool _guitarEnabled;
        

        
        void Start()
        {
            
            double startTime = AudioSettings.dspTime;
            
            tambourine.PlayScheduled(startTime);
            bass.PlayScheduled(startTime);
            kick.PlayScheduled(startTime);
            drumset.PlayScheduled(startTime);
            marimba.PlayScheduled(startTime);
            guitar.PlayScheduled(startTime);
            
        }
        
        public void StartGameButtonPressed()
        {
            //When the game starts, enable the audio for marimba and guitar

            marimba.volume = menuVolumeSliderSettingsMenu.value;
            _marimbaEnabled = true;
            guitar.volume = menuVolumeSliderSettingsMenu.value;
            _guitarEnabled = true;
        }
        public void DifficultySelectButtonPressed()
        {
            //When the difficulty button is pressed, enable the audio for kick and drumset

            kick.volume = menuVolumeSliderSettingsMenu.value;
            _kickEnabled = true;
            drumset.volume = menuVolumeSliderSettingsMenu.value;
            _drumsetEnabled = true;
        }

        public void BackToMainMenu()
        {
            marimba.volume = 0.001f;
            _marimbaEnabled = false;
            guitar.volume = 0.001f;
            _guitarEnabled = false;
            kick.volume = 0.001f;
            _kickEnabled = false;
            drumset.volume = 0.001f;
            _drumsetEnabled = false;
        }
        

        public void ShopEntered()
        {
            marimba.volume = 0.001f;
            _marimbaEnabled = false;
            guitar.volume = 0.001f;
            _guitarEnabled = false;
            drumset.volume = 0.001f;
            _drumsetEnabled = false;
        }

        public void ShopExited()
        {
            marimba.volume = menuVolumeSliderSettingsMenu.value;
            _marimbaEnabled = true;
            guitar.volume = menuVolumeSliderSettingsMenu.value;
            _guitarEnabled = true;
            drumset.volume = menuVolumeSliderSettingsMenu.value;
            _drumsetEnabled = true;
        }

        public void AdjustAllEnabledAudioSourceVolume(float volume)
        {
            //Edit audio display text
            AudioVolumeText.text = (volume / 100).ToString("P0");
            
            //Edit only enabled volumes
            if (_tambourineEnabled)
            {
                tambourine.volume = volume;
            }
            if (_bassEnabled)
            {
                bass.volume = volume;
            }
            if (_kickEnabled)
            {
                kick.volume = volume;
            }
            if (_drumsetEnabled)
            {
                drumset.volume = volume;
            }
            if (_marimbaEnabled)
            {
                marimba.volume = volume;
            }
            if (_guitarEnabled)
            {
                guitar.volume = volume;
            }
        }
        
        public void SetMusicVolume(float value)
        {
            //Calculated to translate values from the slider
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("musicVolume", value);
        }
        public void SetSfxVolume(float value)
        {
            //Calculated to translate values from the slider
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("sfxVolume", value);
        }
        
        public void GameWin()
        {
            //TURN IT ALL OFF
            tambourine.volume = 0;
            bass.volume = 0;
            kick.volume = 0;
            drumset.volume = 0;
            marimba.volume = 0;
            guitar.volume = 0;
            
            
        }

        public void PlayGenerateNumberSound()
        {
            switch (Random.Range(1, 10))
            {
                case 1:
                    numberGen1.Play();
                    break;
                case 2:
                    numberGen2.Play();
                    break;
                case 3:
                    numberGen3.Play();
                    break;
                case 4:
                    numberGen4.Play();
                    break;
                case 5:
                    numberGen5.Play();
                    break;
                case 6:
                    numberGen6.Play();
                    break;
                case 7:
                    numberGen7.Play();
                    break;
                case 8:
                    numberGen8.Play();
                    break;
                case 9:
                    numberGen9.Play();
                    break;
                default:
                    break;
            }
        }

        public void PlayUpgradeTokenSound()
        {
            gainUpgradeToken.Play();
        }
    }
}