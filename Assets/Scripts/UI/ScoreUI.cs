using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI _currentScoreText;
    public TextMeshProUGUI _highScoreText;

    private ScoreManager _scoreManager;
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _scoreManager.OnHighScoreChanged += ScoreManager_OnHighScoreChanged;
        _scoreManager.OnScoreChanged += ScoreManager_OnScoreChanged;
        _highScoreText.text = $"{_scoreManager.HighScore:00}";
    }

    private void ScoreManager_OnScoreChanged(int score)
    {
        _currentScoreText.text = $"{score:00}";
    }

    private void ScoreManager_OnHighScoreChanged(int highscore)
    {
        _highScoreText.text = $"{highscore:00}";
    }

}
