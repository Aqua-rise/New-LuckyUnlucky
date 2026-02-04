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
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;


    void Start()
    {
        // fullscreen
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen") == 1;

        // audio
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
        
        // sliders
        InitializeMenuSliders();
        InitializeMenuToggles();
        InitializeMenuDropdowns();
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }
    
    public void SetTargetFPS(int optionNumber)
    {
        Debug.Log(optionNumber);
        switch (optionNumber)
        {
            case 0:         
                Application.targetFrameRate = -1;
                PlayerPrefs.SetInt("framerateDropdownValue", 0);
                break;
            case 1:
                Application.targetFrameRate = 30;
                PlayerPrefs.SetInt("framerateDropdownValue", 1);
                break;
            case 2:
                Application.targetFrameRate = 60;
                PlayerPrefs.SetInt("framerateDropdownValue", 2);
                break;
            case 3:        
                Application.targetFrameRate = 90;
                PlayerPrefs.SetInt("framerateDropdownValue", 3);
                break;
            case 4:        
                Application.targetFrameRate = 120;
                PlayerPrefs.SetInt("framerateDropdownValue", 4);
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
            PlayerPrefs.SetInt("vsync", 1);
        }
        else
        {
            framerateDropdown.interactable = true;
            PlayerPrefs.SetInt("vsync", 0);
        }
    }
    
    public void InitializeMenuSliders()
    {
        menuVolumeSliderSettingsMenu.value = PlayerPrefs.GetFloat("musicVolume");
        menuVolumeSliderPauseMenu.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSliderSettingsMenu.value = PlayerPrefs.GetFloat("sfxVolume");
        sfxVolumeSliderPauseMenu.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void InitializeMenuToggles()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        vsyncToggle.isOn = PlayerPrefs.GetInt("vsync", 1) == 1;
    }

    public void InitializeMenuDropdowns()
    {
        if (PlayerPrefs.GetInt("vsync") == 1)
        {
            framerateDropdown.interactable = false;
        }
        framerateDropdown.value = PlayerPrefs.GetInt("framerateDropdownValue", 0);
    }
    
    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
