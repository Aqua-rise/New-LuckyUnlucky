using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

    public TMP_Dropdown framerateDropdown;
    Resolution[] resolutions;
    public AudioMixer audioMixer;


    void Awake()
    {
        // fullscreen
        bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        Screen.fullScreen = fullscreen;

        // audio
        float master = PlayerPrefs.GetFloat("masterVolume", 0.8f);
        float music = PlayerPrefs.GetFloat("musicVolume", 0.8f);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 0.8f);

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);

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
    
    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("masterVolume", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sliderValue);
    }
    
}
