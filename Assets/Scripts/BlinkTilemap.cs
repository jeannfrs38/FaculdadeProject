using UnityEngine;
using UnityEngine.Tilemaps;

public class BlinkTilemap : MonoBehaviour
{
    public Tilemap _tileMap;
    public Color color1;
    public Color color2;

    public float interval;
    public float _nextStateTime;
    public bool _isColor1;

    void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        _tileMap.color = color1;
        _isColor1 = true;
        _nextStateTime = Time.time + interval;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextStateTime)
        {
            _tileMap.color = _isColor1 ? color2 : color1;
            _isColor1 = !_isColor1;
            _nextStateTime = Time.time + interval;
        }
    }
}
