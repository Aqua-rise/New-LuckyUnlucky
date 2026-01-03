using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FallingPrefabScript : MonoBehaviour
{
    public float moveSpeed = -1;
    public TMP_Text displayText;
    public bool hasBeenClicked;
    public Button textButton;
    public List<string> placeholderText;
    public float lifeTimer;
    
    private int randomlyGeneratedNumber { get; set; }

    public void Update()
    {
        transform.Translate(0, moveSpeed, 0);
    }

    public void Start()
    {
        lifeTimer = 15;
        StartCoroutine(StartLifeTimer());
        
    }

    public void GenerateRandomNumber()
    {
        if (hasBeenClicked) { return; }

        hasBeenClicked = true;
        int generatedNumber = Random.Range(1, 4097);
        randomlyGeneratedNumber = generatedNumber;
        DisplayRandomNumber();
    }

    private void DisplayRandomNumber()
    {
        displayText.text = randomlyGeneratedNumber.ToString();
    }

    public void EditDisplayText(string text)
    {
        displayText.text = text;
    }

    private void ResetObject()
    {
        displayText.text = "";
        hasBeenClicked = false;
        textButton.interactable = true;
    }
    
    private IEnumerator StartLifeTimer()
    {
        yield return new WaitForSeconds(lifeTimer);
        Destroy(transform.gameObject);
    }
    
    
}
