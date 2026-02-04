using System.Collections;
using System.Collections.Generic;
using C__Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject meterModeFolder;

    public TMP_Text numericalMeterText;

    public Image meterImage;
    
    public float gameModeTimerValue = 0f;
    public int gameModeAttemptsValue = 0;
    public long gameModeMeterMaxValue = 0;
    
    public bool isInTimedMode;
    public bool isInAttemptsMode;
    public bool isInMeterMode;

    public bool isTimerRunning;
    public bool isAttemptsRunning;
    public bool isMeterRunning;

    public bool hasGameStarted;
    

    public TMP_Text gameModeText;
    
    void Update()
    {
        if (!isTimerRunning || !isInTimedMode || !hasGameStarted) return;
        
        if (gameModeTimerValue > 0)
        {
            gameModeTimerValue -= Time.deltaTime;
            DisplayTime(gameModeTimerValue);
        }
        else
        {
            gameModeTimerValue = 0;
            isTimerRunning = false;
            DisplayTime(gameModeTimerValue);

            Debug.Log("Time's up!");
            gameManager.EndGame();
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt((timeToDisplay * 100f) % 100f);

        gameModeText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void GameStarted()
    {
        if (isInMeterMode)
        {
            InitializeMeterModeComponents();
        }
    }
    
    public void InitializeTimedGameMode()
    {
        isInTimedMode = true;
    }

    public void InitializeAttemptsGameMode()
    {
        isInAttemptsMode = true;
    }        
        
    public void InitializeMeterGameMode()
    {
        isInMeterMode = true;
    }

    public void ReturnToGameModeMenu()
    {
        isInTimedMode = false;
        isInMeterMode = false;
        isInAttemptsMode = false;
    }

    public void SetGameModeDifficulty(string difficulty)
    {
        switch (difficulty.ToLower())
        {
            case "easy":
                gameModeTimerValue = 60f;
                gameModeAttemptsValue = 200;
                gameModeMeterMaxValue = 100000;
                break;
            case "medium":
                gameModeTimerValue = 180f;
                gameModeAttemptsValue = 300;
                gameModeMeterMaxValue = 1000000;
                break;
            case "hard":
                gameModeTimerValue = 300f;
                gameModeAttemptsValue = 500;
                gameModeMeterMaxValue = 100000000;
                break;
            default:
                break;
        }

        if (isInTimedMode)
        {
            isTimerRunning = true;
            UpdateGameModeTextTime();
            gameModeText.gameObject.SetActive(true);
        }
        
        else if (isInAttemptsMode)
        {
            isAttemptsRunning = true;
            UpdateGameModeTextAttempts();
            gameModeText.gameObject.SetActive(true);
        }
        
        else if (isInMeterMode)
        {
            isMeterRunning = true;
            gameModeText.gameObject.SetActive(true);
        }
    }

    private void UpdateGameModeTextAttempts()
    {
        gameModeText.text = "Attempts Remaining: " + gameModeAttemptsValue;
    }   
    private void UpdateGameModeTextTime()
    {
        DisplayTime(gameModeTimerValue);
    }

    public void InitializeMeterModeComponents()
    {
        meterModeFolder.SetActive(true);
        numericalMeterText.text = "0 / " + gameModeMeterMaxValue.ToString("N0");
        gameManager.UpdateSlowAndSteadyShopDescription(2);
        gameManager.UpdateIncreaseMyOddsLowerShopDescription();
        gameManager.upperDecreaseShopDescriptionValue = 0;
        gameManager.UpdateIncreaseMyOddsUpperShopDescription();
    }

    public void PauseTimedModeTimer()
    {
        if (!isInTimedMode) return;
        
        isTimerRunning = false;
    }

    public void ResumeTimedModeTimer()
    {
        if (!isInTimedMode) return;
        
        isTimerRunning = true;
    }

    public void DecreaseRemainingAttempts()
    {
        gameModeAttemptsValue--;
        gameModeText.text = "Attempts Remaining: " + gameModeAttemptsValue;
    }

    public void StartTimer()
    {
        if (hasGameStarted && isInTimedMode) return;
        
        hasGameStarted = true;
    }


    public void IncreaseMeterFillAmount(long generatedNumber, long accumulatedPoints, int pointMultiplier)
    {
        var fillFormattedNumber = (float)generatedNumber * pointMultiplier / gameModeMeterMaxValue;
        
        meterImage.fillAmount += fillFormattedNumber;

        if (accumulatedPoints > gameModeMeterMaxValue)
        {
            numericalMeterText.text = gameModeMeterMaxValue.ToString("N0") + " / " + gameModeMeterMaxValue.ToString("N0");
        }

        numericalMeterText.text = accumulatedPoints.ToString("N0") + " / " + gameModeMeterMaxValue.ToString("N0");
        
        
    }
}
