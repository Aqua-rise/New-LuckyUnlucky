using System;
using System.Collections;
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
        
        public long upperProbabilityOdds;
        public long lowerProbabilityOdds = 1;
        
        //This variable holds the value of the upper probability and is used to know what that max value was.
        public long staticUpperProbabilityOdds;
        
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
        public Button slowAndSteadyButton;
        public Button openShopButton;
        public Button fasterCooldownShopButton;
        public Button skipAnimationShopButton;
        public Image upgradeTokenImage;
        public Image doublePointsImage;
        public float numberSlideInDelay = .2f;
        public float buttonEnableDelay = .2f;
        public float upperDecrease = .9f;
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
        public int fasterCooldownPrice = 5;
        public int upgradeCoins;
        
        //Other shop descriptions update with their modifier number 
        private int upperDecreaseShopDescriptionValue = 10;
        
        public int upgradeCoinGenerationValue;
        public long doublePointsAmount;
        public long displayDoublePointsAmount;
        public long maximumDoublePointsAmount;

        public int luckStars;
        
        public Animator canvasAnimator;
        public Animator canvasManagerAnimator;
        private bool firstGeneration = true;
        public bool inputLock;
        public bool canPause;
        public bool decrementUpperBound;
        public bool gameWin;
        public bool isDoublePointsActive;
        public bool wasDoublePointsJustBought;
        public bool secretShopLock;
        
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
            
            //Secret Shop Lock Initialization
            
        }

        private void GameStart()
        {
            staticUpperProbabilityOdds = upperProbabilityOdds;
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            EnablePausing();
            UpdatePurchasePointsShopText();
            if (lowerProbabilityOdds != 1)
            {
                UpdateDoublePointsShopText();
            }
        }
        
        public void HandleDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "easy":
                    upperProbabilityOdds = easyProbability;
                    increaseMyOddsUpperCost = 10000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 5000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 1000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    break;
                case "medium": upperProbabilityOdds = mediumProbability;
                    increaseMyOddsUpperCost = 50000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 10000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 5000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    break;
                case "hard": upperProbabilityOdds = hardProbability;
                    increaseMyOddsUpperCost = 500000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 250000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 100000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
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
            increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
            increaseMyOddsLowerCost = upperProbabilityOdds * multiplier / 2;
            increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
            slowAndSteadyCost = upperProbabilityOdds;
            slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
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

        public void DecreaseUpperProbabilityOdds(int decreaseMethod)
        {
            //For Increase My Odds Upper
            if (decreaseMethod == 1)
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
            if (decreaseMethod == 2)
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
            if (isDoublePointsActive)
            {
                generatedNumberText.SetText(GenerateRandomNumber().ToString("N0") + "x2");
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
            
            //If a max value is reached that has a probability less than or equal to 1%, play the orchestral sting and give one luck star
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
                DecreaseUpperProbabilityOdds(2);
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
            //Upgrade coin generation disabled when double points is active
            if(isDoublePointsActive)return;

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
                    upgradeCoinGenerationValue += 20;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .2f;
                    break;
                case 4:
                    upgradeCoinGenerationValue += 10;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .1f;
                    break;
                case 5:
                    upgradeCoinGenerationValue += 5;
                    upgradeTokenImageNewValue = upgradeTokenImage.fillAmount + .05f;
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

            UpdateUpgradeCoinGenerationValueText();

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

        private void UpdateUpgradeCoinGenerationValueText()
        {
            //add later
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

        public void StoreProbabilityPoints()
        {
            //if double points was just purchased, it only starts applying to the next number generated
            if (wasDoublePointsJustBought)
            {
                probabilityPoints += GetRandomlyGeneratedNumber();
                totalAccumulatedPoints += GetRandomlyGeneratedNumber();
                wasDoublePointsJustBought = false;
                displayDoublePointsAmount--;
                UpdateDoublePointsDisplay();
                return;
            }
            
            // Make sure this happens before a new number is generated
            if (isDoublePointsActive && doublePointsAmount > 0)
            {
                probabilityPoints += GetRandomlyGeneratedNumber() * 2;
                totalAccumulatedPoints += GetRandomlyGeneratedNumber() * 2;
                
                if (doublePointsAmount == 1)
                {
                    isDoublePointsActive = false;
                    doublePointsAmount = 0;
                    DoublePointsFolder.SetActive(false);
                    UpgradeTokenFolder.SetActive(true);
                }
                else
                {
                    doublePointsAmount--;
                    displayDoublePointsAmount--;
                    UpdateDoublePointsDisplay();
                }
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
            if (firstGeneration)
            {
                UpdateGeneratedNumberText();
                SlideInGeneratedText();
                firstGeneration = false;
                CheckIfWinGame();
            }
            else
            {
                SlideOutGeneratedText();
                yield return new WaitForSeconds(numberSlideInDelay);
                UpdateGeneratedNumberText();
                SlideInGeneratedText();
                CheckIfWinGame();
            }

            yield return new WaitForSeconds(buttonEnableDelay);
            
            // Condition to see if the orchestral sting should play
            if (gameWin == false && !isProbabilityLessEqualToOnePercent())
            {
                EnableGenerationButton();
            }
            else
            {
                // let the button turn back on after the orchestral sting coroutine finishes
            }
        }

        public void UserClickedButton()
        {
            DisableGenerationButton();
            
            // Make sure this happens before a new number is generated
            StoreProbabilityPoints();
            
            UpdateProbabilityPointsText();
            var coroutine = PlaySwitchingAnimation();
            StartCoroutine(coroutine);
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
            audioManager.GameWin();
            
            inputLock = true;
            gameWin = true;
            DisableGenerationButton();
            canvasAnimator.SetBool("GameWin", true);
            backgroundMusic.Stop();
            StartPlayingVictoryMusic();
            seeResultsButton.SetActive(true);
            openShopButton.interactable = false;
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
            openShopButton.interactable = false;
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
            openShopButton.interactable = true;
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
            if (probabilityPoints >= slowAndSteadyCost)
            {
                if (decrementUpperBound == false)
                {
                    StartDecrementUpperBound();
                }

                currentSlowAndSteadyDecreaseValue = upgradedSlowAndSteadyDecreaseValue;
                slowAndSteadyShopDescriptionText.text =
                    "When purchased, decreases the upper probability odds by " + upgradedSlowAndSteadyDecreaseValue.ToString("N0") + " for every number generated\n(Currently " + currentSlowAndSteadyDecreaseValue.ToString("N0") + ")";
                
                probabilityPoints -= slowAndSteadyCost;
                slowAndSteadyButton.interactable = false;
                checkMark.SetActive(true);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();
            }
        }
        public void AttemptPurchaseUpgradeSlowAndSteady()
        {
            if (upgradeCoins <= 0) return;
            upgradeCoins--;
            upgradedSlowAndSteadyDecreaseValue++;
            slowAndSteadyCost += staticUpperProbabilityOdds;
            slowAndSteadyButton.interactable = true;
            checkMark.SetActive(false);
            UpdateUpgradeCoinCountText();
            UpdateSlowAndSteadyPriceText();
            
            
            slowAndSteadyShopDescriptionText.text =
                "When purchased, decreases the upper probability odds by " + upgradedSlowAndSteadyDecreaseValue.ToString("N0") + " for every number generated\n(Currently " + currentSlowAndSteadyDecreaseValue.ToString("N0") + ")";
        }

        private void UpdateSlowAndSteadyPriceText()
        {
            slowAndSteadyShopCostText.text = slowAndSteadyCost.ToString("N0");
        }
        
        public void AttemptPurchaseIncreaseMyOddsUpper()
        {
            if (probabilityPoints >= increaseMyOddsUpperCost)
            {
                probabilityPoints -= increaseMyOddsUpperCost;
                DecreaseUpperProbabilityOdds(1);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();
            }
        }

        public void AttemptPurchaseUpgradeMyOddsUpper()
        {
            if (upgradeCoins <= 0) return;


            switch (upperDecreaseShopDescriptionValue)
            {
                case 10:
                    upperDecrease = .8f;
                    break;
                case 20:
                    upperDecrease = .7f;
                    break;
                case 30:
                    upperDecrease = .6f;
                    break;
                case 40:
                    upperDecrease = .5f;
                    break;
                default:
                    return;
            }

            upgradeCoins--;
            UpdateIncreaseMyOddsUpperShopDescription();
            UpdateUpgradeCoinCountText();
            
        }

        private void UpdateIncreaseMyOddsUpperShopDescription()
        {
            upperDecreaseShopDescriptionValue += 10;
            increaseUpperOddsShopDescriptionText.text =
                "Decreases the upper bound by " + upperDecreaseShopDescriptionValue + "%.";
        }

        public void AttemptPurchaseIncreaseMyOddsLower()
        {
            if (probabilityPoints >= increaseMyOddsLowerCost)
            {
                probabilityPoints -= increaseMyOddsLowerCost;
                lowerProbabilityOdds += lowerIncrease;
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();

            }
        }
        public void AttemptPurchaseUpgradeMyOddsLower()
        {
            if (upgradeCoins <= 0) return;
            
            upgradeCoins--;
            lowerIncrease++;
            UpdateIncreaseMyOddsLowerShopDescription();
            UpdateUpgradeCoinCountText();
        }

        private void UpdateIncreaseMyOddsLowerShopDescription()
        {
            increaseLowerOddsShopDescriptionText.text =
                "Increases the lower bound by " + lowerIncrease.ToString("N0") + ".";
        }

        private void UpdatePurchasePointsShopText()
        {
            purchasePointsShopDescription.text = "Instantly obtain " + staticUpperProbabilityOdds.ToString("N0") + " probability points \n(Not compatible with Double Points)";
        }

        public void UpdateDoublePointsShopText()
        {
            doublePointsShopDescription.text = "Doubles the points gained for the next " + lowerProbabilityOdds.ToString("N0") + " generations. \nDisables Upgrade Token Generation while active.";
        }

        public void AttemptPurchasePoints()
        {
            if (upgradeCoins <= 0) return;

            upgradeCoins--;
            probabilityPoints += staticUpperProbabilityOdds;
            UpdateProbabilityPointsText();
            UpdateUpgradeCoinCountText();
        }

        public void AttemptPurchaseDoublePoints()
        {
            if (upgradeCoins <= 0) return;

            upgradeCoins--;
            doublePointsAmount = lowerProbabilityOdds;
            isDoublePointsActive = true;
            wasDoublePointsJustBought = true;
            DoublePointsFolder.SetActive(true);
            UpgradeTokenFolder.SetActive(false);
            maximumDoublePointsAmount = doublePointsAmount;
            displayDoublePointsAmount = doublePointsAmount;
            UpdateUpgradeCoinCountText();
            UpdateDoublePointsDisplay();
        }

        public void AttemptPurchaseSkipAnimation()
        {
            if (upgradeCoins <= 4) return;
            
            UnlockFasterCooldown();
            upgradeCoins -= 5;
            canvasAnimator.SetBool("NoSlideOut", true);
            numberSlideInDelay = 0f;
            buttonEnableDelay = 0.35f;
            skipAnimationShopButton.interactable = false;
            UpdateUpgradeCoinCountText();
        }

        private void UnlockFasterCooldown()
        {
            fasterCooldownLock.gameObject.SetActive(false);
            fasterCooldownShopButton.interactable = true;
            fasterCooldownShopDescription.text = "Slightly decreases the cooldown after generating a number.";
        }

        public void AttemptPurchaseFasterCooldown()
        {
            if (upgradeCoins <= 4) return;

            if (buttonEnableDelay <= .05)
            {
                buttonEnableDelay = 0;
                upgradeCoins -= 5;
                fasterCooldownShopButton.interactable = false;
                UpdateUpgradeCoinCountText();
                return;
            }

            buttonEnableDelay -= .05f;
            upgradeCoins -= 5;
            UpdateUpgradeCoinCountText();
        }

        public void UpdateDoublePointsDisplay()
        {
            doublePointsImage.fillAmount = (float)displayDoublePointsAmount / maximumDoublePointsAmount;
            doublePointsDisplayText.text = "x" + displayDoublePointsAmount;
        }

        public bool isProbabilityLessEqualToOnePercent()
        {
            if (upperProbabilityOdds >= 100 && GetRandomlyGeneratedNumber() == upperProbabilityOdds)
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

        /*public void GiveLuckStar()
        {
            luckStars++;
            PlayerPrefs.SetInt("stars", luckStars);
            PlayerPrefs.Save();
            luckStarsDisplayText.SetText("x" + luckStars.ToString("N0"));
        }*/

        public void ToggleReducedAnimations(bool value)
        {
            canvasAnimator.SetBool("NoSlideOut", value);
        }
    }
}
