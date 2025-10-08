using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMP_Text collectiblesText;
    private TMP_Text victoryText;

    private int totalCollectibles;
    private int collectedCount = 0;

    void Start()
    {
        // Find references to both text elements
        collectiblesText = GameObject.Find("text-collectibles").GetComponent<TMP_Text>();
        victoryText = GameObject.Find("text-victory").GetComponent<TMP_Text>();

        // Count how many collectibles are in the scene at the start
        totalCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;

        // Hide the victory text initially
        victoryText.gameObject.SetActive(false);

        // Update UI initially
        UpdateCollectibleText();
    }

    public void CollectibleCollected()
    {
        collectedCount++;

        UpdateCollectibleText();

        if (collectedCount >= totalCollectibles)
        {
            // All collectibles collected
            victoryText.gameObject.SetActive(true);
        }
    }

    void UpdateCollectibleText()
    {
        int remaining = totalCollectibles - collectedCount;
        collectiblesText.text = "Score: " + collectedCount + "/" + totalCollectibles;
    }
}
