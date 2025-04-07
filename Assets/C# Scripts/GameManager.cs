using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
        public TMP_InputField lowerBoundEntryField;
        public TMP_InputField upperBoundEntryField;
        public RectTransform generatedNumberTransform;
        public GameObject mainMenu;
        public GameObject errorText;
        public GameObject pauseMenu;
        public GameObject seeResultsButton;
        public GameObject checkMark;
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
        
        public Vector3 startingPosition = new Vector3(442, -215, 0);

        public GameObject winMusic;
        public GameObject backgroundMusic;

        private void Update()
        {
            if (canPause && Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
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
                winMusic.SetActive(true);
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
            generatedNumber = Random.Range(lowerProbabilityOdds, upperProbabilityOdds + 1);
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
            if (probabilityDisplayText.TryGetComponent<Button>(out Button nothing))
            {
                EnableGenerationButton();
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
            Destroy(probabilityDisplayText.GetComponent<Button>());
            canvasAnimator.SetBool("GameWin", true);
            winMusic.SetActive(true);
            backgroundMusic.SetActive(false);
            seeResultsButton.SetActive(true);
            openShopButton.interactable = false;
            accumulatedPointsText.SetText(totalAccumulatedPoints.ToString("N0"));
            numAttemptsText.SetText(numAttempts.ToString("N0"));

            DisablePausing();
        }

        public void TitleScreenScrollUp()
        {
            canvasManagerAnimator.SetTrigger("ScrollUpMainMenu");
        }

        public void TestCustomOddsValidity()
        {
            if (int.Parse(lowerBoundEntryField.text) < int.Parse(upperBoundEntryField.text))
            {
                SetLowerProbabilityOdds(int.Parse(lowerBoundEntryField.text));
                SetUpperProbabilityOdds(int.Parse(upperBoundEntryField.text));
                TitleScreenScrollUp();
                HandleProbabilityDisplayFormatting(lowerProbabilityOdds, upperProbabilityOdds);
                errorText.SetActive(false);
                EnablePausing();
            }
            else
            {
                errorText.SetActive(true);
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
            }
        }
        
        public void AttemptPurchaseIncreaseMyOddsUpper()
        {
            if (probabilityPoints >= increaseMyOddsUpperCost)
            {
                probabilityPoints -= increaseMyOddsUpperCost;
                upperProbabilityOdds -= upperDecrease;
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

        public void DisablePausing()
        {
            canPause = false;
        }
    }
}
