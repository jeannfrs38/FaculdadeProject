using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int Lives;

    public event Action<int> OnRemovedLives;

    public void RemoveLives()
    {
        Lives--;
        OnRemovedLives?.Invoke(Lives);
    }
}
