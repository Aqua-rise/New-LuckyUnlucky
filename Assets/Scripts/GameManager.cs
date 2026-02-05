using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

namespace C__Scripts
{
    public class GameManager : MonoBehaviour
    {
        public AudioManager audioManager;
        public GameModeManager gameModeManager;
        
        public long upperProbabilityOdds;
        public long lowerProbabilityOdds = 1;
        
        //This variable holds the value of the upper probability and is used to know what that max value was.
        public long staticUpperProbabilityOdds;

        public List<Sprite> shopUpgradeTokenSprites;
        
        public TMP_Text probabilityDisplayText;
        public TMP_Text generatedNumberText;
        public TMP_Text probabilityPointsText;
        public TMP_Text shopProbabilityPointsText;
        public TMP_Text increaseUpperOddsShopCostText;
        public TMP_Text increaseUpperOddsShopDescriptionText;
        public TMP_Text increaseLowerOddsShopCostText;
        public TMP_Text increaseLowerOddsShopDescriptionText;
        public TMP_Text slowAndSteadyShopCostText;
        public TMP_Text slowAndSteadyShopDescriptionText;
        public TMP_Text upgradeCoinCountText;
        public TMP_Text upgradeCoinCountShopText;
        public TMP_Text doublePointsShopDescription;
        public TMP_Text purchasePointsShopDescription;
        public TMP_Text accumulatedPointsText;
        public TMP_Text numAttemptsText;
        public TMP_Text shopLowerBoundText;
        public TMP_Text shopUpperBoundText;
        public TMP_Text finalProbabilityResultsText;
        public TMP_Text finalProbabilityResultsTextPercentage;
        public TMP_Text customDifficultyHintText;
        public TMP_Text doublePointsDisplayText;
        public TMP_Text luckStarsDisplayText;
        public TMP_Text fasterCooldownShopDescription;
        public TMP_Text tokenGeneratorShopDescription;
        public TMP_Text tokenGeneratorShopCostText;
        public TMP_Text increaseUpperOddsShopUpgradeName;
        public TMP_Text increaseLowerOddsShopUpgradeName;
        public TMP_Text slowAndSteadyShopUpgradeName;
        public TMP_Text tokenGeneratorShopUpgradeName;
        public TMP_Text fasterCooldownShopCostText;
        public TMP_Text skipAnimationShopCostText;
        public TMP_InputField multiplierInputText;
        public TMP_InputField lowerBoundEntryField;
        public TMP_InputField upperBoundEntryField;
        public RectTransform generatedNumberTransform;
        public GameObject mainMenu;
        public GameObject errorTextDefault;
        public GameObject errorTextNull;
        public GameObject errorTextZero;
        public GameObject errorTextEqual;
        public GameObject pauseMenu;
        public GameObject seeResultsButton;
        public GameObject checkMark;
        public GameObject customDifficultyMenu1;
        public GameObject customDifficultyMenu2;
        public GameObject UpgradeTokenFolder;
        public GameObject DoublePointsFolder;
        public GameObject fasterCooldownLock;
        public GameObject increaseUpperOddsShopUpgradeButton;
        public GameObject increaseLowerOddsShopUpgradeButton;
        public GameObject slowAndSteadyShopUpgradeButton;
        public GameObject tokenGeneratorShopUpgradeButton;
        public Button slowAndSteadyButton;
        public Button openPointShopButton;
        public Button openTokenShopButton;
        public Button fasterCooldownShopButton;
        public Button skipAnimationShopButton;
        public Button tokenGeneratorButton;
        public Image upgradeTokenImage;
        public Image doublePointsImage;
        public Image slowAndSteadyShopUpgradeImage;
        public Image increaseLowerOddsShopUpgradeImage;
        public Image increaseUpperOddsShopUpgradeImage;
        public Image tokenGeneratorShopUpgradeImage;
        public float numberSlideInDelay = .2f;
        public float buttonEnableDelay = .2f;
        public float upperDecrease = .99f;
        
        private int lowerIncrease = 1;
        private int upgradedSlowAndSteadyDecreaseValue = 1;
        private int currentSlowAndSteadyDecreaseValue = 0;
        private int easyProbability = 1000;
        private int mediumProbability = 10000;
        private int hardProbability = 100000;
        public long randomlyGeneratedNumber;
        public int numAttempts;
        public long probabilityPoints;
        public long totalAccumulatedPoints;
        public long slowAndSteadyCost = 1000;
        public long increaseMyOddsUpperCost = 10000;
        public long increaseMyOddsLowerCost = 5000;
        public long tokenGeneratorCost;
        public int upgradeCoins;
        public int upgradedTokenGeneratorValue = 1;
        public int currentTokenGeneratorValue = 0;
        public int currentPointMultiplier = 1;
        public int increaseUpperOddsUpgradeLevel = 1;
        public int increaseLowerOddsUpgradeLevel = 1;
        public int slowAndSteadyUpgradeLevel = 1;
        public int tokenGeneratorUpgradeLevel = 1;
        
        
        //Other shop descriptions update with their modifier number 
        public int upperDecreaseShopDescriptionValue = 1;
        
        public int upgradeCoinGenerationValue;

        public int luckStars;
        
        public Animator canvasAnimator;
        public Animator canvasManagerAnimator;
        private bool firstGeneration = true;
        public bool inputLock;
        public bool canPause;
        public bool decrementUpperBound;
        public bool incrementUpperBound;
        public bool gameWin;
        public bool gameEnd;
        public bool isPointMultiplierActive;
        public bool wasPointMultiplierJustBought;
        public bool secretShopLock;
        public bool isTokenGeneratorOn;
        
        public Vector3 startingPosition = new Vector3(442, -215, 0);
        
        public AudioSource winMusic;
        public AudioSource orchestralSting;
        public AudioSource backgroundMusic;
        public AudioClip winMusicClipPart2;
        
        /*private void Update()
        {
            //Disabled in WebGL Build
            
            if (canPause && Input.GetKeyDown(KeyCode.Escape))
            {
                ShowPauseMenu();
            }
            
        }*/

        //Things that should happen once when the game starts


        public void Start()
        {
            HandleGameInitialization();
        }

        private void HandleGameInitialization()
        {
            //Luck Star Initialization
            luckStars = PlayerPrefs.GetInt("stars");
            luckStarsDisplayText.SetText("x" + luckStars.ToString("N0"));
            
            
            
        }

        private void GameStart()
        {
            staticUpperProbabilityOdds = upperProbabilityOdds;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            EnablePausing();
            UpdatePurchasePointsShopText();
        }
        
        public void HandleDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "easy":
                    upperProbabilityOdds = easyProbability;
                    increaseMyOddsUpperCost = 10000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("C0"));
                    increaseMyOddsLowerCost = 5000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("C0"));
                    slowAndSteadyCost = 1000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("C0"));
                    tokenGeneratorCost = 100000;
                    tokenGeneratorShopCostText.SetText(tokenGeneratorCost.ToString("C0"));
                    break;
                case "medium": upperProbabilityOdds = mediumProbability;
                    increaseMyOddsUpperCost = 50000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("C0"));
                    increaseMyOddsLowerCost = 10000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("C0"));
                    slowAndSteadyCost = 5000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("C0"));
                    tokenGeneratorCost = 500000;
                    tokenGeneratorShopCostText.SetText(tokenGeneratorCost.ToString("C0"));
                    break;
                case "hard": upperProbabilityOdds = hardProbability;
                    increaseMyOddsUpperCost = 500000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("C0"));
                    increaseMyOddsLowerCost = 250000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("C0"));
                    slowAndSteadyCost = 100000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("C0"));
                    tokenGeneratorCost = 1000000;
                    tokenGeneratorShopCostText.SetText(tokenGeneratorCost.ToString("C0"));
                    break;
            }
            GameStart();
        }
        
        public void HandleCustomDifficulty()
        {

            var multiplier = int.Parse(multiplierInputText.text);
            
            if (multiplier == 0)
            {
                multiplier = 1;
            }
            increaseMyOddsUpperCost = upperProbabilityOdds * multiplier;
            increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("C0"));
            increaseMyOddsLowerCost = upperProbabilityOdds * multiplier / 2;
            increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("C0"));
            slowAndSteadyCost = upperProbabilityOdds;
            slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("C0"));
            GameStart();
        }

        public void UpdateCustomDifficultyHintText(string priceModifier)
        {
            if (priceModifier.Length < 1) return;
            
            if(int.Parse(priceModifier) == 0)
            {
                priceModifier = "1";
            }
            customDifficultyHintText.text = "Upper Bound = " + GetUpperProbabilityOdds().ToString("N0") + "\n+\nPrice Modifier = " + priceModifier + "x\n=\nShop Price = " + (GetUpperProbabilityOdds() * int.Parse(priceModifier)).ToString("N0") + "";
        }

        public void HandleProbabilityDisplayFormatting(long lower, long upper)
        {
            probabilityDisplayText.SetText(lower.ToString("N0") + " in " + upper.ToString("N0"));
            UpdateShopBoundsText();
        }

        public void UpdateShopBoundsText()
        {
            shopLowerBoundText.SetText("Lower Bound Probability Odds: " + lowerProbabilityOdds.ToString("N0"));
            shopUpperBoundText.SetText("Upper Bound Probability Odds: " + upperProbabilityOdds.ToString("N0"));

        }

        public void SetUpperProbabilityOdds(long value)
        {
            upperProbabilityOdds = value;
        }

        public void IncreaseMyOddsUpper(int method)
        {
            //For Increase My Odds Upper
            if (method == 1)
            {
                if ((long)(upperProbabilityOdds * upperDecrease) > lowerProbabilityOdds)
                {
                    upperProbabilityOdds = (long)(upperProbabilityOdds * upperDecrease);
                }
                else
                {
                    upperProbabilityOdds = lowerProbabilityOdds + 1;
                }
            }

            //For Slow and Steady
            if (method == 2)
            {
                if (upperProbabilityOdds - currentSlowAndSteadyDecreaseValue > lowerProbabilityOdds)
                {
                    upperProbabilityOdds -= currentSlowAndSteadyDecreaseValue;
                }
                else
                {
                    upperProbabilityOdds = lowerProbabilityOdds + 1;
                }
            }
            
            //For MeterMode Increase My Odds Upper
            if (method == 3)
            {
                upperProbabilityOdds = (long)(upperProbabilityOdds / upperDecrease);
            }
            
            //For MeterMode Slow and Steady
            if (method == 4)
            {
                upperProbabilityOdds += currentSlowAndSteadyDecreaseValue;
            }

        }

        public long GetUpperProbabilityOdds()
        {
            return upperProbabilityOdds;
        }
        
        public void SetLowerProbabilityOdds(long value)
        {
            lowerProbabilityOdds = value;
        }

        public long GetLowerProbabilityOdds()
        {
            return lowerProbabilityOdds;
        }

        public void UpdateGeneratedNumberText()
        {
            //Will automatically generate a new number and update at the same time
            if (isPointMultiplierActive)
            {
                generatedNumberText.SetText(GenerateRandomNumber().ToString("N0") + "x" +currentPointMultiplier);
                return;
            }
            generatedNumberText.SetText(GenerateRandomNumber().ToString("N0"));
        }

        //Checks every number generated to see if the user were to win the game
        private void CheckIfWinGame()
        {
            if (GetRandomlyGeneratedNumber() <= lowerProbabilityOdds)
            {
                WinGame();
            }
            
            //If a max value is reached that has a probability less than or equal to 1%, play the orchestral sting
            else if (GetRandomlyGeneratedNumber() == upperProbabilityOdds && (double)lowerProbabilityOdds/upperProbabilityOdds <= 0.01)
            {
                backgroundMusic.Pause();
                PlayOrchestralSting();
                
            }
            
            else if (gameModeManager.isAttemptsRunning)
            {
                if (gameModeManager.gameModeAttemptsValue <= 1)
                {
                    gameModeManager.DecreaseRemainingAttempts();
                    EndGame();
                }
                else
                {
                    gameModeManager.DecreaseRemainingAttempts();
                }
                
            }
            
        }

        private void CheckIfLoseGame()
        {
            if (GetRandomlyGeneratedNumber() <= lowerProbabilityOdds)
            {
                EndGame();
            }
            
            if (totalAccumulatedPoints >= gameModeManager.gameModeMeterMaxValue)
            {
                WinGame();
            }
            
            //If a max value is reached that has a probability less than or equal to 1%, play the orchestral sting
            else if (GetRandomlyGeneratedNumber() == upperProbabilityOdds && (double)lowerProbabilityOdds/upperProbabilityOdds <= 0.01)
            {
                backgroundMusic.Pause();
                PlayOrchestralSting();
                //GiveLuckStar();
            }
        }

        public long GenerateRandomNumber()
        {
            IncrementNumAttempts();
            long generatedNumber = 0;
            if (decrementUpperBound)
            {
                IncreaseMyOddsUpper(2);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            }

            if (incrementUpperBound)
            {
                IncreaseMyOddsUpper(4);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            }
            generatedNumber = (long)Random.Range(1, upperProbabilityOdds + 1);
            SetRandomlyGeneratedNumber(generatedNumber);
            HandleUpgradeCoinGeneration(CountDigits(generatedNumber));
            return generatedNumber;
        }

        //Numbers generated increase the coin generation value by an amount inversely proportional to the size of the number generated
        private void HandleUpgradeCoinGeneration(long numDigits)
        {

            var upgradeTokenImageNewValue = 0f;
            
            switch (numDigits)
            {
                case 1:
                    upgradeCoinGenerationValue += 100;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + 1;
                    break;
                case 2:
                    upgradeCoinGenerationValue += 50;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .5f;
                    break;
                case 3:
                    upgradeCoinGenerationValue += 10;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .1f;
                    break;
                case 4:
                    upgradeCoinGenerationValue += 6;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .06f;
                    break;
                case 5:
                    upgradeCoinGenerationValue += 4;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .04f;
                    break;
                case 6:
                    upgradeCoinGenerationValue += 2;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .02f;
                    break;
                default:
                    upgradeCoinGenerationValue += 1;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .01f;
                    break;
            }

            //If greater than or equal to 100, subtract 100, keep the change, and give a token, and play sfx
            if (upgradeCoinGenerationValue >= 100)
            {
                upgradeCoins++;
                upgradeCoinGenerationValue -= 100;
                upgradeTokenImageNewValue -= 1;
                UpdateUpgradeCoinCountText();
                audioManager.PlayUpgradeTokenSound();
            }

            upgradeTokenImage.fillAmount = upgradeTokenImageNewValue;

        }
        
        long CountDigits(long n)
        {
            if (n == 0) return 1;
            n = Math.Abs(n);
            return (long)Math.Floor(Math.Log10(n)) + 1;
        }

        private void UpdateUpgradeCoinCountText()
        {
            upgradeCoinCountText.text = "x" + upgradeCoins;
            upgradeCoinCountShopText.text = "" + upgradeCoins;
        }


        public void IncrementNumAttempts()
        {
            numAttempts++;
        }

        public void IncrementLowerBound()
        {
            lowerProbabilityOdds++;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
        }

        public void StartDecrementUpperBound()
        {
            decrementUpperBound = true;
        }
        
        public void StartIncrementUpperBound()
        {
            incrementUpperBound = true;
        }

        public void StoreProbabilityPoints()
        {
            //if point multiplier was just purchased, it only starts applying to the next number generated
            if (wasPointMultiplierJustBought)
            {
                probabilityPoints += GetRandomlyGeneratedNumber();
                totalAccumulatedPoints += GetRandomlyGeneratedNumber();
                wasPointMultiplierJustBought = false;
                return;
            }
            
            // Make sure this happens before a new number is generated
            if (isPointMultiplierActive)
            {
                probabilityPoints += GetRandomlyGeneratedNumber() * currentPointMultiplier;
                totalAccumulatedPoints += GetRandomlyGeneratedNumber() * currentPointMultiplier;
                return;
            } 
            probabilityPoints += GetRandomlyGeneratedNumber();
            totalAccumulatedPoints += GetRandomlyGeneratedNumber();
        }

        public long GetProbabilityPoints()
        {
            return probabilityPoints;
        }

        public void SlideInGeneratedText()
        {
            canvasAnimator.SetTrigger("SlideIn");
        }
        
        public void SlideOutGeneratedText()
        {
            canvasAnimator.SetTrigger("SlideOut");
        }
        private IEnumerator PlaySwitchingAnimation()
        {
            //For Freeplay and other Lucky Modes
            if (firstGeneration)
            {
                firstGeneration = false;
            }
            else
            {
                SlideOutGeneratedText();
                yield return new WaitForSeconds(numberSlideInDelay);
            }
            
            UpdateGeneratedNumberText();
            SlideInGeneratedText();
            if (gameModeManager.isInMeterMode)
            {
                CheckIfLoseGame();
            }
            else
            {
                CheckIfWinGame();
            }
            
            
            yield return new WaitForSeconds(buttonEnableDelay);
            
            // If the user wins, loses, or if the orchestral sting plays, escape the IEnumerator without enabling the generation button
            if (gameWin == false && gameEnd == false && !isProbabilityLessEqualToOnePercent())
            {
                EnableGenerationButton();
            }
        }

        public void UserClickedButton()
        {
            DisableGenerationButton();
            
            // Make sure this happens before a new number is generated
            StoreProbabilityPoints();
            
            UpdateProbabilityPointsText();
            
            if (gameModeManager.isInMeterMode)
            {
                gameModeManager.IncreaseMeterFillAmount(GetRandomlyGeneratedNumber(), totalAccumulatedPoints, currentPointMultiplier);
                IncrementLowerBoundForMeterMode();
            }
            
            StartCoroutine(PlaySwitchingAnimation());
            
            //Only works once
            gameModeManager.StartTimer();
        }

        public void UpdateProbabilityPointsText()
        {
            probabilityPointsText.SetText(GetProbabilityPoints().ToString("N0"));
            shopProbabilityPointsText.SetText(GetProbabilityPoints().ToString("N0"));
        }

        public void SetRandomlyGeneratedNumber(long input)
        {
            randomlyGeneratedNumber = input;
        }

        public long GetRandomlyGeneratedNumber()
        {
            return randomlyGeneratedNumber;
        }

        public void DisableGenerationButton()
        {
            probabilityDisplayText.GetComponent<Button>().interactable = false;
        }
        public void EnableGenerationButton()
        {
            probabilityDisplayText.GetComponent<Button>().interactable = true;
        }
        
        public void WinGame()
        {
            if (gameModeManager.isInTimedMode)
            {
                gameModeManager.PauseTimedModeTimer();
            }
            audioManager.GameWin();
            
            inputLock = true;
            gameWin = true;
            isTokenGeneratorOn = false;
            DisableGenerationButton();
            canvasAnimator.SetBool("GameWin", true);
            backgroundMusic.Stop();
            StartPlayingVictoryMusic();
            seeResultsButton.SetActive(true);
            openPointShopButton.interactable = false;
            openTokenShopButton.interactable = false;
            accumulatedPointsText.SetText(totalAccumulatedPoints.ToString("N0"));
            numAttemptsText.SetText(numAttempts.ToString("N0"));
            finalProbabilityResultsText.SetText(lowerProbabilityOdds.ToString("N0") + " in " + upperProbabilityOdds.ToString("N0"));
            finalProbabilityResultsTextPercentage.SetText((FormatPercentage((double)lowerProbabilityOdds/upperProbabilityOdds)) + " out of 100% \n or \n " + ((double)lowerProbabilityOdds/upperProbabilityOdds).ToString("0.#######") + " in 1");
            DisablePausing();
        }
        
        public void EndGame()
        {
            if (gameWin)
            {
                WinGame();
                return;
            }
            inputLock = true;
            isTokenGeneratorOn = false;
            gameEnd = true;
            DisableGenerationButton();
            backgroundMusic.Stop();
            seeResultsButton.SetActive(true);
            openPointShopButton.interactable = false;
            openTokenShopButton.interactable = false;
            accumulatedPointsText.SetText(totalAccumulatedPoints.ToString("N0"));
            numAttemptsText.SetText(numAttempts.ToString("N0"));
            finalProbabilityResultsText.SetText(lowerProbabilityOdds.ToString("N0") + " in " + upperProbabilityOdds.ToString("N0"));
            finalProbabilityResultsTextPercentage.SetText((FormatPercentage((double)lowerProbabilityOdds/upperProbabilityOdds)) + " out of 100% \n or \n " + ((double)lowerProbabilityOdds/upperProbabilityOdds).ToString("0.#######") + " in 1");
            DisablePausing();
        }
        
        string FormatPercentage(double value)
        {
            if (value == 0f)
                return "0%";

            double percent = value * 100f;
            //double abs = Mathf.Abs(percent);

            if (percent >= 1f)
                return percent.ToString("0.##") + "%"; // Up to 2 decimals
            if (percent >= 0.01f)
                return percent.ToString("0.###") + "%"; // Up to 3 decimals
            
            return percent.ToString("0.######") + "%"; // Up to 6 decimals

        }

        public void PlayOrchestralSting()
        {
            DisablePausing();
            openPointShopButton.interactable = false;
            openTokenShopButton.interactable = false;
            orchestralSting.Play();
            StartCoroutine(DelayOrchestralStingFocus());
            
        }

        public void TitleScreenScrollUp()
        {
            canvasManagerAnimator.SetTrigger("ScrollUpMainMenu");
        }

        public void StartPlayingVictoryMusic()
        {
            winMusic.Play();
            StartCoroutine(DelaySwitchToLoopedMusic());
        }
        
        private IEnumerator DelaySwitchToLoopedMusic()
        {
            //Wait until the first clip finishes playing
            yield return new WaitWhile(() => winMusic.isPlaying);

            //Now switch to the looping track
            winMusic.clip = winMusicClipPart2;
            
            //Enable looping
            winMusic.loop = true;
            
            //Play audio
            winMusic.Play();
        }

        private IEnumerator DelayOrchestralStingFocus()
        {
            yield return new WaitWhile(() => orchestralSting.isPlaying);

            backgroundMusic.Play();
            EnableGenerationButton();
            EnablePausing();
            openPointShopButton.interactable = true;
            openTokenShopButton.interactable = true;
            
        }
        
        public void TestCustomOddsValidity()
        {
            //If at least one field is empty
            if (lowerBoundEntryField.text == "" || upperBoundEntryField.text == "")
            {
                errorTextEqual.SetActive(false);
                errorTextNull.SetActive(true);
                errorTextDefault.SetActive(false);
                errorTextZero.SetActive(false);
                return;
            }

            //If at least one field is 0
            if (long.Parse(lowerBoundEntryField.text) == 0 || long.Parse(upperBoundEntryField.text) == 0)
            {
                errorTextEqual.SetActive(false);
                errorTextZero.SetActive(true);
                errorTextDefault.SetActive(false);
                errorTextNull.SetActive(false);
                return;
            }

            //If both fields equal each other
            if (long.Parse(lowerBoundEntryField.text) == long.Parse(upperBoundEntryField.text))
            {
                errorTextEqual.SetActive(true);
                errorTextDefault.SetActive(false);
                errorTextZero.SetActive(false);
                errorTextNull.SetActive(false);
                return;
            }

            //If the UPPER bound is smaller than the LOWER bound
            if (long.Parse(upperBoundEntryField.text) < long.Parse(lowerBoundEntryField.text))
            {
                errorTextDefault.SetActive(true);
                errorTextZero.SetActive(false);
                errorTextNull.SetActive(false);
                errorTextEqual.SetActive(false);
                return;
            }
            
            SetLowerProbabilityOdds(long.Parse(lowerBoundEntryField.text));
            SetUpperProbabilityOdds(long.Parse(upperBoundEntryField.text));
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            errorTextDefault.SetActive(false);
            errorTextZero.SetActive(false);
            errorTextNull.SetActive(false);
            errorTextEqual.SetActive(false);
            customDifficultyMenu1.SetActive(false);
            customDifficultyMenu2.SetActive(true);
        }

        public void EnablePausing()
        {
            canPause = true;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void CloseGame()
        {
            Application.Quit();
        }

        public void AttemptPurchaseSlowAndSteady()
        {
            if (probabilityPoints < slowAndSteadyCost)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            //For Unlucky Gamemodes
            //Most of the lucky gamemode code works the same, it's just being applied in a different way.
            if (gameModeManager.isInMeterMode)
            {
                if (incrementUpperBound == false)
                {
                    StartIncrementUpperBound();
                }
            }
            else
            {
                //For Lucky Gamemodes
                if (decrementUpperBound == false)
                {
                    StartDecrementUpperBound();
                }
            }
            currentSlowAndSteadyDecreaseValue = upgradedSlowAndSteadyDecreaseValue;
            UpdateSlowAndSteadyShopDescription(gameModeManager.isInMeterMode ? 2 : 1);
                
            probabilityPoints -= slowAndSteadyCost;
            slowAndSteadyButton.interactable = false;
            //checkMark.SetActive(true);
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            UpdateProbabilityPointsText();
            audioManager.PlayUpgradeSuccessfulSound(false);

        }
        public void AttemptPurchaseUpgradeSlowAndSteady()
        {
            if (upgradeCoins < slowAndSteadyUpgradeLevel)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            
            upgradeCoins-= slowAndSteadyUpgradeLevel;
            slowAndSteadyUpgradeLevel++;
            
            upgradedSlowAndSteadyDecreaseValue++;
            slowAndSteadyCost += staticUpperProbabilityOdds;
            slowAndSteadyButton.interactable = true;
            checkMark.SetActive(false);
            UpdateUpgradeCoinCountText();
            UpdateSlowAndSteadyPriceText();
            UpdateSpecificShopUpgradeName(slowAndSteadyShopUpgradeName, slowAndSteadyShopUpgradeImage, "Slow and Steady", slowAndSteadyUpgradeLevel);

            if (slowAndSteadyUpgradeLevel >= 10)
            {
                slowAndSteadyShopUpgradeButton.SetActive(false);
                slowAndSteadyShopCostText.text = "Limit Reached";
            }

            UpdateSlowAndSteadyShopDescription(gameModeManager.isInMeterMode ? 2 : 1);
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        public void UpdateSlowAndSteadyShopDescription(int luckyMode)
        {
            //For Lucky GameModes
            if (luckyMode == 1)
            {
                slowAndSteadyShopDescriptionText.text =
                    "When purchased, decreases the upper probability odds by " + 
                    upgradedSlowAndSteadyDecreaseValue.ToString("N0") + " for every number generated\n(Currently " + 
                    currentSlowAndSteadyDecreaseValue.ToString("N0") + ")";
            }
            //For Unlucky GameModes
            else if (luckyMode == 2)
            {
                slowAndSteadyShopDescriptionText.text =
                    "When purchased, increases the upper probability odds by " + 
                    upgradedSlowAndSteadyDecreaseValue.ToString("N0") + " for every number generated\n(Currently " + 
                    currentSlowAndSteadyDecreaseValue.ToString("N0") + ")";
            }
        }

        private void UpdateSlowAndSteadyPriceText()
        {
            slowAndSteadyShopCostText.text = slowAndSteadyCost.ToString("N0");
        }
        
        public void AttemptPurchaseIncreaseMyOddsUpper()
        {
            if (probabilityPoints < increaseMyOddsUpperCost)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            //For Unlucky GameModes : Method 4
            //For Lucky GameModes : Method 1
            IncreaseMyOddsUpper(gameModeManager.isInMeterMode ? 3 : 1);

            probabilityPoints -= increaseMyOddsUpperCost;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            UpdateProbabilityPointsText();
            audioManager.PlayUpgradeSuccessfulSound(false);
        }

        public void AttemptPurchaseUpgradeMyOddsUpper()
        {
            if (upgradeCoins < increaseUpperOddsUpgradeLevel)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            
            upperDecrease = 1 - (upperDecreaseShopDescriptionValue/100f) - .01f;

            upgradeCoins-= increaseUpperOddsUpgradeLevel;
            increaseUpperOddsUpgradeLevel++;
            UpdateIncreaseMyOddsUpperShopDescription();
            UpdateUpgradeCoinCountText();
            UpdateSpecificShopUpgradeName(increaseUpperOddsShopUpgradeName, increaseUpperOddsShopUpgradeImage,"Upper Bound", increaseUpperOddsUpgradeLevel);

            
            if (increaseUpperOddsUpgradeLevel >= 10)
            {
                increaseUpperOddsShopUpgradeButton.SetActive(false);
            }
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        public void UpdateIncreaseMyOddsUpperShopDescription()
        {
            if (gameModeManager.isInMeterMode)
            {
                upperDecreaseShopDescriptionValue += 1;
                increaseUpperOddsShopDescriptionText.text =
                    "Increases the upper bound by " + upperDecreaseShopDescriptionValue + "%.";
            }
            else
            {
                upperDecreaseShopDescriptionValue += 1;
                increaseUpperOddsShopDescriptionText.text =
                    "Decreases the upper bound by " + upperDecreaseShopDescriptionValue + "%.";
            }
        }

        public void AttemptPurchaseIncreaseMyOddsLower()
        {
            if (probabilityPoints < increaseMyOddsLowerCost)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            //For Unlucky Gamemodes
            if (gameModeManager.isInMeterMode)
            {
                lowerProbabilityOdds -= lowerIncrease;
            }
            else //For Lucky Gamemodes
            {
                lowerProbabilityOdds += lowerIncrease;
            }
            
            probabilityPoints -= increaseMyOddsLowerCost;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            UpdateProbabilityPointsText();
            audioManager.PlayUpgradeSuccessfulSound(false);
        }
        public void AttemptPurchaseUpgradeMyOddsLower()
        {
            if (upgradeCoins < increaseLowerOddsUpgradeLevel)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            
            upgradeCoins -= increaseLowerOddsUpgradeLevel;
            increaseLowerOddsUpgradeLevel++;
            
            lowerIncrease++;
            UpdateIncreaseMyOddsLowerShopDescription();
            UpdateUpgradeCoinCountText();
            UpdateSpecificShopUpgradeName(increaseLowerOddsShopUpgradeName, increaseLowerOddsShopUpgradeImage, "Lower Bound", increaseLowerOddsUpgradeLevel);

            
            if (increaseLowerOddsUpgradeLevel >= 10)
            {
                increaseLowerOddsShopUpgradeButton.SetActive(false);
            }
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        public void UpdateIncreaseMyOddsLowerShopDescription()
        {
            //For Unlucky GameModes
            if (gameModeManager.isInMeterMode)
            {
                increaseLowerOddsShopDescriptionText.text =
                    "Decreases the lower bound by " + lowerIncrease.ToString("N0") + ".";
            }

            //For Lucky GameModes
            else
            {
                increaseLowerOddsShopDescriptionText.text =
                    "Increases the lower bound by " + lowerIncrease.ToString("N0") + ".";
            }
        }

        private void UpdatePurchasePointsShopText()
        {
            purchasePointsShopDescription.text = "Instantly obtain " + staticUpperProbabilityOdds.ToString("N0") + " probability points \n(Not compatible with Double Points)";
        }

        private void UpdatePointMultiplierShopText()
        {
            doublePointsShopDescription.text = "Multiplies the points gained when generating numbers by "+ (currentPointMultiplier+1) +"x\n(Currently "+currentPointMultiplier+"x)";
        }

        public void AttemptPurchasePoints()
        {
            if (upgradeCoins <= 0)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }

            upgradeCoins--;
            probabilityPoints += staticUpperProbabilityOdds;
            UpdateProbabilityPointsText();
            UpdateUpgradeCoinCountText();
        }

        public void AttemptPurchasePointMultiplier()
        {
            if (upgradeCoins <= 4)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }

            upgradeCoins -= 5;
            isPointMultiplierActive = true;
            wasPointMultiplierJustBought = true;
            currentPointMultiplier++;
            UpdateUpgradeCoinCountText();
            UpdatePointMultiplierShopText();
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        public void AttemptPurchaseSkipAnimation()
        {
            if (upgradeCoins <= 4)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            
            UnlockFasterCooldown();
            upgradeCoins -= 5;
            canvasAnimator.SetBool("NoSlideOut", true);
            numberSlideInDelay = 0f;
            buttonEnableDelay = 0.35f;
            skipAnimationShopButton.interactable = false;
            UpdateUpgradeCoinCountText();
            skipAnimationShopCostText.text = "Limit Reached";
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        private void UnlockFasterCooldown()
        {
            fasterCooldownLock.gameObject.SetActive(false);
            fasterCooldownShopButton.interactable = true;
            fasterCooldownShopDescription.text = "Slightly decreases the cooldown after generating a number.";
        }

        public void AttemptPurchaseFasterCooldown()
        {
            if (upgradeCoins <= 4)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }

            if (buttonEnableDelay <= .05)
            {
                buttonEnableDelay = 0;
                upgradeCoins -= 5;
                fasterCooldownShopButton.interactable = false;
                fasterCooldownShopCostText.text = "Limit Reached";
                UpdateUpgradeCoinCountText();
                return;
            }

            buttonEnableDelay -= .05f;
            upgradeCoins -= 5;
            UpdateUpgradeCoinCountText();
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        public void AttemptPurchaseTokenGenerator()
        {
            if (probabilityPoints < tokenGeneratorCost)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }

            if (!isTokenGeneratorOn)
            {
                isTokenGeneratorOn = true;
                probabilityPoints -= tokenGeneratorCost;
                currentTokenGeneratorValue = upgradedTokenGeneratorValue;
                tokenGeneratorButton.interactable = false;
                UpdateTokenGeneratorShopDescription();
                UpdateProbabilityPointsText();
                GenerateTokenPercentageInitializer();
                audioManager.PlayUpgradeSuccessfulSound(false);
                return;
            }
            
            probabilityPoints -= tokenGeneratorCost;
            currentTokenGeneratorValue = upgradedTokenGeneratorValue;
            tokenGeneratorButton.interactable = false;
            UpdateProbabilityPointsText();
            UpdateTokenGeneratorShopDescription();
            audioManager.PlayUpgradeSuccessfulSound(false);
        }

        private void GenerateTokenPercentageInitializer()
        {
            if (isTokenGeneratorOn)
            {
                StartCoroutine(GenerateTokenPercentage());
            }
        }

        private IEnumerator GenerateTokenPercentage()
        {
            if (!isTokenGeneratorOn) yield return null;
            
            yield return new WaitForSeconds(1f);
            HandleUpgradeCoinGeneration();
            Debug.Log("Generating Token Percentage...");
        }
        
        private void HandleUpgradeCoinGeneration()
        {
            var upgradeTokenImageNewValue = 0f;

           
            upgradeCoinGenerationValue += currentTokenGeneratorValue;
            upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + (currentTokenGeneratorValue/100f);
            

            //If greater than or equal to 100, subtract 100, keep the change, and give a token, and play sfx
            if (upgradeCoinGenerationValue >= 100)
            {
                upgradeCoins++;
                upgradeCoinGenerationValue -= 100;
                upgradeTokenImageNewValue -= 1;
                UpdateUpgradeCoinCountText();
                audioManager.PlayUpgradeTokenSound();
            }

            upgradeTokenImage.fillAmount = upgradeTokenImageNewValue;
            
            //Temporary Recursive Call
            StartCoroutine(GenerateTokenPercentage());

        }

        public void AttemptPurchaseUpgradeTokenGenerator()
        {
            if (upgradeCoins < tokenGeneratorUpgradeLevel)
            {
                audioManager.PlayUpgradeFailedSound();
                return;
            }
            
            upgradedTokenGeneratorValue += 1;
            upgradeCoins -= tokenGeneratorUpgradeLevel;
            tokenGeneratorUpgradeLevel++;
            tokenGeneratorButton.interactable = true;
            UpdateUpgradeCoinCountText();
            UpdateTokenGeneratorShopDescription();
            UpdateSpecificShopUpgradeName(tokenGeneratorShopUpgradeName, tokenGeneratorShopUpgradeImage, "Token Generator", tokenGeneratorUpgradeLevel);

            
            if (tokenGeneratorUpgradeLevel >= 10)
            {
                tokenGeneratorShopUpgradeButton.SetActive(false);
                tokenGeneratorShopCostText.text = "Limit Reached";
            }
            audioManager.PlayUpgradeSuccessfulSound(true);
        }

        private void UpdateTokenGeneratorShopDescription()
        {
            tokenGeneratorShopDescription.text = "When purchased, generates a token by " + upgradedTokenGeneratorValue +
                                                 "% every second\n(Currently " + currentTokenGeneratorValue +
                                                 "% per second)";
        }

        public bool isProbabilityLessEqualToOnePercent()
        {
            if ((double)lowerProbabilityOdds/upperProbabilityOdds <= 0.01 && GetRandomlyGeneratedNumber() == upperProbabilityOdds)
            {
                return true;
            }
            return false;
        }

        public void DisablePausing()
        {
            canPause = false;
        }

        public void ShowPauseMenu()
        {
            pauseMenu.SetActive(true);
        }
        public void HidePauseMenu()
        {
            pauseMenu.SetActive(false);
        }

        public void UpdateSpecificShopUpgradeName(TMP_Text textComponent, Image upgradeButtonImage, string upgradeName, int upgradeLevel)
        {
            if (upgradeLevel <= 9)
            {
               upgradeButtonImage.sprite = shopUpgradeTokenSprites[upgradeLevel - 1]; 
            }
            
            
            switch (upgradeLevel)
            {
                case 2:
                    textComponent.text = upgradeName + " II";
                    break;
                case 3:
                    textComponent.text = upgradeName + " III";
                    break;
                case 4:
                    textComponent.text = upgradeName + " IV";
                    break;
                case 5:
                    textComponent.text = upgradeName + " V";
                    break;
                case 6:
                    textComponent.text = upgradeName + " VI";
                    break;
                case 7:
                    textComponent.text = upgradeName + " VII";
                    break;
                case 8:
                    textComponent.text = upgradeName + " VIII";
                    break;
                case 9:
                    textComponent.text = upgradeName + " IX";
                    break;
                case 10:
                    textComponent.text = upgradeName + " X";
                    break;
                
            }
        }

        public void IncrementLowerBoundForMeterMode()
        {
            lowerProbabilityOdds++;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
        }
    }
}
