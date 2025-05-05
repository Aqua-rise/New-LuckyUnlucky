using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace C__Scripts
{
    public class GameManager : MonoBehaviour
    {
        public int upperProbabilityOdds;
        public int lowerProbabilityOdds = 1;
        
        public TMP_Text probabilityDisplayText;
        public TMP_Text generatedNumberText;
        public TMP_Text probabilityPointsText;
        public TMP_Text shopProbabilityPointsText;
        public TMP_Text increaseUpperOddsShopCostText;
        public TMP_Text increaseLowerOddsShopCostText;
        public TMP_Text slowAndSteadyShopCostText;
        public TMP_Text accumulatedPointsText;
        public TMP_Text numAttemptsText;
        public TMP_Text shopLowerBoundText;
        public TMP_Text shopUpperBoundText;
        public TMP_Text finalProbabilityResultsText;
        public TMP_Text finalProbabilityResultsTextPercentage;
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
        public Button slowAndSteadyButton;
        public Button openShopButton;
        public int upperDecrease;
        private int easyProbability = 1000;
        private int mediumProbability = 10000;
        private int hardProbability = 100000;
        public int randomlyGeneratedNumber;
        public int numAttempts;
        public int probabilityPoints;
        public int totalAccumulatedPoints;
        public int slowAndSteadyCost = 1000;
        public int increaseMyOddsUpperCost = 10000;
        public int increaseMyOddsLowerCost = 5000;
        public Animator canvasAnimator;
        public Animator canvasManagerAnimator;
        private bool firstGeneration = true;
        public bool inputLock;
        public bool canPause;
        public bool decrementUpperBound;
        public bool gameWin;
        
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
                    upperDecrease = 100;
                    break;
                case "medium": upperProbabilityOdds = mediumProbability;
                    increaseMyOddsUpperCost = 50000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 10000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 5000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    upperDecrease = 1000;
                    break;
                case "hard": upperProbabilityOdds = hardProbability;
                    increaseMyOddsUpperCost = 500000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 250000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 100000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    upperDecrease = 10000;
                    break;
            }
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            EnablePausing();
        }
        
        public void HandleCustomDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "easy":
                    increaseMyOddsUpperCost = 10000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 5000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 1000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    upperDecrease = 100;
                    break;
                case "medium":
                    increaseMyOddsUpperCost = 50000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 10000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 5000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    upperDecrease = 1000;
                    break;
                case "hard":
                    increaseMyOddsUpperCost = 500000;
                    increaseUpperOddsShopCostText.SetText(increaseMyOddsUpperCost.ToString("N0"));
                    increaseMyOddsLowerCost = 250000;
                    increaseLowerOddsShopCostText.SetText(increaseMyOddsLowerCost.ToString("N0"));
                    slowAndSteadyCost = 100000;
                    slowAndSteadyShopCostText.SetText(slowAndSteadyCost.ToString("N0"));
                    upperDecrease = 10000;
                    break;
            }
            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            EnablePausing();
        }

        public void HandleProbabilityDisplayFormatting(int lower, int upper)
        {
            probabilityDisplayText.SetText(lower.ToString("N0") + " in " + upper.ToString("N0"));
            UpdateShopBoundsText();
        }

        public void UpdateShopBoundsText()
        {
            shopLowerBoundText.SetText("Lower Bound Probability Odds: " + lowerProbabilityOdds);
            shopUpperBoundText.SetText("Upper Bound Probability Odds: " + upperProbabilityOdds);

        }

        public void SetUpperProbabilityOdds(int value)
        {
            upperProbabilityOdds = value;
        }

        public void DecreaseUpperProbabilityOdds(int value)
        {
            if (upperProbabilityOdds - value <= lowerProbabilityOdds)
            {
                upperProbabilityOdds = lowerProbabilityOdds + 1;
            }
            else
            {
                upperProbabilityOdds -= value;
            }
        }

        public int GetUpperProbabilityOdds()
        {
            return upperProbabilityOdds;
        }
        
        public void SetLowerProbabilityOdds(int value)
        {
            lowerProbabilityOdds = value;
        }

        public int GetLowerProbabilityOdds()
        {
            return lowerProbabilityOdds;
        }

        public void UpdateGeneratedNumberText()
        {
            //Will automatically generate a new number and update at the same time
            generatedNumberText.SetText(GenerateRandomNumber().ToString("N0"));
        }

        private void CheckIfWinGame()
        {
            if (GetRandomlyGeneratedNumber() <= lowerProbabilityOdds)
            {
                WinGame();
            }
            else if (GetRandomlyGeneratedNumber() == upperProbabilityOdds && (float)lowerProbabilityOdds/upperProbabilityOdds <= 0.01)
            {
                backgroundMusic.Pause();
                PlayOrchestralSting();
            }
            
        }

        public int GenerateRandomNumber()
        {
            IncrementNumAttempts();
            var generatedNumber = 0;
            if (decrementUpperBound)
            {
                upperProbabilityOdds--;
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
            }
            generatedNumber = Random.Range(1, upperProbabilityOdds + 1);
            SetRandomlyGeneratedNumber(generatedNumber);
            return generatedNumber;
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
            // Make sure this happens before a new number is generated
            probabilityPoints += GetRandomlyGeneratedNumber();
            totalAccumulatedPoints += GetRandomlyGeneratedNumber();
        }

        public int GetProbabilityPoints()
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
                yield return new WaitForSeconds(0.2f);
                UpdateGeneratedNumberText();
                SlideInGeneratedText();
                CheckIfWinGame();
            }

            yield return new WaitForSeconds(0.2f);
            
            // Condition to see if the orchestral sting should play
            if (gameWin == false && !isProbabilityLessEqualToOnePercent())
            {
                EnableGenerationButton();
            }
            else
            {
                // let the button turn back on after the orchestral sting couroutine finishes
            }
        }

        public void UserClickedButton()
        {
            DisableGenerationButton();
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

        public void SetRandomlyGeneratedNumber(int input)
        {
            randomlyGeneratedNumber = input;
        }

        public int GetRandomlyGeneratedNumber()
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
            finalProbabilityResultsTextPercentage.SetText((FormatPercentage((float)lowerProbabilityOdds/upperProbabilityOdds)) + " out of 100% \n or \n " + ((float)lowerProbabilityOdds/upperProbabilityOdds).ToString("0.#######") + " in 1");
            DisablePausing();
        }
        
        string FormatPercentage(float value)
        {
            if (value == 0f)
                return "0%";

            float percent = value * 100f;
            float abs = Mathf.Abs(percent);

            if (abs >= 1f)
                return percent.ToString("0.##") + "%"; // Up to 2 decimals
            if (abs >= 0.01f)
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
            // Wait until the first clip finishes playing
            yield return new WaitWhile(() => winMusic.isPlaying);

            // Now switch to the looping track
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

        // Will optimize inefficient code in a future update
        public void TestCustomOddsValidity()
        {
            //If is not null
            if (lowerBoundEntryField.text != "" && upperBoundEntryField.text != "")
            {
                //and is not zero
                if (int.Parse(lowerBoundEntryField.text) != 0 && int.Parse(upperBoundEntryField.text) != 0)
                {
                    //and is not equal to each other
                    if(int.Parse(lowerBoundEntryField.text) != int.Parse(upperBoundEntryField.text))
                    {
                        // and upper is not less than lower bound
                        if (int.Parse(lowerBoundEntryField.text) < int.Parse(upperBoundEntryField.text))
                        {
                            SetLowerProbabilityOdds(int.Parse(lowerBoundEntryField.text));
                            SetUpperProbabilityOdds(int.Parse(upperBoundEntryField.text));
                            HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                            errorTextDefault.SetActive(false);
                            errorTextZero.SetActive(false);
                            errorTextNull.SetActive(false);
                            customDifficultyMenu1.SetActive(false);
                            customDifficultyMenu2.SetActive(true);
                        }
                        else
                        {
                            errorTextDefault.SetActive(true);
                            errorTextZero.SetActive(false);
                            errorTextNull.SetActive(false);
                            errorTextEqual.SetActive(false);

                        }
                    }
                    else
                    {
                        errorTextEqual.SetActive(true);
                        errorTextDefault.SetActive(false);
                        errorTextZero.SetActive(false);
                        errorTextNull.SetActive(false);
                    }
                    // and is less than lower bound

                }
                else
                {
                    errorTextEqual.SetActive(false);
                    errorTextZero.SetActive(true);
                    errorTextDefault.SetActive(false);
                    errorTextNull.SetActive(false);
                }
            }
            else
            {
                errorTextEqual.SetActive(false);
                errorTextNull.SetActive(true);
                errorTextDefault.SetActive(false);
                errorTextZero.SetActive(false);
            }
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
                StartDecrementUpperBound();
                probabilityPoints -= slowAndSteadyCost;
                slowAndSteadyButton.interactable = false;
                checkMark.SetActive(true);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();
            }
        }
        
        public void AttemptPurchaseIncreaseMyOddsUpper()
        {
            if (probabilityPoints >= increaseMyOddsUpperCost)
            {
                probabilityPoints -= increaseMyOddsUpperCost;
                DecreaseUpperProbabilityOdds(upperDecrease);
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();
                
            }

        }
        public void AttemptPurchaseIncreaseMyOddsLower()
        {
            if (probabilityPoints >= increaseMyOddsLowerCost)
            {
                probabilityPoints -= increaseMyOddsLowerCost;
                lowerProbabilityOdds++;
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                UpdateProbabilityPointsText();

            }
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
    }
}
