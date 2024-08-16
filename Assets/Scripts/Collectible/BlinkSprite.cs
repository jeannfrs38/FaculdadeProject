using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkSprite : MonoBehaviour
{
    public float interval;
    private SpriteRenderer _spriteRenderer;

    public float _changeState;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = true;
        _changeState = Time.time + interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _changeState)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            _changeState = Time.time + interval;
        }
    }
}
