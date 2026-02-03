using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public TMP_Dropdown framerateDropdown;
    Resolution[] resolutions;
    public AudioMixer audioMixer;
    public Slider menuVolumeSliderSettingsMenu;
    public Slider sfxVolumeSliderSettingsMenu;
    public Slider menuVolumeSliderPauseMenu;
    public Slider sfxVolumeSliderPauseMenu;


    void Awake()
    {
        // fullscreen
        bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        Screen.fullScreen = fullscreen;

        // audio
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
        
        // sliders
        InitializeMenuSliders();

        // FPS
        int fps = PlayerPrefs.GetInt("targetFPS", 60);
        Application.targetFrameRate = fps;
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
    
    public void SetTargetFPS(int fps)
    {
        Debug.Log(fps);
        switch (fps)
        {
            case 0:         
                Application.targetFrameRate = -1;
                PlayerPrefs.SetInt("targetFPS", -1);
                break;
            case 1:
                Application.targetFrameRate = 30;
                PlayerPrefs.SetInt("targetFPS", 30);
                break;
            case 2:
                Application.targetFrameRate = 60;
                PlayerPrefs.SetInt("targetFPS", 60);
                break;
            case 3:        
                Application.targetFrameRate = 90;
                PlayerPrefs.SetInt("targetFPS", 90);
                break;
            case 4:        
                Application.targetFrameRate = 120;
                PlayerPrefs.SetInt("targetFPS", 120);
                break;
        }
    }

    public void SetVsync(bool isOn)
    {
        Debug.Log("Vsync enabled: " + isOn);
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        if (isOn)
        {
            framerateDropdown.value = 0;
            framerateDropdown.interactable = false;
        }
        else
        {
            framerateDropdown.interactable = true;
        }
    }
    
    public void InitializeMenuSliders()
    {
        menuVolumeSliderSettingsMenu.value = PlayerPrefs.GetFloat("musicVolume");
        menuVolumeSliderPauseMenu.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSliderSettingsMenu.value = PlayerPrefs.GetFloat("sfxVolume");
        sfxVolumeSliderPauseMenu.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    
    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
