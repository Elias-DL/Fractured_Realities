using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // Reference to the Text UI component
    private int placeholderScore = 0; // Placeholder score

    void Start()
    {
        // Ensure the placeholder score is displayed at the start
        UpdateScore(placeholderScore);
    }

    // Method to update the score text
    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString();
    }
}
