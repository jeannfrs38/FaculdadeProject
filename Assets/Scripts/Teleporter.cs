using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform ExitPosition;


    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = ExitPosition.position;
    }
}
