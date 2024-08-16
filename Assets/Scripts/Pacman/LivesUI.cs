using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public GameObject[] lives;
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var life = player.GetComponent<Life>();
        life.OnRemovedLives += Life_OnRemovedLives;
        UpdateLivesSprite(life.Lives);
    }

    private void Life_OnRemovedLives(int remaingLives)
    {
        UpdateLivesSprite(remaingLives);
    }

    private void UpdateLivesSprite(int currentLives)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(i < currentLives);
        }
    }
}
