using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Starting,
        Playing,
        LifeLost,
        Victory,
        GameOver
    }

    public float startUpTime;
    public float LifeLostTimer;
    private GameState _gameState;
    private int _victoryCount;
    private CharacterMotor _pacmanMotor;
    private GhostAI[] _allGhosts;
    private GhostHouse _ghostHouse;
    private float _lifeLostTimer;
    private bool isGameOver;
    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action OnGameVictory;



    void Start()
    {
        var allCollectibles = FindObjectsOfType<Collectible>();
        _victoryCount = 0;
        foreach (var collectible in allCollectibles)
        {
            _victoryCount++;
            collectible.OnCollected += Collectible_OnCollected;
        }

        var pacman = GameObject.FindWithTag("Player");
        _pacmanMotor = pacman.GetComponent<CharacterMotor>();
        _allGhosts = FindObjectsOfType<GhostAI>();
        _gameState = GameState.Starting;
        _ghostHouse =  FindObjectOfType<GhostHouse>();
        _ghostHouse.enabled = false;

        pacman.GetComponent<Life>().OnRemovedLives += GameManager_OnRemovedLives;
        StopAllCaracters();

    }

    private void GameManager_OnRemovedLives(int remaninglives)
    {
        StopAllCaracters();
        _lifeLostTimer = LifeLostTimer;
        _gameState = GameState.LifeLost;

        isGameOver = remaninglives <= 0;
    }

    private void Collectible_OnCollected(int _, Collectible collectible)
    {
        _victoryCount--;
        if (_victoryCount <= 0)
        {
            Debug.Log("Victory!!");
            _gameState = GameState.Victory;
            StopAllCaracters();
            OnGameVictory?.Invoke();
        }

        collectible.OnCollected -= Collectible_OnCollected;
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.Starting:
                startUpTime -= Time.deltaTime;

                if (startUpTime <= 0)
                {
                    _ghostHouse.enabled = true;
                    _gameState = GameState.Playing;
                    OnGameStarted?.Invoke();
                    StartAllCaracters();
                }
                break;

            case GameState.LifeLost:
                _lifeLostTimer -= Time.deltaTime;

                if (_lifeLostTimer <= 0)
                {
                    if (isGameOver)
                    {
                        _gameState = GameState.GameOver;
                        OnGameOver?.Invoke();
                    }
                    else
                    {
                        ResetAllCaracters();
                        _gameState = GameState.Playing;
                    }

                }
                break;
            case GameState.GameOver:
            case GameState.Victory:

                if (Input.anyKey)
                {
                    SceneManager.LoadScene(0);

                }
                break;
        }
    }
    private void ResetAllCaracters()
    {
        _pacmanMotor.ResetPosition();

        foreach (var ghosts in _allGhosts)
        {
            ghosts.ResetPosition();
        }
        StartAllCaracters();
    }
    private void StartAllCaracters()
    {
        _pacmanMotor.enabled = true;

        foreach (var ghosts in _allGhosts)
        {
            ghosts.StartMoving();
        }
    }
    private void StopAllCaracters()
    {
        _pacmanMotor.enabled = false;

        foreach (var ghosts in _allGhosts)
        {
            ghosts.StopMoving();
        }

    }
}
