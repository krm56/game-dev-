using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour 
{
    public static int scoreValue = 0;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;

    private int lastKnownScore = -1; 

    void Start() {
        
        UpdateScoreUI();
        UpdateHighScoreDisplay();
    }

    void Update() {
        if (scoreValue != lastKnownScore) {
            UpdateScoreUI();
            CheckHighScore();
            lastKnownScore = scoreValue;
        }
    }

    public void UpdateScoreUI() {
        if (scoreDisplay != null) {
            scoreDisplay.text = "Score: " + scoreValue;
        }
    }

    void CheckHighScore() {
        int currentSavedHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (scoreValue > currentSavedHigh) {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            PlayerPrefs.Save(); 
            UpdateHighScoreDisplay();
        }
    }

    void UpdateHighScoreDisplay() {
        if (highScoreDisplay != null) {
            highScoreDisplay.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public static void ResetScore() {
        scoreValue = 0;
    }
}