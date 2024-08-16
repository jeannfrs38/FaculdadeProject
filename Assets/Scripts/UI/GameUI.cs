using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject readyText;
    public GameObject gameOverText;
    public BlinkTilemap BlinkyTilemap;

    public AudioSource _audioSource;
    public AudioClip beginniAudio;

    public GameManager _gameManager;
    void Start()
    {
        _audioSource.PlayOneShot(beginniAudio);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnGameStarted += _gameManager_OnGameStarted;
        _gameManager.OnGameVictory += _gameManager_OnGameVictory;
        _gameManager.OnGameOver += _gameManager_OnGameOver;

    }

    private void _gameManager_OnGameVictory()
    {
        BlinkyTilemap.enabled = true;
    }

    private void _gameManager_OnGameStarted()
    {
        readyText.SetActive(false);
    }
    private void _gameManager_OnGameOver()
    {
        gameOverText.SetActive(true);
    }


}
