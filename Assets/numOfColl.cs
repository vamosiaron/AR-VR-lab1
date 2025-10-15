using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMP_Text collectiblesText;
    private TMP_Text victoryText;
    private TMP_Text metricsText; // Add this line

    private List<Transform> collectibleList = new List<Transform>();
    private int collectedCount = 0;

    // Metrics
    private float experimentStartTime;
    private List<float> collectibleTimes = new List<float>();

    void Start()
    {
        collectiblesText = GameObject.Find("text-collectibles").GetComponent<TMP_Text>();
        victoryText = GameObject.Find("text-victory").GetComponent<TMP_Text>();
        metricsText = GameObject.Find("text-metrics").GetComponent<TMP_Text>(); // Add this line

        // Find the parent GameObject containing all collectibles
        GameObject collectiblesParent = GameObject.Find("Awards");
        if (collectiblesParent != null)
        {
            foreach (Transform child in collectiblesParent.transform)
            {
                collectibleList.Add(child);
                child.gameObject.SetActive(false); // Hide all collectibles initially
            }
        }

        // Show only the first collectible
        if (collectibleList.Count > 0)
        {
            collectibleList[0].gameObject.SetActive(true);
        }

        victoryText.gameObject.SetActive(false);
        metricsText.gameObject.SetActive(false); // Hide metrics at start
        UpdateCollectibleText();

        // Start timing
        experimentStartTime = Time.time;
    }

    public void CollectibleCollected()
    {
        // Record time for this collectible
        collectibleTimes.Add(Time.time - experimentStartTime);

        // Hide the collected collectible
        if (collectedCount < collectibleList.Count)
        {
            collectibleList[collectedCount].gameObject.SetActive(false);
        }

        collectedCount++;

        // Show the next collectible in the sequence
        if (collectedCount < collectibleList.Count)
        {
            collectibleList[collectedCount].gameObject.SetActive(true);
        }
        else
        {
            // All collectibles collected
            victoryText.gameObject.SetActive(true);
            ShowMetrics();
        }

        UpdateCollectibleText();
    }

    void UpdateCollectibleText()
    {
        int totalCollectibles = collectibleList.Count;
        collectiblesText.text = "Score: " + collectedCount + "/" + totalCollectibles;
    }

    void ShowMetrics()
    {
        // Build metrics string
        string metrics = "Experiment finished!\nTimes for each collectible:\n";
        for (int i = 0; i < collectibleTimes.Count; i++)
        {
            metrics += $"Collectible {i + 1}: {collectibleTimes[i] - (i > 0 ? collectibleTimes[i - 1] : 0):F2} s\n";
        }
        metrics += $"Total time: {(Time.time - experimentStartTime):F2} s";

        // Show on screen
        metricsText.text = metrics;
        metricsText.gameObject.SetActive(true);

        // Still log to console if you want
        Debug.Log(metrics);
    }
}