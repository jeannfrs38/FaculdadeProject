using UnityEngine;

public class Energizer : MonoBehaviour
{
    public int duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ghosts = FindObjectsOfType<GhostAI>();

        foreach (var ghost in ghosts)
        {
            ghost.SetVulnerability(duration);
        }
    }
}
