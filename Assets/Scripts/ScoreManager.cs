using System;

using System.Runtime.Serialization;

using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;
    private int _highScore;
    private int combo;
    private int ghostScore = 200;
    public int HighScore { get => _highScore; }
    public int CurrentScore { get => _currentScore; }

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;
    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("high-score", 0);
    }
    void Start()
    {
        var ghosts = FindObjectsOfType<GhostAI>();
        foreach (GhostAI ghost in ghosts)
        {
            ghost.OnDefeated += Ghost_OnDefeated;
            ghost.OnGhostStateChanged += Ghost_OnGhostStateChanged;    
        }

        var allCollectibles = FindObjectsOfType<Collectible>();
        foreach (Collectible collectible in allCollectibles)
        {
            collectible.OnCollected += Collectible_OnCollected;
        }
    }

    private void Ghost_OnDefeated()
    {
        if (combo < 4)
        {
            combo += 1;
        }
        int value = combo * ghostScore;
        _currentScore += value;
        Debug.Log(value);
        OnScoreChanged?.Invoke(_currentScore);
        if (_currentScore >= _highScore)
        {
           _highScore = _currentScore;
           OnHighScoreChanged?.Invoke(_highScore);
        }
    }
    private void Ghost_OnGhostStateChanged(GhostState ghostState)
    {
        if (ghostState == GhostState.Active)
        {
            combo = 0;
        }
    }

    private void Collectible_OnCollected(int score, Collectible collectible)
    {
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);
        if (_currentScore >= _highScore)
        {
            _highScore = _currentScore;
            OnHighScoreChanged?.Invoke(_highScore);
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("high-score", _highScore);
    }
}
