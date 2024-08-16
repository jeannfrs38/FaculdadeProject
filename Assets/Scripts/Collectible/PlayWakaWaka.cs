using UnityEngine;

public class PlayWakaWaka : MonoBehaviour
{
    public AudioClip wakaWaka1;
    public AudioClip wakaWaka2;

    public AudioSource _audioSource;

    public static bool _switchClip;

    private void OnDestroy()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        if (_audioSource != null)
        {
            _audioSource.PlayOneShot(_switchClip ? wakaWaka1 : wakaWaka2);
            _switchClip = !_switchClip;
        }
    }
}
