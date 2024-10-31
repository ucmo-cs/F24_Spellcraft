using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public Text scoreText;
    public GameObject victoryCanvas;
    public int score;
    public const int TOTAL_SCORE = 5;
    void Start()
    {
        score = 0;
        victoryCanvas.SetActive(false);
    }

    void Update()
    {
        scoreText.text = score + "/" + TOTAL_SCORE;
        if(score >= TOTAL_SCORE) {
            Debug.Log("Goal reached!");
            Time.timeScale = 0f;        // Pause game
            victoryCanvas.SetActive(true);
        }
    }
}
