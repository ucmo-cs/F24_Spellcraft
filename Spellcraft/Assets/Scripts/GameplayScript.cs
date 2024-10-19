using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    public Text scoreText;
    public int totalCollectibles = 5;
    public GameObject victoryCanvas;
    private int currentScore = 0;

    void Start()
    {
        UpdateScoreText();
        victoryCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("Collectible hit: " + collision.gameObject.name);
            Destroy(collision.gameObject);   // Destroy the collectible
            currentScore++;                  // Increment the score
            UpdateScoreText();               // Update the score text
        }

        if (currentScore >= totalCollectibles && collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Goal reached!");
            Time.timeScale = 0f;             // Pause the game
            victoryCanvas.SetActive(true);   // Show the victory screen canvas
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = currentScore + "/" + totalCollectibles;
    }
}
