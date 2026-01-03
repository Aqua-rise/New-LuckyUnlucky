using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEffects : MonoBehaviour
{

    public GameObject fallingPrefab;
    public GameObject parentFolder;
    public int spawnCooldown;
    public Button spawnButton;
    public List<string> placeholderText;
    public float spawnLowerLimit1;
    public float spawnUpperLimit1;
    public float spawnLowerLimit2;
    public float spawnUpperLimit2;

    public float spawnButtonDelay;

    public void SpawnFallingPrefab()
    {
        GameObject newPrefab = Instantiate(fallingPrefab, GenerateSpawnPosition(), new Quaternion(), parentFolder.transform);
        newPrefab.GetComponent<FallingPrefabScript>().EditDisplayText(placeholderText[Random.Range(0, placeholderText.Count)]);
        spawnButton.interactable = false;
        StartCoroutine(HandleSpawnButtonDelay());
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomSpawnPoint = 0f;
        float randomSpawnPoint1 = Random.Range(spawnLowerLimit1, spawnUpperLimit1);
        float randomSpawnPoint2 = Random.Range(spawnLowerLimit2, spawnUpperLimit2);
        
        randomSpawnPoint = Random.Range(1, 3) == 1 ? randomSpawnPoint1 : randomSpawnPoint2;
        
        return new Vector3(randomSpawnPoint, 1100, 0);
    }
    
    private IEnumerator HandleSpawnButtonDelay()
    {
        yield return new WaitForSeconds(spawnButtonDelay);
        spawnButton.interactable = true;
    }

}
